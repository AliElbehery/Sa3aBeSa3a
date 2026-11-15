namespace Sa3aBeSa3a.Domain.Dtos;

public class JwtOptions
{
    public const string Section = "Jwt";

    public string Issuer { get; set; } = "Sa3aBeSa3a";
    public string Audience { get; set; } = "Sa3aBeSa3a.Clients";

    /// <summary>HMAC-SHA256 signing key, at least 32 characters. Keep it OUT of source control.</summary>
    public string Key { get; set; } = string.Empty;

    public int ExpiryMinutes { get; set; } = 60;
}