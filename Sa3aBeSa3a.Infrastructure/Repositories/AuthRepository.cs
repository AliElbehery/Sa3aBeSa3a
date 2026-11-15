using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Sa3aBeSa3a.Domain.Interfaces;

namespace Sa3aBeSa3a.Infrastructure.Repositories
{
    public class AuthRepository(AppDbContext _context) : IAuthRepository
    {
        private static readonly PasswordHasher<Members> Hasher = new();
        private static readonly string DummyHash = Hasher.HashPassword(new Members(), "Dummy-Password-1");
        
        public async Task<Members> AddAsync(RegisterDto dto)
        {
            var member = new Members
            {
                Email = dto.Email.Trim().ToLowerInvariant(),
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                JoinedDate = DateTime.UtcNow,
                Role = MemberRole.Member,
                IsActive = true

            };
            member.PasswordHash = Hasher.HashPassword(member, dto.Password);
            await _context.Members.AddAsync(member);
            return member;
        }
        public async Task<Members> GetByEmailAsync(string email)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
        }
        public async Task<Members> GetByIdAsync(int id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Members.AnyAsync(m => m.Email == email);
        }
        public async Task<IReadOnlyList<Members>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

    }
}
