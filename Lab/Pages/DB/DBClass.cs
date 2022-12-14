using Lab.Pages.DataClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Data.SqlClient;

namespace Lab.Pages.DB
{
    public class DBClass
    {
        //private static readonly string LabConnStr
        //    = @"Server=capstone.cjzu3rtz5sjd.us-east-1.rds.amazonaws.com;
        //            Database=Capstone;uid=adminuser;password=Swoopes1;Pooling=false";

        //private static readonly string AuthConnStr
        //    = @"Server=capstone.cjzu3rtz5sjd.us-east-1.rds.amazonaws.com;
        //            Database=AUTH;uid=adminuser;password=Swoopes1;Pooling=false";

        private static readonly string LabConnStr
            = @"Server=Localhost;Database=Capstone;Trusted_Connection=True;Pooling=false";

        private static readonly string AuthConnStr
           = @"Server=Localhost;Database=AUTH;Trusted_Connection=True;Pooling=false";

        // method to read data from user file
        public static SqlDataReader UserReader(string username)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT * FROM [User] WHERE username = '" + username + "'";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return tempReader;
        }
        // method to create users
        public static void InsertUser(User u)
        {
            string sqlQuery = "INSERT INTO [User] (firstName,secondName,username,email,jmuType,interests,experience,gradYear,major,minor,jobtitle,department,moreInfo) VALUES (";
            sqlQuery += "@firstName,";
            sqlQuery += "@secondName,";
            sqlQuery += "@username,";
            //sqlQuery += "'" + u.passphrase + "',";
            sqlQuery += "@email,";
            sqlQuery += "@jmuType,";
            sqlQuery += "@interests,";
            sqlQuery += "@experience,";
            sqlQuery += "@gradYear,";
            sqlQuery += "@major,";
            sqlQuery += "@minor,";
            sqlQuery += "@jobTitle,";
            sqlQuery += "@department,";
            sqlQuery += "@moreInfo)";

            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
            cmdUserRead.Parameters.AddWithValue("@firstName", u.firstName);
            cmdUserRead.Parameters.AddWithValue("@secondName", u.secondName);
            cmdUserRead.Parameters.AddWithValue("@username", u.username);
            cmdUserRead.Parameters.AddWithValue("@email", u.email);
            cmdUserRead.Parameters.AddWithValue("@jmuType", u.jmuType);
            cmdUserRead.Parameters.AddWithValue("@interests", u.interests);
            cmdUserRead.Parameters.AddWithValue("@experience", u.experience);
            cmdUserRead.Parameters.AddWithValue("@gradYear", u.gradYear);
            cmdUserRead.Parameters.AddWithValue("@major", u.major);
            cmdUserRead.Parameters.AddWithValue("@minor", u.minor);
            cmdUserRead.Parameters.AddWithValue("@jobTitle", u.jobTitle);
            cmdUserRead.Parameters.AddWithValue("@department", u.department);
            cmdUserRead.Parameters.AddWithValue("@moreInfo", u.moreInfo);
            cmdUserRead.Connection.Open();
            cmdUserRead.ExecuteNonQuery();

        }

