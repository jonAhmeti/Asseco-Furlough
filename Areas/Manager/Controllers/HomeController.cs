using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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
