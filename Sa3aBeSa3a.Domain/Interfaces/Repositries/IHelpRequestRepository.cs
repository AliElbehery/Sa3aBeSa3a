using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Dtos;
namespace Sa3aBeSa3a.Domain.Interfaces.Repositries

{
    public interface IHelpRequestRepository
    {
        Task<HelpRequests> AddAsync(int userId, CreateHelpRequestDto dto);
        Task<HelpRequests?> GetByIdAsync(int id);
        Task<IReadOnlyList<HelpRequests>> GetAllAsync();
        Task SaveChangesAsync();
        Task<Members?> GetMemberById(int memberId);
        Task<Categories?> GetCategoryByIdAsync(int categoryId);
        Task<HelpRequests> CompleteHelpRequestAsync(HelpRequests helpRequest, int ownerId, int helperId, decimal hoursWorked);
    }
}
