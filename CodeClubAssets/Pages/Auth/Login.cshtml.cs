using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using CodeClubAssets.Data;

namespace CodeClubAssets.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [Required]
        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(SignInManager<ApplicationUser> sim, UserManager<ApplicationUser> um)
        {
            _signInManager = sim;
            _userManager = um;
        }
        public async Task OnGetAsync()
        {
            var u = await _userManager.FindByEmailAsync("assets@codeclub.im");
            if (u == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = "assets@codeclub.im", UserName = "assets@codeclub.im" };
                var result = await _userManager.CreateAsync(admin, "loveYourChamber91021!");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Email, Password, false, false);
                if (result.Succeeded)
                {
                    return Redirect("/Manage");
                }
                ModelState.AddModelError(string.Empty, "Invalid Logon Attempt");
            }
            return Page();
        }
    }
}
