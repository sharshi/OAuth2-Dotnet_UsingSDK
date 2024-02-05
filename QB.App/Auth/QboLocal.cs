using Intuit.Ipp.OAuth2PlatformClient;
using System.Text.Json;
// using Azure.Identity;
// using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace QB.Auth;
public class QboLocal
{
    public static QboAuthTokens? Tokens { get; set; } = null;
    public static OAuth2Client? Client { get; set; } = null;
    
    public static void Initialize(IOptions<QboAuthTokens> config)
    {
        Tokens = config.Value;

        if (!string.IsNullOrEmpty(config.Value.ClientId) && !string.IsNullOrEmpty(config.Value.ClientSecret))
        {
            Client = new(config.Value.ClientId, config.Value.ClientSecret, config.Value.RedirectUrl, config.Value.Environment);
        }
        else {
            throw new InvalidDataException(
                "The ClientId or ClientSecret was null or empty.\n" +
                "Make sure that 'QB' auth section is setup with your credentials."
            );
        }
    }
}
