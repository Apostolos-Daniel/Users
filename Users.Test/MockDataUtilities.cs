using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Users.Test
{
    public static class MockDataUtilities
    {
        public static string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ!.?";
            Random oRandom = new Random();
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[oRandom.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }

        public static string RandomValidPassword(int length)
        {
            var password = RandomString(length);
            while (!Regex.IsMatch(password, $"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{{{length},}}$"))
            {
                password = RandomString(length);
            }

            return password;
        }

        public static string RandomInvalidPassword(int length)
        {
            var password = RandomString(length);
            while (Regex.IsMatch(password, $"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{{{length},}}$"))
            {
                password = RandomString(length);
            }

            return password;
        }
    }
}
