using QB.Auth;

namespace QB.App.Models
{
    public class ReceiverViewModel
    {
        public ReceiverViewModel(string title, QboAuthTokens authTokens)
        {
            Title = title;
            AuthTokens = authTokens;
        }

        public string Title { get; set; }
        public QboAuthTokens AuthTokens { get; set; }
    }
}
