using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.Pages.Projects
{
    public class CompleteMeetingModel : PageModel
    {
        [BindProperty]
        public string meetingSummary { get; set; }

        [BindProperty]
        public int teamMeetingID { get; set; }

        [BindProperty]
        public int projectID { get; set; }

        public IActionResult OnGet(int teammeetingid, int projectid)
        {
            projectID = projectid;
            teamMeetingID = teammeetingid;
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            DBClass.UpdateMeeting(meetingSummary, teamMeetingID);

            return RedirectToPage("MyProjects");
        }
    }
}
