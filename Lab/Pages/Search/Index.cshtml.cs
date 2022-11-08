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

        [BindProperty]
        public string SearchString { get; set; }

        [BindProperty]
        public List<UserSoftHardSkillHobbies> UserSearchList { get; set; }

        public IndexModel()
        {
            UserSearchList = new List<UserSoftHardSkillHobbies>();
        }


        // pulls all skills from UserSkill
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
            //SqlDataReader skillReader = DBClass.GeneralReaderQuery("SELECT Distinct Skill from UserSkill");
            //// populate select list item
            //SkillNames = new List<SelectListItem>();

            //while (skillReader.Read())
            //{
            //    SkillNames.Add(
            //        new SelectListItem(
            //            skillReader["skill"].ToString(),
            //            skillReader["skill"].ToString()));
            //}
            //skillReader.Close();

            //if (HttpContext.Session.GetString("username") == null)
            //{
            //    return RedirectToPage("/Login/HashedLogin");
            //}

            //return Page();


        }
        // but this isn't working 
        public IActionResult OnPost()
        {
            //if (SearchString.Length < 4)
            //{
            //    return RedirectToPage("/Search/Index");
            //}

            string sqlQuery = "SELECT DISTINCT u.userID, firstName, secondName, username, email, jmuType, interests, experience, gradYear, major, minor, jobtitle, department, moreInfo FROM [User] u, UserSkill us, UserSoftSkill uss, UserHobbies uh WHERE (u.userID = us.userID AND u.userID = uss.userID AND u.userID = uh.userID AND us.userID = uss.userID AND us.userID = uh.userID AND uss.userID = uh.userID) AND (firstName like '%";
            sqlQuery += SearchString + "%' OR secondName like '%";
            sqlQuery += SearchString + "%' OR email like '%";
            sqlQuery += SearchString + "%' OR jmuType like '%";
            sqlQuery += SearchString + "%' OR interests like '%";
            sqlQuery += SearchString + "%' OR experience like '%";
            sqlQuery += SearchString + "%' OR gradYear like '%";
            sqlQuery += SearchString + "%' OR major like '%";
            sqlQuery += SearchString + "%' OR minor like '%";
            sqlQuery += SearchString + "%' OR jobTitle like '%";
            sqlQuery += SearchString + "%' OR department like '%";
            sqlQuery += SearchString + "%' OR moreInfo like '%";
            sqlQuery += SearchString + "%' OR skill like '%";
            sqlQuery += SearchString + "%' OR skillLevel like '%";
            sqlQuery += SearchString + "%' OR softSkill like '%";
            sqlQuery += SearchString + "%' OR hobby like '%";
            sqlQuery += SearchString + "%')";

            SqlDataReader usersearch = DBClass.GeneralReaderQuery(sqlQuery);

            while (usersearch.Read())
            {
                UserSearchList.Add(new UserSoftHardSkillHobbies
                {

                    userID = Int32.Parse(usersearch["userID"].ToString()),
                    firstName = usersearch["firstName"].ToString(),
                    secondName = usersearch["secondName"].ToString(),
                    email = usersearch["email"].ToString(),
                    jmuType = usersearch["jmuType"].ToString(),
                    interests = usersearch["interests"].ToString(),
                    experience = usersearch["experience"].ToString(),
                    gradYear = usersearch["gradYear"].ToString(),
                    major = usersearch["gradYear"].ToString(),
                    minor = usersearch["minor"].ToString(),
                    jobTitle = usersearch["jobTitle"].ToString(),
                    department = usersearch["department"].ToString(),
                    moreInfo = usersearch["moreInfo"].ToString(),


                });
            }
            usersearch.Close();

            return Page();

            //HttpContext.Session.SetString("skill", Skill);
            //return RedirectToPage("ViewSkills");


        }


    }
}