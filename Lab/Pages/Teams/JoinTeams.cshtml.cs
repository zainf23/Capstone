using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Lab.Pages.Teams
{
    public class JoinTeamsModel : PageModel
    {

        [BindProperty]
        public ProjectTeam JoinTeam { get; set; }

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int userID { get; set; }
       
        public JoinTeamsModel()
        {
            JoinTeam = new ProjectTeam();

        }

        public IActionResult OnGet(int teamid)
        {
            username = HttpContext.Session.GetString("username");
            string sqlQuery = "SELECT userID from [USER] WHERE username = '" + username + "'";
            SqlDataReader userFinder = DBClass.GeneralReaderQuery(sqlQuery);
            while (userFinder.Read())
            {
                JoinTeam.userID = Int32.Parse(userFinder["userID"].ToString());
            }
            userFinder.Close();

            string sqlQuery1 = "INSERT INTO TeamUser (userID, teamID) VALUES (" + JoinTeam.userID + "," + teamid + ")";
            DBClass.InsertMemberQuery(sqlQuery1);
            if (JoinTeam.userID != null)
            {
                return RedirectToPage("Index");
            }
           
            return Page();
        }

    }
}
