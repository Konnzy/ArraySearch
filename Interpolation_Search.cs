using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

namespace ArraySearch
{
    internal class Interpolation_Search : ISearchMethod
    {
        public RichTextBox richTextBox1 { get; set; }
        public RichTextBox richTextBox2 { get; set; }
        public ProgressBar progressBar1 { get; set; }
        public int Comparisons { get; private set; }
        public int Search(string[] sortedArray,string[] originalArray, string key)
        {
            richTextBox2.SelectAll();
            richTextBox2.SelectionBackColor = Color.Yellow;
            richTextBox2.DeselectAll();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = sortedArray.Length;
            progressBar1.Value = 0;

            Comparisons = 0;
            var found = false;
            var checkedEl = new HashSet<int>();

            if (!int.TryParse(key, out int k))
            {
                MessageBox.Show("Інтерполяційний метод шукає лише числа.", "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                richTextBox2.SelectAll();
                richTextBox2.SelectionBackColor = Color.Yellow;
                return -1;
            }

            int[] arr;
            try {
                arr = Array.ConvertAll(sortedArray, int.Parse); 
            }
            catch
            {
                MessageBox.Show("У масиві не всі значення — цифри.", "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            int low = 0;
            int high = arr.Length - 1;
            int pos = -1;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = arr.Length;

            var sw = Stopwatch.StartNew();

            while (low <= high && k >= arr[low] && k <= arr[high])
            {
                pos = low + (int)((long)(k - arr[low]) * (high - low) / Math.Max(1, arr[high] - arr[low]));

                Comparisons++;
                checkedEl.Add(pos);
                if (arr[pos] == k)
                {
                    break;
                }
                if (arr[pos] < k)
                {
                    low = pos + 1;
                }
                else
                {
                    high = pos - 1;
                }
            }

            sw.Stop();

                int Pos = 0;
                for (int i = 0; i < originalArray.Length; i++)
                {
                    int value = int.Parse(originalArray[i]);
                    int sortedIndex = Array.IndexOf(arr, value);
                    if (sortedIndex >= 0 && checkedEl.Contains(sortedIndex))
                    {
                        richTextBox2.Select(Pos, originalArray[i].Length);
                        richTextBox2.SelectionBackColor = Color.Cyan;
                    }
                    if (value == k)
                    {
                        richTextBox2.Select(Pos, key.Length);
                        richTextBox2.SelectionBackColor = Color.Lime;
                        found = true;
                    }
                    Pos += originalArray[i].Length + 1;
                }
            if(!found)
            {
                MessageBox.Show("Елемент не знайдено", "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            string resultText = ($"Складність: O(log log n)\n" + $"Час виконання: {sw.Elapsed.TotalMilliseconds:F4} ms\n" + $"Порівнянь: {Comparisons}");
            richTextBox1.Text = resultText;
            progressBar1.Visible = false;
            richTextBox1.Visible = true;
            return pos;
        }
    }
}
