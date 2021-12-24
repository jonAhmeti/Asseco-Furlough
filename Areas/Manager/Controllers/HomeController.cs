using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        private readonly DAL.Request _contextRequest;

        public HomeController(DAL.Request contextRequest)
        {
            _contextRequest = contextRequest;
        }
        public IActionResult Index()
        {
            ViewBag.requests = _contextRequest.GetAllByRowCount(5);
            return View();
        }

        [AllowAnonymous]
        [HttpPost]

        public IActionResult ChangeLang(string lang, string returnUrl)
        {
            var cultureInfo = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);

            this.HttpContext.Response.Cookies.Append
            (
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang))
            );

            return LocalRedirect(returnUrl);
        }
    }
}
