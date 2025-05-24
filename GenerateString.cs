using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArraySearch
{
    internal class GenerateString
    {
        public CheckedListBox checkedListBox1 { get; set; }

        public string[] GenerateRandomString(int length, CheckedListBox.CheckedItemCollection checkedItems)
        {
            var chars = "";
            if (checkedItems.Contains("Кирилиця"))
            {
                chars += "АБВГДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгдеєжзиіїйклмнопрстуфхцчшщьюя";
            }
            if (checkedItems.Contains("Латиниця"))
            {
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            }
            if (checkedItems.Contains("Цифри"))
            {
                chars += "0123456789";
            }
            if (string.IsNullOrEmpty(chars))
            {
                return null;
            }

            var random = new Random();
            var set = new HashSet<string>();

            while (set.Count < length)
            {
                var strLength = random.Next(5, 5);
                var stringbuild = new StringBuilder();
                for (int i = 0; i < strLength; i++)
                    stringbuild.Append(chars[random.Next(chars.Length)]);
                set.Add(stringbuild.ToString());
            }

            return set.ToArray();
        }
    }
}
