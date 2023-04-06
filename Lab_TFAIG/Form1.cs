using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_TFAIG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string Str = Convert.ToString(textBox1.Text);
            Checker checker = new Checker(/*Str, textBox1, listBox1, listBox2, textBox2*/);
            checker.check(Str, textBox1, listBox1, listBox2, textBox2);
            if (Str[Str.Length-1] != ';')
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Нет символа конца цепочки");
            }
        }
    }
}
