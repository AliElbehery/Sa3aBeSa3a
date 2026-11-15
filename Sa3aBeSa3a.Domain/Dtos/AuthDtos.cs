using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Enums;


namespace Sa3aBeSa3a.Domain.Dtos
{
    public record MemberDto(int MemberIdId, string Username, string Email, string Phone,MemberRole role);
    public record AuthDtos(string Token, DateTime ExpiresAt, MemberDto Member);
    
}
