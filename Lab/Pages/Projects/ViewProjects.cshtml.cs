using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class ViewProjectsModel : PageModel
    {
        [BindProperty]
        public ProjectTeam ProjectToView { get; set; }

        [BindProperty]
        public int ProjectID { get; set; }
        [BindProperty]
        public List<Project> ProjectInfoList { get; set; }


        public ViewProjectsModel()
        {
            ProjectToView = new ProjectTeam();
            ProjectInfoList = new List<Project>();
        }
        public IActionResult OnGet(int projectid)
        {
            //if (HttpContext.Session.GetInt32("projectID") == null)
            //{
            //    return RedirectToPage("Index");

            //}
            //ProjectID = Int32.Parse(HttpContext.Session.GetInt32("projectID").ToString());
            
            SqlDataReader singleProject = DBClass.SingleProjectReader(projectid);

            string sqlQuery = "Select * from Team where projectID =" + projectid;
            SqlDataReader singleTeam = DBClass.GeneralReaderQuery(sqlQuery);

            while (singleProject.Read())
            {
                ProjectInfoList.Add(new Project
                {
                    projectID = ProjectID,
                    projectName = singleProject["projectName"].ToString(),
                    projectOwner = singleProject["projectOwner"].ToString(),
                    projectOwnerEmail = singleProject["projectOwnerEmail"].ToString(),
                    projectMissionStatement = singleProject["projectMissionStatement"].ToString(),
                    projectDescription = singleProject["projectDescription"].ToString(),
                    projectDate = singleProject["projectDate"].ToString(),
                });

            }
            singleProject.Close();

            while (singleTeam.Read())
            {
                ProjectToView.teamID = Int32.Parse(singleTeam["teamID"].ToString());
                ProjectToView.projectID = Int32.Parse(singleTeam["projectID"].ToString());
                ProjectToView.teamName = singleTeam["teamName"].ToString();
                ProjectToView.teamDescription = singleTeam["teamDescription"].ToString();
                ProjectToView.teamEmailAddress = singleTeam["teamEmailAddress"].ToString();
            }
            singleTeam.Close();
            


            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }
    }
}
