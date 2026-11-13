using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Enums;

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
                throw new InvalidOperationException($"Members cannot make offers on their own help requests.");
            }
            if (helpRequest == null)
            {
                throw new ArgumentException($"Help request with ID {helpRequestId} does not exist.");
            }
            if (helpRequest.Status != HelpRequestStatus.Open)
            {
                throw new InvalidOperationException($"Cannot make an offer on a help request that is not open.");
            }
            if (member == null)
            {
                throw new ArgumentException($"Member with ID {userId} does not exist.");
            }
            if (!member.IsActive)
            {
                throw new InvalidOperationException($"Inactive members cannot make offers.");
            }
            if(await repository.HasMemberSubmittedOfferAsync(userId, helpRequestId))
            {
                throw new InvalidOperationException($"Members cannot make multiple offers on the same help request.");
            }
            if(dto.OfferedHours <= 0 || dto.OfferedHours % 0.5m != 0)
            {
                throw new ArgumentException($"Offered hours must be greater than zero and in half-hour increments.");
            }
            if (dto.Message != null && dto.Message.Length > 500)
            {
                throw new ArgumentException($"Message cannot exceed 500 characters.");
            }
            if (!helpRequest.Category.Skills.Any(s =>  member.MemberSkills.Any(ms => ms.SkillId == s.Id)))
            {
                throw new InvalidOperationException($"Member does not have the required skill to make an offer on this help request.");
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
                throw new ArgumentException($"Offer with ID {offerId} does not exist.");
            }
            if(offer.HelpRequest.MemberId != userId)
            {
                throw new InvalidOperationException($"Only the owner of the help request can accept an offer.");
            }
            if (!offer.Member.IsActive)
            {
                throw new InvalidOperationException($"Inactive members cannot accept offers.");
            }
            if (offer.Status != OfferStatus.Pending)
            {
                throw new InvalidOperationException($"Only pending offers can be accepted.");
            }
            if(offer.HelpRequest.Status != HelpRequestStatus.Open)
            {
                throw new InvalidOperationException($"Cannot accept an offer for a help request that is not open.");
            }
            var availableBalance = await repository.GetMemberBalanceAsync(offer.MemberId) - await repository.GetLockedHoursAsync(offer.MemberId);
            if (availableBalance < offer.OfferedHours)
            {
                throw new InvalidOperationException($"Member does not have enough balance to accept this offer.");
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
