using Lab.Pages.DataClasses;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;

namespace Lab.Pages.DB
{
    public class DBClass
    {
        private static readonly string LabConnStr
            = @"Server=Localhost;Database=Capstone;Trusted_Connection=True";

        private static readonly string AuthConnStr
           = @"Server=Localhost;Database=AUTH;Trusted_Connection=True";
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
            sqlQuery += "'" + u.firstName + "',";
            sqlQuery += "'" + u.secondName + "',";
            sqlQuery += "'" + u.username + "',";
            //sqlQuery += "'" + u.passphrase + "',";
            sqlQuery += "'" + u.email + "',";
            sqlQuery += "'" + u.jmuType + "',";
            sqlQuery += "'" + u.interests + "',";
            sqlQuery += "'" + u.experience + "',";
            sqlQuery += "'" + u.gradYear + "',";
            sqlQuery += "'" + u.major + "',";
            sqlQuery += "'" + u.minor + "',";
            sqlQuery += "'" + u.jobTitle + "',";
            sqlQuery += "'" + u.department + "',";
            sqlQuery += "'" + u.moreInfo + "')";

            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
            cmdUserRead.Connection.Open();
            cmdUserRead.ExecuteNonQuery();

        }

        public static void InsertType(string jmuType, string firstName, string secondName,string username)
        {
            string sqlQuery = "INSERT INTO [User] (firstName,secondName,username,jmuType) VALUES (";
            sqlQuery += "'" + firstName + "',";
            sqlQuery += "'" + secondName + "',";
            sqlQuery += "'" + username + "',";
            sqlQuery += "'" + jmuType + "')";

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
            sqlQuery += userID + ",";
            sqlQuery += "'" + skill + "',";
            sqlQuery += "'" + skillLevel + "')";

            SqlCommand cmdSkillRead = new SqlCommand();
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
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
            sqlQuery += p.userID + ",";
            sqlQuery += "'" + p.projectName + "',";
            sqlQuery += "'" + p.projectOwner + "',";
            sqlQuery += "'" + p.projectOwnerEmail + "',";
            sqlQuery += "'" + p.projectMissionStatement + "',";
            sqlQuery += "'" + p.projectDescription + "',";
            sqlQuery += "'" + p.projectDate + "')";

            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = LabConnStr;
            cmdUserRead.CommandText = sqlQuery;
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

            sqlQuery += "projectName='" + p.projectName + "',";
            sqlQuery += "projectOwner='" + p.projectOwner + "',";
            sqlQuery += "projectOwnerEmail='" + p.projectOwnerEmail + "',";
            sqlQuery += "projectMissionStatement='" + p.projectMissionStatement + "',";
            sqlQuery += "projectDescription='" + p.projectDescription + "',";
            sqlQuery += "projectDate='" + p.projectDate + "' WHERE projectID=" + p.projectID;

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
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
        public static void UpdateTheSkill(User_Skill s)
        {
            string sqlQuery = "UPDATE UserSkill SET ";

            sqlQuery += "skillLevel='" + s.skillLevel + "' WHERE skill= '" + s.skill + "'AND userID=" + s.userID;

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

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            cmdLogin.ExecuteNonQuery();

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

        public static void UpdateRejectedRequest(int requestid)
        {
            string sqlQuery = "UPDATE Request SET ";
            sqlQuery += "accepted= -1" + "WHERE requestID =" + requestid;

            SqlCommand cmdSkillRead = new SqlCommand(sqlQuery);
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = LabConnStr;
            cmdSkillRead.CommandText = sqlQuery;
            cmdSkillRead.Connection.Open();
            cmdSkillRead.ExecuteNonQuery();
        }






    }
}
