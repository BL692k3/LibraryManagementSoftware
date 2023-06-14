using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public class LoginController
    {
        private User _currentUser;

        public void Login()
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            // Check if user exists and credentials match
            User user = GetUser(username, password);
            if (user != null)
            {
                _currentUser = user;
                Console.WriteLine("Login successful.");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }
        
        public User GetCurrentUser()
        {
            return _currentUser;
        }

        public void Logout()
        {
            _currentUser = null;
            Console.WriteLine("Logout successful.");
        }

        public bool IsLoggedIn()
        {
            return _currentUser != null;
        }

        private static User GetUser(string username, string password)
        {
            UserController userController = new UserController();
            List<User> users = userController.GetUserList();
            return users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
