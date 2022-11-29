using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab.Pages.Messages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public User UserData { get; set; }

        [BindProperty]
        public List<Message> MyMessages { get; set; }

        [BindProperty]
        public List<Message> MyReadMessages { get; set; }

        public string username { get; set; }

        public IndexModel()
        {          
            UserData = new User();
            MyMessages = new List<Message>();
            MyReadMessages = new List<Message>();
        }
        public void OnGet()
        {
            username = HttpContext.Session.GetString("username");
            SqlDataReader userReader = DBClass.UserReader(username);

            while (userReader.Read())
            {
                UserData.userID = Int32.Parse(userReader["userID"].ToString());
                UserData.firstName = userReader["firstName"].ToString();
                UserData.secondName = userReader["secondName"].ToString();

            }
            userReader.Close();
            //SqlDataReader singleprofile = DBClass.SingleProfileReader(userid);
            HttpContext.Session.SetInt32("userid", UserData.userID);


            string sqlQuery = "Select distinct m.messageID, m.userID, m.otherUserID, m.subject, m.message, m.readMessage, m.date, m.senderName, upp.fileName from Messages m, UserProfilePic upp WHERE readMessage is null AND m.userID = upp.userID AND m.otherUserID = " + UserData.userID + "Order by messageID desc";
            
            SqlDataReader messageReader = DBClass.GeneralReaderQuery(sqlQuery);

            while (messageReader.Read())
            {
                MyMessages.Add(new Message
                {
                    messageID = Int32.Parse(messageReader["messageID"].ToString()),
                    userID = Int32.Parse(messageReader["userID"].ToString()),
                    otherUserID = Int32.Parse(messageReader["otherUserID"].ToString()),
                    subject = messageReader["subject"].ToString(),
                    message = messageReader["message"].ToString(),
                    date = messageReader["date"].ToString(),
                    senderName = messageReader["senderName"].ToString(),
                    fileName = messageReader["fileName"].ToString(),


                });
            }
            messageReader.Close();


            string sqlQuery1 = "Select distinct m.messageID, m.userID, m.otherUserID, m.subject, m.message, m.readMessage, m.date, m.senderName, upp.fileName from Messages m, UserProfilePic upp WHERE readMessage = 1 AND m.userID = upp.userID AND m.otherUserID = " + UserData.userID + "Order by messageID desc";

            SqlDataReader readReader = DBClass.GeneralReaderQuery(sqlQuery1);

            while (readReader.Read())
            {
                MyReadMessages.Add(new Message
                {
                    messageID = Int32.Parse(readReader["messageID"].ToString()),
                    userID = Int32.Parse(readReader["userID"].ToString()),
                    otherUserID = Int32.Parse(readReader["otherUserID"].ToString()),
                    subject = readReader["subject"].ToString(),
                    message = readReader["message"].ToString(),
                    date = readReader["date"].ToString(),
                    senderName = readReader["senderName"].ToString(),
                    fileName = readReader["fileName"].ToString(),


                });
            }
            readReader.Close();
        }
    }
}
