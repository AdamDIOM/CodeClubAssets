using CodeClubAssets.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CodeClubAssets.Pages.Auth
{
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetCallbackAsync()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToPage("../Index");
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded) return RedirectToPage("../Index");
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var name = info.Principal.FindFirstValue(ClaimTypes.Name);
                ApplicationUser user = new ApplicationUser { UserName = email, Email = email };
                var uresult = await _userManager.CreateAsync(user);
                if (uresult.Succeeded)
                {
                    uresult = await _userManager.AddLoginAsync(user, info);
                    if (uresult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        return RedirectToPage("../Index");
                    }
                }
            }
            return RedirectToPage("Login");
        }
    }
}
