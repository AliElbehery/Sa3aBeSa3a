using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Dtos;

namespace Sa3aBeSa3a.Domain.Interfaces.Repositries
{
    public interface IOfferRepository
    {
        Task<Offers> AddAsync(int userId,int helpRequestId, CreateOfferDto dto);
        Task<Offers?> GetByIdAsync(int id);
        Task<IReadOnlyList<Offers>> GetAllAsync();
        Task SaveChangesAsync();
        Task<Members?> GetMemberById(int memberId);
        Task<Skills?> GetSkillsByIdAsync(int skillId);
        Task<IReadOnlyList<Offers>> GetOfferByHelpRequestIdAsync(int helpRequestId);
        Task<HelpRequests?> GetHelpRequestByIdAsync(int helpRequestId);
        Task<bool> HasMemberSubmittedOfferAsync(int memberId, int helpRequestId);
    }
}
