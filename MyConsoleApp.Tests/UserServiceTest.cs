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
        public void AddUser_ShouldAddUser() 
        {
            UserService userService = new UserService();
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
    }
}
