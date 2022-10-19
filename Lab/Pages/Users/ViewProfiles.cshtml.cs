using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Users
{
    public class ViewProfilesModel : PageModel
    {
        [BindProperty]
        public User_Skill UserToView { get; set; }

        public List<User_Skill> SkillList { get; set; }

        [BindProperty]
        public User UserProfile { get; set; }
       
        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int userID { get; set; }

        public ViewProfilesModel()
        {
            UserToView = new User_Skill();
            SkillList = new List<User_Skill>();
            UserProfile = new User();
        }
        public IActionResult OnGet()
        {
            username = HttpContext.Session.GetString("username");
            SqlDataReader userReader = DBClass.UserReader(username);

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
            //SqlDataReader singleprofile = DBClass.SingleProfileReader(userid);
            HttpContext.Session.SetInt32("userid", userID);
            SqlDataReader someskills = DBClass.SomeSkills(UserProfile.userID);

            //while (singleprofile.Read())
            //{
            //    UserToView.userID = userid;
            //    UserToView.firstName = singleprofile["firstName"].ToString();
            //    UserToView.secondName = singleprofile["secondName"].ToString();
            //    UserToView.email = singleprofile["email"].ToString();
            //    UserToView.jmuType = singleprofile["jmuType"].ToString();
            //    UserToView.interests = singleprofile["interests"].ToString();
            //    UserToView.experience = singleprofile["experience"].ToString();
            //    UserToView.gradYear = singleprofile["gradYear"].ToString();
            //    UserToView.major = singleprofile["major"].ToString();
            //    UserToView.minor = singleprofile["minor"].ToString();
            //    UserToView.jobTitle = singleprofile["jobTitle"].ToString();
            //    UserToView.department = singleprofile["department"].ToString();
            //    UserToView.moreInfo = singleprofile["moreInfo"].ToString();

            //}

            while (someskills.Read())
            {
                SkillList.Add(new User_Skill
                {

                    skill = someskills["skill"].ToString(),
                    skillLevel = someskills["skillLevel"].ToString()


                });

            }
            someskills.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }
    }
}

