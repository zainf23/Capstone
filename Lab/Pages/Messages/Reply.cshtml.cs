using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Messages
{
    public class ReplyModel : PageModel
    {
        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public string firstName { get; set; }

        [BindProperty]
        public string lastName { get; set; }

        [BindProperty]
        public int otherUserID { get; set; }

        [BindProperty]
        public string Subject { get; set; }

        [BindProperty]
        public string message { get; set; }

        [BindProperty]
        public string date { get; set; }

        [BindProperty]
        public string time { get; set; }

        [BindProperty]
        public User UserData { get; set; }

        [BindProperty]
        public string senderName { get; set; }

        [BindProperty]
        public string fName { get; set; }

        [BindProperty]
        public string lName { get; set; }

        public IActionResult OnGet(int userid, string subject, int otheruserid)
        {
            otherUserID = userid;
            userID = otheruserid;
            Subject = subject;

            String sqlQuery = "Select firstName,secondName from [User] WHERE userID = " + otherUserID;
            SqlDataReader nameReader = DBClass.GeneralReaderQuery(sqlQuery);

            while (nameReader.Read())
            {
                fName = nameReader["firstName"].ToString();
                lName = nameReader["secondName"].ToString();
            }
            nameReader.Close();



            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            String sqlQuery2 = "Select firstName,secondName from [User] WHERE userID = " + otherUserID;
            SqlDataReader senderReader = DBClass.GeneralReaderQuery(sqlQuery2);

            while (senderReader.Read())
            {
                firstName = senderReader["firstName"].ToString();
                lastName = senderReader["secondName"].ToString();
            }
            senderReader.Close();
            senderName = firstName + " " + lastName;

            DBClass.SendMessage(otherUserID,userID, Subject, message, senderName);
            return RedirectToPage("Index");
        }
    }
}
