using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.Pages.Messages
{
    public class MarkModel : PageModel
    {
        public IActionResult OnGet(int messageid)
        {

            DBClass.MessageRead(messageid);

            return RedirectToPage("Index");
        }
    }
}
