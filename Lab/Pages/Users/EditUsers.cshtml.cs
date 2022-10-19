using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Users
{
    public class EditUsersModel : PageModel
    {
        [BindProperty]
        public User UserToUpdate { get; set; }

        public EditUsersModel()
        {
            UserToUpdate = new User();
        }

        // get user info from SingleUserReader

        public IActionResult OnGet(int userid)
        {
            SqlDataReader singleUser = DBClass.SingleUserReader(userid);

            while (singleUser.Read())
            {
                UserToUpdate.userID = userid;
                UserToUpdate.firstName = singleUser["firstName"].ToString();
                UserToUpdate.secondName = singleUser["secondName"].ToString();
                UserToUpdate.email = singleUser["email"].ToString();
                UserToUpdate.jmuType = singleUser["jmuType"].ToString();
                UserToUpdate.interests = singleUser["interests"].ToString();
                UserToUpdate.experience = singleUser["experience"].ToString();
                UserToUpdate.gradYear = singleUser["gradYear"].ToString();
                UserToUpdate.major = singleUser["major"].ToString();
                UserToUpdate.minor = singleUser["minor"].ToString();
                UserToUpdate.jobTitle = singleUser["jobTitle"].ToString();
                UserToUpdate.department = singleUser["department"].ToString();
                UserToUpdate.moreInfo = singleUser["moreInfo"].ToString();
            }

            singleUser.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }
        // update user info
        public IActionResult OnPost()
        {
            DBClass.UpdateUser(UserToUpdate);

            return RedirectToPage("ViewProfiles");
        }
    }
}
