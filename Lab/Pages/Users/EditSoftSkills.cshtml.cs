using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Users
{
    public class EditSoftSkillsModel : PageModel
    {
        [BindProperty]
        public User_Skill UserSoftSkillToUpdate { get; set; }

        [BindProperty]
        public int userID { get; set; }

        public EditSoftSkillsModel()
        {
            UserSoftSkillToUpdate = new User_Skill();
        }
        public IActionResult OnGet(string softSkill, int userid)
        {
            if (userid == null)
            {
                return RedirectToPage("Index");
            }

            DBClass.UpdateTheSoftSkill(softSkill,userid);
            return RedirectToPage("AddSoftSkills");
        }


    }
}
