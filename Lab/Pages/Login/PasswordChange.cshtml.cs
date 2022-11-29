using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.SqlClient;

namespace Lab.Pages.Login
{
    public class PasswordChangeModel : PageModel
    {
        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public string passphrase { get; set; }

        [BindProperty]
        public string passphrase2 { get; set; }

        [BindProperty]
        public string username { get; set; }

        public void OnGet()
        {
            userID = (int)HttpContext.Session.GetInt32("userID");

        }

        public IActionResult OnPost()
        {
            if (userID == 0)
            {
                userID = (int)HttpContext.Session.GetInt32("userID");
            }

            String SqlQuery = "Select username from [User] where userID = " + userID;
            SqlDataReader UserReader = DBClass.GeneralReaderQuery(SqlQuery);
            while (UserReader.Read())
            {
                username = UserReader["username"].ToString();
            }
            UserReader.Close();

            if (passphrase.Equals(passphrase2))
            {
                DBClass.UpdateHashedUser(username,passphrase);
                return RedirectToPage("/Login/HashedLogin");
            }
            ViewData["ErrorMessage"] = "Passwords don't match, please try again!";
            return Page();
        }
    }
}
