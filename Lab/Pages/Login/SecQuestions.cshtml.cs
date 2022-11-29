using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Login
{
    public class SecQuestionsModel : PageModel
    {
        [BindProperty]
        public string answer { get; set; }
        [BindProperty]
        public string secQuestion { get; set; }
        [BindProperty]
        public string secQuestionMom { get; set; }
        [BindProperty]
        public string secQuestionPet { get; set; }
        [BindProperty]
        public string secQuestionParents { get; set; }
        [BindProperty]
        public int userID { get; set; }
        [BindProperty]
        public string enteredusername { get; set; }

        public void OnGet(string enteredusername)
        {

            String usernameQuery = "SELECT userID FROM [User] WHERE username = '" + enteredusername + "'";
            SqlDataReader idReader = DBClass.GeneralReaderQuery(usernameQuery);
            while (idReader.Read())
            {
                userID = Int32.Parse(idReader["userID"].ToString());
            }
            idReader.Close();


            HttpContext.Session.SetInt32("userID", userID);

            String SqlQuery = "Select secQuestionMom,secQuestionPet,secQuestionParents from [User] where userID = " + userID;
            SqlDataReader UserReader = DBClass.GeneralReaderQuery(SqlQuery);
            while (UserReader.Read())
            {
                secQuestionMom = UserReader["secQuestionMom"].ToString();
                secQuestionPet = UserReader["secQuestionPet"].ToString();
                secQuestionParents = UserReader["secQuestionParents"].ToString();
            }
            UserReader.Close();
        }

        public IActionResult OnPost()
        {
            if (userID == 0)
            {
                userID = (int)HttpContext.Session.GetInt32("userID");
            }


            String SqlQuery = "Select secQuestionMom,secQuestionPet,secQuestionParents from [User] where userID = " + userID;
            SqlDataReader UserReader = DBClass.GeneralReaderQuery(SqlQuery);
            while (UserReader.Read())
            {
                secQuestionMom = UserReader["secQuestionMom"].ToString();
                secQuestionPet = UserReader["secQuestionPet"].ToString();
                secQuestionParents = UserReader["secQuestionParents"].ToString();
            }
            UserReader.Close();


            if (secQuestion.Equals("secQuestionMom"))
            {
                if (answer.Equals(secQuestionMom))
                {
                    return RedirectToPage("PasswordChange");
                }

            }
            else if (secQuestion.Equals("secQuestionPet"))
            {
                if (answer.Equals(secQuestionPet))
                {
                    return RedirectToPage("PasswordChange");
                }
            }
            else if (secQuestion.Equals("secQuestionParents"))
            {
                if (answer.Equals(secQuestionParents))
                {
                    return RedirectToPage("PasswordChange");
                }
            }
            ViewData["ErrorMessage"] = "Security Question Incorrect";
            return Page();


        }

    }
}
