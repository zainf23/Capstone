using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class AcceptModel : PageModel
    {
        public Request RequestStatus { get; set; }

        public AcceptModel()
        {
            RequestStatus = new Request();
        }
        public IActionResult OnGet(int requestid)
        {
            DBClass.UpdateAcceptedRequest(requestid);

            string sqlQuery = "Select userID,teamID from Request where requestID = " + requestid;
            SqlDataReader requestFinder = DBClass.GeneralReaderQuery(sqlQuery);

            while (requestFinder.Read())
            {
                RequestStatus.userID = Int32.Parse(requestFinder["userID"].ToString());
                RequestStatus.teamID = Int32.Parse(requestFinder["teamID"].ToString());


            }

            string sqlQuery1 = "INSERT INTO TeamUser (userID, teamID) VALUES (" + RequestStatus.userID + "," + RequestStatus.teamID + ")";
            DBClass.InsertMemberQuery(sqlQuery1);

            return RedirectToPage("MyProjects");
        }
    }
}
