using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Enums;
using Sa3aBeSa3a.Domain.Exceptions;

namespace Sa3aBeSa3a.Application.Services
{
    public class OfferServices(IOfferRepository repository) : IOfferServices
    {
        public async Task<Offers> AddOfferAsync(int userId, int helpRequestId, CreateOfferDto dto)
        {
            var helpRequest = await repository.GetHelpRequestByIdAsync(helpRequestId);
            var member = await repository.GetMemberById(userId);
            if(userId == helpRequest?.MemberId)
            {
                throw new BusinessRuleException("Members cannot make offers on their own help requests.",Err.SameMember);
            }
            if (helpRequest == null)
            {
                throw new NotFoundException($"Help request with ID {helpRequestId} does not exist.",Err.HelpRequestNotFound);
            }
            if (helpRequest.Status != HelpRequestStatus.Open)
            {
                throw new ConflictException("Cannot make an offer on a help request that is not open.",Err.HelpRequestNotAvailable);
            }
            if (member == null)
            {
                throw new NotFoundException($"Member with ID {userId} does not exist.",Err.UserNotFound);
            }
            if (!member.IsActive)
            {
                throw new UnauthorizedException("Inactive members cannot make offers.", Err.NotActive);
            }
            if(await repository.HasMemberSubmittedOfferAsync(userId, helpRequestId))
            {
                throw new BusinessRuleException("Members cannot make multiple offers on the same help request.",Err.MemberNotQualified);
            }
            if(dto.OfferedHours <= 0 || dto.OfferedHours % 0.5m != 0)
            {
                throw new BusinessRuleException("Offered hours must be greater than zero and in half-hour increments.", Err.OutOfRangeHours);
            }
            if (dto.Message != null && dto.Message.Length > 500)
            {
                throw new BusinessRuleException("Messege cannot exceed 500 characters.", Err.OutOfRangeMessage);
            }
            if (!helpRequest.Category.Skills.Any(s =>  member.MemberSkills.Any(ms => ms.SkillId == s.Id)))
            {
                throw new BusinessRuleException($"Member does not have the required skill to make an offer on this help request.", Err.MemberNotQualified);
            }

            var offer = await repository.AddAsync(userId, helpRequestId, dto);
            await repository.SaveChangesAsync();
            return offer;
        }
        public async Task<Offers?> GetOfferByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }
        public async Task<IReadOnlyList<Offers>> GetAllOffersAsync()
        {
            return await repository.GetAllAsync();
        }
        public async Task<ICollection<Offers>> GetOffersByHelpRequestIdAsync(int helpRequestId)
        {
            return await repository.GetOffersByHelpRequestIdAsync(helpRequestId);
        }
        public async Task<Offers> AcceptOfferAsync(int offerId, int userId)
        {
            var offer = await repository.GetByIdAsync(offerId);
            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID {offerId} does not exist.", Err.OfferNotFound);
            }
            if(offer.HelpRequest.MemberId != userId)
            {
                throw new BusinessRuleException("Only the owner of the help request can accept an offer.", Err.InCorrectMember);
            }
            if (!offer.Member.IsActive)
            {
                throw new UnauthorizedException("Inactive members cannot accept offers.",Err.NotActive);
            }
            if (offer.Status != OfferStatus.Pending)
            {
                throw new ConflictException("Only pending offers can be accepted.",Err.OfferNotAvailable);
            }
            if(offer.HelpRequest.Status != HelpRequestStatus.Open)
            {
                throw new ConflictException($"Cannot accept an offer for a help request that is not open.", Err.HelpRequestNotAvailable);
            }
            var availableBalance = await repository.GetMemberBalanceAsync(offer.MemberId) - await repository.GetLockedHoursAsync(offer.MemberId);
            if (availableBalance < offer.OfferedHours)
            {
                throw new ConflictException($"Member does not have enough balance to accept this offer.",Err.NoAvailableHours);
            }

            offer.Status = OfferStatus.Accepted;
            offer.HelpRequest.Status = HelpRequestStatus.InProgress;
            foreach (var o in await repository.GetOffersByHelpRequestIdAsync(offer.HelpRequestId))
            {
                if(o.Id != offerId)
                {
                    o.Status = OfferStatus.Rejected;
                }
            }
            await repository.SaveChangesAsync();
            return offer;
        }
    }
}
