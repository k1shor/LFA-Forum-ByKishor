using LFA.ConsoleForum.Ado;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFAForumClassLib
{
    public class Users
    {
        public int ID{get;set;}
        public String UserName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActivity { get; set; }
        public Boolean IsModerator { get; set; }
        private SqlHelper sh;
        private string sqlQuerry;
            

        public Users()
        {
            sh = new SqlHelper();
        }


        public bool LoginSuccess()
        {
            Console.WriteLine("Enter Username");
            UserName = Console.ReadLine();
            String sqlQuerry = "Select * from Users where UserName=@UserName";
            SqlDataReader dr1 = sh.ExecuteReader(sqlQuerry, "@UserName", UserName);
            if (dr1.Read())
            {
                Console.WriteLine("Welcome  " + UserName + " !");
                Console.WriteLine("");
                dr1.Close();
                return true; 
            }
            else
            {
                dr1.Close();
                Console.WriteLine("Invalid Username or " + UserName + " does not exits.");
                Console.WriteLine("Do you wish to continue? enter Y to continue and any other to exit");
                string ch = Console.ReadLine().ToString();
                if (ch == "Y" || ch == "y")
                {
                    return LoginSuccess(); ;
                }
                else
                {
                    Console.WriteLine("You have chose to exit.");
                    return false;
                }
                    
            }            
        }

        public void AddNewUser()
        {
            Console.WriteLine("Fill up the followings:");
            Console.WriteLine("Username:");
            bool ok = false;
            while (!ok)
            {                
                UserName = Console.ReadLine();
                sqlQuerry = "Select UserName from Users where UserName=@UserName";
                
                if (CheckUser(sh,sqlQuerry,UserName))
                {
                    Console.WriteLine("Username already exists. Please choose another UserName.");
                    Console.WriteLine("Username:");
                }
                else
                {
                    ok = true;
                }
            }
            Console.WriteLine("First Name:");
            FirstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            LastName = Console.ReadLine();
            Console.WriteLine("Email:");
            Email = Console.ReadLine();
            sqlQuerry = "uspAddUser";
            int result = sh.ExecNonQueryProcedure(sqlQuerry, "@UserName", UserName, "@FirstName", FirstName, "@LastName", LastName, "@Email", Email);
            if (result == 1)
                Console.WriteLine(" Thank you for registering. An email has been sent to your email address. Please Verify");
        
        }

        public void ViewAll()
        {
            sqlQuerry = "Select * from Users ORDER BY ID";
            SqlDataReader dr1 = sh.ExecuteReader(sqlQuerry);
            while (dr1.Read())
            {
                Console.WriteLine(dr1[0].ToString() + " " + dr1[1].ToString() + " " + dr1[2].ToString() + " " + dr1[3].ToString() + " " + dr1[4].ToString());
            }
            dr1.Close();
        }

        private static bool CheckUser(SqlHelper sh, string sqlQuerry, String strUserName)
        {
            SqlDataReader dr1;
            dr1 = sh.ExecuteReader(sqlQuerry, "@UserName", strUserName);
            if (dr1.Read())
            {
                dr1.Close();
                return true;
            }                
            else
            {
                dr1.Close();
                return false;
            }
        }

        public void UpdateUser()
        {
            sqlQuerry = "Select UserName from Users where UserName=@UserName";
            Console.WriteLine("Username:");
            
            bool ok = false;
            while (!ok)
            {
                UserName = Console.ReadLine();                
                if (CheckUser(sh,sqlQuerry,UserName))
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    Console.WriteLine("Username does not exists. Please try again.");
                    Console.WriteLine("Username:");
                }
            }
            Console.WriteLine("First Name:");
            FirstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            LastName = Console.ReadLine();
            Console.WriteLine("Email:");
            Email = Console.ReadLine();
            sqlQuerry = "uspUpdateUser";
            int result = sh.ExecNonQueryProcedure(sqlQuerry, "@UserName", UserName, "@FirstName", FirstName, "@LastName", LastName, "@Email", Email);
            if (result == 1)
            {
                Console.WriteLine("");
                Console.WriteLine(" Update Successful ");
            }            
        }

        public void DeleteUser()
        {
            Console.WriteLine("Username:");
            UserName = "";
            bool ok = false;
            while (!ok)
            {
                UserName = Console.ReadLine();
                sqlQuerry = "Select UserName from Users where UserName=@UserName";

                if (CheckUser(sh, sqlQuerry, UserName))
                {
                    ok = true;                   
                }
                else
                {
                    Console.WriteLine("Username does not exists. Please try again.");
                    Console.WriteLine("Username:");
                }
            }
            sqlQuerry = "uspRemoveUser";
            int result = sh.ExecNonQueryProcedure(sqlQuerry, "@UserName", UserName);
            if (result == 1)
            {
                Console.WriteLine("");
                Console.WriteLine(" Delete Successful ");
            }
                                    
        }
    
    }

    public class Category
    {
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Creator { get; set; }
        DateTime Created { get; set; }
        int StatusID { get; set; }
        int ParentCategoryID { get; set; }
    
    }

    public class Groups
    {
        int ID { get; set; }
        string Name { get; set; }
        int CategoryID { get; set; }
        int CreatorID { get; set; }
    }

    public class Posts
    {
        int ID { get; set; }
        string Subject { get; set; }
        string Content { get; set; }
        DateTime Created { get; set; }
        int ThreadID { get; set; }
        int UserID { get; set; }
        int StatusID { get; set; }
    }

    public class Topic
    {
        int ID { get; set; }
        string Subject { get; set; }
        DateTime Created { get; set; }
        int UserID { get; set; }
        int StatusID { get; set; }
    }

    public class UserGroup
    {
        int UserID { get; set; }
        int GroupID { get; set; }
    }

    public class Status
    {
        int ID { get; set; }
        string name { get; set; }
    }

    public class UserStatus
    {
        int ID { get; set; }
        string name { get; set; }
    }
}
