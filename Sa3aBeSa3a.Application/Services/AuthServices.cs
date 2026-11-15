using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Interfaces;

namespace Sa3aBeSa3a.Application.Services
{
    public class AuthServices(IAuthRepository authRepository, ITokenServices tokens) : IAuthServices
    {
        public async Task<AuthResultDtos> RegisterAsync(RegisterDto dto)
        {
            if(dto.Email == null || dto.Password == null || dto.PhoneNumber == null || dto.Name == null)
            {
                throw new ArgumentException("Missing Inputs");
            }
            if (await authRepository.IsEmailExistsAsync(dto.Email))
            {
                throw new ArgumentException("The Email is already exist");
            }
            if(string.IsNullOrEmpty(dto.Name))
            {
                throw new ArgumentException("Name can't be null or empty");
            }
            var member = await authRepository.AddAsync(dto);
            await authRepository.SaveChangesAsync();
            return BuildResult(member);
        }

        private AuthResultDtos BuildResult(Members member)
        {
            var t = tokens.Create(member);
            return new AuthResultDtos(t.Token, t.ExpiresAt, ToDto(member));
        }
        private static MemberDto ToDto(Members u)
            => new(u.Id, u.Name, u.Email, u.PhoneNumber, u.Role);
    }
}
