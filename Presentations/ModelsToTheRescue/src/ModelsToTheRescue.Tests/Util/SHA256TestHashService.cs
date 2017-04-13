using System;
using System.Security.Cryptography;
using System.Text;
using ModelsToTheRescue.Refactored;

namespace ModelsToTheRescue.Tests
{
    public class SHA256TestHashService : IPasswordHashService
    {
        public string GetPasswordHash(string password)
        {
            using (var hash = new SHA256Managed())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashedPassword = hash.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedPassword);
            }
        }
    }
}
