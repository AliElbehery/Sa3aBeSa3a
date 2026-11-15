using Microsoft.AspNetCore.Identity;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Sa3aBeSa3a.Application.Services
{
    public class AuthServices(IAuthRepository authRepository, ITokenServices tokens) : IAuthServices
    {
        private static readonly PasswordHasher<Members> Hasher = new();
        private static readonly string DummyHash = Hasher.HashPassword(new Members(), "Dummy-Password-1");
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
            var hashed = Hasher.HashPassword(member, dto.Password);
            member.PasswordHash = hashed;
            await authRepository.SaveChangesAsync();
            return BuildResult(member);
        }
        public async Task<AuthResultDtos> Login(LoginDto dto)
        {
            var email = dto.Email.Trim().ToLowerInvariant();
            var member = authRepository.GetByEmailAsync(email);
            if(member == null)
            {
                var dummy = Hasher.VerifyHashedPassword(new Members, DummyHash, dto.Password);
                throw new Exception("InvalidCredentials");
            }

            var result = Hasher.VerifyHashedPassword(member, member.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            { throw new Exception("InvalidCredentials"); }
            if(!member.IsActive)
            {
                throw new Exception("Account is disabled.");
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
        private static MemberDto ToDto(Members u)
            => new(u.Id, u.Name, u.Email, u.PhoneNumber, u.Role);
    }
}
