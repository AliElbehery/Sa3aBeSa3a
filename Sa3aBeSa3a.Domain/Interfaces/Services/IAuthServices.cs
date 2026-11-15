using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Dtos;


namespace Sa3aBeSa3a.Domain.Interfaces.Services
{
    public interface IAuthServices
    {
        Task<AuthResultDtos> RegisterAsync(RegisterDto dto);
        Task<AuthResultDtos> LoginAsync(LoginDto dto);
    }
}
