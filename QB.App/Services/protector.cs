using Newtonsoft.Json;
using QB.Auth;

namespace QB.App.Services;

public static class TokenProtector
{
    public static string Protect(QboAuthTokens tokens)
    {
        // quick terrible base64 encoding instead od encryption 
        var s = JsonConvert.SerializeObject(tokens);
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(s));
    }

    public static QboAuthTokens Unprotect(string protectedData)
    {
        byte[] data = Convert.FromBase64String(protectedData);
        string json = System.Text.Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<QboAuthTokens>(json);
    }
}