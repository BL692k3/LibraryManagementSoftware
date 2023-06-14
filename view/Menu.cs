
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public class Menu
    {
        public void Display(User currentUser)
        {
            UserController userController = new UserController();
            
            Console.WriteLine("Welcome to My Library App");
            Console.WriteLine("-------------------------");
            
            if (userController.IsAdmin(currentUser))
            {
                Console.WriteLine("1. Show all books");
                Console.WriteLine("2. Show all members");
                Console.WriteLine("3. Show all books borrowed");
                Console.WriteLine("4. Log out");
            }
            else
            {
                Console.WriteLine("1. Show all books");
                Console.WriteLine("2. Show all books borrowed");
                Console.WriteLine("3. Log out");
            }

            Console.WriteLine();
        }


        public int GetInput()
        {
            Console.Write("Enter your choice: ");
            int choice = int.Parse(s: Console.ReadLine());
            Console.WriteLine();
            return choice;
        }
    }
}