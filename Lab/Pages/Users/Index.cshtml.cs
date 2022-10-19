using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace Lab.Pages.Users
{
    public class IndexModel : PageModel
    {

        public List<User> UserList { get; set; }

        public IndexModel()
        {
            UserList = new List<User>();
        }

        // get method that pulls all user data from user table
        public IActionResult OnGet(string username)
        {
            username = HttpContext.Session.GetString("username");
            SqlDataReader userReader = DBClass.UserReader(username);
            while (userReader.Read())
            {
                UserList.Add(new User
                {
                    userID = Int32.Parse(userReader["userID"].ToString()),
                    firstName = userReader["firstName"].ToString(),
                    secondName = userReader["secondName"].ToString(),
                    email = userReader["email"].ToString(),
                    jmuType = userReader["jmuType"].ToString(),
                    interests = userReader["interests"].ToString(),
                    experience = userReader["experience"].ToString(),
                    gradYear = userReader["gradYear"].ToString(),
                    major = userReader["gradYear"].ToString(),
                    minor = userReader["minor"].ToString(),
                    jobTitle = userReader["jobTitle"].ToString(),
                    department = userReader["department"].ToString(),
                    moreInfo = userReader["moreInfo"].ToString(),


                });
            }

            userReader.Close();
           
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }


    }
}
