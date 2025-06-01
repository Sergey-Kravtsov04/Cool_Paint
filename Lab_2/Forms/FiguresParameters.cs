using Lab_2.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    public partial class FiguresParameters : Form
    {

        internal List<Figure> FigList;
        internal object[] Props = new object[] { };
        Control[] FigureControls = new Control[] { };
        Form2 ActiveChild;
        internal FiguresParameters(List<Figure> figList, Form2 activeChild)
        {
            InitializeComponent();
            ActiveChild = activeChild;
            FigList = figList;
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveChild.SelectedList.Clear();
            ActiveChild.DopList.Clear();
            flowLayoutPanel1.Controls.Clear();
            if (ListViewFigures.SelectedIndices.Count > 0)
            {
                int index = ListViewFigures.SelectedItems[0].Index;
                ActiveChild.SelectedList.Add(FigList[index]);
                FigureControls = FigList[index].GetControls();
                foreach (Control c in FigureControls)
                {
                    flowLayoutPanel1.Controls.Add(c);
                }
            }

        }
        private void FiguresEditList_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < FigList.Count; i++)
            {
                var row = new string[] { Convert.ToString(i + 1), FigList[i].GetFigureType(), FigList[i].Point1.ToString() };
                var lvi = new ListViewItem(row);
                ListViewFigures.Items.Add(lvi);

            }
        }
        private void OneDown_Click(object sender, EventArgs e)
        {
            if (ListViewFigures.SelectedIndices.Count > 0)
            {
                int index = ListViewFigures.SelectedItems[0].Index;
                Figure fake = (Figure)FigList[index].Clone();
                Figure fake1 = (Figure)FigList[index + 1].Clone();
                FigList[index] = fake1;
                FigList[index + 1] = fake;
                ListViewFigures.Items.Clear();
                FiguresEditList_Load(sender, e);
            }
        }
        private void OneUp_Click(object sender, EventArgs e)
        {
            if (ListViewFigures.SelectedIndices.Count > 0)
            {
                int index = ListViewFigures.SelectedItems[0].Index;
                Figure fake = (Figure)FigList[index].Clone();
                Figure fake1 = (Figure)FigList[index - 1].Clone();
                FigList[index] = fake1;
                FigList[index - 1] = fake;
                ListViewFigures.Items.Clear();
                FiguresEditList_Load(sender, e);
            }
        }
        private void FullDown_Click(object sender, EventArgs e)
        {
            if (ListViewFigures.SelectedIndices.Count > 0)
            {
                int itterCount = FigList.Count - (ListViewFigures.SelectedItems[0].Index + 1);
                int index = ListViewFigures.SelectedItems[0].Index;
                for (int i = 0; i < itterCount; i++)
                {
                    Figure fake = (Figure)FigList[index + i].Clone();
                    Figure fake1 = (Figure)FigList[index + 1 + i].Clone();
                    FigList[index + i] = fake1;
                    FigList[index + 1 + i] = fake;

                }
                ListViewFigures.Items.Clear();
                FiguresEditList_Load(sender, e);
            }
        }
        private void FullUp_Click(object sender, EventArgs e)
        {
            if (ListViewFigures.SelectedIndices.Count > 0)
            {
                int itterCount = ListViewFigures.SelectedItems[0].Index;
                int index = ListViewFigures.SelectedItems[0].Index;
                for (int i = 0; i < itterCount; i++)
                {
                    Figure fake = (Figure)FigList[index - i].Clone();
                    Figure fake1 = (Figure)FigList[index - 1 - i].Clone();
                    FigList[index - i] = fake1;
                    FigList[index - 1 - i] = fake;
                }
                ListViewFigures.Items.Clear();
                FiguresEditList_Load(sender, e);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (ListViewFigures.SelectedItems.Count > 0)
            {
                int index = ListViewFigures.SelectedItems[0].Index;
                FigList[index].Point1.X = Convert.ToInt32(FigureControls[1].Text);
                FigList[index].Point1.Y = Convert.ToInt32(FigureControls[3].Text);
                FigList[index].Point2.X = Convert.ToInt32(FigureControls[5].Text);
                FigList[index].Point2.Y = Convert.ToInt32(FigureControls[7].Text);
                FigList[index].NotNormPoint1.X = Convert.ToInt32(FigureControls[1].Text);
                FigList[index].NotNormPoint1.Y = Convert.ToInt32(FigureControls[3].Text);
                FigList[index].NotNormPoint2.X = Convert.ToInt32(FigureControls[5].Text);
                FigList[index].NotNormPoint2.Y = Convert.ToInt32(FigureControls[7].Text);
                FigList[index].LineThickness = Convert.ToInt32(FigureControls[9].Text);
                FigList[index].LineColor = FigureControls[11].BackColor;
                if (FigureControls[13] != null)
                    FigList[index].BackgroundColor = FigureControls[13].BackColor;
                if (FigureControls[14] != null)
                {
                    FigList[index].Font = (Font)FigureControls[15].Tag;
                    FigList[index].InnerText = FigureControls[17].Text;
                }
                if (FigureControls[19] != null)
                {
                    int itterCount = FigureControls[19].Tag.ToString().Split(',').Count()-1;
                    if (FigList[index].NotNormPointArray.Count != itterCount)
                    {
                        FigList[index].NotNormPointArray.Clear();
                        FigList[index].FakeNotNormPointArray.Clear();
                        for (int i = 0; i < itterCount; i++)
                        {
                            //MessageBox.Show(Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i])+" "+ Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i + 1]));
                            FigList[index].NotNormPointArray.Add(new Point(Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i]), Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i + 1])));
                            FigList[index].FakeNotNormPointArray.Add(new Point(Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i]), Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i + 1])));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < itterCount; i++)
                        {
                            FigList[index].NotNormPointArray[i] = new Point(Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i]), Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i + 1]));
                            FigList[index].FakeNotNormPointArray[i] = new Point(Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i]), Convert.ToInt32(FigureControls[19].Tag.ToString().Split(';', ',')[i + i + 1]));
                        }
                    }
                    FigList[index].Point1.X = FigList[index].GetMinPoint(FigList[index].NotNormPointArray).X;
                    FigList[index].Point1.Y = FigList[index].GetMinPoint(FigList[index].NotNormPointArray).Y;
                    FigList[index].Point2.X = FigList[index].GetMaxPoint(FigList[index].NotNormPointArray).X;
                    FigList[index].Point2.Y = FigList[index].GetMaxPoint(FigList[index].NotNormPointArray).Y;
                    FigList[index].NotNormPoint1.X = FigList[index].GetMinPoint(FigList[index].NotNormPointArray).X;
                    FigList[index].NotNormPoint1.Y = FigList[index].GetMinPoint(FigList[index].NotNormPointArray).Y;
                    FigList[index].NotNormPoint2.X = FigList[index].GetMaxPoint(FigList[index].NotNormPointArray).X;
                    FigList[index].NotNormPoint2.Y = FigList[index].GetMaxPoint(FigList[index].NotNormPointArray).Y;
                }

            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (ListViewFigures.SelectedIndices.Count > 0)
            {
                int index = ListViewFigures.SelectedItems[0].Index;
                ActiveChild.SelectedList.Clear();
                ActiveChild.DopList.Clear();
                FigList.Remove(FigList[index]);
            }
            ListViewFigures.Items.Clear();
            FiguresEditList_Load(sender, e);
        }

        private void Create_Click(object sender, EventArgs e)
        {
            FigureChoice forma = new FigureChoice(FigList, ActiveChild);
            DialogResult result = forma.ShowDialog();
            ListViewFigures.Items.Clear();
            FiguresEditList_Load(sender, e);
        }
    }
}
