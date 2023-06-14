using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    internal class MemberCRUD
    {
        internal void MemberCrud()
        {
            Menu menu = new Menu();
            UserController userController = new UserController();
            int choice = 0;

            while (choice != 4)
            {
                Display();
                choice = menu.GetInput();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter username:");
                        string username = Console.ReadLine();

                        Console.WriteLine("Enter password:");
                        string password = Console.ReadLine();

                        Console.WriteLine("Enter email:");
                        string email = Console.ReadLine();

                        // Check if username already exists
                        if (userController.GetUserList().Any(u => u.Username == username))
                        {
                            Console.WriteLine("Username already exists. Please try again.");
                            break;
                        }

                        // Validate email format
                        if (!IsValidEmail(email))
                        {
                            Console.WriteLine("Invalid email. Please try again.");
                            break;
                        }

                        // Create new member
                        Member member = new Member(username, password, email);
                        userController.AddUser(member);
                        Console.WriteLine("Member added successfully.");
                        Console.Clear(); // Clear the console screen
                        break;
                    case 2:
                        Console.WriteLine("Enter ID of the user to update:");
                        int idToUpdate = int.Parse(Console.ReadLine());

                        User userToUpdate = userController.GetUserList().FirstOrDefault(u => u.Id == idToUpdate);

                        // Check if user exists
                        if (userToUpdate == null)
                        {
                            Console.WriteLine("User not found. Please try again.");
                            break;
                        }

                        Console.WriteLine($"Updating user with ID {idToUpdate}.");
                        Console.WriteLine("Enter new username:");
                        string updatedUsername = Console.ReadLine();

                        Console.WriteLine("Enter new password:");
                        string updatedPassword = Console.ReadLine();

                        Console.WriteLine("Enter new email:");
                        string updatedEmail = Console.ReadLine();

                        // Check if username already exists
                        if (userController.GetUserList().Any(u => u.Username == updatedUsername && u.Id != idToUpdate))
                        {
                            Console.WriteLine("Username already exists. Please try again.");
                            break;
                        }

                        // Validate email format
                        if (!IsValidEmail(updatedEmail))
                        {
                            Console.WriteLine("Invalid email. Please try again.");
                            break;
                        }

                        // Update user
                        User updatedUser = new Member(updatedUsername, updatedPassword, updatedEmail);
                        userController.UpdateUser(idToUpdate, updatedUser);
                        Console.WriteLine("User updated successfully.");
                        Console.Clear(); // Clear the console screen
                        break;
                    case 3:
                        Console.WriteLine("Enter ID of the user to delete:");
                        int idToDelete = int.Parse(Console.ReadLine());

                        User userToDelete = userController.GetUserList().FirstOrDefault(u => u.Id == idToDelete);

                        // Check if user exists
                        if (userToDelete == null)
                        {
                            Console.WriteLine("User not found. Please try again.");
                            break;
                        }

                        Console.WriteLine($"Are you sure you want to delete user with ID {idToDelete}? (Y/N)");
                        string confirmation = Console.ReadLine();

                        if (confirmation.ToUpper() == "Y")
                        {
                            userController.DeleteUser(idToDelete);
                            Console.WriteLine("User deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Deletion cancelled.");
                        }
                        Console.Clear(); // Clear the console screen
                        break;
                    case 4:
                        Console.WriteLine("Going back to main Menu.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void Display()
        {
            UserController userController = new UserController();
            List<User> users = userController.GetUserList(); 
            foreach (User user in users)
            {
                Console.WriteLine($"Id({user.Id}): Username: {user.Username}. Password:{user.Password}. Email: {user.Email}. User's role: {user.GetUserRole()}");
            }
            Console.WriteLine("-------------------------");
            Console.WriteLine("1. Add a user");
            Console.WriteLine("2. Update a user");
            Console.WriteLine("3. Delete a user");
            Console.WriteLine("4. Quit");
            Console.WriteLine();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}