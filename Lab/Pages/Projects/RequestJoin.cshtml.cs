using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

using System.Reflection;
using System.Web.Helpers;
namespace Lab.Pages.Projects
{
    public class RequestJoinModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public int projectID { get; set; }


        [BindProperty]
        public int teamID { get; set; }

        [BindProperty]
        public string userPitch { get; set; }

        [BindProperty]
        public string rEmail { get; set; }

        public List<TeamUser> Members { get; set; }

        public RequestJoinModel()
        {
            Members = new List<TeamUser>();
        }
        



        public void OnGet(int teamid)
        {
            teamID = teamid;
            username = HttpContext.Session.GetString("username");
            string sqlQuery = "SELECT userID from [USER] WHERE username = '" + username + "'";

            SqlDataReader userFinder = DBClass.GeneralReaderQuery(sqlQuery);
            while (userFinder.Read())
            {
                userID = Int32.Parse(userFinder["userID"].ToString());
            }
            userFinder.Close();


            string sqlQuery2 = "SELECT projectID from Team WHERE teamID = " + teamid;

            SqlDataReader projectFinder = DBClass.GeneralReaderQuery(sqlQuery2);
            while (projectFinder.Read())
            {
                projectID = Int32.Parse(projectFinder["projectID"].ToString());
            }
            userFinder.Close();
        }

        public IActionResult OnPost()
        {


            string sqlQuery3 = "Select userID from TeamUser Where teamID = " + teamID;
            SqlDataReader memberReader = DBClass.GeneralReaderQuery(sqlQuery3);

            while (memberReader.Read())
            {
                Members.Add(new TeamUser
                {
                    userID = Int32.Parse(memberReader["userID"].ToString())
                }); ;

            }
            memberReader.Close();

            foreach (var prod in Members)
            {
                if (prod.userID == userID)
                {
                    ViewData["ErrorMessage"] = "You are already a member on this Project!";
                    return Page();

                }
            }

            DBClass.RequestJoinQuery(userID, projectID, teamID, userPitch);
            HttpContext.Session.SetInt32("projectID", projectID);

            var errorMessage = "";
            var debuggingFlag = false;
            try
            {

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    // the NetworkCredential has 2 parameters the email account sending the email and the app password for that account(email, app password)
                    // https://youtu.be/lk5dhDzfzsU this video walk you through the proccess of setting and changing the app password gkkfxcqvycrzhbsw
                    Credentials = new NetworkCredential("madisonconjmu@gmail.com", "gkkfxcqvycrzhbsw"),

                    EnableSsl = true,
                };
                String otherEmail = "Select projectOwnerEmail from project WHERE projectID ='" + projectID + "'";
                SqlDataReader otherEmailReader = DBClass.GeneralReaderQuery(otherEmail);
                while (otherEmailReader.Read())
                {
                    rEmail = otherEmailReader["projectOwnerEmail"].ToString();
                }
                otherEmailReader.Close();

                Console.WriteLine(rEmail);
                smtpClient.Send("madisonconjmu@gmail.com", rEmail, "Madison Connect Update", "Hello, \n\tYou have recived an update on Madison Connect please sign in to view.\nhttp://lab-dev.eba-he83pxes.us-east-1.elasticbeanstalk.com");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToPage("ViewOtherProjects");
        }
    }
}
