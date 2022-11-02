using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.Pages.Search
{
    public class RejectModel : PageModel
    {
        public IActionResult OnGet(int requestIDTwo)
        {
            DBClass.UpdateRejectedRequestTwo(requestIDTwo);

            return RedirectToPage("Index");
        }
    }
}
