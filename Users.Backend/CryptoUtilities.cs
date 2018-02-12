using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Users.Backend
{
    public static class CryptoUtilities
    {
        public static byte[] CreateHashWithSalt(string password)
        {
            byte[] hashedBytes;
            // SHA256 is disposable by inheritance.
            using (var sha256 = SHA256.Create()) {
                // Send a sample text to hash.
                hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{password}{GetSalt()}"));
            }

            return hashedBytes;
        }

        private static string GetSalt() {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create()) {
                keyGenerator.GetBytes(bytes);
 
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
