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
    public partial class FigureChoice : Form
    {
        internal string[] FigureTypes = { "Прямоугольник", "Эллипс", "Прямая линия", "Кривая линия", "Текст"};
        internal List<Figure> FigList;
        internal Form2 ActiveChild;
        readonly List<Point> PointArray = new List<Point>();
        internal Font Font = new Font("Times New Roman", 12);
        internal FigureChoice(List<Figure> figList,Form2 activeChild)
        {
            ActiveChild = activeChild;
            FigList = figList;
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FigureChoice_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < FigureTypes.Length; i++)
            {
                comboBox1.Items.Add(FigureTypes[i]);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "Прямоугольник":
                    FigList.Add(new OurRectangle(new Point(0, 0), new Point(0, 0), Color.Black, Color.White, false, 1));
                    break;
                case "Эллипс":
                    FigList.Add(new OurEllipse(new Point(0, 0), new Point(0, 0), Color.Black, Color.White, false, 1));
                    break;
                case "Прямая линия":
                    FigList.Add(new OurLine(new Point(0, 0), new Point(0, 0), Color.Black, Color.White, false, 1));
                    break;
                case "Кривая линия":
                    FigList.Add(new OurCurve(new Point(0, 0), new Point(0, 0), Color.Black, Color.White, false, 1, PointArray));
                    break;
                case "Текст":
                    FigList.Add(new OurText(new Point(0, 0), new Point(0, 0), Color.Black, Color.White, false, 1, Font ,ActiveChild));
                    break;
            }
            DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
