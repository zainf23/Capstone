using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Login
{
    public class CreateHashedLoginModel : PageModel
    {

        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string passphrase { get; set; }

        [BindProperty]
        public string jmuType { get; set; }

        [BindProperty]
        public string firstName { get; set; }

        [BindProperty]
        public string secondName { get; set; }

        [BindProperty]
        public string secQuestionMom { get; set; }

        [BindProperty]
        public string secQuestionPet { get; set; }

        [BindProperty]
        public string secQuestionParents { get; set; }

        [BindProperty]
        public int userID { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Perform Validation First on Form
            // then...

            DBClass.CreateHashedUser(username, passphrase);
            DBClass.InsertType(jmuType,firstName,secondName,username,secQuestionMom, secQuestionPet, secQuestionParents);
            
            String SqlQuery = "Select userID from [User] where username ='";
            SqlQuery += username + "' and jmuType ='";
            SqlQuery += jmuType + "' AND firstName ='";
            SqlQuery += firstName + "' AND secondName ='";
            SqlQuery += secondName + "'";
            SqlDataReader UserReader = DBClass.GeneralReaderQuery(SqlQuery);
            while (UserReader.Read())
            {
                userID = Int32.Parse(UserReader["userID"].ToString());
            }
            DBClass.DataHolderForUPP(userID);

            DBClass.InsertHobby("Place Holder", userID);
            DBClass.InsertSkill("Place Holder", "Starter", userID);
            DBClass.InsertSoftSkill("Place Holder", userID);


            // Perform actual logic to check if user was successfully
            //  added in your projects but for demo purposes we can say:

            ViewData["UserCreate"] = "User Successfully Created!";

            return RedirectToPage("HashedLogin");
        }
    }
}
