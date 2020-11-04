using System;
using System.Linq;

namespace SallyPAA.Helpers
{
    public static class RandomString
    {
        private static Random _random = new Random();
        public static string GenerateRandomString(int length, bool startWithDigit = false)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());

            if (startWithDigit)
            {
                if (char.IsDigit(randomString[0])) return randomString;
                return GenerateRandomString(length, true);
            }
            else
            {
                if (char.IsDigit(randomString[0]))
                    return GenerateRandomString(length);
            }

            return randomString;
        }
    }
}
