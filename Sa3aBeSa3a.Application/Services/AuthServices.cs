using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Interfaces;

namespace Sa3aBeSa3a.Application.Services
{
    public class AuthServices(IAuthRepository authRepository, ITokenServices tokens) : IAuthServices
    {
        public async Task<MemberDto> RegisterAsync(RegisterDto dto)
        {
            if(dto.Email == null || dto.Password == null || dto.PhoneNumber == null || dto.Name == null)
            {
                throw new ArgumentException("Missing Inputs");
            }
            if (await authRepository.IsEmailExistsAsync(dto.Email))
            {
                throw new ArgumentException("The Email is already exist");
            }
            var member = await authRepository.AddAsync(dto);
            await authRepository.SaveChangesAsync();
            return ToDto(member);
        }
        
        private AuthDtos BuildResult(Members member)
        {
            var t = tokens.Create(member);
            return new AuthDtos(t.Token, t.ExpiresAt, ToDto(member));
        }
        private static MemberDto ToDto(Members u)
            => new(u.Id, u.Name, u.Email, u.PhoneNumber, u.Role);
    }
}
