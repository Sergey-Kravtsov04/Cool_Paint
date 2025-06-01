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
    public partial class GridStep : Form
    {
        internal int GridStep1 = 10;
        public GridStep()
        {
            InitializeComponent();
            textBox1.Text = "10";
        }

        private void GridStep_Load(object sender, EventArgs e)
        {

        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            try 
            { 
            GridStep1 = Convert.ToInt32(textBox1.Text);
            if (GridStep1 > 0)
                DialogResult = DialogResult.OK;
            else
                GridStep1 = 10;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
                DialogResult = DialogResult.Cancel;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }
    }
}
