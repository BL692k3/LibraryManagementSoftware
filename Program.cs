using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    class Program
    {
        static void Main(string[] args)
        {
            // Display the menu and read user input
            LoginController loginController = new LoginController();
            UserController userController = new UserController();
            Login login = new Login(loginController);
            BookCRUD bookCrud = new BookCRUD();
            MemberCRUD memberCRUD = new MemberCRUD();
            BorrowMenu borrowMenu = new BorrowMenu();
            Menu menu = new Menu();
            int choice = 0;
            login.ShowLoginPrompt();
            User currentUser = loginController.GetCurrentUser();
            if (userController.IsAdmin(loginController.GetCurrentUser()))
            {
                while (choice != 4)
                {
                    Console.Clear(); // Clear the console screen
                    menu.Display(currentUser);
                    choice = menu.GetInput();

                    switch (choice)
                    {
                        case 1:
                            // Code to show all books
                            Console.Clear(); // Clear the console screen
                            bookCrud.BookCrud(currentUser);
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadLine();
                            break;
                        case 2:
                            // Code to show all members
                            Console.Clear(); // Clear the console screen
                            memberCRUD.MemberCrud();
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadLine();
                            break;
                        case 3:
                            // Code to show all borrowed books
                            borrowMenu.borrowMenu(currentUser);
                            Console.WriteLine("\nPress any key to continue...");
                            break;
                        case 4:
                            Console.WriteLine("Goodbye!");
                            loginController.Logout();
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            Console.ReadLine();
                            break;
                    }
                }
            } else
            {
                while (choice != 3)
                {
                    Console.Clear(); // Clear the console screen
                    menu.Display(currentUser);
                    choice = menu.GetInput();

                    switch (choice)
                    {
                        case 1:
                            // Code to show all books
                            Console.Clear(); // Clear the console screen
                            bookCrud.BookCrud(currentUser);
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadLine();
                            break;
                        case 2:
                            // Code to show all borrowed books
                            borrowMenu.borrowMenu(currentUser);
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("Goodbye!");
                            loginController.Logout();
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            Console.ReadLine();
                            break;
                    }
                }
            }
        }
    }
}