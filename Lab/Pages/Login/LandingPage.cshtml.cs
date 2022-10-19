using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.Pages.Login
{
    public class LandingPageModel : PageModel
    {
        [BindProperty]
        public string logout { get; set; }
        public IActionResult OnGet()
        {
            //to ensure a user is loged in
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("HashedLogin");
            }
          
            return Page();

        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Users/AddUsers");

        }
    }
}
