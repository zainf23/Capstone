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
        public List<Project> MyProjectList { get; set; }

        public MyProjectsModel()
        {
            MyProject = new Project();
            MyProjectList = new List<Project>();
        }

        public void OnGet()
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

                });
            }
            projectFinder.Close();
        }
    }
}
