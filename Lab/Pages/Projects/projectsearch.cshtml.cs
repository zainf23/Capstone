using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class projectsearchModel : PageModel
    {

        [BindProperty]
        public List<Project> ProjectSearchList { get; set; }

        public projectsearchModel()
        {
            ProjectSearchList= new List<Project>();
        }
        public void OnGet(string searchstring)
        {
            string sqlQuery = "Select * from Project WHERE projectName like '%" + searchstring + "%'";
            SqlDataReader projectsearch = DBClass.GeneralReaderQuery(sqlQuery);

            while (projectsearch.Read())
            {
                ProjectSearchList.Add(new Project
                {
                    projectID = Int32.Parse(projectsearch["projectID"].ToString()),
                    userID = Int32.Parse(projectsearch["projectID"].ToString()),
                    projectName = projectsearch["projectName"].ToString(),
                    projectOwner = projectsearch["projectOwner"].ToString(),
                    projectOwnerEmail = projectsearch["projectOwnerEmail"].ToString(),
                    projectMissionStatement = projectsearch["projectMissionStatement"].ToString(),
                    projectDescription = projectsearch["projectDescription"].ToString(),
                    projectDate = projectsearch["projectDate"].ToString(),

                });
            }
            projectsearch.Close();


        }
    }
}
