using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Infrastructure.Data;
using Sa3aBeSa3a.Domain.Dtos;
using Microsoft.EntityFrameworkCore;
namespace Sa3aBeSa3a.Infrastructure.Repositories
{
    public class HelpRequestRepository : IHelpRequestRepository
    {
        private readonly AppDbContext _context;

        public HelpRequestRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<HelpRequests> AddAsync(int userId, CreateHelpRequestDto dto)
        {
            var helpRequest = new HelpRequests
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                HoursNeeded = dto.HoursNeeded,
                NeededBy = dto.NeededBy,
                RequestDate = DateTime.UtcNow,
                Status = Domain.Enums.HelpRequestStatus.Open,
                District = dto.District,
                MemberId = userId
            };
            await _context.HelpRequests.AddAsync(helpRequest);
            return helpRequest;
        }
        public async Task<Categories?> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }
        public async Task<HelpRequests?> GetByIdAsync(int id)
        {
            return await _context.HelpRequests.FindAsync(id);
        }
        public async Task<IReadOnlyList<HelpRequests>> GetAllAsync()
        {
            var helpRequests = await _context.HelpRequests.AsNoTracking().ToListAsync();
            return helpRequests;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Members?> GetMemberById(int memberId)
        {
            return await _context.Members.FindAsync(memberId);
        }
    }
}
