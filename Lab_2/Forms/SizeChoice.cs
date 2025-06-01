using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    public partial class SizeChoice : Form
    {
        public int Width1;
        public int Height1;
        public SizeChoice()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox1.Text = "Введите ширину";
            textBox2.Text = "Введите высоту";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Width1 = 320;
                Height1 = 240;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                Width1 = 640;
                Height1 = 480;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
               Width1 = 800;
               Height1 = 600;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Width1 = Convert.ToInt32(textBox1.Text);
            }
            catch(Exception)
            { 

            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Height1 = Convert.ToInt32(textBox2.Text);
            }
            catch(Exception)
            { 

            }

        }


private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox1.Text = "Введите ширину";
                textBox2.Text = "Введите высоту";
            }
        }

        private void SizeChoice_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
