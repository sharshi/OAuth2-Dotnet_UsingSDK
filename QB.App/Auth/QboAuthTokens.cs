namespace QB.Auth;

public class QboAuthTokens
{
    public string? AccessToken { get; set; }

    public string? AccessTokenExpiration { get; set; }

    public string? RefreshToken { get; set; }
    public string? RealmId { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string RedirectUrl { get; set; } = string.Empty;
    public string Environment { get; set; } = "sandbox";
}
