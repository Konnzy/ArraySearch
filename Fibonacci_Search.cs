using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ArraySearch
{
    internal class Fibonacci_Search : ISearchMethod
    {
        public RichTextBox richTextBox1 { get; set; }
        public RichTextBox richTextBox2 { get; set; }
        public ProgressBar progressBar1 { get; set; }
        public int Comparisons { get; private set; }
        public int Search(string[] sortedArray, string[] originalArray, string key)
        {

            richTextBox2.SelectAll();
            richTextBox2.SelectionBackColor = Color.Yellow;
            richTextBox2.DeselectAll();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = sortedArray.Length;
            progressBar1.Value = 0;

            Comparisons = 0;
            var found = false;
            int foundIndex = -1;
            var sw = Stopwatch.StartNew();
            var n = sortedArray.Length;

            var fibM2 = 0; 
            var fibM1 = 1; 
            var fibM = fibM2 + fibM1; 
            while (fibM < n)
            {
                fibM2 = fibM1;
                fibM1 = fibM;
                fibM = fibM2 + fibM1;
            }
            var offset = -1;
            var checkedEl = new HashSet<int>();

            while (fibM > 1)
            {
                int i = Math.Min(offset + fibM2, n - 1);


                int cmp = string.Compare(sortedArray[i], key,StringComparison.Ordinal);
                Comparisons++;
                checkedEl.Add(i);

                if (cmp == 0)
                {
                    foundIndex = i;
                    break;
                }
                else if (cmp < 0)
                {
                    fibM = fibM2;
                    fibM1 = fibM1 - fibM2;
                    fibM2 = fibM - fibM1;
                    offset = i;
                }
                else
                {
                    fibM = fibM1;
                    fibM1 = fibM2;
                    fibM2 = fibM - fibM1;
                }
            }

            if (fibM1 == 1 && offset + 1 < n && string.Compare(sortedArray[offset + 1], key,StringComparison.Ordinal) == 0)
            {
                foundIndex = offset + 1;
                Comparisons++;
                checkedEl.Add(offset+1);
            }


                int pos = 0;
                for (int i = 0; i < originalArray.Length; i++)
                {
                    int sortedIndex = Array.IndexOf(sortedArray, originalArray[i]);
                    if(sortedIndex >=0 && checkedEl.Contains(sortedIndex))
                    {
                        richTextBox2.Select(pos, originalArray[i].Length);
                        richTextBox2.SelectionBackColor = Color.Cyan;
                    }
                    if (originalArray[i].Equals(key,StringComparison.Ordinal))
                    {
                        richTextBox2.Select(pos, key.Length);
                        richTextBox2.SelectionBackColor = Color.Lime;
                        found = true;
                    }
                    pos += originalArray[i].Length + 1;
                }
            sw.Stop();
            string resultText = ($"Складність: O(log n)\n" + $"Час виконання: {sw.Elapsed.TotalMilliseconds:F1} ms\n" + $"Порівнянь: {Comparisons}");
            richTextBox1.Text = resultText;
            progressBar1.Visible = false;
            richTextBox1.Visible = true;
            if (!found)
            {
                MessageBox.Show(("Ключ не знайдено"), "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return foundIndex;
        }
    }
}
