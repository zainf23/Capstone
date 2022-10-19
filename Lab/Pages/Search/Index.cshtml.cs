using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace Lab.Pages.Search
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Skill { get; set; }

        public List<SelectListItem> SkillNames { get; set; }
        // pulls all skills from UserSkill
        public IActionResult OnGet()
        {
            SqlDataReader skillReader = DBClass.GeneralReaderQuery("SELECT Distinct Skill from UserSkill");
            // populate select list item
            SkillNames = new List<SelectListItem>();

            while (skillReader.Read())
            {
                SkillNames.Add(
                    new SelectListItem(
                        skillReader["skill"].ToString(),
                        skillReader["skill"].ToString()));
            }
            skillReader.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();


        }
        // but this isn't working 
        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("skill", Skill);
            return RedirectToPage("ViewSkills");

        }


    }
}