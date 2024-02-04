using Intuit.Ipp.OAuth2PlatformClient;
using System.Text.Json;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QBO.Shared;
public class QboLocal
{
    public static QboAuthTokens? Tokens { get; set; } = null;
    public static OAuth2Client? Client { get; set; } = null;

    public static void Initialize(string path = "./Tokens.jsonc")
    {
        // Loading the tokens and client once (on sign-in/start up)
        // and saving them in static properties saves us from
        // deserializing again when we want to read or write the data.
        Tokens = JsonSerializer.Deserialize<QboAuthTokens>(File.ReadAllText(path), new JsonSerializerOptions() {
            ReadCommentHandling = JsonCommentHandling.Skip
        }) ?? new();

        // In the case that the data failed to deserialize, the ClientId
        // and ClientSecret will be null, we need to make sure that's
        // handled correctly.
        if (!string.IsNullOrEmpty(Tokens.ClientId) && !string.IsNullOrEmpty(Tokens.ClientSecret)) {
            Client = new(Tokens.ClientId, Tokens.ClientSecret, Tokens.RedirectUrl, Tokens.Environment);
        }
        else {
            throw new InvalidDataException(
                "The ClientId or ClientSecret was null or empty.\n" +
                "Make sure that 'Tokens.jsonc' is setup with your credentials."
            );
        }
    }

    // public static MyOptions Initialize()
    // {
    //     var kvURL = "https://sharshi-qb-keyvault.vault.azure.net/";

    //     var client = new SecretClient(new Uri(kvURL), new DefaultAzureCredential());

    //     var options = new MyOptions();
    //     var id = client.GetSecretAsync("QB-CLIENT-ID").GetAwaiter().GetResult();
    //     var secret = client.GetSecretAsync("QB-CLIENT-SECRET").GetAwaiter().GetResult();

    //     options.QbClientId = id.Value.Value;
    //     options.QbClientSecret = secret.Value.Value;

    //     return options;
    // }
}


// public class MyOptions
// {
//     public string QbClientId { get; set; }
//     public string QbClientSecret { get; set; }
// }

// public static class SecretExtentions
// {

//     public static IServiceCollection AddSecretsOptions(this IServiceCollection services)
//     {
//         var options = QboLocal.Initialize();

//         services.AddOptions().Configure<MyOptions>((settings) =>
//         {
//             settings.QbClientId = options.QbClientId;
//             settings.QbClientSecret = options.QbClientSecret;
//         });

//         return services;
//     }
// }