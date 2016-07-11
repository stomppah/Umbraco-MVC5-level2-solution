using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Umbraco.Course.Controllers
{
    public class LoginController : SurfaceController
    {
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            if (Members.Login(model.Username, model.Password))
                return Redirect("/stream");

            ModelState.AddModelError("", "Invalid Login");

            return CurrentUmbracoPage();
        }

        public ActionResult Logout()
        {
            Members.Logout();
            return Redirect("/");
        }
    }
}
