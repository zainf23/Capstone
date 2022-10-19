using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class ViewUserRequestModel : PageModel
    {

        [BindProperty]
        public UserRequest MyRequests { get; set; }


        public ViewUserRequestModel()
        {
            MyRequests = new UserRequest();
        }

        public void OnGet(int userid)
        {
            string sqlQuery2 = "Select * from [User] u, Request r WHERE u.userID = r.UserID AND r.userID = " + userid;
            SqlDataReader requestFinder = DBClass.GeneralReaderQuery(sqlQuery2);

            while (requestFinder.Read())
            {
                MyRequests.requestID = Int32.Parse(requestFinder["requestID"].ToString());
                MyRequests.userID = Int32.Parse(requestFinder["userID"].ToString());
                MyRequests.projectID = Int32.Parse(requestFinder["projectID"].ToString());
                MyRequests.teamID = Int32.Parse(requestFinder["teamID"].ToString());
                MyRequests.accepted = Int32.Parse(requestFinder["accepted"].ToString());
                MyRequests.firstName = requestFinder["firstName"].ToString();
                MyRequests.secondName = requestFinder["secondName"].ToString();
                MyRequests.email = requestFinder["email"].ToString();
                MyRequests.userType = requestFinder["userType"].ToString();
                MyRequests.userPitch = requestFinder["userPitch"].ToString();


            }
            requestFinder.Close();

        }
    }
}
