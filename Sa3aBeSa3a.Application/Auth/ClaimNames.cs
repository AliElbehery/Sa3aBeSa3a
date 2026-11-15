
using System.Security.Claims;

namespace Sa3aBeSa3a.Application.Auth;

public static class ClaimNames
{
    public const string MemberId = "sub";
    public const string Name = "name";
    public const string Role = "role";
}

public static class ClaimsPrincipalExtensions
{
    /// <summary>The id of the logged-in user (always taken from the token, never from the request body).</summary>
    public static int GetUserId(this ClaimsPrincipal user)
        => int.TryParse(user.FindFirst(ClaimNames.MemberId)?.Value, out var id)
            ? id
            : throw new Exception("Invalid token.");


}