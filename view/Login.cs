using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LibraryManagementSoftware
{
    public class Login
    {
        private LoginController _loginController;

        public Login(LoginController loginController)
        {
            _loginController = loginController;
        }

        public void ShowLoginPrompt()
        {
            while (!_loginController.IsLoggedIn())
            {
                _loginController.Login();

                if (!_loginController.IsLoggedIn())
                {
                    Console.WriteLine("Invalid username or password. Please try again.");
                }
            }
        }
    }
}