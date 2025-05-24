using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArraySearch
{
    public partial class MainForm : Form
    {
        private string[] originalArray;
        private string[] sortedArray;
        private SaveFileDialog sfd;
        public MainForm()
        {
            InitializeComponent();
            checkedListBox1.Items.Add("Кирилиця");
            checkedListBox1.Items.Add("Латиниця");
            checkedListBox1.Items.Add("Цифри");
            comboBox1.Items.Add("Послідовний метод");
            comboBox1.Items.Add("Метод Фібоначчі");
            comboBox1.Items.Add("Інтерполяційний метод");
            comboBox1.Items.Add("Метод хеш-функції");
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
                numericUpDown1.Minimum = 100;
                numericUpDown1.Maximum = 5000;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Оберіть хоча б один варіант налаштування масиву","Array Search",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            richTextBox1.Visible = false;

            var length = (int)numericUpDown1.Value;
            originalArray = new string[length];

            var GenString = new GenerateString();
            var gen = new GenerateString { checkedListBox1 = checkedListBox1 };

            originalArray = gen.GenerateRandomString(length, checkedListBox1.CheckedItems);
            richTextBox2.Text = string.Join(" ", originalArray);

            sortedArray = (string[])originalArray.Clone();
            Array.Sort(sortedArray, StringComparer.OrdinalIgnoreCase);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            richTextBox1.Visible = false;
            if (originalArray == null || originalArray.Length == 0)
            {
                MessageBox.Show("Спочатку згенеруйте масив", "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            originalArray = null;
            richTextBox2.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            richTextBox1.Visible = false;
            if (originalArray == null||originalArray.Length == 0)
            {
                MessageBox.Show("Спочатку згенеруйте масив", "Array Search",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Інтерполяційний метод")
            {
                if (checkedListBox1.CheckedItems.Count != 1 || !checkedListBox1.CheckedItems.Contains("Цифри"))
                {
                    MessageBox.Show("Інтерполяційний метод підтримує лише чисельний тип значень. Оберіть тільки 'Цифри'.", "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    richTextBox2.SelectAll();
                    richTextBox2.SelectionBackColor = Color.Yellow;
                    return;
                }
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Спочатку введіть ключ для пошуку", "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Послідовний метод")
              {
               var linearSearch = new Linear_Search();
               linearSearch.richTextBox1 = richTextBox1;
               linearSearch.richTextBox2 = richTextBox2;
               linearSearch.progressBar1 = progressBar1;
               linearSearch.Search(sortedArray,originalArray, textBox1.Text);
               }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Метод Фібоначчі")
            {
                var fibonacciSearch = new Fibonacci_Search();
                fibonacciSearch.richTextBox1 = richTextBox1;
                fibonacciSearch.richTextBox2 = richTextBox2;
                fibonacciSearch.progressBar1 = progressBar1;
                fibonacciSearch.Search(sortedArray,originalArray, textBox1.Text);
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Інтерполяційний метод")
            {
                var interpolationSearch = new Interpolation_Search();
                interpolationSearch.richTextBox1 = richTextBox1;
                interpolationSearch.richTextBox2 = richTextBox2;
                interpolationSearch.progressBar1 = progressBar1;
                interpolationSearch.Search(sortedArray,originalArray, textBox1.Text);
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Метод хеш-функції")
            {
                var hashSearch = new Hash_Search();
                hashSearch.richTextBox1 = richTextBox1;
                hashSearch.richTextBox2 = richTextBox2;
                hashSearch.progressBar1 = progressBar1;
                hashSearch.Search(sortedArray, originalArray, textBox1.Text);
            }
             else if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Спочатку оберіть спосіб пошуку", "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            sfd = new SaveFileDialog();
            sfd.Filter = "Text files (*.rtf)|*.rtf|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.Title = "Save a Text File";
            if (originalArray == null)
            {
                DialogResult choice = MessageBox.Show("Ви дійсно хочете зберегти порожній файл?", "Array Search", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (choice == DialogResult.No)
                {
                    return;
                }
                richTextBox2.Clear();
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filePath = sfd.FileName;
                if (System.IO.File.Exists(filePath))
                {
                        MessageBox.Show("Файл успішно змінено", "Array Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                var results = new RichTextBox();

                results.Rtf = richTextBox1.Rtf;
                results.AppendText(Environment.NewLine + Environment.NewLine);

                results.Select(results.TextLength, 0);           
                results.SelectedRtf = richTextBox2.Rtf;     

                if (System.IO.Path.GetExtension(sfd.FileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    System.IO.File.WriteAllText(sfd.FileName, results.Text, Encoding.UTF8);
                else
                    results.SaveFile(sfd.FileName, RichTextBoxStreamType.RichText);
            }
        }
    }
}