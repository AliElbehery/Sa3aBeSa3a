using Microsoft.AspNetCore.Identity;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Sa3aBeSa3a.Domain.Exceptions;


namespace Sa3aBeSa3a.Application.Services
{
    public class AuthServices(IAuthRepository authRepository, ITokenServices tokens) : IAuthServices
    {
        private static readonly PasswordHasher<Members> Hasher = new();
        private static readonly string DummyHash = Hasher.HashPassword(new Members(), "Dummy-Password-1");
        public async Task<AuthResultDtos> RegisterAsync(RegisterDto dto)
        {
            if(string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Email) || 
                string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.PhoneNumber))
            {
                throw new ForbiddenException("Inputs can't be null", Err.NullInput);
            }
            if (await authRepository.IsEmailExistsAsync(dto.Email))
            {
                throw new ConflictException("The Email is already exist", Err.EmailExist);
            }
            var member = await authRepository.AddAsync(dto);
            var hashed = Hasher.HashPassword(member, dto.Password);
            member.PasswordHash = hashed;
            await authRepository.SaveChangesAsync();
            return BuildResult(member);
        }
        public async Task<AuthResultDtos> LoginAsync(LoginDto dto)
        {
            if(string.IsNullOrEmpty(dto.Email) || (string.IsNullOrEmpty(dto.Password)))
            {
                throw new ForbiddenException("Inputs can't be null", Err.NullInput);
            }
            var email = dto.Email.Trim().ToLowerInvariant();
            var member =await authRepository.GetByEmailAsync(email);
            if(member == null)
            {
                var dummy = Hasher.VerifyHashedPassword(new Members(), DummyHash, dto.Password);
                throw new UnauthorizedException("Email is not valid", Err.InCorrectEmail);
            }

            var result = Hasher.VerifyHashedPassword(member, member.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Password is not valid", Err.InCorrectPassword);
            }
            if(!member.IsActive)
            {
                throw new ForbiddenException("User is disable", Err.NotActive);
            }
            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                member.PasswordHash = Hasher.HashPassword(member, dto.Password);
                await authRepository.SaveChangesAsync();
            }

            return BuildResult(member);
        }

        private AuthResultDtos BuildResult(Members member)
        {
            var t = tokens.Create(member);
            return new AuthResultDtos(t.Token, t.ExpiresAt, ToDto(member));
        }
        public async Task<MemberDto> GetProfileAsync(int id)
        {
            var member = await authRepository.GetProfileAsync(id)
                ?? throw new NotFoundException("User not found.", Err.UserNotFound);
            return ToDto(member);
        }
        private static MemberDto ToDto(Members u)
            => new MemberDto { MemberId = u.Id, Name = u.Name, Email = u.Email, Phone = u.PhoneNumber, Role = u.Role };

    }
}
