using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace Lab.Pages.Search
{
    public class BookmarkUserModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int mainUserID { get; set; }

        [BindProperty]
        public int otherUserID { get; set; }

        [BindProperty]
        public string reason { get; set; }
       
        public void OnGet(int userid)
        {
            otherUserID = userid;
            
            username = HttpContext.Session.GetString("username");

            string sqlQuery = "SELECT userID from [USER] WHERE username = '" + username + "'";

            SqlDataReader userFinder = DBClass.GeneralReaderQuery(sqlQuery);
            while (userFinder.Read())
            {
               mainUserID = Int32.Parse(userFinder["userID"].ToString());
            }
            userFinder.Close();


        }
        public IActionResult OnPost()
        {

            string sqlQuery = "INSERT INTO Bookmark (userID, otherUserID, reason) VALUES (";
            sqlQuery += mainUserID + ",";
            sqlQuery += otherUserID + ",'";
            sqlQuery += reason + "')";

            DBClass.InsertQuery(sqlQuery);
            return RedirectToPage("Index");
        }
    }
}
