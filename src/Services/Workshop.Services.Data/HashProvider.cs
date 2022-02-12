﻿namespace Workshop.Services.Data
{
    public class HashProvider : IHashProvider
    {
        public string Hash(string first, string second)
        {
            if (first == second)
            {
                return this.GetHashString(first + second);
            }

            return this.GetHashString(this.ConcatTwoStrings(first.ToCharArray(), second.ToCharArray()));
        }

        private string ConcatTwoStrings(char[] first, char[] second)
        {
            string result = string.Empty;
            for (int i = 0; i < first.Length; i++)
            {
                if ((int)first[i] == (int)second[i])
                {
                    continue;
                }

                if ((int)first[i] < (int)second[i])
                {
                    result = new string(first) + new string(second);
                }
                else
                {
                    result = new string(second) + new string(first);
                }

                break;
            }

            return result;
        }

        private string GetHashString(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));

                return hashedInputStringBuilder.ToString();
            }
        }
    }
}