using System.Collections.Specialized;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Mvc;
using QB.Auth;
using QB.App.Models;
using System.Diagnostics;
using System.Web;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QB.App.Services;

namespace QB.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOptions<QboAuthTokens> _config;
    private readonly OAuth2Client _auth2Client;

    public HomeController(ILogger<HomeController> logger, IOptions<QboAuthTokens> config) {
        _logger = logger;
        _config = config;
        _auth2Client = new OAuth2Client(config.Value.ClientId, config.Value.ClientSecret, config.Value.RedirectUrl, config.Value.Environment);
    }
    
    [HttpPost("/Auth")]
    public ActionResult InitiateAuth()
    {

        List<OidcScopes> scopes = new List<OidcScopes>();
        scopes.Add(OidcScopes.Accounting);
        string authorizeUrl = _auth2Client.GetAuthorizationURL(scopes);
        QboLocal.Initialize(_config);
        return Redirect(authorizeUrl);
    }

    [HttpGet("/Receiver")]
    public async Task<ActionResult> Receiver()
    {

        string query = Request.QueryString.Value ?? "";

        if (QboHelper.CheckQueryParamsAndSet(query) && QboLocal.Tokens != null) {
            return View("Customers", new ReceiverViewModel("Customers", QboLocal.Tokens));
        }
        else {
            return View("Error");
        }
    }
    
    [HttpGet("/CustomerInfo")]
    public async Task<ActionResult> CustomerInfo()
    {
        var cookies = Request.Cookies;

        string authCookie = cookies["auth"];
        QboAuthTokens qboAuthTokens = TokenProtector.Unprotect(authCookie);
        
        OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(qboAuthTokens.AccessToken);
        ServiceContext serviceContext = new ServiceContext(qboAuthTokens.RealmId, IntuitServicesType.QBO, oauthValidator);
        serviceContext.IppConfiguration.MinorVersion.Qbo = "23";
        serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
        
        QueryService<Customer> querySvc = new QueryService<Customer>(serviceContext);
        List<Customer> customerInfo = querySvc.ExecuteIdsQuery("select * from Customer").ToList<Customer>();
        
        return Json(customerInfo);
    }

    
    [HttpGet("/")]
    public IActionResult Index()
    {
        if (Request.Cookies["auth"] != null)
        {
            // return RedirectToAction("Redirect", "Home");
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