        public static void InsertType(string jmuType, string firstName, string secondName,string username, string secQuestionMom, string secQuestionPet, string secQuestionParents)
        {
            string sqlQuery = "INSERT INTO [User] (firstName,secondName,username,jmuType,secQuestionMom,secQuestionPet,secQuestionParents) VALUES (";
            sqlQuery += "'" + firstName + "',";
            sqlQuery += "'" + secondName + "',";
            sqlQuery += "'" + username + "',";
            sqlQuery += "'" + jmuType + "',";
            sqlQuery += "'" + secQuestionMom + "',";
            sqlQuery += "'" + secQuestionPet + "',";
            sqlQuery += "'" + secQuestionParents + "')";

            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
            cmdUserRead.Connection.Open();
            cmdUserRead.ExecuteNonQuery();

        }
        // method to view user data
        public static SqlDataReader SingleUserReader(int userID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT * FROM [User] WHERE userID = " + userID;
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }
        //method to update user
        public static void UpdateUser(User u)
        {
            string sqlQuery = "UPDATE [User] SET ";

            sqlQuery += "firstName='" + u.firstName + "',";
            sqlQuery += "secondName='" + u.secondName + "',";
            sqlQuery += "email='" + u.email + "',";
            sqlQuery += "jmuType='" + u.jmuType + "',";
            sqlQuery += "interests='" + u.interests + "',";
            sqlQuery += "experience='" + u.experience + "',";
            sqlQuery += "gradYear='" + u.gradYear + "',";
            sqlQuery += "major='" + u.major + "',";
            sqlQuery += "minor='" + u.minor + "',";
            sqlQuery += "jobTitle='" + u.jobTitle + "',";
            sqlQuery += "department='" + u.department + "',";
            sqlQuery += "moreInfo='" + u.moreInfo + "' WHERE userID=" + u.userID;

            SqlCommand cmdUserRead = new SqlCommand(sqlQuery);
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
            cmdUserRead.Connection.Open();
            cmdUserRead.ExecuteNonQuery();
        }
        //public static void UpdateUser(User u)
        //{
        //    string sqlQuery = "UPDATE [User] SET ";

        //    sqlQuery += "firstName= @firstName,";
        //    sqlQuery += "secondName= @secondName,";
        //    sqlQuery += "email= @email,";
        //    sqlQuery += "jmuType= @jmuType,";
        //    sqlQuery += "interests= @interests,";
        //    sqlQuery += "experience= @experience,";
        //    sqlQuery += "gradYear= @gradYear,";
        //    sqlQuery += "major= @major,";
        //    sqlQuery += "minor= @minor,";
        //    sqlQuery += "jobTitle= @jobTitle,";
        //    sqlQuery += "department= @department,";
        //    sqlQuery += "moreInfo= @moreInfo WHERE userID= @userID";

        //    SqlCommand cmdUserRead = new SqlCommand(sqlQuery);
        //    cmdUserRead.Connection = new SqlConnection();
        //    cmdUserRead.Connection.ConnectionString = LabConnStr;
        //    cmdUserRead.CommandText = sqlQuery;
        //    cmdUserRead.Parameters.AddWithValue("@userID", u.userID);
        //    cmdUserRead.Parameters.AddWithValue("@firstName", u.firstName);
        //    cmdUserRead.Parameters.AddWithValue("@secondName", u.secondName);
        //    cmdUserRead.Parameters.AddWithValue("@email", u.email);
        //    cmdUserRead.Parameters.AddWithValue("@jmuType", u.jmuType);
        //    cmdUserRead.Parameters.AddWithValue("@interests", u.interests);
        //    cmdUserRead.Parameters.AddWithValue("@experience", u.experience);
        //    cmdUserRead.Parameters.AddWithValue("@gradYear", u.gradYear);
        //    cmdUserRead.Parameters.AddWithValue("@major", u.major);
        //    cmdUserRead.Parameters.AddWithValue("@minor", u.minor);
        //    cmdUserRead.Parameters.AddWithValue("@jobTitle", u.jobTitle);
        //    cmdUserRead.Parameters.AddWithValue("@department", u.department);
        //    cmdUserRead.Parameters.AddWithValue("@moreInfo", u.moreInfo);
        //    cmdUserRead.Connection.Open();
        //    cmdUserRead.ExecuteNonQuery();
        //}
        //method to read skills into list
        public static SqlDataReader SkillReader()
        {
            SqlCommand cmdSkillRead = new SqlCommand();
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = "SELECT * FROM UserSkill";
            cmdSkillRead.Connection.Open();
            SqlDataReader tempReader = cmdSkillRead.ExecuteReader();

            return tempReader;
        }
        // method to find skills for a certain user
        public static SqlDataReader SingleSkillReader(int userID, string skill)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT * FROM UserSkill WHERE userID = " + userID +"and skill ='" + skill + "'";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }

        public static SqlDataReader SingleSoftSkillReader(int userID, string softSkill)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT * FROM UserSoftSkill WHERE userID = " + userID + "and softSkill ='" + softSkill + "'";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }

