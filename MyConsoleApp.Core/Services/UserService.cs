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
        string dataPath = @"..\..\..\..\Data\";
        string fileName = "users.json";
        string fullPath;
        static int nextId = 0;

        public UserService()
        {
            fullPath = Path.Combine(dataPath, fileName);
            LoadData();
        }
        public UserService(string file_Name)
        {
            fileName = file_Name;

            fullPath = Path.Combine(dataPath, file_Name);
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

        public bool Exist(string email)
        {

            return GetUserByEmail(email) != null;
        }
        public bool Exist(int id)
        {
            return GetUserById(id) != null;
        }


        public void RemoveUser(User user)
        {
            users.Remove(user);
        }

        public void UpdateUser(User updateduser) 
        {
            User user=GetUserById(updateduser.Id);
            user.Email=updateduser.Email;
            user.Name=updateduser.Name; 
            user.Password=updateduser.Password;
            user.Role=updateduser.Role;
                       
        }

        public int GetUserIndex(User user)
        {

            return users.IndexOf(user);
        }        


        public void SaveData()
        {

            string jsonusers = JsonSerializer.Serialize(users);
            File.WriteAllText(fullPath, jsonusers);

        }
        public void LoadData()
        {
            if (!File.Exists(fullPath))
                return;
            
               

            try
            {
                string data = File.ReadAllText(fullPath);
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

        public int GetUserSize()
        {
            return users.Count;
        }

        public int GetListSize(List<User> items)
        {
            return items.Count;
        }

        public User? UserLogin(string email, string password) 
        {  

            User user = GetUserByEmail(email);
            if(user==null) return null;

            if (password != user.Password) return null;

            return user;
        }
    }
}
