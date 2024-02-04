using Microsoft.AspNetCore.Mvc;
using QBO.Shared;
using QBO.WebApp.Models;

namespace QBO.WebApp.Controllers
{
    public class ReceiverController : Controller
    {
        [HttpGet("/Receiver")]
        public IActionResult Index()
        {
            string query = Request.QueryString.Value ?? "";

            if (QboHelper.CheckQueryParamsAndSet(query) && QboLocal.Tokens != null) {
                QboHelper.WriteTokensAsJson(QboLocal.Tokens, "./NewTokens.json");

                return View(new ReceiverViewModel("Success!"));
            }
            else {
                return View(new ReceiverViewModel("Authentication failed."));
            }
        }
    }
}
