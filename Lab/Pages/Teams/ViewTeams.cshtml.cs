using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace Lab.Pages.Teams
{
    public class ViewTeamsModel : PageModel
    {
        [BindProperty]
        public ProjectTeam ProjectTeam { get; set; }

        [BindProperty]
        public List<ProjectTeam> TeamList { get; set; }

        [BindProperty]
        public List<User> UserList { get; set; }
       
        [BindProperty]
        public string username { get; set; }

        public ViewTeamsModel()
        {
            ProjectTeam = new ProjectTeam();
            TeamList = new List<ProjectTeam>();
            UserList = new List<User>();

        }
        public IActionResult OnGet(int teamid)
        {
            // send teamid to dbclass where method pulls team info
            SqlDataReader singleteam = DBClass.SingleTeamReader(teamid);
            // send teamid to dbclass where method pulls corresponding project info
            SqlDataReader singleproject = DBClass.GetProjectID(teamid);
            // query that gets team members in specific team
            string sqlQuery = "SELECT firstName, secondName FROM [User] u, TeamUser tu WHERE u.userID = tu.userID AND tu.teamID =" + teamid;
            SqlDataReader teamMembers = DBClass.GeneralReaderQuery(sqlQuery);
            HttpContext.Session.SetInt32("teamid", teamid);
            
            // read team info
            while (singleteam.Read())
            {
                ProjectTeam.teamID = teamid;
                ProjectTeam.teamName = singleteam["teamName"].ToString();
                ProjectTeam.teamDescription = singleteam["teamDescription"].ToString();
                ProjectTeam.teamEmailAddress = singleteam["teamEmailAddress"].ToString();
            }
            singleteam.Close();
           
            // read corresponding project info
            while (singleproject.Read())
            {
                TeamList.Add(new ProjectTeam
                {

                    projectName = singleproject["projectName"].ToString(),
                    projectOwner = singleproject["projectOwner"].ToString(),
                    projectOwnerEmail = singleproject["projectOwnerEmail"].ToString(),
                    projectMissionStatement = singleproject["projectMissionStatement"].ToString(),
                    projectDescription = singleproject["projectDescription"].ToString(),
                    projectDate = singleproject["projectDate"].ToString(),



                });

            }
            singleproject.Close();
           
            // read team member info
            while (teamMembers.Read())
            {
                UserList.Add(new User
                {
                    firstName = teamMembers["firstName"].ToString(),
                    secondName = teamMembers["secondName"].ToString(),
                });

            }
            teamMembers.Close();


            // LOGIN AWARE
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

        public void OnPost()
        {
        }

    }
}
