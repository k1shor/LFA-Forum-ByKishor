
using LFAForumClassLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFA.ConsoleForum.Ado
{
    class Program
    {
        static void Main(string[] args)
        {
            //String connString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=LFAFORUM;Data Source=.\\SQLEXPRESS";

            //string sqlQuerry="";
            //SqlDataReader dr1;
            #region - test region
            /*
            #region - view all UserStatus
            sqlQuerry = "Select * from UserStatus ORDER BY ID";
            dr1 = sh.ExecuteReader(sqlQuerry);
            while (dr1.Read())
            {
                Console.WriteLine(dr1[0].ToString() + " " + dr1[1].ToString());
            }
            sh.closeconn();
            #endregion

            Console.WriteLine();

            #region - view user by username

            sh.reconn();
            Console.WriteLine("Enter the name you want to display: ");
            String name = Console.ReadLine();
            
            sqlQuerry = "Select * from Users where FirstName=@FirstName";
            dr1 = sh.ExecuteReader(sqlQuerry, "@FirstName", name);
            while (dr1.Read())
            {
                Console.WriteLine(dr1[0].ToString() + " " + dr1[1].ToString() + " " + dr1[2].ToString() + " " + dr1[3].ToString() + " " + dr1[4].ToString());
            }
            dr1.Close();
            sh.closeconn();
            #endregion


            Console.WriteLine();

            #region - view user by first name using procedure
            sh.reconn();
            Console.WriteLine("Enter Firstname: ");
            String FirstName = Console.ReadLine();

            sqlQuerry = "uspViewUserByFirstName";
            dr1 = sh.ExecReaderProcedure(sqlQuerry,"@FirstName",FirstName);
            while (dr1.Read())
            {
                Console.WriteLine(dr1[0].ToString() + " " + dr1[1].ToString() + " " + dr1[2].ToString() + " " + dr1[3].ToString() + " " + dr1[4].ToString());
            }
            sh.closeconn();
            #endregion


            //Console.WriteLine(conn.State);
            //conn.Close();
            //Console.WriteLine(conn.State);
            Console.ReadKey();
             * 
             * /
             */
            #endregion


            #region - practice

            int choice = StartUpMenu();

            #region - login region

            if (choice==1)
            {
                Console.Clear();
                Users u = new Users();
                                                        
                if (u.LoginSuccess())
                {
                    bool loop = true;
                    while(loop)
                    {
                        ShowUserMenu();
                        int UserChoice = Convert.ToInt16(Console.ReadLine());
                        switch (UserChoice)
                        {
                            case 1:
                                u.ViewAll();
                                loop = Continue(loop);
                                break;
                            case 2:
                                u.UpdateUser();
                                loop=Continue(loop);
                                break;
                            case 3:

                                u.DeleteUser();
                                loop = Continue(loop);
                                break;

                            case 4:
                                Console.WriteLine("You have chose to exit.");
                                loop = false;
                                break;

                            default:
                                Console.WriteLine("You have chosen wrong option. ");
                                loop = Continue(loop);
                                break;
                        }
                    }
                }
            }
            #endregion

            #region - SIGN UP
            if (choice == 2)
            {
                Users u = new Users();
                u.AddNewUser();

                
            }

            #endregion

            Console.WriteLine("Bye Bye");
            #endregion

            Console.ReadLine();


        }

        private static int StartUpMenu()
        {
            Console.WriteLine("1. LOGIN:");
            Console.WriteLine("2. SIGN UP");
            int choice = Convert.ToInt16(Console.ReadLine());
            return choice;
        }

        private static void ShowUserMenu()
        {
            Console.Clear();
            Console.WriteLine("1. View User");
            Console.WriteLine("2. Update User");
            Console.WriteLine("3. Remove User");
            Console.WriteLine("4. Exit");
            Console.WriteLine("");
            Console.WriteLine("WHAT DO YOU WANT TO DO?");
        }

        private static bool Continue(bool loop)
        {
            Console.WriteLine("");
            Console.WriteLine("Do you want to continue ?? Type 1 and enter to continue 0 to exit.");
            int yesORno = Convert.ToInt16(Console.ReadLine());
            if (yesORno == 0)
                loop = false;
            Console.Clear();
            return loop;
        }

        
    }
}
