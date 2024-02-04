using Microsoft.AspNetCore.Mvc;
using QB.Auth;
using QB.App.Models;

namespace QB.App.Controllers
{
    public class ReceiverController : Controller
    {
        [HttpGet("/Receiver")]
        public IActionResult Index()
        {
            string query = Request.QueryString.Value ?? "";

            if (QboHelper.CheckQueryParamsAndSet(query) && QboLocal.Tokens != null) {
                return View(new ReceiverViewModel("Welcome!", QboLocal.Tokens));
            }
            else {
                return View(new ReceiverViewModel("Authentication failed.", null));
            }
        }
    }
}
