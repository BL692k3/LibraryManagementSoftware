using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public class UserController
    {
        private readonly List<User> _users;
        private string _userFilePath;

        public UserController()
        {
            _userFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\data", "users.txt");
            _users = new List<User>();

            LoadUsersFromFile();
        }

        public void AddUser(User user)
        {
            if (_users.Count == 0)
            {
                user.Id = 1;
            }
            else
            {
                user.Id = _users.Max(u => u.Id) + 1;
            }

            _users.Add(user);
            SaveUsersToFile();
        }

        public void UpdateUser(int id, User updatedUser)
        {
            User user = _users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.Username = updatedUser.Username;
                user.Password = updatedUser.Password;
                user.Email = updatedUser.Email;

                SaveUsersToFile();
            }
        }

        public void DeleteUser(int id)
        {
            User user = _users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                _users.Remove(user);
                SaveUsersToFile();
            }
        }

        public bool Login(string username, string password)
        {
            foreach (User user in _users)
            {
                if (user.Username == username && user.Password == password)
                {
                    return true;
                }
            }

            return false;
        }

        public List<User> GetUserList()
        {
            LoadUsersFromFile();
            return _users;
        }

        public List<User> GetMemberList()
        {
            List<User> members = new List<User>();

            foreach (User user in _users)
            {
                if (user is Member)
                {
                    members.Add(user);
                }
            }

            return members;
        }

        public bool IsAdmin(User currentUser)
        {
            string isAdmin = currentUser.GetUserRole();
            return isAdmin == "ADM";
        }

        public static User GetUserById(int id)
        {
            UserController userController = new UserController();
            List<User> users = userController.GetUserList();

            User user = users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        private void LoadUsersFromFile()
        {
            _users.Clear(); // Clear the current user list before loading users from file

            if (File.Exists(_userFilePath))
            {
                using (StreamReader reader = new StreamReader(_userFilePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = line.Split(',');

                        if (fields.Length == 5)
                        {
                            int id = int.Parse(fields[0]);
                            string role = fields[1];
                            string username = fields[2];
                            string password = fields[3];
                            string email = fields[4];

                            if (role == "ADM")
                            {
                                Admin admin = new Admin(username, password, email);
                                admin.Id = id;
                                _users.Add(admin);
                            }
                            else if (role == "MEM")
                            {
                                Member member = new Member(username, password, email);
                                member.Id = id;
                                _users.Add(member);
                            }
                        }
                    }
                }
            }
        }

        private void SaveUsersToFile()
        {
            using (StreamWriter writer = new StreamWriter(_userFilePath))
            {
                foreach (User user in _users)
                {
                    int id = user.Id;
                    string role = user.GetUserRole();
                    string username = user.Username;
                    string password = user.Password;
                    string email = user.Email;

                    string line = $"{id},{role},{username},{password},{email}";

                    writer.WriteLine(line);
                }
            }
        }
    }
}