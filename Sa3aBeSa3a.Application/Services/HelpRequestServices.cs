using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Sa3aBeSa3a.Domain.Dtos;


namespace Sa3aBeSa3a.Application.Services
{
    public class HelpRequestServices(IHelpRequestRepository helpRequestRepository) : IHelpRequestServices
    {
        public async Task<HelpRequests> CreateHelpRequestAsync(CreateHelpRequestDto dto)
        {
            if (dto.NeededBy <= DateTime.UtcNow)
            {
                throw new ArgumentException("NeededBy date must be in the future.");
            }
            if (dto.HoursNeeded <= 0.5m || dto.HoursNeeded >= 8.0m || dto.HoursNeeded % 0.5m != 0)
            {
                throw new ArgumentException("HoursNeeded must be between 0.5 and 8.0 hours, and must be a multiple of 0.5 hours.");
            }
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Description) || string.IsNullOrWhiteSpace(dto.District))
            {
                throw new ArgumentException("Title, Description, and District cannot be empty.");
            }
            var helpRequest = await helpRequestRepository.AddAsync(dto);
            await helpRequestRepository.SaveChangesAsync();
            return helpRequest;
        }
        public async Task<HelpRequests> GetHelpRequestByIdAsync(int id)
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
            if (helpRequests == null || !helpRequests.Any())
            {
                throw new KeyNotFoundException("No help requests found.");
            }
            return helpRequests;
        }
    }
}
  
