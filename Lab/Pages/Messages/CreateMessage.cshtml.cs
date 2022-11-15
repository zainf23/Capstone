using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

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
        public string time { get; set; }

        [BindProperty]
        public User UserData { get; set; }

        [BindProperty]
        public string senderName { get; set; }

        [BindProperty]
        public string fName { get; set; }

        [BindProperty]
        public string lName { get; set; }




        public IActionResult OnGet(int userid)
        {

            HttpContext.Session.SetInt32("userID", userid);
            userID = userid;

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

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


            DBClass.SendMessage(userID,otherUserID,subject,message,senderName);


            return RedirectToPage("Index");
        }
    }
}
