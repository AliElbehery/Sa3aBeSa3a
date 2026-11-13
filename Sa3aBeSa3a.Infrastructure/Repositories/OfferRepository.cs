using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Enums;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Infrastructure.Data;
using Sa3aBeSa3a.Domain.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Sa3aBeSa3a.Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _context;
        public OfferRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Offers> AddAsync(int userId,int helpRequestId, CreateOfferDto dto)
        {
            var offer = new Offers
            {
                MemberId = userId,
                HelpRequestId = helpRequestId,
                OfferedHours = dto.OfferedHours,
                Message = dto.Message,
                OfferDate = DateTime.UtcNow,
                Status = OfferStatus.Pending // Assuming you have an enum for offer status
            };
            await _context.Offers.AddAsync(offer);
            return offer;
        }
        public async Task<Offers?> GetByIdAsync(int id)
        {
            return await _context.Offers.Include(o => o.Member).Include(o => o.HelpRequest).ThenInclude(h => h.Member).FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<IReadOnlyList<Offers>> GetAllAsync()
        {
            return await _context.Offers.ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Members?> GetMemberById(int memberId)
        {
            return await _context.Members.Include(m => m.MemberSkills).FirstOrDefaultAsync(m => m.Id == memberId);
        }
        public async Task<ICollection<Offers>> GetOffersByHelpRequestIdAsync(int helpRequestId)
        {
            return await _context.Offers.Where(o => o.HelpRequestId == helpRequestId).ToListAsync();
        }
        public async Task<HelpRequests?> GetHelpRequestByIdAsync(int helpRequestId)
        {
            return await _context.HelpRequests.Include(h => h.Category).ThenInclude(c => c.Skills).FirstOrDefaultAsync(h => h.Id == helpRequestId);
        }
        public async Task<bool> HasMemberSubmittedOfferAsync(int memberId, int helpRequestId)
        {
            return await _context.Offers.AnyAsync(o => o.MemberId == memberId && o.HelpRequestId == helpRequestId);
        }
        public async Task<decimal> GetMemberBalanceAsync(int memberId)
        {

            return await _context.LedgerEntries.Where(l => l.MemberId == memberId).SumAsync(l => (decimal?)l.Amount) ?? 0;


        }
        public async Task<decimal> GetLockedHoursAsync(int memberId)
        {
            var lockedHours = await _context.Offers
                .Where(o => o.MemberId == memberId && o.Status == OfferStatus.Accepted && o.HelpRequest.Status == HelpRequestStatus.InProgress)
                .SumAsync(o => (decimal?)o.OfferedHours) ?? 0;
            return lockedHours;
        }
        


    }
}
