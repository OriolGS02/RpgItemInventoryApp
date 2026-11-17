using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyConsoleApp.Core.Models;

namespace MyConsoleApp.Core.Helpers
{
    internal class PasswordHelper
    {
        private readonly PasswordHasher<User> _hasher = new();
        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyPassword(User user, string password)
        {
            return _hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password);

            
        }
    }
}
