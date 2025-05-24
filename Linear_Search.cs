using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArraySearch
{
    internal class Linear_Search : ISearchMethod
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
            progressBar1.Maximum = originalArray.Length;
            progressBar1.Value = 0;

            Comparisons = 0;
            var found = false;
            var foundIndex = -1;
            var offset = 0;

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < originalArray.Length; i++)
            {
                Comparisons++;
                richTextBox2.Select(offset, originalArray[i].Length);
                richTextBox2.SelectionBackColor = Color.Cyan;
                if (originalArray[i].Equals(key, StringComparison.Ordinal))
                {
                        richTextBox2.SelectionBackColor = Color.Lime;
                    found = true;
                    foundIndex = i;
                    break;
                }
                offset += originalArray[i].Length + 1;
                progressBar1.Value = i + 1;
            }
            sw.Stop();
            string resultText;
            if(Comparisons == 1)
            {
                resultText = ($"Складність: O(1)\n" + $"Час виконання: {sw.Elapsed.TotalMilliseconds:F1} ms\n" + $"Порівнянь: {Comparisons}");
            }
            else
            {
                resultText = ($"Складність: O(n)\n" + $"Час виконання: {sw.Elapsed.TotalMilliseconds:F1} ms\n" + $"Порівнянь: {Comparisons}");
            }
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