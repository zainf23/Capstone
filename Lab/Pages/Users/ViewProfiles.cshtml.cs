using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Http;
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

        public List<User_Skill> SoftSkillList { get; set; }

        public List<User_Skill> HobbyList { get; set; }

        [BindProperty]
        public User UserProfile { get; set; }

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int userID { get; set; }

        [BindProperty]
        public string fileName { get; set; }

        [BindProperty]
        public List<UserBookmark> MyBookMarks { get; set; }

        public ViewProfilesModel()
        {
            UserToView = new User_Skill();
            SkillList = new List<User_Skill>();
            SoftSkillList = new List<User_Skill>();
            HobbyList = new List<User_Skill>();
            MyBookMarks = new List<UserBookmark>();
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
           
            // hard skills
            
            SqlDataReader someskills = DBClass.SomeSkills(UserProfile.userID);

            while (someskills.Read())
            {
                SkillList.Add(new User_Skill
                {

                    skill = someskills["skill"].ToString(),
                    skillLevel = someskills["skillLevel"].ToString()


                });

            }
            someskills.Close();

           // soft skills
            
            SqlDataReader somesoftskills = DBClass.SomeSoftSkills(UserProfile.userID);

            while (somesoftskills.Read())
            {
                SoftSkillList.Add(new User_Skill
                {

                    softSkill = somesoftskills["softSkill"].ToString(),


                });

            }
            somesoftskills.Close();

            SqlDataReader somehobbies = DBClass.SomeHobbies(UserProfile.userID);

            while (somehobbies.Read())
            {
                HobbyList.Add(new User_Skill
                {

                    hobby = somehobbies["hobby"].ToString(),


                });

            }
            somehobbies.Close();

            String sqlQuery = "Select [fileName] FROM UserProfilePic WHERE userID =" + UserProfile.userID;

            SqlDataReader picReader = DBClass.GeneralReaderQuery(sqlQuery);

            while (picReader.Read())
            {
                fileName = picReader["fileName"].ToString();
            }
            picReader.Close();

            String sqlQueryTwo = "Select Distinct u.userID, u.firstName, u.secondName, u.jmuType, u.gradYear, u.major, u.minor, u.jobtitle, u.department, upp.fileName from [User] u, UserProfilePic upp, Bookmark b WHERE (u.userID = upp.userID AND u.userID = b.otherUserID) AND b.userID =" + UserProfile.userID;
            SqlDataReader BookmarkReader = DBClass.GeneralReaderQuery(sqlQueryTwo);
            
            while (BookmarkReader.Read())
            {
                MyBookMarks.Add(new UserBookmark
                {
                    userID = Int32.Parse(BookmarkReader["userID"].ToString()),
                    firstName = BookmarkReader["firstName"].ToString(),
                    secondName = BookmarkReader["SecondName"].ToString(),
                    jmuType = BookmarkReader["jmuType"].ToString(),
                    gradYear = BookmarkReader["gradYear"].ToString(),
                    major = BookmarkReader["major"].ToString(),
                    minor = BookmarkReader["minor"].ToString(),
                    jobTitle = BookmarkReader["jobtitle"].ToString(),
                    department = BookmarkReader["department"].ToString(),
                    fileName = BookmarkReader["fileName"].ToString(),

                });
            }
            BookmarkReader.Close();

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/HashedLogin");
            }

            return Page();
        }

    }
}

