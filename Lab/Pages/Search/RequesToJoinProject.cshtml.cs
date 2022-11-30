using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Reflection;

namespace Lab.Pages.Search
{
    public class RequesToJoinProjectModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public int userIDTwo { get; set; }

        [BindProperty]
        public int projectID { get; set; }

        [BindProperty]
        public int teamID { get; set; }

        [BindProperty]
        public string userPitch { get; set; }

        public List<SelectListItem> Projects { get; set; }

        public List<TeamUser> Members { get; set; }

        public RequesToJoinProjectModel()
        {
            Members = new List<TeamUser>();
        }


        public void OnGet(int userid)
        {
            userID = userid;
            username = HttpContext.Session.GetString("username");

            string sqlQuery = "SELECT userID from [USER] WHERE username = '" + username + "'";
            SqlDataReader userFinder = DBClass.GeneralReaderQuery(sqlQuery);
            while (userFinder.Read())
            {
                userIDTwo = Int32.Parse(userFinder["userID"].ToString());
            }
            userFinder.Close();

            string sqlQuery2 = "Select * from Project Where userID = " + userIDTwo;
            SqlDataReader projectReader = DBClass.GeneralReaderQuery(sqlQuery2);
            Projects = new List<SelectListItem>();

            while (projectReader.Read())
            {
                Projects.Add(
                    new SelectListItem(
                        projectReader["projectName"].ToString(),
                        projectReader["projectID"].ToString()));
            }
            projectReader.Close();

        }

        public IActionResult OnPost()
        {
            string sqlQueryOne = "Select teamID from Team Where projectID = " + projectID;
            SqlDataReader teamFinder = DBClass.GeneralReaderQuery(sqlQueryOne);
            while (teamFinder.Read())
            {
                teamID = Int32.Parse(teamFinder["teamID"].ToString());
            }
            teamFinder.Close();

            string sqlQuery3 = "Select userID from TeamUser Where teamID = " + teamID;
            SqlDataReader memberReader = DBClass.GeneralReaderQuery(sqlQuery3);

            while (memberReader.Read())
            {
                Members.Add(new TeamUser
                {
                    userID = Int32.Parse(memberReader["userID"].ToString())
                }); ;

            }
            memberReader.Close();

            foreach (var prod in Members)
            {
                if (prod.userID == userID)
                {
                    ViewData["ErrorMessage"] = "This user is already a member on your Project!";
                    return Page();

                }
            }

                string sqlQuery = "INSERT INTO RequestTwo (userID, projectID, teamID, acceptedTwo, userPitchTwo) VALUES (";
            sqlQuery += userID + ",";
            sqlQuery += projectID + ",";
            sqlQuery += teamID + ",";
            sqlQuery += 0 + ",'";
            sqlQuery += userPitch + "')";

            DBClass.InsertQuery(sqlQuery);
            return RedirectToPage("Index");
        }
    }
}
