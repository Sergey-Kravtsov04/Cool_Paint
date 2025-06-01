using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab_2
{
    public partial class LineThicknessForm : Form
    {
        private string SizeString;
        internal int LineThickness;
        internal LineThicknessForm()
        {
            InitializeComponent();
            int[] comboArray = new int[] { 1, 2, 5, 8, 10, 12, 15 };           
            for(int i = 0; i<comboArray.Length; i++)
            {
                comboBox1.Items.Add(comboArray[i]);
            }
            comboBox1.SelectedIndex = 0;
        }
        private void LineThicknessForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SizeString = comboBox1.Text;
            LineThickness = Convert.ToInt32(SizeString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
