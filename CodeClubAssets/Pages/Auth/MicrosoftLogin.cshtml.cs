using CodeClubAssets.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeClubAssets.Pages.Auth
{
    public class MicrosoftLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public MicrosoftLoginModel(
            SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult OnGet()
        {
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Microsoft", redirectUrl);
            return new ChallengeResult("Microsoft", properties);
        }
    }
}
