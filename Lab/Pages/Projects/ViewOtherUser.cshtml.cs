using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Projects
{
    public class ViewOtherUserModel : PageModel
    {
        [BindProperty]
        public User UserProfile { get; set; }

        [BindProperty]
        public List<User_Skill> SkillList { get; set; }

        [BindProperty]
        public List<User_Skill> SoftSkillList { get; set; }

        [BindProperty]
        public List<User_Skill> HobbyList { get; set; }

        [BindProperty]
        public string fileName { get; set; }

        public ViewOtherUserModel()
        {
            UserProfile = new User();
            SkillList = new List<User_Skill>();
            SoftSkillList = new List<User_Skill>();
            HobbyList = new List<User_Skill>();
        }
        public IActionResult OnGet(int userid)
        {
            SqlDataReader userReader = DBClass.SingleUserReader(userid);

            while (userReader.Read())
            {

                UserProfile.userID = Int32.Parse(userReader["userID"].ToString());
                UserProfile.firstName = userReader["firstName"].ToString();
                UserProfile.secondName = userReader["secondName"].ToString();
                UserProfile.email = userReader["email"].ToString();
                UserProfile.jmuType = userReader["jmuType"].ToString();
                UserProfile.interests = userReader["interests"].ToString();
                UserProfile.experience = userReader["experience"].ToString();
                UserProfile.gradYear = userReader["gradYear"].ToString();
                UserProfile.major = userReader["major"].ToString();
                UserProfile.minor = userReader["minor"].ToString();
                UserProfile.jobTitle = userReader["jobTitle"].ToString();
                UserProfile.department = userReader["department"].ToString();
                UserProfile.moreInfo = userReader["moreInfo"].ToString();

            }
            userReader.Close();

            SqlDataReader someskills = DBClass.SomeSkills(userid);

            while (someskills.Read())
            {
                SkillList.Add(new User_Skill
                {

                    skill = someskills["skill"].ToString(),
                    skillLevel = someskills["skillLevel"].ToString()


                });

            }
            someskills.Close();

            SqlDataReader somesoftskills = DBClass.SomeSoftSkills(userid);

            while (somesoftskills.Read())
            {
                SoftSkillList.Add(new User_Skill
                {

                    softSkill = somesoftskills["softSkill"].ToString(),


                });

            }
            somesoftskills.Close();

            SqlDataReader somehobbies = DBClass.SomeHobbies(userid);

            while (somehobbies.Read())
            {
                HobbyList.Add(new User_Skill
                {

                    hobby = somehobbies["hobby"].ToString(),


                });

            }
            somehobbies.Close();

            String sqlQuery = "Select [fileName] FROM UserProfilePic WHERE userID =" + userid;

            SqlDataReader picReader = DBClass.GeneralReaderQuery(sqlQuery);

            while (picReader.Read())
            {
                fileName = picReader["fileName"].ToString();
            }


            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();

        }
    }
}
