using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EmptyCoreAppTest.Pages.Users
{
    public class AddSkillsModel : PageModel
    {

        [BindProperty]
        [Required]
        public UserSkill NewSkill { get; set; }

        [BindProperty]
        public string skill { get; set; }

        [BindProperty]
        public string skillLevel { get; set; }

        [BindProperty]
        public int userID { get; set; }

        
        public IActionResult OnGet(string userid)
        {

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            DBClass.InsertSkill(skill,skillLevel,userID);

            return RedirectToPage("ViewProfiles");
        }

        //public IActionResult OnPostPopulateHandler()
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.Clear();
        //        NewSkill.skill = "Python";
        //        NewSkill.skillLevel = "Intermediate";

        //    }

        //    return Page();
        //}
        //public IActionResult OnPostClearHandler()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ModelState.Clear();
        //        NewSkill.skill = "";
        //        NewSkill.skillLevel = "";
        //    }
        //    return Page();

        //}
    }
}
