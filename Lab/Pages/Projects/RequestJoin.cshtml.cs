using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class RequestJoinModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public int projectID { get; set; }


        [BindProperty]
        public int teamID { get; set; }

        [BindProperty]
        public string userPitch { get; set; }



        public void OnGet(int teamid)
        {
            teamID = teamid;
            username = HttpContext.Session.GetString("username");
            string sqlQuery = "SELECT userID from [USER] WHERE username = '" + username + "'";

            SqlDataReader userFinder = DBClass.GeneralReaderQuery(sqlQuery);
            while (userFinder.Read())
            {
                userID = Int32.Parse(userFinder["userID"].ToString());
            }
            userFinder.Close();


            string sqlQuery2 = "SELECT projectID from Team WHERE teamID = " + teamid;

            SqlDataReader projectFinder = DBClass.GeneralReaderQuery(sqlQuery2);
            while (projectFinder.Read())
            {
                projectID = Int32.Parse(projectFinder["projectID"].ToString());
            }
            userFinder.Close();
        }

        public IActionResult OnPost()
        {
            string sqlQuery = "INSERT INTO Request (userID, projectID, teamID, accepted, userPitch) VALUES (";
            sqlQuery += userID + ",";
            sqlQuery += projectID + ",";
            sqlQuery += teamID + ",";
            sqlQuery += 0 + ",'";
            sqlQuery += userPitch + "')";

            DBClass.InsertQuery(sqlQuery);
            return RedirectToPage("ViewProjects");
        }
    }
}
