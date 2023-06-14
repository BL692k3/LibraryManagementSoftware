using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public class Member : User
    {
        private string userRole = "MEM";

        public Member(string username, string password, string email) : base(username, password, email)
        {
        }

        public Member(int id, string username, string password, string email) : base(id, username, password, email)
        {
        }

        public override string GetUserRole()
        {
            return userRole;
        }
    }
}