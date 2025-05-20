using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseController.Services
{
    public static class PasswordHelper
    {
        public static byte[] HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                Debug.WriteLine($"[PasswordHelper] Hashed password: {Convert.ToBase64String(hash)}");
                return hash;
            }
        }

        public static bool VerifyPassword(string inputPassword, byte[] storedHash)
        {
            if (string.IsNullOrWhiteSpace(inputPassword) || storedHash == null)
            {
                Debug.WriteLine("[PasswordHelper] Verification failed: empty input or stored hash");
                return false;
            }

            var inputHash = HashPassword(inputPassword);
            bool match = inputHash.SequenceEqual(storedHash);
            Debug.WriteLine($"[PasswordHelper] Verification result: {match}");
            return match;
        }

        public static string ToBase64(byte[] hash)
        {
            string result = hash != null ? Convert.ToBase64String(hash) : string.Empty;
            Debug.WriteLine($"[PasswordHelper] Hash to Base64: {result}");
            return result;
        }
    }
}
