using Application.AccountApp;
using Domain.UserAgg;
using Infrastructure;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Server.Pages.Account
{
    public class LoginModel : BasePageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly ApplicationSettings _applicationSettings;

        public LoginModel
            (IAccountApplication accountApplication,
            ApplicationSettings applicationSettings) : base()
        {
            ViewModel = new();
            _accountApplication = accountApplication;
            _applicationSettings = applicationSettings;
        }

        [Microsoft.AspNetCore.Mvc.BindProperty]
        public string? ReturnUrl { get; set; }

        [Microsoft.AspNetCore.Mvc.BindProperty]
        public ViewModels.Pages.Account.LoginViewModel ViewModel { get; set; }


        public void OnGet(string? returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        public async Task
            <Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }


            // **************************************************
            var user = new Domain.UserAgg.User();

            if ((ViewModel.Username == _applicationSettings.AdminUserPass.Username) &&
                 (ViewModel.Password == _applicationSettings.AdminUserPass.Password))
            {
                user = new Domain.UserAgg.User
                {
                    FirstName = "مدیر",
                    LastName = "سیستم",
                    EmailAddress = "Admin@domain.local",
                    Username = "Admin",
                    Role = "Admin"
                };
            }
            else
            {
                user = await _accountApplication.AuthenticateUser(ViewModel);

                if (user == null)
                {
                    AddPageError(message:
                        "نام کاربری و رمز عبور اشتباه است");

                    return Page();
                }

            }
            var claims =
                new List<Claim>();

            Claim claim;

            claim = GetUserClaims(user, claims);

            var claimsIdentity =
                new ClaimsIdentity(claims: claims,
                authenticationType: Infrastructure.Security.Utility.AuthenticationScheme);

            var claimsPrincipal =
                new ClaimsPrincipal(identity: claimsIdentity);

            var authenticationProperties =
                new AuthenticationProperties
                {
                    IsPersistent = ViewModel.RememberMe,
                };

            await HttpContext.SignInAsync
                (scheme: Infrastructure.Security.Utility.AuthenticationScheme,
                principal: claimsPrincipal, properties: authenticationProperties);


            if (string.IsNullOrWhiteSpace(ReturnUrl))
            {
                return RedirectToPage(pageName: "/Index");
            }
            else
            {
                return Redirect(url: ReturnUrl);
            }
        }

        private static Claim GetUserClaims(User user, List<Claim> claims)
        {
            // **************************************************
            Claim claim = new Claim
                (type: "FullName", value: string.Concat(user.FirstName, " ", user.LastName));
            claims.Add(item: claim);
            // **************************************************

            // **************************************************
            claim =
                new Claim
                (type: ClaimTypes.Role, value: user.Role ?? "User");

            claims.Add(item: claim);
            // **************************************************

            // **************************************************
            claim =
                new Claim
                (type: ClaimTypes.Name, value: user.Username);

            claims.Add(item: claim);
            // **************************************************

            // **************************************************
            claim =
                new Claim
                (type: ClaimTypes.Email, value: user.EmailAddress);

            claims.Add(item: claim);
            return claim;
        }
    }
}
