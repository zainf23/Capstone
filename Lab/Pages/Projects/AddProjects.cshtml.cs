using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class AddProjectsModel : PageModel
    {
        [BindProperty]
        [Required]
        public Project NewProject { get; set; }

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int userID { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            username = HttpContext.Session.GetString("username");
            string sqlQuery = "SELECT userID from [USER] WHERE username = '" + username + "'";
            SqlDataReader userFinder = DBClass.GeneralReaderQuery(sqlQuery);
            while (userFinder.Read())
            {
                NewProject.userID = Int32.Parse(userFinder["userID"].ToString());
            }
            userFinder.Close();

            DBClass.InsertProject(NewProject);

            return RedirectToPage("Index");
        }

        // populate button code
        public IActionResult OnPostPopulateHandler()
        {

            if (!ModelState.IsValid)
            {
                ModelState.Clear();

                NewProject.projectName = "Learn Sql";
                NewProject.projectOwner = "Nick Carpenter";
                NewProject.projectOwnerEmail = "carpenternick801@gmail.com";
                NewProject.projectMissionStatement = "To Learn like a g";
                NewProject.projectDescription = "I wanna learn SQL";
                NewProject.projectDate = "09-15-2022";

            }

            return Page();

        }
        // clear button code

        public IActionResult OnPostClearHandler()
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                NewProject.projectName = "";
                NewProject.projectOwner = "";
                NewProject.projectOwnerEmail = "";
                NewProject.projectMissionStatement = "";
                NewProject.projectDescription = "";
                NewProject.projectDate = "";
            }
            return Page();

        }
    }
}
