namespace Lab.Pages.DataClasses
{
    public class Message
    {
        public int messageID { get; set; }

        public int userID { get; set; }

        public int otherUserID { get; set; }

        public string senderName { get; set; }

        public string subject { get; set; }

        public string message { get; set; }

        public int readMessage { get; set; }

        public string date { get; set; }

        public string time { get; set; }

        public string fileName { get; set; }

    }
}
