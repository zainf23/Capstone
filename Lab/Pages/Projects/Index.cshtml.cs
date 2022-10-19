using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class IndexModel : PageModel
    {
        //Bind ProjectID

        [BindProperty]
        [Required]
        public Project NewProject { get; set; }

        [BindProperty]
        [Required]
        public int ProjectID { get; set; }

        [BindProperty]
        public string SearchString { get; set; }


        public IndexModel()
        {
            NewProject = new Project();
        }

        // create list for dropdown menu
        public List<SelectListItem> ProjectNames { get; set; }

        // get method that pulls all data from Projects, and puts it into list

        public IActionResult OnGet()
        {
            SqlDataReader projectReader = DBClass.GeneralReaderQuery("Select * FROM Project");

            ProjectNames = new List<SelectListItem>();

            while (projectReader.Read())
            {
                ProjectNames.Add(
                    new SelectListItem(
                        projectReader["ProjectName"].ToString(),
                        projectReader["ProjectID"].ToString()));
            }
            projectReader.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();


        }
        // when dropdown menu selected, send projectID to next page for query
        public IActionResult OnPost(string searchstring)
        {
            HttpContext.Session.SetInt32("projectID", ProjectID);
            return RedirectToPage("ViewProjects");

        }

                      
    }
}
