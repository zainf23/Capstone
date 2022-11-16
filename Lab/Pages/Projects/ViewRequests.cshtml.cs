using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class ViewRequestsModel : PageModel
    {
        [BindProperty]
        public Request MyRequests { get; set; }

        [BindProperty]
        public List<UserRequest> MyRequestsList { get; set; }


        [BindProperty]
        public int userID { get; set; }


        public ViewRequestsModel()
        {
            MyRequests = new Request();
            MyRequestsList = new List<UserRequest>();
        }

        public void OnGet(int projectid)
        {
            string sqlQuery = "SELECT distinct r.requestID, u.userID, u.firstName, u.secondName, u.email, u.jmuType, r.userPitch, u.interests, upp.fileName FROM [User] u, Request r, UserProfilePic upp WHERE u.userID = upp.userID AND u.userID = r.userID AND r.accepted = 0 AND r.projectID = " + projectid;
            SqlDataReader requestFinder = DBClass.GeneralReaderQuery(sqlQuery);


            //string sqlQuery2 = "Select * from [User] u, Request r WHERE u.userID = r.UserID AND r.userID = " + userID;
            //SqlDataReader requestUserFinder = DBClass.GeneralReaderQuery(sqlQuery2);

            while (requestFinder.Read())
            {
                MyRequestsList.Add(new UserRequest
                {
                    requestID = Int32.Parse(requestFinder["requestID"].ToString()),
                    userID = Int32.Parse(requestFinder["userID"].ToString()),
                    firstName = requestFinder["firstName"].ToString(),
                    secondName = requestFinder["secondName"].ToString(),
                    email = requestFinder["email"].ToString(),
                    jmuType = requestFinder["jmuType"].ToString(),
                    userPitch = requestFinder["userPitch"].ToString(),
                    interests = requestFinder["interests"].ToString(),
                    fileName = requestFinder["fileName"].ToString(),
                });
            }
            requestFinder.Close();

        }
    }
}
