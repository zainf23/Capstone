using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class AddMeetingModel : PageModel
    {


        [BindProperty]
        public int projectID { get; set;}

        [BindProperty]
        public string meetingTitle { get; set; }

        [BindProperty]
        public string meetingDate { get; set; }

        [BindProperty]
        public string meetingTime { get; set; }

        [BindProperty]
        public string meetingPlan { get; set; }

        [BindProperty]
        public string meetingLocation { get; set; }

        public IActionResult OnGet(int projectid)
        {
            HttpContext.Session.SetInt32("projectID", projectid);
            projectID = projectid;

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            DBClass.InsertMeeting(projectID,meetingTitle,meetingDate,meetingTime,meetingPlan,meetingLocation);


            return RedirectToPage("ViewProjects");
        }


    }
}
