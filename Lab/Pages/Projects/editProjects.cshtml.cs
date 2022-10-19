using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class editProjectsModel : PageModel
    {

        [BindProperty]
        public Project ProjectToUpdate { get; set; }

        public editProjectsModel()
        {
            ProjectToUpdate = new Project();
        }
        // get method that reads project info into model
        public IActionResult OnGet(int projectid)
        {
            SqlDataReader singleProject = DBClass.SingleProjectReader(projectid);

            while (singleProject.Read())
            {
                ProjectToUpdate.projectID = projectid;
                ProjectToUpdate.projectName = singleProject["projectName"].ToString();
                ProjectToUpdate.projectOwner = singleProject["projectOwner"].ToString();
                ProjectToUpdate.projectOwnerEmail = singleProject["projectOwnerEmail"].ToString();
                ProjectToUpdate.projectMissionStatement = singleProject["projectMissionStatement"].ToString();
                ProjectToUpdate.projectDescription = singleProject["projectDescription"].ToString();
                ProjectToUpdate.projectDate = singleProject["projectDate"].ToString();
            }

            singleProject.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            DBClass.UpdateProject(ProjectToUpdate);

            return RedirectToPage("Index");
        }
    }
}
