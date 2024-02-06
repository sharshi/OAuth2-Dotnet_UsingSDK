using System.Text.Json;
using System.IO;
using QB.Auth;
using QB.App.Services;

namespace QB.App.Models;
public class ReceiverViewModel
{
    public ReceiverViewModel(string title, long expiresIn, QboAuthTokens? authTokens)
    {
        Title = title; 
        AuthTokens = TokenProtector.Protect(authTokens);
        ExpiresIn = expiresIn;
    }

    public string Title { get; set; }
    public string AuthTokens { get; set; }
    public long ExpiresIn { get; set; }
}
