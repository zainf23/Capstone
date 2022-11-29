using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;

namespace Lab.Pages.Login
{
    public class ChangePasswordModel : PageModel
    {
        [BindProperty]
        public string answer { get; set; }
        [BindProperty]
        public string secQuestion { get; set;}
        [BindProperty]
        public string secQuestionMom { get; set; }
        [BindProperty]
        public string secQuestionPet { get; set; }
        [BindProperty]
        public string secQuestionParents { get; set; }

        public void OnGet(int userid)
        {
            String SqlQuery = "Select secQuestionMom,secQuestionPet,secQuestionParents from [User] where userID = " + userid;
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
            if (secQuestion.Equals("What is your Mother's Maiden Name?"))
            {
                if (answer == secQuestionMom)
                {
                    return RedirectToPage("PasswordChange");
                }

            }
            else if (secQuestion == "What was the name of your first pet?")
            {
                if (answer == secQuestionPet)
                {
                    return RedirectToPage("PasswordChange");
                }
            }
            else if (secQuestion == "What city did your Parents meet in?")
            {
                if (answer == secQuestionParents)
                {
                    return RedirectToPage("PasswordChange");
                }
            }          
                ViewData["ErrorMessage"] = "Security Question Incorrect";
                return Page();
            
            
        }

    }
}
