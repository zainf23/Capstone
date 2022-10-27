using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.Pages.Users
{
    public class EditHobbiesModel : PageModel
    {
        [BindProperty]
        public User_Skill HobbyToUpdate { get; set; }

        [BindProperty]
        public int userID { get; set; }

        public EditHobbiesModel()
        {
            HobbyToUpdate = new User_Skill();
        }
        public IActionResult OnGet(string hobby, int userid)
        {
            if (userid == null)
            {
                return RedirectToPage("Index");
            }

            DBClass.UpdateTheHobby(hobby, userid);
            return RedirectToPage("AddHobbies", userid);
        }
    }
}
