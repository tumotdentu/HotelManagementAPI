using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelManagementAPI.Utils
{
    public static class StringExtension
    {
        public static string RandomString(int length, bool useAlphabet, bool useDigit, bool useSpecialChar)
        {
            string allowedChars = string.Empty;
            if (useAlphabet) allowedChars += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            if (useDigit) allowedChars += "0123456789";
            if (useSpecialChar) allowedChars += "~!@#$%^&*()-+={}[];:\"'\\|,.?/";
            char[] result = new char[length];
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                result[i] = allowedChars[rand.Next(0, allowedChars.Length)];
            }
            return new string(result);
        }

        public static bool CheckGuid(string? id)
        {
            if (id != null)
            {
                string[] idSplit = Regex.Split(id, string.Empty);
                string validId = "0123456789abcdef-";
                foreach (string s in idSplit)
                {
                    if (s != "" && !validId.Contains(s))
                    {
                        return false;
                    }
                }
                if (id.Length != 36)
                {
                    return false;
                }
                string[] idCuts = id.Split("-");
                int[] lens = { 8, 4, 4, 4, 12 };
                for (int i = idCuts.Length - 1; i >= 0; i--)
                {
                    if (idCuts[i].Length != lens[i])
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public static string ReplaceAtFirst(string word, string valueCompare, string valueReplace )
        {
            if (word.Substring(0,1)==valueCompare){
                word = valueReplace + word.Remove(0, 1);
            }
            return word;
        }

        public static string GetSHA256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        public static bool IsSubstring(string a, string b)
        {
            // Kiểm tra nếu a hoặc b chứa chuỗi còn lại
            return a.Trim().Contains(b) || b.Trim().Contains(a);
        }
        public static string RemoveDiacriticsAndToLower(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Chuyển đổi chuỗi thành chữ thường
            input = input.ToLowerInvariant();

            // Normalize the string to FormD, which separates the characters and diacritics
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                // Get the Unicode category of the character
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                // Append characters that are not diacritics
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            // Return the normalized string to FormC, which is the composed form
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
