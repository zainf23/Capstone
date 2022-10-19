using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Lab.Pages.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data.SqlClient;

namespace Lab.Pages.Search
{
    // This page does not work, dont know why
    public class ViewSkillsModel : PageModel
    {
        [BindProperty]
        public User_Skill UserSkillToView { get; set; }

        [BindProperty]
        public string skill { get; set; }

        public List<User_Skill> UserSkillList { get; set; }

        public ViewSkillsModel()
        {
            UserSkillToView = new User_Skill();
            UserSkillList = new List<User_Skill>();

        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("skill") == null)
            {
                return RedirectToPage("Index");
            }
            skill = HttpContext.Session.GetString("skill").ToString();

            SqlDataReader singleSkill = DBClass.SingleUserSkillReader(skill);

            while (singleSkill.Read())
            {
                UserSkillList.Add(new User_Skill
                {
                    skill = skill,
                    firstName = singleSkill["firstName"].ToString(),
                    secondName = singleSkill["secondName"].ToString(),
                    skillLevel = singleSkill["skillLevel"].ToString(),


                });
            }
            singleSkill.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
              
        }
    }
}