        // method to update skills
        public static void UpdateSkill(UserSkill s)
        {
            string sqlQuery = "UPDATE UserSkill SET ";

            sqlQuery += "skill='" + s.skill + "',";
            sqlQuery += "skillLevel='" + s.skillLevel + "' WHERE userID=" + s.userID;

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }
        // method to insert skills into db
        public static void InsertSkill(string skill,string skillLevel,int userID)
        {
            string sqlQuery = "INSERT INTO UserSkill (userID,skill,skillLevel) VALUES (";
            sqlQuery += "@userID,";
            sqlQuery += "@skill,";
            sqlQuery += "@skillLevel)";

            SqlCommand cmdSkillRead = new SqlCommand();
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Parameters.AddWithValue("@userID", userID);
            cmdSkillRead.Parameters.AddWithValue("@skill", skill);
            cmdSkillRead.Parameters.AddWithValue("@skillLevel", skillLevel);
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();

        }

        public static void InsertSoftSkill(string softSkill, int userID)
        {
            string sqlQuery = "INSERT INTO UserSoftSkill (userID,softSkill) VALUES (";
            sqlQuery += "@userID,";
            sqlQuery += "@softSkill)";

            SqlCommand cmdSkillRead = new SqlCommand();
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Parameters.AddWithValue("@softSkill", softSkill);
            cmdSkillRead.Parameters.AddWithValue("@userID", userID);
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();

        }
        // method to read project info into list
        public static SqlDataReader ProjectReader()
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = LabConnStr;
            cmdProjectRead.CommandText = "SELECT projectName FROM Project";
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();

