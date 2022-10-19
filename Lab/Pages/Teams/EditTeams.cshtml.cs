using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Teams
{
    public class EditTeamsModel : PageModel
    {
        [BindProperty]
        public Team TeamToUpdate { get; set; }

        public EditTeamsModel()
        {
            TeamToUpdate = new Team();
        }

        public IActionResult OnGet(int teamid)
        {
            SqlDataReader singleUser = DBClass.SingleTeamReader(teamid);

            while (singleUser.Read())
            {
                TeamToUpdate.teamID = teamid;
                TeamToUpdate.teamName = singleUser["teamName"].ToString();
                TeamToUpdate.teamDescription = singleUser["teamDescription"].ToString();
                TeamToUpdate.teamEmailAddress = singleUser["teamEmailAddress"].ToString();
            }

            singleUser.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            DBClass.UpdateTeam(TeamToUpdate);

            return RedirectToPage("Index");
        }
    }
}

