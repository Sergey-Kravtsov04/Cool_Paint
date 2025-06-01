using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Lab_2
{
    public partial class Form1 : Form
    {
        internal Font PFont = new Font("Times New Roman", 12);
        internal Color PLineColor = Color.Black, PBackgroundColor = Color.White;
        internal bool PBackgroundFill = false, CanPaste = false, IsCut = false;
        internal int PLineThickness = 1, FormWidth = 1000, FormHeight = 1000, FormWidthDialog = 800, FormHeightDialog = 600, GridStep = 10;
        internal string[] FigureTypes = { "Прямоугольник", "Эллипс", "Прямая линия", "Кривая линия", "Текст", "Выделить" };
        internal string PCurrentFigure;
        internal Form2 ActiveChild = null;
        internal IState Pstate = new DrawingState();

        internal Form1()
        {
            InitializeComponent();
            DrawItemRefresh();
            PCurrentFigure = FigureTypes[0];
            прямоугольникToolStripMenuItem.Checked = true;
            RectangleButton.Checked = true;
        }
        private void Form1_Load(object sender, EventArgs e) { }
        //Меню(Создание)
        internal void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form CreateForm = new Form2(FormWidthDialog, FormHeightDialog)
            {
                Size = new Size(FormWidthDialog, FormHeightDialog),
                Text = "Рисунок " + Convert.ToString(MdiChildren.Length + 1),
                MdiParent = this,
                state = Pstate
            };
            CreateForm.Show();
            сохранитьToolStripMenuItem.Enabled = true;
            сохранитьКакToolStripMenuItem.Enabled = true;
            DrawItemRefresh();
        }
        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 OpenForm = new Form2(FormWidth, FormHeight) { MdiParent = this };
            OpenForm.Show();
            OpenForm.Open();
            сохранитьToolStripMenuItem.Enabled = true;
            сохранитьКакToolStripMenuItem.Enabled = true;
            DrawItemRefresh();
        }
        public void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveChild.Save();
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveChild.SaveAs();
        }
        //Меню(параметры)
        private void цветФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog choiceColor = new ColorDialog();
            DialogResult result = choiceColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (ActiveChild.state.GetType().ToString() != "Lab_2.EditingState")
                {
                PBackgroundColor = choiceColor.Color;
                DrawItemRefresh();
                }
                else if (ActiveChild.state.GetType().ToString() == "Lab_2.EditingState")
                {
                    ActiveChild.SelectedList[0].BackgroundColor = choiceColor.Color;
                    ActiveChild.Invalidate();
                }
            }
        }
        private void толщинаЛинииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineThicknessForm choiceThickness = new LineThicknessForm();
            DialogResult result = choiceThickness.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                if (ActiveChild.state.GetType().ToString() != "Lab_2.EditingState")
                {
                    PLineThickness = choiceThickness.LineThickness;
                    DrawItemRefresh();
                }
                else if (ActiveChild.state.GetType().ToString() == "Lab_2.EditingState")
                {
                    ActiveChild.SelectedList[0].LineThickness = choiceThickness.LineThickness;
                    ActiveChild.Invalidate();
                }
            }
        }
        private void цветЛинииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog choiceColor = new ColorDialog();
            DialogResult result = choiceColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (ActiveChild.state.GetType().ToString() != "Lab_2.EditingState")
                {
                    PLineColor = choiceColor.Color;
                    DrawItemRefresh();
                }
                else if (ActiveChild.state.GetType().ToString() == "Lab_2.EditingState")
                {
                    ActiveChild.SelectedList[0].LineColor = choiceColor.Color;
                    ActiveChild.Invalidate();
                }
            }
        }
        private void заливкаФигурыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveChild.state.GetType().ToString() != "Lab_2.EditingState")
            {
                switch (PBackgroundFill)
                {
                    case false:
                        FillFlagButton.Checked = true;
                        PBackgroundFill = true;
                        заливкаФигурыToolStripMenuItem.Checked = true;
                        break;
                    default:
                        FillFlagButton.Checked = false;
                        PBackgroundFill = false;
                        заливкаФигурыToolStripMenuItem.Checked = false;
                        break;
                }
            }
            else if (ActiveChild.state.GetType().ToString() == "Lab_2.EditingState")
            {
                switch (ActiveChild.SelectedList[0].BackgroundFill)
                {
                    case false:
                        ActiveChild.SelectedList[0].BackgroundFill = true;
                        break;
                    default:
                        ActiveChild.SelectedList[0].BackgroundFill = false;
                        break;
                }
            }
        }
        private void размерОкнаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SizeChoice sizeChoice = new SizeChoice();
            DialogResult result = sizeChoice.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                FormWidthDialog = sizeChoice.Width1;
                FormHeightDialog = sizeChoice.Height1;
            }
        }
        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            DialogResult result = fontDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (ActiveChild.state.GetType().ToString() != "Lab_2.EditingState")
                { 
                    PFont = fontDialog.Font;
                    DrawItemRefresh();
                }
                if (ActiveChild.state.GetType().ToString() == "Lab_2.EditingState")
                {
                    ActiveChild.SelectedList[0].Font = fontDialog.Font;
                }

            }
        }
        //Меню(Фигуры)
        private void прямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PCurrentFigure = FigureTypes[0];
            Switcher(PCurrentFigure);

        }
        private void эллипсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PCurrentFigure = FigureTypes[1];
            Switcher(PCurrentFigure);
        }
        private void прямаяЛинияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PCurrentFigure = FigureTypes[2];
            Switcher(PCurrentFigure);
        }
        private void криваяЛинияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PCurrentFigure = FigureTypes[3];
            Switcher(PCurrentFigure);
        }
        private void текстToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PCurrentFigure = FigureTypes[4];
            Switcher(PCurrentFigure);
        }
        //Меню(выделение)
        private void выделитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PCurrentFigure = FigureTypes[5];
            Pstate = ActiveChild.state = new SelectingState();
            Switcher(PCurrentFigure);
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveChild.Delete();
            ActiveChild.Invalidate();
        }
        //Меню(правка)
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveChild.Copy();
            ActiveChild.Invalidate();
            CanPaste = true;
        }
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveChild.Paste();
            ActiveChild.Invalidate();
            if (IsCut)
            {
                IsCut = false;
                CanPaste = false;
            }
        }
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveChild.Cut();
            ActiveChild.Invalidate();
            IsCut = true;
            CanPaste = true;
        }
        private void копироватьМетафайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveChild.MetaFileCopy();
            ActiveChild.Invalidate();
        }
        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveChild.SelectAll();
            ActiveChild.Invalidate();
        }
        //Меню(Атрибуты)
        private void сеткаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (сеткаToolStripMenuItem.Checked == true)
            {
                сеткаToolStripMenuItem.Checked = false;
                ActiveChild.GridList.Clear();
                ActiveChild.Invalidate();
            }
            else
            {
                сеткаToolStripMenuItem.Checked = true;
                ActiveChild.PaintGrid(GridStep);
                ActiveChild.Invalidate();

            }
        }
        private void шагСеткиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridStep myDialog = new GridStep();
            DialogResult result = myDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                GridStep = myDialog.GridStep1;
                if (ActiveChild.GridList.Count != 0) //Перерисовка сетки, если она включена
                {
                    ActiveChild.GridList.Clear();
                    ActiveChild.PaintGrid(GridStep);
                    ActiveChild.Invalidate();
                }
            }
        }
        private void привязкаКСеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (привязкаКСеткеToolStripMenuItem.Checked == false)
            {
                привязкаКСеткеToolStripMenuItem.Checked = true;
                ActiveChild.SnapGrid = true;
            }
            else
            {
                привязкаКСеткеToolStripMenuItem.Checked = false;
                ActiveChild.SnapGrid = false;
            }
        }
        private void выровнятьПоСеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveChild.AlignGrid(GridStep);
            ActiveChild.Invalidate();
        }
        private void FileToolStripMenuItem_Click(object sender, EventArgs e) { }
        //Нижняя панель
        private void statusBar1_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
        {
            Thickness.Text = $"Толщина Линии: {PLineThickness}";
            Graphics g = statusBar1.CreateGraphics();
            Point LineColorP1 = new Point(LineColor.Width, 0);
            Point LineColorP2 = new Point(LineColorP1.X + 100, 20);
            Rectangle LineColorRectangle = Rectangle.FromLTRB(LineColorP1.X, LineColorP1.Y, LineColorP2.X, LineColorP2.Y);
            g.FillRectangle(new SolidBrush(PLineColor), LineColorRectangle);
            Point BackColorP1 = new Point(LineColorP2.X, 0);
            Point BackColorP2 = new Point(BackColorP1.X + 100, 20);
            Rectangle BackColorRectangle = Rectangle.FromLTRB(BackColorP1.X, BackColorP1.Y, BackColorP2.X, BackColorP2.Y);
            g.FillRectangle(new SolidBrush(PBackgroundColor), BackColorRectangle);
            //
            TextFont.Text = Convert.ToString(PFont.FontFamily.Name);
            FontSize.Text = Convert.ToString(PFont.Size);
            if (ActiveChild != null)
                FormSize.Text = $"{ActiveChild.FormWidth}:{ActiveChild.FormHeight}";

        }
        public void MouseCoordinateStatus(Point m)
        {
            MousePos.Text = $"X:{m.X}Y:{m.Y}";
        }
        private void DrawItemRefresh()
        {
            statusBar1.Refresh();
        }
        //Кнопки
        private void CreateButton_Click(object sender, EventArgs e)
        {
            создатьToolStripMenuItem_Click(sender, e);
        }
        private void OpenButton_Click(object sender, EventArgs e)
        {
            открытьToolStripMenuItem1_Click(sender, e);
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem_Click(sender, e);
        }
        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            сохранитьКакToolStripMenuItem_Click(sender, e);
        }
        private void SizeButton_Click(object sender, EventArgs e)
        {
            размерОкнаToolStripMenuItem_Click(sender, e);
        }
        private void ThicknessButton_Click(object sender, EventArgs e)
        {
            толщинаЛинииToolStripMenuItem_Click(sender, e);
        }
        private void LineColorButton_Click(object sender, EventArgs e)
        {
            цветЛинииToolStripMenuItem_Click(sender, e);
        }
        private void BackgroundButton_Click(object sender, EventArgs e)
        {
            цветФонаToolStripMenuItem_Click(sender, e);
        }
        private void FillFlagButton_Click(object sender, EventArgs e)
        {
            if (ActiveChild.state.GetType().ToString() != "Lab_2.EditingState")
            {
                if (!FillFlagButton.Checked)
                {
                    заливкаФигурыToolStripMenuItem_Click(sender, e);
                    FillFlagButton.Checked = true;
                }
                else
                {
                    заливкаФигурыToolStripMenuItem_Click(sender, e);
                    FillFlagButton.Checked = false;
                }
            }
            else 
            {
                заливкаФигурыToolStripMenuItem_Click(sender, e);
            }
        }
        private void RectangleButton_Click(object sender, EventArgs e)
        {
            прямоугольникToolStripMenuItem_Click(sender, e);
        }
        private void EllipseButton_Click(object sender, EventArgs e)
        {
            эллипсToolStripMenuItem_Click(sender, e);
        }
        private void LineButton_Click(object sender, EventArgs e)
        {
            прямаяЛинияToolStripMenuItem_Click(sender, e);
        }
        private void CurveButton_Click(object sender, EventArgs e)
        {
            криваяЛинияToolStripMenuItem_Click(sender, e);
        }
        private void TextButton_Click(object sender, EventArgs e)
        {
            текстToolStripMenuItem_Click(sender, e);
        }
        private void FontButton_Click(object sender, EventArgs e)
        {
            шрифтToolStripMenuItem_Click(sender, e);
        }
        private void SelectButton_Click(object sender, EventArgs e)
        {
            выделитьToolStripMenuItem_Click(sender, e);
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            удалитьToolStripMenuItem_Click(sender, e);
        }
        //Кнопки(сетка)
        private void GridButton_Click(object sender, EventArgs e)
        {
            сеткаToolStripMenuItem_Click(sender, e);
            if (сеткаToolStripMenuItem.Checked == true)
                GridButton.Checked = true;
            if (сеткаToolStripMenuItem.Checked == false)
                GridButton.Checked = false;
        }
        private void GridStepButton_Click(object sender, EventArgs e)
        {
            шагСеткиToolStripMenuItem_Click(sender, e);
        }
        private void GridAlignButton_Click(object sender, EventArgs e)
        {
            выровнятьПоСеткеToolStripMenuItem_Click(sender, e);
        }

        private void редактированиеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FiguresParameters d = new FiguresParameters(ActiveChild.FiguresList,ActiveChild);
            d.ShowDialog();
        }

        private void GridSnapButton_Click(object sender, EventArgs e)
        {
            привязкаКСеткеToolStripMenuItem_Click(sender, e);
            if (привязкаКСеткеToolStripMenuItem.Checked == true)
                GridSnapButton.Checked = true;
            if (привязкаКСеткеToolStripMenuItem.Checked == false)
                GridSnapButton.Checked = false;
        }
        private void Form1_MdiChildActivate(object sender, EventArgs e) //Метод который определяет смену активного окна(кликнул на другой рисунок)
        {
            ActiveChild = (Form2)ActiveMdiChild;
            DrawItemRefresh();
        }
        //Опенинги (не аниме!!!)
        private void файлToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (MdiChildren.Length != 0)
            {
                сохранитьКакToolStripMenuItem.Enabled = true;
                if (ActiveChild.Changes)
                    сохранитьToolStripMenuItem.Enabled = true;
                else
                    сохранитьToolStripMenuItem.Enabled = false;

            }
            else
            {
                сохранитьToolStripMenuItem.Enabled = false;
                сохранитьКакToolStripMenuItem.Enabled = false;
            }
        }
        private void правкаToolStripMenuItem_DropDownOpening(object sender, EventArgs e) //Кнопки "Правка"
        {
            if (MdiChildren.Length != 0)
            {
                if (CanPaste)
                    вставитьToolStripMenuItem.Enabled = true;
                else
                    вставитьToolStripMenuItem.Enabled = false;
                if (ActiveChild.FiguresList.Count != 0)
                    выделитьВсёToolStripMenuItem.Enabled = true;
                else
                    выделитьВсёToolStripMenuItem.Enabled = false;
                if (ActiveChild.SelectedList.Count != 0)
                {
                    копироватьToolStripMenuItem.Enabled = true;
                    вырезатьToolStripMenuItem.Enabled = true;
                    копироватьМетафайлToolStripMenuItem.Enabled = true;
                }
                else
                {
                    копироватьToolStripMenuItem.Enabled = false;
                    вырезатьToolStripMenuItem.Enabled = false;
                    копироватьМетафайлToolStripMenuItem.Enabled = false;
                }
            }
            if (MdiChildren.Length == 0 || ActiveChild.state.GetType().ToString() == "Lab_2.EditingState")
            {
                копироватьToolStripMenuItem.Enabled = false;
                выделитьВсёToolStripMenuItem.Enabled = false;
                копироватьМетафайлToolStripMenuItem.Enabled = false;
                вставитьToolStripMenuItem.Enabled = false;
                вырезатьToolStripMenuItem.Enabled = false;
            }
        }
        private void атрибутыToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (MdiChildren.Count() == 0 || MdiChildren == null)
            {
                сеткаToolStripMenuItem.Enabled = false;
                шагСеткиToolStripMenuItem.Enabled = false;
                выровнятьПоСеткеToolStripMenuItem.Enabled = false;
                привязкаКСеткеToolStripMenuItem.Enabled = false;
            }
            else
            {
                сеткаToolStripMenuItem.Enabled = true;
                шагСеткиToolStripMenuItem.Enabled = true;
                if (сеткаToolStripMenuItem.Checked == true)
                {
                    выровнятьПоСеткеToolStripMenuItem.Enabled = true;
                    привязкаКСеткеToolStripMenuItem.Enabled = true;
                }
                else
                {
                    выровнятьПоСеткеToolStripMenuItem.Enabled = false;
                    привязкаКСеткеToolStripMenuItem.Enabled = false;
                }
            }
        }
        private void редактированиеToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (ActiveChild != null)
            {
                выделитьToolStripMenuItem.Enabled = true;
                if (ActiveChild.SelectedList.Count != 0)
                    удалитьToolStripMenuItem.Enabled = true;
                else
                    удалитьToolStripMenuItem.Enabled = false;
            }
            else
            {
                выделитьToolStripMenuItem.Enabled = false;
                удалитьToolStripMenuItem.Enabled = false;
            }
        }
        public void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void Switcher(string curFigure)
        {
            прямоугольникToolStripMenuItem.Checked = false;
            эллипсToolStripMenuItem.Checked = false;
            прямаяЛинияToolStripMenuItem.Checked = false;
            криваяЛинияToolStripMenuItem.Checked = false;
            текстToolStripMenuItem.Checked = false;
            выделитьToolStripMenuItem.Checked = false;
            RectangleButton.Checked = false;
            EllipseButton.Checked = false;
            LineButton.Checked = false;
            CurveButton.Checked = false;
            TextButton.Checked = false;
            SelectButton.Checked = false;
            Pstate = ActiveChild.state = new DrawingState();
            switch (curFigure)
            {
                case "Прямоугольник":
                    прямоугольникToolStripMenuItem.Checked = true;
                    RectangleButton.Checked = true;
                    break;
                case "Эллипс":
                    эллипсToolStripMenuItem.Checked = true;
                    EllipseButton.Checked = true;
                    break;
                case "Прямая линия":
                    прямаяЛинияToolStripMenuItem.Checked = true;
                    LineButton.Checked = true;
                    break;
                case "Кривая линия":
                    криваяЛинияToolStripMenuItem.Checked = true;
                    CurveButton.Checked = true;
                    break;
                case "Текст":
                    текстToolStripMenuItem.Checked = true;
                    TextButton.Checked = true;
                    break;
                case "Выделить":
                    выделитьToolStripMenuItem.Checked = true;
                    SelectButton.Checked = true;
                    Pstate = ActiveChild.state = new SelectingState();
                    break;

            }
        }
    }
}
