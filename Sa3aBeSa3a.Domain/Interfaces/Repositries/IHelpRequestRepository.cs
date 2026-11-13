using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Dtos;
namespace Sa3aBeSa3a.Domain.Interfaces.Repositries

{
    public interface IHelpRequestRepository
    {
        Task<HelpRequests> AddAsync(CreateHelpRequestDto dto);
        Task<HelpRequests> GetByIdAsync(int id);
        Task<IReadOnlyList<HelpRequests>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
