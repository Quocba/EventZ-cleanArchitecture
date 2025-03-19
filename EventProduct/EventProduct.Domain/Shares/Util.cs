using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProduct.Infrastructure.Shares
{
    public static class Util
    {
        private static readonly Random random = new();

        public static string Generate6DigitCode()
        {
            int code = random.Next(100000, 1000000);
            return code.ToString();
        }

        public static string GenerateUniqueString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            string randomString = new string(Enumerable.Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)]).ToArray());

            string uniqueString = $"{randomString}-{Guid.NewGuid().ToString("N").Substring(4, 6)}";

            return uniqueString;
        }

    }
}
