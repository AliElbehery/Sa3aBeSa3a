using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Enums;


namespace Sa3aBeSa3a.Domain.Dtos
{
    public record AuthResultDtos(string Token, DateTime ExpiresAt, MemberDto Member);
    
}
