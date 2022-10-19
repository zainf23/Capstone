using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EmptyCoreAppTest.Pages.Users
{
    public class AddUsersModel : PageModel
    {
        [BindProperty]
        public User NewUser { get; set; }

        [BindProperty]
        public string username { get; set; }

        //public IActionResult OnGet()
        //{
        //    HttpContext.Session.GetString("username");
        //    return Page();

        //}

        // post method that adds new user
        public IActionResult OnPost()
        {
            NewUser.username = HttpContext.Session.GetString("username");
            DBClass.InsertUser(NewUser);

            return RedirectToPage("Index");
        }

        // populate and clear button
        //public IActionResult OnPostPopulateHandler()
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.Clear();

        //        NewUser.firstName = "Lebron";
        //        NewUser.secondName = "James";
        //        NewUser.username = "lebronjames";
        //        NewUser.passphrase = "james123";
        //        NewUser.email = "theking23@gmail.com";
        //        NewUser.userType = "Professional";
        //        NewUser.professionalCompany = "Lakers";
        //        NewUser.professionalEmail = "celticsownMe@gmail.com";
        //        NewUser.facultyAssociation = " ";

        //    }

        //    return Page();
        //}

        //public IActionResult OnPostClearHandler()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ModelState.Clear();
        //        NewUser.firstName = "";
        //        NewUser.secondName = "";
        //        NewUser.username = "";
        //        NewUser.passphrase = "";
        //        NewUser.email = "";
        //        NewUser.userType = "";
        //        NewUser.professionalCompany = "";
        //        NewUser.professionalEmail = "";
        //        NewUser.facultyAssociation = "";
        //    }
        //    return Page();
        //}
    }
}
