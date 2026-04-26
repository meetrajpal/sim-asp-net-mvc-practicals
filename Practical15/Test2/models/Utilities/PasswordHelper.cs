using System;
using System.Security.Cryptography;
using System.Text;

namespace Test2.models.Utilities
{
    public class PasswordHelper
    {
        public static string Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool Verify(string password, string storedHash)
        {
            var hash = Hash(password);
            return hash == storedHash;
        }
    }
}