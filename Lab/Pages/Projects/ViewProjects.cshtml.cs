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
        public Project ProjectInfo { get; set; }

        [BindProperty]
        public string fileName { get; set; }

        [BindProperty]
        public List<User> UserList { get; set; }

        [BindProperty]
        public List<TeamMeeting> MeetingList { get; set; }

        [BindProperty]
        public List<TeamMeeting> CompletedMeetingList { get; set; }


        public ViewProjectsModel()
        {
            ProjectToView = new ProjectTeam();
            ProjectInfo = new Project();
            UserList = new List<User>();
            MeetingList = new List<TeamMeeting>();
            CompletedMeetingList = new List<TeamMeeting>();
        }
        public IActionResult OnGet(int projectid)

        {
            HttpContext.Session.SetInt32("projectid", projectid);
            if (projectid == 0)
            {
                projectid = (int)HttpContext.Session.GetInt32("projectID");
            }
           

            SqlDataReader singleProject = DBClass.SingleProjectReader(projectid);

            string sqlQuery = "Select * from Team where projectID =" + projectid;
            SqlDataReader singleTeam = DBClass.GeneralReaderQuery(sqlQuery);

            while (singleProject.Read())
            {
                    ProjectInfo.projectID = Int32.Parse(singleProject["projectID"].ToString());
                    ProjectInfo.projectName = singleProject["projectName"].ToString();
                    ProjectInfo.projectOwner = singleProject["projectOwner"].ToString();
                    ProjectInfo.projectOwnerEmail = singleProject["projectOwnerEmail"].ToString();
                    ProjectInfo.projectMissionStatement = singleProject["projectMissionStatement"].ToString();
                    ProjectInfo.projectDescription = singleProject["projectDescription"].ToString();
                    ProjectInfo.projectDate = singleProject["projectDate"].ToString();
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

            string sqlQuery1 = "SELECT u.userID, firstName, secondName FROM [User] u, TeamUser tu WHERE u.userID = tu.userID AND tu.teamID =" + ProjectToView.teamID;
            SqlDataReader teamMembers = DBClass.GeneralReaderQuery(sqlQuery1);

            while (teamMembers.Read())
            {
                UserList.Add(new User
                {
                    userID = Int32.Parse(teamMembers["userID"].ToString()),
                    firstName = teamMembers["firstName"].ToString(),
                    secondName = teamMembers["secondName"].ToString(),
                });

            }
            teamMembers.Close();

            String sqlQuery2 = "Select [fileName] FROM Project WHERE projectID =" + ProjectInfo.projectID;

            SqlDataReader picReader = DBClass.GeneralReaderQuery(sqlQuery2);

            while (picReader.Read())
            {
                fileName = picReader["fileName"].ToString();
            }
            picReader.Close();

            string sqlQuery3 = "SELECT teamMeetingID, projectID, meetingTitle,meetingDate,meetingTime,meetingPlan,attended,meetingLocation FROM TeamMeeting WHERE attended is null AND projectID =" + ProjectInfo.projectID;
            SqlDataReader meetings = DBClass.GeneralReaderQuery(sqlQuery3);

            while (meetings.Read())
            {
                MeetingList.Add(new TeamMeeting
                {
                    teamMeetingID = Int32.Parse(meetings["teamMeetingID"].ToString()),
                    projectID = Int32.Parse(meetings["projectID"].ToString()),
                    meetingTitle = meetings["meetingTitle"].ToString(),
                    meetingDate = meetings["meetingDate"].ToString(),
                    meetingTime = meetings["meetingTime"].ToString(),
                    meetingPlan = meetings["meetingPlan"].ToString(),
                    meetingLocation = meetings["meetingLocation"].ToString(),

                });

            }
            meetings.Close();

            string sqlQuery4 = "SELECT teamMeetingID, projectID, meetingTitle,meetingDate,meetingTime,meetingPlan,attended,meetingLocation,meetingSummary FROM TeamMeeting WHERE attended = 1 AND projectID =" + ProjectInfo.projectID;
            SqlDataReader meetings2 = DBClass.GeneralReaderQuery(sqlQuery4);

            while (meetings2.Read())
            {
                CompletedMeetingList.Add(new TeamMeeting
                {
                    teamMeetingID = Int32.Parse(meetings2["teamMeetingID"].ToString()),
                    projectID = Int32.Parse(meetings2["projectID"].ToString()),
                    meetingTitle = meetings2["meetingTitle"].ToString(),
                    meetingDate = meetings2["meetingDate"].ToString(),
                    meetingTime = meetings2["meetingTime"].ToString(),
                    meetingPlan = meetings2["meetingPlan"].ToString(),
                    meetingSummary = meetings2["meetingSummary"].ToString(),
                    meetingLocation = meetings2["meetingLocation"].ToString(),

                });

            }
            meetings2.Close();




            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }
    }
}
