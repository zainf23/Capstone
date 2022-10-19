using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.Pages.Projects
{
    public class RejectModel : PageModel
    {
        public IActionResult OnGet(int requestid)
        {
            DBClass.UpdateRejectedRequest(requestid);

            return RedirectToPage("MyProjects");
        }
    }
}
