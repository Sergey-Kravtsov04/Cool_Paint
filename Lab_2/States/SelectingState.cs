using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    internal class SelectingState : AbstractState, IState
    {
        public override void PerformMouseDown(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            if (e.Button == MouseButtons.Left && e.X < parent.FormWidth && e.Y < parent.FormHeight && e.X > 0 && e.Y > 0)
            {
                base.PerformMouseDown(sender, e, scrollPosition, parent);
                int counter = 0;
                foreach (Figure f in parent.FiguresList)
                {
                    if (f.Check(scrollPosition, parent.StartPoint) && parent.SelectedList.Contains(f))
                    {
                        //SelectedList.Remove(f);
                    }
                    else if (f.Check(scrollPosition, parent.StartPoint) && !parent.SelectedList.Contains(f))
                        parent.SelectedList.Add(f);
                    for (int i = 0; i < parent.SelectedList.Count; i++)
                        if (f.Check(scrollPosition, parent.StartPoint))
                            counter++;
                }
                if (counter == 0 || parent.SelectedList.Count == 0)
                {
                    parent.SelectedList.Clear();
                    parent.MoveFigures = true;
                }
                else
                {
                    parent.DopList.Clear();
                    foreach (Figure f in parent.SelectedList)
                        parent.DopList.Add((Figure)f.Clone());
                    parent.MoveFigures = false;
                }
            }
        }
        public override void PerformMouseMove(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            base.PerformMouseMove(sender, e, scrollPosition, parent);
            if (parent.MousePressed && e.X < parent.FormWidth && e.Y < parent.FormHeight && parent.StartPoint.X < parent.FormWidth && parent.StartPoint.Y < parent.FormHeight)
            {
                parent.Fig = new OurRectangle(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness);
            }
            parent.Invalidate();
        }
        public override void PerformMouseUp(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            base.PerformMouseUp(sender, e, scrollPosition, parent);
            bool arrayClear = false;
            if (!parent.MoveFigures)
            {
                foreach (Figure f in parent.DopList)
                {
                    if (f.GetMaxPoint().X > parent.FormWidth || f.GetMaxPoint().Y > parent.FormHeight || f.GetMinPoint().X < 0 || f.GetMinPoint().Y < 0) //Проверка вхождения нового положения фигуры в границы холста
                    {
                        arrayClear = true;
                        parent.DopList.Clear();
                        break;
                    }
                }
                if (!arrayClear) //Перемещение фигур в новое положение при условии их вхождения в границы холста
                {
                    if (parent.SnapGrid)
                    {
                        foreach (Figure f in parent.DopList)
                            f.AlignFigure(parent._parent.GridStep);
                    }
                    foreach (Figure f in parent.SelectedList)
                    {
                        if (parent.FiguresList.Contains(f))
                            parent.FiguresList.Remove(f); //Удаляем Старое положение фигур
                    }
                    parent.SelectedList.Clear(); //Очищаем старое положение выделенных фигур
                    foreach (Figure f in parent.DopList)
                    {
                        parent.FiguresList.Add(f); //Добавляем новое положение фигур в основной массив
                        parent.SelectedList.Add(f); //Сохраняем выделенные фигуры в режиме выделения
                    }
                    parent.DopList.Clear(); //Очищаем новое положение фигур, так как они уже сохранены в основной массив
                }
            }
            //
            parent.Invalidate();
        }
        public override void PerformPaint(object sender, Graphics e, Point scrollPosition, Form2 parent)
        {
            base.PerformPaint(sender, e, scrollPosition, parent);
            if (parent.MousePressed)
            {
                if (parent.MoveFigures)
                {
                    foreach (Figure f in parent.FiguresList)
                    {
                        if (parent.SnapGrid && parent.CurrentFigure != "Кривая линия")
                            parent.Fig.AlignFigure(parent._parent.GridStep);
                        if (parent.Fig.Check(e, scrollPosition, f) && !parent.SelectedList.Contains(f))
                            parent.SelectedList.Add(f);
                        if (!parent.Fig.Check(e, scrollPosition, f) && parent.SelectedList.Contains(f))
                            parent.SelectedList.Remove(f);
                    }
                }
                else
                {
                    Point offset = new Point(parent.EndPoint.X - parent.previousMousePosition.X, parent.EndPoint.Y - parent.previousMousePosition.Y);
                    foreach (Figure f in parent.DopList)
                        f.FigureMove(offset);
                    parent.previousMousePosition.X = parent.EndPoint.X;
                    parent.previousMousePosition.Y = parent.EndPoint.Y;
                }
            }
        }
    }
}
