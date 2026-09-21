using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp.Core.Models
{
    public class User

    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Role { get; set; }

        public User() { }
        public User(int id,string name,string email, string password,UserType role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;

        }

    }


    public enum UserType 
    {
        User,
        Admin
    }
}
