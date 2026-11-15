using Sa3aBeSa3a.Domain.Entities;

namespace Sa3aBeSa3a.Domain.Interfaces
{
    public record TokenResult(string Token, DateTime ExpiresAt);
    public interface ITokenServices
    {
        TokenResult Create(Members member);
    }
}
