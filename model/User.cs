using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public abstract class User
    {
        private int _id;
        private string _username;
        private string _password;
        private string _email;

        public User(string username, string password, string email)
        {
            _username = username;
            _password = password;
            _email = email;
        }
        public User(int id, string username, string password, string email)
        {
            _id = id;
            _username = username;
            _password = password;
            _email = email;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public abstract string GetUserRole();
        public override string ToString()
        {
            return $"{GetUserRole()}";
        }
    }
}