using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Lab.Pages.Users
{
    public class AddSoftSkillsModel : PageModel
    {

        [BindProperty]
        [Required]
        public UserSoftSkill NewSoftSkill { get; set; }

        [BindProperty]
        public string softSkill { get; set; }

        [BindProperty]
        public int userID { get; set; }
       
        [BindProperty]
        public string username { get; set; }


        public IActionResult OnGet(string userid)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();

            username = HttpContext.Session.GetString("username");

            SqlDataReader userReader = DBClass.UserReader(username);

            while (userReader.Read())
            {
                userID = Int32.Parse(userReader["userID"].ToString());
                


            }
            userReader.Close();
 
            HttpContext.Session.SetInt32("userID", userID);
        }

        public IActionResult OnPost()
        {
            DBClass.InsertSoftSkill(softSkill, userID);

            return RedirectToPage("ViewProfiles");
        }
    }
}
