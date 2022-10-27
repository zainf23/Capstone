using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Lab.Pages.Users
{
    public class AddHobbiesModel : PageModel
    {

        [BindProperty]
        [Required]
        public UserSoftSkill NewHobby { get; set; }

        [BindProperty]
        public string hobby { get; set; }

        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public string username { get; set; }

        public IActionResult OnGet(int userid)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }


            userID = userid;

            username = HttpContext.Session.GetString("username");

            SqlDataReader userReader = DBClass.UserReader(username);

            while (userReader.Read())
            {
                userID = Int32.Parse(userReader["userID"].ToString());



            }
            userReader.Close();

            HttpContext.Session.SetInt32("userID", userID);

            return Page();
        }
        public IActionResult OnPost()
        {
            DBClass.InsertHobby(hobby, userID);

            return RedirectToPage("ViewProfiles");
        }
    }
}
