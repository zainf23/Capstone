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

namespace Lab.Pages.Messages

{
    public class CreateMessageModel : PageModel
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
        public string subject { get; set; }

        [BindProperty]
        public string message { get; set; }

        [BindProperty]
        public string date { get; set; }

        [BindProperty]
        public User UserData { get; set; }

        [BindProperty]
        public string senderName { get; set; }

        [BindProperty]
        public string fName { get; set; }

        [BindProperty]
        public string lName { get; set; }

        [BindProperty]
        public string userName { get; set; }

        [BindProperty]
        public string rEmail { get; set; }




        public IActionResult OnGet(string username, int userid)
        {


            userID = userid;
            userName = username;

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            String sqlQuery = "Select userID,firstName,secondName from [User] WHERE username ='" + userName + "'";
            SqlDataReader userReader = DBClass.GeneralReaderQuery(sqlQuery);

            while (userReader.Read())
            {
                otherUserID = Int32.Parse(userReader["userID"].ToString());
                firstName = userReader["firstName"].ToString();
                lastName = userReader["secondName"].ToString();
            }
            userReader.Close();

            return Page();
        }

        public IActionResult OnPost()
        {

            String sqlQuery = "Select userID from [User] WHERE firstName= '" + firstName + "'AND secondName ='" + lastName + "'";
            SqlDataReader userReader = DBClass.GeneralReaderQuery(sqlQuery);

            while (userReader.Read())
            {
                otherUserID = Int32.Parse(userReader["userID"].ToString());
            }
            userReader.Close();

            String sqlQuery2 = "Select firstName,secondName from [User] WHERE userID = " + userID;
            SqlDataReader nameReader = DBClass.GeneralReaderQuery(sqlQuery2);

            while (nameReader.Read())
            {
                fName = nameReader["firstName"].ToString();
                lName = nameReader["secondName"].ToString();
            }
            nameReader.Close();

            senderName = fName + " " + lName;

            date = System.DateTime.Now.ToString("F");


            DBClass.SendMessage(userID, otherUserID, subject, message, senderName, date);



            var errorMessage = "";
            var debuggingFlag = false;
            try
            {

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    // the NetworkCredential has 2 parameters the email account sending the email and the app password for that account(email, app password)
                    // https://youtu.be/lk5dhDzfzsU this video walk you through the proccess of setting and changing the app password gkkfxcqvycrzhbsw
                    Credentials = new NetworkCredential("madisonconnectjmu@gmail.com", "mvmktbowgtrltcsm"),

                    EnableSsl = true,
                };
                String otherEmail = "Select email from [User] WHERE userID ='" + otherUserID + "'";
                SqlDataReader otherEmailReader = DBClass.GeneralReaderQuery(otherEmail);
                while (otherEmailReader.Read())
                {
                    rEmail = otherEmailReader["email"].ToString();
                }
                otherEmailReader.Close();
                Console.WriteLine(otherEmailReader);
                smtpClient.Send("madisonconnectjmu@gmail.com", rEmail, "Madison Connect Update", "Hello, \n\tYou have recived an update on Madison Connect please sign in to view.\nhttp://lab-dev.eba-he83pxes.us-east-1.elasticbeanstalk.com");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            return RedirectToPage("Index");
        }
    }
}
