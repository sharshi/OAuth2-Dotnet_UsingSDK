namespace QB.App.Services;

public static class TokenProtector
{
    public static string Protect(string plaintext)
    {
        // quick terrible base64 encoding instead od encryption 
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plaintext));
    }

    public static string Unprotect(string protectedData)
    {
        byte[] data = Convert.FromBase64String(protectedData);
        return System.Text.Encoding.UTF8.GetString(data);
    }
}