using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Dtos;

namespace Sa3aBeSa3a.Domain.Interfaces.Services
{
    public interface IHelpRequestServices
    {
        Task<HelpRequests> CreateHelpRequestAsync(int userId, CreateHelpRequestDto dto);
        Task<HelpRequests?> GetHelpRequestByIdAsync(int id);
        Task<IReadOnlyList<HelpRequests>> GetAllHelpRequestsAsync();
        Task<HelpRequests> CompleteHelpRequestAsync(int helpRequestId, int ownerId, int helperId, decimal hoursWorked);

    }
}
