using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.Pages.Login
{
    public class HashedLoginModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string passphrase { get; set; }

        public IActionResult OnGet(string logout)
        {
            //if log out is pressed, clear session state and send success message
            if (logout != null)
            {
                HttpContext.Session.Clear();
                ViewData["ErrorMessage"] = "Logged Out Successfully!";
            }

            //if user is already logged in, send to landing page
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToPage("LandingPage");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (DBClass.HashedParameterLogin(username, passphrase))
            {
                HttpContext.Session.SetString("username", username);
                ViewData["LoginMessage"] = "Login Successful!";
                return RedirectToPage("LandingPage");
            }
            else
            {
                ViewData["LoginMessage"] = "Username and/or Password Incorrect";
                return Page();
            }

        }
    }
}
