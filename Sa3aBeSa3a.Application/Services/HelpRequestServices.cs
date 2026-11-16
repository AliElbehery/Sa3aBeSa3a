using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Enums;
using Sa3aBeSa3a.Domain.Exceptions;

namespace Sa3aBeSa3a.Application.Services
{
    public class HelpRequestServices(IHelpRequestRepository helpRequestRepository) : IHelpRequestServices
    {
        public async Task<HelpRequests> CreateHelpRequestAsync(int userId, CreateHelpRequestDto dto)
        {
            var user = await helpRequestRepository.GetMemberById(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.", Err.UserNotFound);
            }
            if (await helpRequestRepository.GetCategoryByIdAsync(dto.CategoryId) == null)
            {
                throw new NotFoundException($"Category with ID {dto.CategoryId} not found.", Err.CategoryNotFound);
            }
            if (!user.IsActive)
            {
                throw new ForbiddenException("Only active users can create help requests.", Err.NotActive);
            }
            if (dto.NeededBy <= DateTime.UtcNow)
            {
                throw new BusinessRuleException("NeededBy date must be in the future.",Err.InPastDate);
            }
            if (dto.HoursNeeded < 0.5m || dto.HoursNeeded > 8.0m || dto.HoursNeeded % 0.5m != 0)
            {
                throw new BusinessRuleException("HoursNeeded must be between 0.5 and 8.0 hours in 0.5 increments", Err.OutOfRangeHours);
            }
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Description) || string.IsNullOrWhiteSpace(dto.District))
            {
                throw new ForbiddenException("Title, Description, and District cannot be empty.", Err.NullInput);
            }
            if (dto.Title.Length > 100 || dto.Description.Length > 500 || dto.District.Length > 100)
            {
                throw new BusinessRuleException("Title cannot exceed 100 characters, Description cannot exceed 500 characters, and District cannot exceed 100 characters.", Err.OutOfRangeLength);
            }
            var helpRequest = await helpRequestRepository.AddAsync(userId, dto);

            await helpRequestRepository.SaveChangesAsync();
            return helpRequest;
        }
        public async Task<HelpRequests?> GetHelpRequestByIdAsync(int id)
        {
            var helpRequest = await helpRequestRepository.GetByIdAsync(id);
            if (helpRequest == null)
            {
                throw new NotFoundException($"Help request with ID {id} not found.", Err.HelpRequestNotFound);
            }
            return helpRequest;
        }
        public async Task<IReadOnlyList<HelpRequests>> GetAllHelpRequestsAsync()
        {
            return await helpRequestRepository.GetAllAsync();
        }
        public async Task<HelpRequests> CompleteHelpRequestAsync(int helpRequestId, int ownerId, int helperId, decimal hoursWorked)
        {
            var helpRequest = await helpRequestRepository.GetByIdAsync(helpRequestId);

            if (helpRequest == null)
            {
                throw new NotFoundException($"Help request with ID {helpRequestId} not found.",Err.HelpRequestNotFound);
            }
            if (hoursWorked < .5m || hoursWorked > 8.0m || hoursWorked % 0.5m != 0)
            {
                throw new BusinessRuleException("Hours worked must be between 0.5 and 8.0 hours in 0.5 increments.",Err.OutOfRangeHours);
            }
            if (helpRequest.MemberId != ownerId)
            {
                throw new UnauthorizedException("You do not have permission to complete this help request.", Err.NotAuthorized);
            }
            if (helpRequest.Status != HelpRequestStatus.InProgress)
            {
                throw new ConflictException("Help request must be in progress to be completed.",Err.HelpRequestNotAvailable);
            }
            var acceptedOffer = await helpRequestRepository.GetAcceptedOfferAsync(helpRequest.Id);
            if(acceptedOffer == null)
            {
                throw new BusinessRuleException("No accepted offer found for this help request.",Err.NoAcceptedOffer);
            }
            if (acceptedOffer?.MemberId != helperId )
            {
                throw new ForbiddenException("The specified helper does not have an accepted offer for this help request.",Err.InCorrectMember);
            }
            if(acceptedOffer?.OfferedHours < hoursWorked)
            {
                throw new BusinessRuleException("Hours worked cannot exceed the offered hours.",Err.NoAvailableHours);
            }
            if(hoursWorked > helpRequest.HoursNeeded)
            {
                throw new BusinessRuleException("Hours worked must be greater than 0 and less than or equal to the hours needed.",Err.OutOfRangeHours);
            }
            await helpRequestRepository.CompleteHelpRequestAsync(helpRequest, ownerId, helperId, hoursWorked);

            await helpRequestRepository.SaveChangesAsync();
            return helpRequest;
        }
    }
}
  
