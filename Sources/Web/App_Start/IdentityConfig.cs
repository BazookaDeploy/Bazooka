using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Auth;
using Web.Models;
using Microsoft.Owin.Security.DataProtection;
using System.Net.Mail;

namespace Web.App_Start
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public override Task SendEmailAsync(string userId, string subject, string body)
        {
            var mail = new System.Net.Mail.MailMessage("bazooka@cgn.it", userId);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient client = new SmtpClient();
            return client.SendMailAsync(mail);
        }

        public override Task<bool> IsEmailConfirmedAsync(string userId)
        {
            var t = Task.FromResult<bool>(true);
            return t;
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            var provider = new DpapiDataProtectionProvider("Sample");
            manager.UserTokenProvider =  new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));

            if (ActiveDirectoryAuthentication.IsADAuthenticationEnabled())
            {
                // if AD authentication is enabled we defer to their password policy
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 2,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                };
            }
            else
            {
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };
            }

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;


            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (ActiveDirectoryAuthentication.IsADAuthenticationEnabled())
            {
                if (ActiveDirectoryAuthentication.Authenticate(userName, password))
                {
                    return base.PasswordSignInAsync(userName, "bazooka", isPersistent, shouldLockout);
                }
            }

            return base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
