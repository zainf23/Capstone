using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using System.Data.SqlClient;

namespace Lab.Pages.Users
{
    public class EditUserSkillsModel : PageModel
    {

        [BindProperty]
        public User_Skill UserSkillToUpdate { get; set; }
        [BindProperty]
        public int userID { get; set; }

        public EditUserSkillsModel()
        {
            UserSkillToUpdate = new User_Skill();
        }
        public IActionResult OnGet(string skill, int userid)
        {
            //userID = (int)HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToPage("Index");
            }
            SqlDataReader singleSkill = DBClass.SingleSkillReader(userid, skill);

            while (singleSkill.Read())
            {
                UserSkillToUpdate.userID = userid;
                UserSkillToUpdate.skill = singleSkill["skill"].ToString();
                UserSkillToUpdate.skillLevel = singleSkill["skillLevel"].ToString();

            }
            singleSkill.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }
        // updates skill
        public IActionResult OnPost()
        {
            DBClass.UpdateTheSkill(UserSkillToUpdate);

            return RedirectToPage("Index");
        }
    }
}
