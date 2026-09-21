using MyConsoleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyConsoleApp.Core.Services
{
    public class UserService
    {
        List<User> users = new List<User>();
        //string filepath = "data.json";
        string filepath = @"C:\VisualStudioC#Projects\MyConsoleApp\Data\users.json";

        static int nextId = 0;

        public UserService() { LoadData(); }
        public UserService(string fileName)
        {
            filepath = fileName;
            LoadData();
        }
        public void AddUser(User user)
        {

            user.Id = nextId;
            users.Add(user);

            nextId++;
        }
        public List<User> GetUsers()
        {
            return users;
        }
        public List<User> GetUsersByRole(UserType role)
        {
            List<User> userlist = users.FindAll(us => us.Role == role) ;
            return userlist;
        }

        public User GetUserById(int id)
        {
            User user = users.Find(us => us.Id == id);
            return user;
        }
        public User GetUserByEmail(string email)
        {
            User user = users.FirstOrDefault(us => us.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return user;
        }

        /*expected to change for more security*/
        public string GetUserPaswordByEmail(string email) 
        {
            string password = users.FirstOrDefault(us => us.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).Password;
            return password;
        }

        public UserType GetUserRole(int id)
        {
            UserType type = users.Find(us => us.Id == id).Role;
            return type;
        }


        public void SaveData()
        {

            string jsonitem = JsonSerializer.Serialize(users);
            File.WriteAllText(filepath, jsonitem);

        }
        public void LoadData()
        {
            if (!File.Exists(filepath))
                return;
            
               

            try
            {
                string data = File.ReadAllText(filepath);
                users = JsonSerializer.Deserialize<List<User>>(data);

                nextId = users.Any() ?
                users.Max(i => i.Id) + 1
                : 0;
            }
            catch (JsonException jex)
            {
                Console.WriteLine("Something went wrong trying to read the JSON file.");
                nextId = 0;
            }



        }
    }
}
