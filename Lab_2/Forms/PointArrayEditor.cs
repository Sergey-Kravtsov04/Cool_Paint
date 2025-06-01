using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2.Forms
{
    public partial class PointArrayEditor : Form
    {
        internal String Coordinates;
        internal Control Control;
        public PointArrayEditor(string coordinates,Button control)
        {
            Control = control;
            Coordinates = coordinates;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PointArrayEditor_Load(object sender, EventArgs e)
        {
            textBox1.Text = Coordinates;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Control.Tag = textBox1.Text;
            DialogResult = DialogResult.OK;
            
        }
    }
}
