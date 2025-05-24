using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ArraySearch
{
    internal class Hash_Search : ISearchMethod
    {
        public RichTextBox richTextBox1 { get; set; }
        public RichTextBox richTextBox2 { get; set; }
        public ProgressBar progressBar1 { get; set; }
        public int Comparisons { get; private set; }
        public int Hashing(string key)
        {
            int hash = 0;
            foreach (char c in key)
            {
                hash += c;
                hash = (hash << 5) - hash; 
            }
            return hash;
        }
        public int QuadraticProbing(int hash, int i, int size)
        {
            return (hash + i * i) % size;
        }
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
            var checkedEl = new HashSet<int>();
            int size = originalArray.Length * 2 + 1;
            var hashTable = new string[size];
            //  Insert
            for (int i = 0; i < originalArray.Length; i++)
            {
                string val = originalArray[i];
                int raw = Hashing(val) & 0x7FFFFFFF;
                int hash0 = raw % size;

                for (int j = 0; j < size; j++)
                {
                    int bucket = QuadraticProbing(hash0, j, size);
                    if (hashTable[bucket] == null)
                    {
                        hashTable[bucket] = val;
                        break;
                    }
                }
            }
            var sw = Stopwatch.StartNew();
            // Search
            int foundBucket = -1;
            {
                int raw = Hashing(key) & 0x7FFFFFFF;
                int hash0 = raw % size;

                for (int j = 0; j < size; j++)
                {
                    int bucket = QuadraticProbing(hash0, j, size);
                    Comparisons++;
                    checkedEl.Add(bucket);
                    progressBar1.Value = Math.Min(progressBar1.Maximum, j + 1);

                    if (hashTable[bucket] == null)
                        break;

                    if (hashTable[bucket] == key)
                    {
                        foundBucket = bucket;
                        break;
                    }
                }
            }
            sw.Stop();
            
            if (foundBucket != -1)
            {
                int index = Array.IndexOf(originalArray, key);
                if (index >= 0)
                {
                    int pos = 0;
                    for (int i = 0; i < index; i++)
                    {
                        pos += originalArray[i].Length + 1;
                    }
                    richTextBox2.Select(pos, key.Length);
                    richTextBox2.SelectionBackColor = Color.Lime;
                    found = true;
                }
            }
            if (!found)
            {
                MessageBox.Show("Елемент не знайдено", "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            string resultText = ($"Складність: O(1)\n" + $"Час виконання: {sw.Elapsed.TotalMilliseconds:F4} ms\n" + $"Порівнянь: {Comparisons}");
            richTextBox1.Text = resultText;
            progressBar1.Visible = false;
            richTextBox1.Visible = true;

            return foundBucket;
        }
    }
}
