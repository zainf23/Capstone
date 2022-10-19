using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab.Pages.Teams
{
    public class AddTeamsModel : PageModel
    {

        [BindProperty]
        public ProjectTeam ProjectTeam { get; set; }
        
        [BindProperty]
        public int projectID {get; set;}

        public List<SelectListItem> Projects { get; set; }

        public IActionResult OnGet()
        {
            //populate the projectname select control
            SqlDataReader projectReader = DBClass.GeneralReaderQuery("Select * from Project");
            Projects = new List<SelectListItem>();

            while (projectReader.Read())
            {
                Projects.Add(
                    new SelectListItem(
                        projectReader["projectName"].ToString(),
                        projectReader["projectID"].ToString()));
            }
            projectReader.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

        // creates new team
        public IActionResult OnPost()
        {
            string insertQuery = "Insert Into Team (teamName,teamDescription,teamEmailAddress,projectID) VALUES (";
            insertQuery += "'" + ProjectTeam.teamName + "',";
            insertQuery += "'" + ProjectTeam.teamDescription + "',";
            insertQuery += "'" + ProjectTeam.teamEmailAddress + "',";
            insertQuery += "'" + projectID + "')";
            //DBClass.InsertTeam(ProjectTeam);
            DBClass.InsertQuery(insertQuery);
            return RedirectToPage("Index");


        }
        // populate and clear form
        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();

                ProjectTeam.teamName = "Learn HTML";
                ProjectTeam.teamDescription = "Learning HTML";
                ProjectTeam.teamEmailAddress = "RIPfranklin@gmail.com";
                SqlDataReader projectReader = DBClass.GeneralReaderQuery("Select * from Project");
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

            return Page();
        }

        public IActionResult OnPostClearHandler()
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                ProjectTeam.teamName = "";
                ProjectTeam.teamDescription = "";
                ProjectTeam.teamEmailAddress = "";
                ProjectTeam.projectName = "";
            }
            return Page();

        }


    }
}
