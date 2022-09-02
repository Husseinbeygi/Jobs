using Application.AccountApp;
using Infrastructure;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            //if ((ViewModel.Username == "Admin") &&
            //    (ViewModel.Password == "Admin"))
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
                new List<System.Security.Claims.Claim>();

            System.Security.Claims.Claim claim;

            // **************************************************
            claim =
                new System.Security.Claims.Claim
                (type: "FullName", value: string.Concat(user.FirstName, " ", user.LastName));

            claims.Add(item: claim);
            // **************************************************

            // **************************************************
            //claim =
            //	new System.Security.Claims.Claim
            //	(type: "Role", value: "Admin");

            claim =
                new System.Security.Claims.Claim
                (type: System.Security.Claims.ClaimTypes.Role, value: user.Role ?? "User");

            claims.Add(item: claim);
            // **************************************************

            // **************************************************
            //claim =
            //	new System.Security.Claims.Claim
            //	(type: "Username", value: "Dariush");

            claim =
                new System.Security.Claims.Claim
                (type: System.Security.Claims.ClaimTypes.Name, value: user.Username);

            claims.Add(item: claim);
            // **************************************************

            // **************************************************
            claim =
                new System.Security.Claims.Claim
                (type: System.Security.Claims.ClaimTypes.Email, value: user.EmailAddress);

            claims.Add(item: claim);
            // **************************************************
            // **************************************************
            // **************************************************

            // **************************************************
            // **************************************************
            // **************************************************
            var claimsIdentity =
                new System.Security.Claims.ClaimsIdentity(claims: claims,
                authenticationType: Infrastructure.Security.Utility.AuthenticationScheme);
            // **************************************************
            // **************************************************
            // **************************************************

            // **************************************************
            // **************************************************
            // **************************************************
            //var claimsPrincipal =
            //	new System.Security.Claims.ClaimsPrincipal();

            //claimsPrincipal.AddIdentity(identity: claimsIdentity);

            var claimsPrincipal =
                new System.Security.Claims.ClaimsPrincipal(identity: claimsIdentity);
            // **************************************************
            // **************************************************
            // **************************************************

            // **************************************************
            // **************************************************
            // **************************************************
            var authenticationProperties =
                new AuthenticationProperties
                {
                    IsPersistent = ViewModel.RememberMe,
                };
            // **************************************************
            // **************************************************
            // **************************************************

            // **************************************************
            // **************************************************
            // **************************************************
            // SignInAsync -> using Microsoft.AspNetCore.Authentication;
            await HttpContext.SignInAsync
                (scheme: Infrastructure.Security.Utility.AuthenticationScheme,
                principal: claimsPrincipal, properties: authenticationProperties);
            // **************************************************
            // **************************************************
            // **************************************************

            if (string.IsNullOrWhiteSpace(ReturnUrl))
            {
                return RedirectToPage(pageName: "/Index");
            }
            else
            {
                return Redirect(url: ReturnUrl);
            }
        }
    }
}
