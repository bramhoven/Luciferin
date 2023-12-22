namespace Luciferin.Infrastructure.GoCardless.Models;

public class OpenIdToken
{
    public string AccessToken { get; set; }

    public TimeSpan AccessTokenExpires { get; set; }

    public DateTime Created { get; set; }

    public string RefreshToken { get; set; }

    public TimeSpan RefreshTokenExpires { get; set; }
}