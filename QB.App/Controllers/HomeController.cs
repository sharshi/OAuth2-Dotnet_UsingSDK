using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Mvc;
using QB.Auth;
using QB.App.Models;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace QB.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOptions<QboAuthTokens> _config;

    public HomeController(ILogger<HomeController> logger, IOptions<QboAuthTokens> config) {
        _logger = logger;
        _config = config;
        QboLocal.Initialize(config);
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        if (Request.Cookies["auth"] != null)
        {
            return RedirectToAction("Redirect", "Home");
        }

        return View();
    }

    [HttpPost("Redirect")]
    [HttpGet("Redirect")]
    public IActionResult Redirect()
    {
        return Redirect(QboHelper.GetAuthorizationURL([OidcScopes.Accounting], _config));
    }

    #region ASP.NET Default Code

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    #endregion
}