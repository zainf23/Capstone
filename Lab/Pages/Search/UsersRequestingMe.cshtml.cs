using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Search
{
    public class UsersRequestingMeModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public List<ProjectTeam> MyRequestsList { get; set; }

        public UsersRequestingMeModel()
        {
            MyRequestsList = new List<ProjectTeam>();
        }
        public void OnGet()
        {
            username = HttpContext.Session.GetString("username");
            
            string sqlQuery = "SELECT userID from [USER] WHERE username = '" + username + "'";
            SqlDataReader userFinder = DBClass.GeneralReaderQuery(sqlQuery);
            while (userFinder.Read())
            {
                userID = Int32.Parse(userFinder["userID"].ToString());
            }
            userFinder.Close();

            
            string sqlQueryTwo = "SELECT rt.requestIDTwo, p.projectID, p.projectOwner, p.projectName, p.projectDescription, p.fileName, t.teamName, rt.userPitchTwo from Project p, Team t, RequestTwo rt WHERE (p.projectID = t.projectID AND t.teamID = rt.teamID AND p.projectID = rt.projectID) AND rt.userID =" + userID + "AND acceptedTwo = 0";
            SqlDataReader requestFinder = DBClass.GeneralReaderQuery(sqlQueryTwo);
            while (requestFinder.Read())
            {
                MyRequestsList.Add(new ProjectTeam
                {
                    requestIDTwo = Int32.Parse(requestFinder["requestIDTwo"].ToString()),
                    projectID = Int32.Parse(requestFinder["projectID"].ToString()),
                    projectOwner = requestFinder["projectOwner"].ToString(),
                    projectName = requestFinder["projectName"].ToString(),
                    projectDescription = requestFinder["projectDescription"].ToString(),
                    fileName = requestFinder["fileName"].ToString(),
                    teamName = requestFinder["teamName"].ToString(),
                    userPitchTwo = requestFinder["userPitchTwo"].ToString(),

                });
            }
            requestFinder.Close();


        }
    }
}