            return tempReader;
        }
        // method to find skills from unique user
        public static SqlDataReader UserSkillReader()
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT u.userID,firstName,secondName,skill,skillLevel FROM [User] u, UserSkill us WHERE u.userID =  us.userID";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }
        // method to find users skills
        public static SqlDataReader SkillUserReader()
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT * FROM [User] u, UserSkill us WHERE u.userID = us.userId";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }
        // method to create projects in db
        public static void InsertProject(Project p)
        {
            string sqlQuery = "INSERT INTO Project (userID,projectName,projectOwner,projectOwnerEmail,projectMissionStatement,projectDescription,projectDate) VALUES (";
            sqlQuery += "@userID,";
            sqlQuery += "@projectName,";
            sqlQuery += "@projectOwner,";
            sqlQuery += "@projectOwnerEmail,";
            sqlQuery += "@projectMissionStatement,";
            sqlQuery += "@projectDescription,";
            sqlQuery += "@projectDate)";

            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
            cmdUserRead.Parameters.AddWithValue("@userID", p.userID);
            cmdUserRead.Parameters.AddWithValue("@projectName", p.projectName);
            cmdUserRead.Parameters.AddWithValue("@projectOwner", p.projectOwner);
            cmdUserRead.Parameters.AddWithValue("@projectOwnerEmail", p.projectOwnerEmail);
            cmdUserRead.Parameters.AddWithValue("@projectMissionStatement", p.projectMissionStatement);
            cmdUserRead.Parameters.AddWithValue("@projectDescription", p.projectDescription);
            cmdUserRead.Parameters.AddWithValue("@projectDate", p.projectDate);
            cmdUserRead.Connection.Open();
            cmdUserRead.ExecuteNonQuery();

        }
        // method that accpets querys from model/view
        public static SqlDataReader GeneralReaderQuery(string sqlQuery)
        {

            SqlCommand cmdContentRead = new SqlCommand(sqlQuery);
            cmdContentRead.Connection = new SqlConnection();
            cmdContentRead.Connection.ConnectionString = LabConnStr;
            cmdContentRead.CommandText = sqlQuery;
            cmdContentRead.Connection.Open();
            SqlDataReader tempReader = cmdContentRead.ExecuteReader();

            return tempReader;
        }
        // method that reads teams
        public static SqlDataReader TeamReaderMethod()
        {
            SqlCommand cmdTeamRead = new SqlCommand();
            cmdTeamRead.Connection = new SqlConnection();
            cmdTeamRead.Connection.ConnectionString = LabConnStr;
            cmdTeamRead.CommandText = "SELECT * FROM Team";
            cmdTeamRead.Connection.Open();
            SqlDataReader tempReader = cmdTeamRead.ExecuteReader();

            return tempReader;
        }

        // method that pulls user data based on their userID
        public static SqlDataReader SingleProfileReader(int userID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT * FROM [User] WHERE userID =" + userID;
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }
        // method that puts skills in a list
        public static SqlDataReader SomeSkills(int userID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT skill, skillLevel from UserSkill where userID =" + userID;
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }

        public static SqlDataReader SomeSoftSkills(int userID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT softSkill from UserSoftSkill where userID =" + userID;
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }

        public static SqlDataReader SomeHobbies(int userID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT hobby from UserHobbies where userID =" + userID;
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }

        // method that pulls project info based on projectID passed
        public static SqlDataReader SingleProjectReader(int projectID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT * FROM Project WHERE projectID = " + projectID;
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }
        // method that updates projects in db
        public static void UpdateProject(Project p)
        {
            string sqlQuery = "UPDATE Project SET ";

            sqlQuery += "projectName= @projectName,";
            sqlQuery += "projectOwner= @projectOwner,";
            sqlQuery += "projectOwnerEmail= @projectOwnerEmail,";
            sqlQuery += "projectMissionStatement= @projectMissionStatement,";
            sqlQuery += "projectDescription= @projectDescription,";
            sqlQuery += "projectDate= @projectDate WHERE projectID= @projectID";

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Parameters.AddWithValue("@projectID", p.projectID);
            cmdSkillRead.Parameters.AddWithValue("@projectName", p.projectName);
            cmdSkillRead.Parameters.AddWithValue("@projectOwner", p.projectOwner);
            cmdSkillRead.Parameters.AddWithValue("@projectOwnerEmail", p.projectOwnerEmail);
            cmdSkillRead.Parameters.AddWithValue("@projectMissionStatement", p.projectMissionStatement);
            cmdSkillRead.Parameters.AddWithValue("@projectDescription", p.projectDescription);
            cmdSkillRead.Parameters.AddWithValue("@projectDate", p.projectDate);
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }
        // method that creates team
        public static void InsertTeam(ProjectTeam t)
        {
            string sqlQuery = "INSERT INTO Team (teamName,teamDescription,teamEmailAddress,projectID) VALUES (";
            sqlQuery += "'" + t.teamName + "',";
            sqlQuery += "'" + t.teamDescription + "',";
            sqlQuery += "'" + t.teamEmailAddress + "',";
            sqlQuery += "'" + t.projectID + "')";

            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
            cmdUserRead.Connection.Open();
            cmdUserRead.ExecuteNonQuery();

        }

        //not used
        public static SqlDataReader ProjectIDFinder(ProjectTeam t)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT projectID from Project WHERE projectName ='" + t.projectName + "'";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);

        }
        // method that reds team info based on teamid
        public static SqlDataReader SingleTeamReader(int teamID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT * FROM Team WHERE teamID = " + teamID;
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }
        // update team
        public static void UpdateTeam(Team t)
        {
            string sqlQuery = "UPDATE Team SET ";

            sqlQuery += "teamName='" + t.teamName + "',";
            sqlQuery += "teamDescription='" + t.teamDescription + "',";
            sqlQuery += "teamEmailAddress='" + t.teamEmailAddress + "' WHERE teamID=" + t.teamID;

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }
        // method that pulls user and skill info based on the skill passed
        public static SqlDataReader SingleUserSkillReader(string skill)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = @"SELECT u.firstName,u.secondName,us.skill,
                                us.skillLevel from[USER] u, UserSkill us WHERE 
                                u.userID = us.userID and skill= '" + skill + "'";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }
        //method to update skill
        public static void UpdateTheSkill(User_Skill s, String skillPlaceHolder)
        {
            string sqlQuery = "UPDATE UserSkill SET ";

            sqlQuery += "skill= @skill,";
            sqlQuery += "skillLevel= @skillLevel WHERE skill= @skillPlaceHolder AND userID= @userID";

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Parameters.AddWithValue("@skill", s.skill);
            cmdSkillRead.Parameters.AddWithValue("@skillLevel", s.skillLevel);
            cmdSkillRead.Parameters.AddWithValue("@skillPlaceHolder", skillPlaceHolder);
            cmdSkillRead.Parameters.AddWithValue("@userID", s.userID);
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }

        public static void UpdateTheSoftSkill(string softSkill, int userid)
        {
            string sqlQuery = "Delete from UserSoftSkill WHERE softskill = '" + softSkill + "' AND userID =" + userid;
            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();

        }

        public static int LoginQuery(string loginQuery)
        {
            // This method expects to receive an SQL SELECT
            // query that uses the COUNT command.

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = LabConnStr;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();

            return rowCount;
        }

        public static int SecureLogin(string username, string passphrase)
        {
            string loginQuery =
                "SELECT COUNT(*) FROM Credentials where username = @username and passphrase = @passphrase";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = LabConnStr;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@username", username);
            cmdLogin.Parameters.AddWithValue("@passphrase", passphrase);

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();

            return rowCount;
        }

        //insert query
        public static void InsertQuery(string sqlQuery)
        {

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = LabConnStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();

        }

        public static void RequestJoinQuery(int userID, int projectID, int teamID, string userPitch)
        {
            string sqlQuery = "INSERT INTO Request (userID, projectID, teamID, accepted, userPitch) VALUES (";
            sqlQuery += "@userID,";
            sqlQuery += "@projectID,";
            sqlQuery += "@teamID,";
            sqlQuery += 0 + ",";
            sqlQuery += "@userPitch)";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = LabConnStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Parameters.AddWithValue("@userID", userID);
            cmdProductRead.Parameters.AddWithValue("@projectID", projectID);
            cmdProductRead.Parameters.AddWithValue("@teamID", teamID);
            cmdProductRead.Parameters.AddWithValue("@userPitch", userPitch);

            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();

        }

        public static void ProjectChatRoomQuery(int userID, int projectID, string subject, string fullName, string recipient, string messageInfo, string date)
        {

            string sqlQuery = "INSERT INTO ProjectChatRoom (userID, projectID, subject, sender, recipient, messageInfo, date) VALUES (";
            sqlQuery += "@userID,";
            sqlQuery += "@projectID,";
            sqlQuery += "@subject,";
            sqlQuery += "@fullName,";
            sqlQuery += "@recipient,";
            sqlQuery += "@messageInfo,";
            sqlQuery += "@date)";


            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = LabConnStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Parameters.AddWithValue("@userID", userID);
            cmdProductRead.Parameters.AddWithValue("@projectID", projectID);
            cmdProductRead.Parameters.AddWithValue("@subject", subject);
            cmdProductRead.Parameters.AddWithValue("@fullName", fullName);
            cmdProductRead.Parameters.AddWithValue("@recipient", recipient);
            cmdProductRead.Parameters.AddWithValue("@messageInfo", messageInfo);
            cmdProductRead.Parameters.AddWithValue("@date", date);
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();

        }


        //method to find project information about the specific project a team is working on
        public static SqlDataReader GetProjectID(int teamID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT projectName, projectOwner, projectOwnerEmail, projectMissionStatement, projectDescription, projectDate FROM Project p, Team t WHERE p.projectID = t.projectID AND teamID = '" + teamID + "'";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }

        //method to add team members
        public static void InsertMemberQuery(string sqlQuery1)
        {

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = LabConnStr;
            cmdProductRead.CommandText = sqlQuery1;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();

        }

        public static void CreateHashedUser(string username, string passphrase)
        {
            string loginQuery =
                "INSERT INTO HashedCredentials (username,passphrase) values (@username, @passphrase)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = AuthConnStr;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@username", username);
            cmdLogin.Parameters.AddWithValue("@passphrase", PasswordHash.HashPassword(passphrase));
            cmdLogin.Connection.Open();
            cmdLogin.ExecuteNonQuery();

        }


        public static void InsertHobby(string hobby, int userID)
        {
            string sqlQuery = "INSERT INTO UserHobbies (userID,hobby) VALUES (@userid, @hobby)";

            SqlCommand cmdHobbyRead = new SqlCommand();
            cmdHobbyRead.Connection = new SqlConnection();
            cmdHobbyRead.Connection.ConnectionString = LabConnStr;
            cmdHobbyRead.CommandText = sqlQuery;
            cmdHobbyRead.Parameters.AddWithValue("@userid", userID);
            cmdHobbyRead.Parameters.AddWithValue("@hobby", hobby);
            cmdHobbyRead.Connection.Open();
            cmdHobbyRead.ExecuteNonQuery();

        }

        public static void UpdateTheHobby(string hobby, int userid)
        {
            string sqlQuery = "Delete from UserHobbies WHERE hobby = '" + hobby + "' AND userID =" + userid;
            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();

        }

        public static bool HashedParameterLogin(string username, string passphrase)
        {

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = AuthConnStr;
            cmdLogin.CommandType = System.Data.CommandType.StoredProcedure;
            cmdLogin.Parameters.AddWithValue("@username", username);
            cmdLogin.CommandText = "sp_Lab3Login";
            cmdLogin.Connection.Open();
            SqlDataReader hashReader = cmdLogin.ExecuteReader();
            if (hashReader.Read())
            {
                string correctHash = hashReader["passphrase"].ToString();

                if (PasswordHash.ValidatePassword(passphrase, correctHash))
                {
                    return true;
                }
            }

            return false;
        }

        public static void UpdateAcceptedRequest(int requestid)
        {
            string sqlQuery = "UPDATE Request SET ";
            sqlQuery += "accepted= 1" + "WHERE requestID =" + requestid;

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }

        public static void DeleteRejectedRequest(int requestid)
        {
            string sqlQuery = "Delete FROM Request WHERE requestID =" + requestid;

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }

        

        public static void UpdateAcceptedRequestTwo(int requestIDTwo)
        {
            string sqlQuery = "UPDATE RequestTwo SET ";
            sqlQuery += "acceptedTwo= 1" + "WHERE requestIDTwo =" + requestIDTwo;

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }

        public static void UpdateRejectedRequestTwo(int requestIDTwo)
        {
            string sqlQuery = "UPDATE RequestTwo SET ";
            sqlQuery += "acceptedTwo= -1" + "WHERE requestIDTwo =" + requestIDTwo;

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }

        public static void DataHolderForUPP(int userID)
        {
            string sqlQuery = "INSERT INTO UserProfilePic ([fileName],userID) VALUES (@fileName, @userID)";

            SqlCommand cmdHobbyRead = new SqlCommand();
            cmdHobbyRead.Connection = new SqlConnection();
            cmdHobbyRead.Connection.ConnectionString = LabConnStr;
            cmdHobbyRead.CommandText = sqlQuery;
            cmdHobbyRead.Parameters.AddWithValue("@fileName", "Placeholder.jpg");
            cmdHobbyRead.Parameters.AddWithValue("@userID", userID);
            cmdHobbyRead.Connection.Open();
            cmdHobbyRead.ExecuteNonQuery();
        }


        public static void InsertMeeting(int projectID, string meetingTitle, string meetingDate, string meetingTime, string meetingPlan, string meetingLocation)
        {
            string sqlQuery = "INSERT INTO TeamMeeting (projectID,meetingTitle,meetingDate,meetingTime,meetingPlan,meetingLocation) VALUES (@projectID, @meetingTitle, @meetingDate, @meetingTime, @meetingPlan, @meetingLocation)";

            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
            cmdUserRead.Parameters.AddWithValue("@projectID", projectID);
            cmdUserRead.Parameters.AddWithValue("@meetingTitle", meetingTitle);
            cmdUserRead.Parameters.AddWithValue("@meetingDate", meetingDate);
            cmdUserRead.Parameters.AddWithValue("@meetingTime", meetingTime);
            cmdUserRead.Parameters.AddWithValue("@meetingPlan", meetingPlan);
            cmdUserRead.Parameters.AddWithValue("@meetingLocation", meetingLocation);
            cmdUserRead.Connection.Open();
            cmdUserRead.ExecuteNonQuery();

        }


        public static void UpdateMeeting(string meetingSummary, int teamMeetingID)
        {
            string sqlQuery = "UPDATE TeamMeeting SET ";
            sqlQuery += "attended= 1,";
            sqlQuery += "meetingSummary = @meetingSummary WHERE teamMeetingID = @teamMeetingID";

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Parameters.AddWithValue("@meetingSummary", meetingSummary);
            cmdSkillRead.Parameters.AddWithValue("@teamMeetingID", teamMeetingID);
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }

        public static void UpdateMeeting(TeamMeeting m)
        {
            string sqlQuery = "UPDATE TeamMeeting SET ";

            sqlQuery += "meetingTitle = @meetingTitle,";
            sqlQuery += "meetingDate = @meetingDate,";
            sqlQuery += "meetingTime = @meetingTime,";
            sqlQuery += "meetingLocation = @meetingLocation,"; 
            sqlQuery += "meetingPLan = @meetingPlan WHERE teamMeetingID = @teamMeetingID";

            SqlCommand cmdUserRead = new SqlCommand(sqlQuery);
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
            cmdUserRead.Parameters.AddWithValue("@teammeetingID", m.teamMeetingID);
            cmdUserRead.Parameters.AddWithValue("@meetingTitle", m.meetingTitle);
            cmdUserRead.Parameters.AddWithValue("@meetingTime", m.meetingTime);
            cmdUserRead.Parameters.AddWithValue("@meetingDate", m.meetingDate);
            cmdUserRead.Parameters.AddWithValue("@meetingLocation", m.meetingLocation);
            cmdUserRead.Parameters.AddWithValue("@meetingPlan", m.meetingPlan);
            cmdUserRead.Connection.Open();
            cmdUserRead.ExecuteNonQuery();
        }

        public static void UpdateHashedUser(string username, string passphrase)
        {
            string loginQuery ="Update HashedCredentials SET ";
            loginQuery += "passphrase = @passphrase WHERE username = @username";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = AuthConnStr;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@username", username);
            cmdLogin.Parameters.AddWithValue("@passphrase", PasswordHash.HashPassword(passphrase));
            cmdLogin.Connection.Open();
            cmdLogin.ExecuteNonQuery();

        }

        public static void UpdateProjectPic(string fileName, int projectID)
        {
            string sqlQuery = "UPDATE Project SET ";

            sqlQuery += "fileName = @fileName WHERE projectID = @projectID";

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Parameters.AddWithValue("@fileName", fileName);
            cmdSkillRead.Parameters.AddWithValue("@projectID", projectID);
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }

        public static SqlDataReader SingleMeetingReader(int teamMeetingID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = "SELECT * FROM TeamMeeting WHERE teamMeetingID = " + teamMeetingID;
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return (tempReader);
        }

        public static void SendMessage(int userID, int otherUserID, string subject, string message,string senderName,string date)
        {
            string sqlQuery = "INSERT INTO Messages (userID,otherUserID,subject,message,senderName,date) VALUES (@userID, @otherUserID, @subject, @message, @senderName, @date)";

            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
            cmdUserRead.Parameters.AddWithValue("@userID", userID);
            cmdUserRead.Parameters.AddWithValue("@otherUserID", otherUserID);
            cmdUserRead.Parameters.AddWithValue("@subject", subject);
            cmdUserRead.Parameters.AddWithValue("@message", message);
            cmdUserRead.Parameters.AddWithValue("@senderName", senderName);
            cmdUserRead.Parameters.AddWithValue("@date", date);
            cmdUserRead.Connection.Open();
            cmdUserRead.ExecuteNonQuery();



        }

        public static void MessageRead(int messageid)
        {
            string sqlQuery = "UPDATE Messages SET ";
            sqlQuery += "readMessage= 1" + "WHERE messageID =" + messageid;

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }

        



    }
}
