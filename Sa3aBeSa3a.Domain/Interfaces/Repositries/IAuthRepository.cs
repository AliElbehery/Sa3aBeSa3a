using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Dtos;

namespace Sa3aBeSa3a.Domain.Interfaces.Repositries
{
    public interface IAuthRepository
    {
        Task<Members> AddAsync (RegisterDto dto);
        Task<Members?> GetByEmailAsync(string email);
        Task<Members?> GetByIdAsync(int id);
        Task SaveChangesAsync();
        Task<bool> IsEmailExistsAsync(string email);
        Task<IReadOnlyList<Members>> GetAllAsync();
        Task<Members?> GetProfileAsync(int memberId);

    }
}
