using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Mvc;
using QBO.Shared;
using QBO.WebApp.Models;
using System.Diagnostics;

namespace QBO.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger) {
        _logger = logger;
        QboLocal.Initialize();
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