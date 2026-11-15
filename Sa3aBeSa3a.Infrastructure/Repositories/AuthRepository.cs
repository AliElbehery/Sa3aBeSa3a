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
        
        
        public async Task<Members> AddAsync(RegisterDto dto)
        {
            var member = new Members
            {
                Email = dto.Email.Trim().ToLowerInvariant(),
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                JoinedDate = DateTime.UtcNow,
                Role = MemberRole.Member,
                IsActive = true,
            };
            var bonus = new LedgerEntries
            {
                Member = member,
                Amount = 2,
                Date = DateTime.UtcNow,
                TransactionType = TransactionTypes.WelcomeBonus
            };
            await _context.Members.AddAsync(member);
            await _context.LedgerEntries.AddAsync(bonus);
            return member;
        }
        public async Task<Members?> GetByEmailAsync(string email)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
        }
        public async Task<Members?> GetByIdAsync(int id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            var e = email.Trim().ToLowerInvariant();
            return await _context.Members.AnyAsync(m => m.Email == e);
        }
        public async Task<IReadOnlyList<Members>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }
        public async Task<Members?> GetProfileAsync(int memberId)
        {
            var member = await _context.Members.AsNoTracking().FirstOrDefaultAsync(m => m.Id == memberId)
           return member;
        }


    }
}
