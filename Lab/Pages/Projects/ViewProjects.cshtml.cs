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


        public ViewProjectsModel()
        {
            ProjectToView = new ProjectTeam();
            ProjectInfo = new Project();
            UserList = new List<User>();
        }
        public IActionResult OnGet(int projectid)
        {
            if (projectid == 0)
            {
                projectid = (int)HttpContext.Session.GetInt32("projectID");
            }
           
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

            string sqlQuery1 = "SELECT firstName, secondName FROM [User] u, TeamUser tu WHERE u.userID = tu.userID AND tu.teamID =" + ProjectToView.teamID;
            SqlDataReader teamMembers = DBClass.GeneralReaderQuery(sqlQuery1);

            while (teamMembers.Read())
            {
                UserList.Add(new User
                {
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




            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }
    }
}
