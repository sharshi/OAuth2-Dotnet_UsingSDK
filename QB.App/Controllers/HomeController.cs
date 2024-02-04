using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Mvc;
using QB.Auth;
using QB.App.Models;
using System.Diagnostics;

namespace QB.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env) {
        _logger = logger;
        _env = env;

        // Determine the path based on the environment
        var path = _env.IsProduction() ? "./Tokens.jsonc" : "../QB.App/Tokens.jsonc";
        QboLocal.Initialize(path);
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("Redirect")]
    public IActionResult Redirect()
    {
        return Redirect(QboHelper.GetAuthorizationURL(OidcScopes.Accounting));
    }

    #region ASP.NET Default Code

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    #endregion
}