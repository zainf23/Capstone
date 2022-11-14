using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class MyProjectsModel : PageModel
    {

        [BindProperty]
        public Project MyProject { get; set; }

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public string fileName { get; set; }

        [BindProperty]
        public List<Project> MyProjectList { get; set; }

        [BindProperty]
        public List<Project> MemberProjectList { get; set; }

        public MyProjectsModel()
        {
            MyProject = new Project();
            MyProjectList = new List<Project>();
            MemberProjectList = new List<Project>();
        }

        public IActionResult OnGet()
        {
            username = HttpContext.Session.GetString("username");
            string sqlQuery = "SELECT userID from [USER] WHERE username = '" + username + "'";

            SqlDataReader userFinder = DBClass.GeneralReaderQuery(sqlQuery);
            while(userFinder.Read())
            {
                userID = Int32.Parse(userFinder["userID"].ToString());
            }
            userFinder.Close();

            
            string sqlQuery2 = "SELECT * from Project WHERE userID = '" + userID + "'";
            SqlDataReader projectFinder = DBClass.GeneralReaderQuery(sqlQuery2);
            
            while (projectFinder.Read())
            {
                MyProjectList.Add(new Project
                {
                    projectID = Int32.Parse(projectFinder["projectID"].ToString()),
                    userID = Int32.Parse(projectFinder["userID"].ToString()),
                    projectName = projectFinder["projectName"].ToString(),
                    projectOwner = projectFinder["projectOwner"].ToString(),
                    projectOwnerEmail = projectFinder["projectOwnerEmail"].ToString(),
                    projectMissionStatement = projectFinder["projectMissionStatement"].ToString(),
                    projectDescription = projectFinder["projectDescription"].ToString(),
                    projectDate = projectFinder["projectDate"].ToString(),
                    fileName = projectFinder["fileName"].ToString(),

                });
            }
            projectFinder.Close();



            string sqlQuery3 = @"Select p.projectID, p.projectName, p.projectOwner, p.projectOwnerEmail, p.projectOwnerEmail, p.projectMissionStatement, 
                            p.projectDescription, p.projectDate, p.fileName from Project p, Team t, TeamUser tu WHERE (p.projectID = t.projectID AND t.teamID = tu.teamID AND
                            p.userID = tu.teamID) AND (tu.userID = " + userID + "AND + p.userID !=" + userID + ")";
            SqlDataReader otherFinder = DBClass.GeneralReaderQuery(sqlQuery3);

            while (otherFinder.Read())
            {
                MemberProjectList.Add(new Project
                {
                    projectID = Int32.Parse(otherFinder["projectID"].ToString()),
                    projectName = otherFinder["projectName"].ToString(),
                    projectOwner = otherFinder["projectOwner"].ToString(),
                    projectOwnerEmail = otherFinder["projectOwnerEmail"].ToString(),
                    projectMissionStatement = otherFinder["projectMissionStatement"].ToString(),
                    projectDescription = otherFinder["projectDescription"].ToString(),
                    projectDate = otherFinder["projectDate"].ToString(),
                    fileName = otherFinder["fileName"].ToString(),
                });
            }
            otherFinder.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();

        }
    }
}
