using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Teams
{
    public class IndexModel : PageModel
    {
        public List<Team> TeamList { get; set; }

        public IndexModel()
        {
            TeamList = new List<Team>();
        }

        // get method that reads team info and adds to list
        public IActionResult OnGet()
        {
            SqlDataReader TeamReader = DBClass.TeamReaderMethod();
            while (TeamReader.Read())
            {
                TeamList.Add(new Team
                {
                    teamID = Int32.Parse(TeamReader["teamID"].ToString()),
                    teamName = TeamReader["teamName"].ToString(),
                    teamDescription = TeamReader["teamDescription"].ToString(),
                    teamEmailAddress = TeamReader["teamEmailAddress"].ToString()

                });
            }
            TeamReader.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }
    }
}
