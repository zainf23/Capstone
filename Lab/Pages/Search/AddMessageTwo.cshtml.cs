using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Search
{
    public class AddMessageTwoModel : PageModel
    {
        [BindProperty]
        public int projectID { get; set; }

        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public string firstName { get; set; }

        [BindProperty]
        public string secondName { get; set; }

        [BindProperty]
        public string subject { get; set; }

        [BindProperty]
        public string fullName { get; set; }

        [BindProperty]
        public string recipient { get; set; }

        [BindProperty]
        public string messageInfo { get; set; }

        [BindProperty]
        public string date { get; set; }


        public void OnGet(int projectid, int userid)
        {
            projectID = projectid;
            userID = userid;

            HttpContext.Session.SetInt32("projectID", projectid);

            string sqlQuery = "Select firstName, secondName from [User] where userID =" + userID;
            SqlDataReader nameFinder = DBClass.GeneralReaderQuery(sqlQuery);

            while (nameFinder.Read())
            {
                firstName = nameFinder["firstName"].ToString();
                secondName = nameFinder["secondName"].ToString();
            }
            nameFinder.Close();

            fullName = firstName + " " + secondName;
        }

        public IActionResult OnPost()
        {
            //string sqlQuery = "INSERT INTO ProjectChatRoom (userID, projectID, subject, sender, recipient, messageInfo) VALUES (";
            //sqlQuery += userID + ",";
            //sqlQuery += projectID + ",";
            //sqlQuery += "'" + subject + "',";
            //sqlQuery += "'" + fullName + "',";
            //sqlQuery += "'" + recipient + "',";
            //sqlQuery += "'" + messageInfo + "')";

            date = System.DateTime.Now.ToString("F");

            DBClass.ProjectChatRoomQuery(userID, projectID, subject, fullName, recipient, messageInfo, date);
            return RedirectToPage("/Search/ViewProject");
        }
    }
}
