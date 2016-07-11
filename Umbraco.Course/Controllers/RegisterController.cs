using System.Web.Mvc;
using Umbraco.Course.Models;
using Umbraco.Web.Mvc;

namespace Umbraco.Course.Controllers
{
    public class RegisterController : SurfaceController
    {
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            var memberService = Services.MemberService;
            if (memberService.GetByEmail(model.Email) != null)
            {
                ModelState.AddModelError(
                    "", "A Member with that email already exists."
                    );
                return CurrentUmbracoPage();
            }

            var member = memberService.CreateMemberWithIdentity(
                model.Email, model.Email, model.Name, "IntranetMember"
                );
            member.SetValue("biography", model.Biography);
            member.SetValue("avatar", model.Avatar);
            // Don't forget to save!
            memberService.Save(member);
            
            // Add to the intranet members group
            memberService.AssignRole(member.Id, "Intranet");

            memberService.SavePassword(member, model.Password);
            Members.Login(model.Email, model.Password);

            return Redirect("/");
        }
    }
}
