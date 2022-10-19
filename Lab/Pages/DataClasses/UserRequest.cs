namespace Lab.Pages.DataClasses
{
    public class UserRequest
    {
        public int requestID { get; set; }

        public int userID { get; set; }

        public int projectID { get; set; }

        public int teamID { get; set; }

        public int accepted { get; set; }

        public string userPitch { get; set; }

        public string firstName { get; set; }

        public string secondName { get; set; }
        public string username { get; set; }

        public string passphrase { get; set; }
        public string email { get; set; }

        public string userType { get; set; }

        public string professionalCompany { get; set; }

        public string professionalEmail { get; set; }

        public string facultyAssociation { get; set; }
    }
}
