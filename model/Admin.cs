using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public class Admin : User
    {
        private string userRole = "ADM";

        public Admin(string username, string password, string email) : base(username, password, email)
        {
        }
        public Admin(int id, string username, string password, string email) : base(id, username, password, email)
        {
        }

        public bool isAdmin()
        {
            return GetUserRole() == userRole;
        }

        public override string GetUserRole()
        {
            return userRole;
        }
    }
}