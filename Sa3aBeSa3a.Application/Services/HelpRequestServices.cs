using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Sa3aBeSa3a.Domain.Dtos;


namespace Sa3aBeSa3a.Application.Services
{
    public class HelpRequestServices(IHelpRequestRepository helpRequestRepository) : IHelpRequestServices
    {
        public async Task<HelpRequests> CreateHelpRequestAsync(int userId, CreateHelpRequestDto dto)
        {
            var user = await helpRequestRepository.GetMemberById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            if (await helpRequestRepository.GetCategoryByIdAsync(dto.CategoryId) == null)
            {
                throw new KeyNotFoundException($"Category with ID {dto.CategoryId} not found.");
            }
            if (!user.IsActive)
            {
                throw new InvalidOperationException("Only active users can create help requests.");
            }
            if (dto.NeededBy <= DateTime.UtcNow)
            {
                throw new ArgumentException("NeededBy date must be in the future.");
            }
            if (dto.HoursNeeded < 0.5m || dto.HoursNeeded > 8.0m || dto.HoursNeeded % 0.5m != 0)
            {
                throw new ArgumentException("HoursNeeded must be between 0.5 and 8.0 hours in 0.5 increments");
            }
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Description) || string.IsNullOrWhiteSpace(dto.District))
            {
                throw new ArgumentException("Title, Description, and District cannot be empty.");
            }
            if (dto.Title.Length > 100 || dto.Description.Length > 200 || dto.District.Length > 100)
            {
                throw new ArgumentException("Title cannot exceed 100 characters, Description cannot exceed 200 characters, and District cannot exceed 100 characters.");
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
                throw new KeyNotFoundException($"Help request with ID {id} not found.");
            }
            return helpRequest;
        }
        public async Task<IReadOnlyList<HelpRequests>> GetAllHelpRequestsAsync()
        {
            var helpRequests = await helpRequestRepository.GetAllAsync();
            if (helpRequests == null)
            {
                throw new KeyNotFoundException("No help requests found.");
            }
            return helpRequests;
        }
    }
}
  
