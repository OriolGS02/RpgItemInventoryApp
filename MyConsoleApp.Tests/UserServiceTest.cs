using MyConsoleApp.Core.Models;
using MyConsoleApp.Core.Helpers;
using MyConsoleApp.Core.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp.Tests
{
    [TestFixture]
    internal class UserServiceTest
    {
        [Test]
        public void TestAddUser_ShouldAddUser() 
        {
            UserService userService = new UserService("null.json");
            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role=UserType.User,

            };

            userService.AddUser(user);

            Assert.That(userService.GetUserById(user.Id),Is.Not.Null);
        }

        [Test]
        public void TestDeleteUser_ShouldDeleteUser()
        {
            UserService userService = new UserService("null.json");
            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };

            userService.AddUser(user);

            userService.RemoveUser(user);

            Assert.That(userService.GetUserById(user.Id), Is.Null);
        }

        [Test]
        public void TestGetUsersByRole_User_ShouldReturnUserList() 
        {
            UserService userService = new UserService("null.json");

            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };

            User user2 = new User
            {
                Name = "Marc",
                Email = "Marc123@Gmail.com",
                Password = "Mark123",
                Role = UserType.User,

            };
            User admin = new User
            {
                Name = "Anna",
                Email = "Annamg@Gmail.com",
                Password = "annamg4321",
                Role = UserType.Admin,

            };

            userService.AddUser(user);
            userService.AddUser(user2);
            userService.AddUser(admin);

           Assert.That(userService.GetUsersByRole(UserType.User).Count, Is.EqualTo(2));

        }

        [Test]
        public void TestGetUsersByRole_Admin_ShouldReturnAdminList()
        {
            UserService userService = new UserService("null.json");

            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };

            User user2 = new User
            {
                Name = "Marc",
                Email = "Marc123@Gmail.com",
                Password = "Mark123",
                Role = UserType.User,

            };
            User admin = new User
            {
                Name = "Anna",
                Email = "Annamg@Gmail.com",
                Password = "annamg4321",
                Role = UserType.Admin,

            };

            userService.AddUser(user);
            userService.AddUser(user2);
            userService.AddUser(admin);

            Assert.That(userService.GetUsersByRole(UserType.Admin).Count, Is.EqualTo(1));
        }

        [Test]
        public void TestGetUsersByRole_ShouldReturnEmptyList()
        {
            UserService userService = new UserService("null.json");

            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };

            User user2 = new User
            {
                Name = "Marc",
                Email = "Marc123@Gmail.com",
                Password = "Mark123",
                Role = UserType.User,

            };                       

            userService.AddUser(user);
            userService.AddUser(user2);
            

            Assert.That(userService.GetUsersByRole(UserType.Admin), Is.Empty);
        }

        [Test]
        public void TestGetUserById_ShouldReturnIdItem() 
        {
            UserService userService = new UserService("null.json");
            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };
            userService.AddUser(user);
            User returnedUser = userService.GetUserById(user.Id);
            Assert.That(returnedUser, Is.SameAs(user));
        }

        [Test]
        public void TestGetUserByEmail_ShouldReturnEmailItem() 
        {
            UserService userService = new UserService("null.json");
            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };
            userService.AddUser(user);

            User returnedUser = userService.GetUserByEmail(user.Email);
            Assert.That(returnedUser, Is.SameAs(user));
        }

        [Test]
        public void TestGetUserRole_ShouldReturnUserRole()
        {
            UserService userService = new UserService("null.json");
            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };

            userService.AddUser(user);

           
            Assert.That(userService.GetUserRole(user.Id), Is.EqualTo(user.Role));
        }

        [Test]
        public void TestUserExistByEmail_ShouldReturnTrue()
        {
            UserService userService = new UserService("null.json");
            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };
            userService.AddUser(user);

            Assert.That(userService.Exist(user.Email),Is.True);

        }

        [Test]
        public void TestUserExistById_ShouldReturnTrue() 
        {
            UserService userService = new UserService("null.json");
            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };
            userService.AddUser(user);

            Assert.That(userService.Exist(user.Id), Is.True);

        }

        [Test]
        public void TestUpdateUser_ShouldUpdateUser() 
        {
            UserService userService = new UserService("null.json");
            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };

            userService.AddUser(user);

            User updateduser= new User
            {
                Id= user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = "lusigm54321",
                Role = user.Role,

            };

            userService.UpdateUser(updateduser);

            User result=userService.GetUserById(updateduser.Id);

            Assert.That(result.Password,Is.EqualTo(updateduser.Password));
                
        }

        [Test]
        public void TestPersistence_ShouldCreateFileAndLoadFile() 
        {
            UserService userService = new UserService("test_users.json");
            User user = new User
            {
                Name = "Luis",
                Email = "LuisGR@Gmail.com",
                Password = "lusigm12345",
                Role = UserType.User,

            };

            userService.AddUser(user);

            userService.SaveData();

            UserService userserviceload= new UserService("test_users.json");

            userserviceload.LoadData();
            Assert.That(userserviceload.GetUsers(),Is.Not.Empty);
        }






    }
}
