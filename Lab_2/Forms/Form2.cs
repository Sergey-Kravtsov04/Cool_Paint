using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Lab_2
{
    public partial class Form2 : Form
    {
        internal List<Figure> FiguresList = new List<Figure>(), DopList = new List<Figure>(), SelectedList = new List<Figure>(), GridList = new List<Figure>();
        internal Point StartPoint, EndPoint, previousMousePosition;
        internal Graphics G;
        internal Figure Fig;
        internal List<Point> CurvePoints;
        internal Form1 _parent = new Form1();
        internal bool MousePressed, ClickWithoutMove, Changes = false, SnapGrid = false, MoveFigures = true;
        internal int FormWidth, FormHeight;
        internal string CurrentFigure;
        internal IState state;
        internal Form2(int formWidth, int formHeight)
        {
            InitializeComponent();
            FormWidth = formWidth;
            FormHeight = formHeight;
            AutoScrollMinSize = new Size(FormWidth - 100, FormHeight - 100);
            AutoScroll = true;
            DoubleBuffered = true;
        }
        private void Form2_Load(object sender, EventArgs e) { }
        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            state.PerformMouseDown(sender, e, AutoScrollPosition, this);
        }
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            state.PerformMouseMove(sender, e, AutoScrollPosition, this);
        }
        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            state.PerformMouseUp(sender, e, AutoScrollPosition, this);
        }
        private void Form2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            state = new SelectingState();
            SelectedList.Clear();
            DopList.Clear();
            if (CurrentFigure == "Выделить")
            {
                foreach (Figure f in FiguresList)
                {
                    if (f.Check(AutoScrollPosition, StartPoint))
                    {
                        SelectedList.Add(f);
                        f.AddEditRectangles(SelectedList, AutoScrollPosition);
                        state = new EditingState();
                        break;
                    }
                }
            }
            Invalidate();
        }
        private void Form2_Paint(object sender, PaintEventArgs e) //Всё рисование здесь
        {
            state.PerformPaint(sender, e.Graphics, AutoScrollPosition, this);
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Delete();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (Changes)
            {
                case true:
                    DialogResult saveMessageChoice = MessageBox.Show("Сохранить файл перед закрытием?", "Закрытие файла", MessageBoxButtons.YesNoCancel);
                    switch (saveMessageChoice)
                    {
                        case DialogResult.Yes:
                            SaveAs();
                            break;
                        case DialogResult.No:
                            break;
                        default:
                            e.Cancel = true;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        internal void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Text, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, FiguresList);
            formatter.Serialize(stream, FormHeight);
            formatter.Serialize(stream, FormWidth);
            stream.Close();
            Changes = false;
        }
        internal void SaveAs()
        {
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                InitialDirectory = Environment.CurrentDirectory,
                FileName = Text
            };
            saveDialog.ShowDialog();
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(saveDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, FiguresList);
            formatter.Serialize(stream, FormHeight);
            formatter.Serialize(stream, FormWidth);
            stream.Close();
            Changes = false;
        }
        internal void Open()
        {
            OpenFileDialog openDialog = new OpenFileDialog()
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "All files (*.*)|*.*"
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(openDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                FiguresList = (List<Figure>)formatter.Deserialize(stream);
                FormHeight = (int)formatter.Deserialize(stream);
                FormWidth = (int)formatter.Deserialize(stream);
                Text = Path.GetFileName(openDialog.FileName);
                stream.Close();
                //
                Rectangle canvas = new Rectangle(0, 0, FormWidth, FormHeight);
                SolidBrush brush = new SolidBrush(Color.White);
                G.FillRectangle(brush, canvas);
                Size = new Size(FormWidth, FormHeight);
                AutoScroll = true;
                AutoScrollMinSize = new Size(FormWidth - 100, FormHeight - 100);
                Changes = false;
            }
        }
        internal void Delete()
        {
            foreach (Figure f1 in SelectedList)
            {
                if (FiguresList.Contains(f1))
                    FiguresList.Remove(f1);
            }
            SelectedList.Clear();
            Invalidate();
        }
        internal void Copy()
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            MemoryStream MemoryStream = new MemoryStream();
            List<Figure> list = new List<Figure>();
            foreach (Figure f in SelectedList)
                list.Add((Figure)f.Clone());
            Formatter.Serialize(MemoryStream, list);
            DataObject dataObject = new DataObject();
            dataObject.SetData("LAB2", MemoryStream);
            Clipboard.SetDataObject(dataObject, true);
            MemoryStream.Close();
        }
        internal void Cut()
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            MemoryStream MemoryStream = new MemoryStream();
            List<Figure> list = new List<Figure>();
            foreach (Figure f in SelectedList)
            {
                list.Add((Figure)f.Clone());
                if (FiguresList.Contains(f))
                    FiguresList.Remove(f);
            }
            SelectedList.Clear();
            Formatter.Serialize(MemoryStream, list);
            DataObject dataObject = new DataObject();
            dataObject.SetData("LAB2", MemoryStream);
            Clipboard.SetDataObject(dataObject, true);
            MemoryStream.Close();
        }
        internal void Paste()
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            MemoryStream MemoryStream = (MemoryStream)Clipboard.GetDataObject().GetData("LAB2"); //Обращаемся к буферу и извлекаем данные с заданным форматом
            List<Figure> bufferList = (List<Figure>)Formatter.Deserialize(MemoryStream); //Десериализуем 
            MemoryStream.Close();
            PasteFromList(bufferList);
        }
        internal void SelectAll()
        {
            foreach (Figure f in FiguresList)
            {
                if (!SelectedList.Contains(f))
                    SelectedList.Add(f);
            }
        }
        internal void MetaFileCopy()
        {
            G = CreateGraphics();
            IntPtr intPtr = G.GetHdc();
            Metafile mf = new Metafile(intPtr, EmfType.EmfOnly);
            G.ReleaseHdc(intPtr);
            G.Dispose();
            G = Graphics.FromImage(mf);
            foreach (Figure f in SelectedList)
            {
                f.Draw(G, AutoScrollPosition);
            }
            G.Dispose();
            ClipboardMetafileHelper.PutEnhMetafileOnClipboard(Handle, mf);
        }
        internal void PasteFromList(List<Figure> list)
        {
            int x_min = list[0].GetMinPoint().X;
            int y_min = list[0].GetMinPoint().Y;
            foreach (Figure f in list)
            {
                if (x_min > f.GetMinPoint().X)
                    x_min = f.GetMinPoint().X;
                if (y_min > f.GetMinPoint().Y)
                    y_min = f.GetMinPoint().Y;
            }
            Point offset = new Point(x_min, y_min); //Расстояние от этой фигуры до координат (0;0) -смещение для всех фигур
            int x_max = list[0].GetMaxPoint().X;
            int y_max = list[0].GetMaxPoint().Y;
            foreach (Figure f in list) //Находим координаты самой правой точки
            {
                if (x_max < f.GetMaxPoint().X)
                    x_max = f.GetMaxPoint().X;
                if (y_max < f.GetMaxPoint().Y)
                    y_max = f.GetMaxPoint().Y;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (x_max - offset.X > FormWidth || y_max - offset.Y > FormHeight)
                {
                    MessageBox.Show("Рисунок больше окна", Text);
                    break;
                }
                else
                {
                    list[i].FigureMoveToStart(offset);
                    FiguresList.Add(list[i]);
                }
            }
            Invalidate();
        }
        internal void PaintGrid(int gridStep)
        {
            int width = FormWidth;
            int height = FormHeight;
            for (int i = 0; i <= width; i += gridStep)
            {
                for (int j = 0; j <= height; j += gridStep)
                {
                    Point start1 = new Point(i - gridStep, j - gridStep);
                    Point finish1 = new Point(i, j);
                    OurRectangle gridRect1 = new OurRectangle(start1, finish1, Color.LightBlue, Color.White, false, 1);
                    GridList.Add(gridRect1);
                }
            }
        }
        internal void AlignGrid(int gridStep)
        {
            foreach (Figure f in FiguresList)
                f.AlignFigure(gridStep);
        }
    }
}

