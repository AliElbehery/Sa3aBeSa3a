using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Dtos;

namespace Sa3aBeSa3a.Domain.Interfaces.Services
{
    public interface IOfferServices
    {
        Task<Offers> AddOfferAsync(int userId,int helpRequestId, CreateOfferDto dto);
        Task<Offers?> GetOfferByIdAsync(int id);
        Task<IReadOnlyList<Offers>> GetAllOffersAsync();
        Task<IReadOnlyList<Offers>> GetOffersByHelpRequestIdAsync(int helpRequestId);
        Task<Offers> AcceptOfferAsync(int offerId);
    }
}
