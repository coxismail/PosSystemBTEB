using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity.Owin;

namespace PosSystem.Account
{
    public partial class Login : Page
    {

        protected async void LogIn(object sender, EventArgs e)
        {
            FailureText.Visible = true;
            ErrorMessage.Visible = true;
            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
                var user = await manager.FindByEmailAsync(Email.Text);
                if (user == null)
                {
                    FailureText.Text = "You are not registered user";
                    return;
                }

                else if (user.EmailConfirmed == false)
                {
                    FailureText.Text = "Please Confirm you account before login";
                    return;
                }
                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                else if (await manager.IsLockedOutAsync(user.Id))
                {
                    FailureText.Text = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", "5 Minite");
                    return;
                }
                var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: true);

                switch (result)
                {
                    case SignInStatus.Success:
                        await manager.ResetAccessFailedCountAsync(user.Id);
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        FailureText.Text = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", "5 Minite");
                        break;
                    case SignInStatus.Failure:
                        var count = await manager.GetAccessFailedCountAsync(user.Id);
                        if (count > 0)
                        {
                            int accessFailedCount = count;
                            int attemptsLeft = 5 - accessFailedCount;
                            FailureText.Text = string.Format(
                                    "Invalid credentials. You have {0} more attempt(s) before your account gets locked out.", attemptsLeft);
                            return;
                        }

                        break;
                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }
    }
}