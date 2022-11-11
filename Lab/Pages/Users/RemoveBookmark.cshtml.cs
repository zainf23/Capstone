using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Users
{
    public class RemoveBookmarkModel : PageModel
    {

        public int userID { get; set; }

        public string username { get; set; }

        public IActionResult OnGet(int userid)
        {

            username = HttpContext.Session.GetString("username");
            SqlDataReader userReader = DBClass.UserReader(username);

            while (userReader.Read())
            {
                userID = Int32.Parse(userReader["userID"].ToString());

            }
            userReader.Close();

            string sqlQuery = "Delete FROM Bookmark WHERE userID =" + userID + " AND otherUserID =" + userid;
            DBClass.GeneralReaderQuery(sqlQuery);

            return RedirectToPage("ViewProfiles");
        }
    }
}
