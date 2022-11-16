using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class EditMeetingModel : PageModel
    {

        [BindProperty]
        public TeamMeeting MeetingToUpdate { get; set; }

        [BindProperty]
        public int projectID {get; set;}

        public EditMeetingModel()
        {
            MeetingToUpdate = new TeamMeeting();
        }

        public void OnGet(int teammeetingid,int projectid)
        {
            HttpContext.Session.SetInt32("projectID", projectid);
            projectID = projectid;
            SqlDataReader singleUser = DBClass.SingleMeetingReader(teammeetingid);

            while (singleUser.Read())
            {
                MeetingToUpdate.teamMeetingID = teammeetingid;
                MeetingToUpdate.meetingTitle = singleUser["meetingTitle"].ToString();
                MeetingToUpdate.meetingTime = singleUser["meetingTime"].ToString();
                MeetingToUpdate.meetingDate = singleUser["meetingDate"].ToString();
                MeetingToUpdate.meetingLocation = singleUser["meetingLocation"].ToString();
                MeetingToUpdate.meetingPlan = singleUser["meetingPlan"].ToString();
            }

            singleUser.Close();
        }

        public IActionResult OnPost()
        {
            DBClass.UpdateMeeting(MeetingToUpdate);

            return RedirectToPage("ViewProjects");
        }
    }
}
