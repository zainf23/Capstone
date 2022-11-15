using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Messages
{
    public class UserSearchModel : PageModel
    {
        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public string otherUserID { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        [BindProperty]
        public List<User> UserSearchList { get; set; }

        public UserSearchModel()
        {
            UserSearchList = new List<User>();
        }

        public IActionResult OnGet(int userid)
        {
            userID = userid;
            HttpContext.Session.SetInt32("userid", userID);

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }
            return Page();

        }

        public IActionResult OnPost()
        {
            userID = (int)HttpContext.Session.GetInt32("userID");

            string sqlQuery = "SELECT DISTINCT u.userID, firstName, secondName, username, email, jmuType, gradYear, major, minor, jobtitle, department, moreInfo, upp.fileName FROM [User] u, UserProfilePic upp WHERE u.userID = upp.userID AND (firstName like '%";
            sqlQuery += SearchString + "%' OR secondName like '%";
            sqlQuery += SearchString + "%')";

            SqlDataReader usersearch = DBClass.GeneralReaderQuery(sqlQuery);

            while (usersearch.Read())
            {
                UserSearchList.Add(new User
                {

                    otherUserID = Int32.Parse(usersearch["userID"].ToString()),
                    firstName = usersearch["firstName"].ToString(),
                    secondName = usersearch["secondName"].ToString(),
                    username = usersearch["username"].ToString(),
                    email = usersearch["email"].ToString(),
                    jmuType = usersearch["jmuType"].ToString(),
                    gradYear = usersearch["gradYear"].ToString(),
                    major = usersearch["gradYear"].ToString(),
                    minor = usersearch["minor"].ToString(),
                    jobTitle = usersearch["jobTitle"].ToString(),
                    department = usersearch["department"].ToString(),
                    moreInfo = usersearch["moreInfo"].ToString(),
                    fileName = usersearch["fileName"].ToString(),


                });
            }
            usersearch.Close();


            return Page();



        }

    }
}
