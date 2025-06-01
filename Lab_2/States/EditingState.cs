using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    internal class EditingState : AbstractState, IState
    {
        public override void PerformMouseDown(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            if (e.Button == MouseButtons.Left && e.X < parent.FormWidth && e.Y < parent.FormHeight && e.X > 0 && e.Y > 0)
            {
                base.PerformMouseDown(sender, e, scrollPosition, parent);

                int counter = 0;
                foreach (Figure f in parent.SelectedList)
                {
                    if (f.Check(scrollPosition, parent.StartPoint))
                    {
                        foreach (Figure f1 in parent.SelectedList)
                            parent.DopList.Add((Figure)f1.Clone());
                        break;
                    }
                    else
                        counter++;
                }
                if (counter == 9)
                {
                    parent.SelectedList.Clear();
                    parent.DopList.Clear();
                    parent._parent.Pstate = parent.state = new SelectingState();
                }
                int selectedListCount = parent.SelectedList.Count; //потому что его длина будет меняться в цикле
                for (int i = 0; i < selectedListCount; i++) //Тут квадраты удаляем из селектед листа, тк проводим все операции на доп листе
                {
                    if (i != 0)
                        parent.SelectedList.Remove(parent.SelectedList[1]); //Тут маг7ия происходит. Если интересно, то пишите мне в лс. @estdvarubla VK
                }
            }
        }
        public override void PerformMouseMove(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            if (e.Button == MouseButtons.Left && e.X < parent.FormWidth && e.Y < parent.FormHeight && e.X > 0 && e.Y > 0)
            {
                base.PerformMouseMove(sender, e, scrollPosition, parent);
            }
            parent.Invalidate();
        }
        public override void PerformMouseUp(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            if (parent.DopList.Count != 0)
                parent.DopList[0].TextEditing(scrollPosition, parent.ClickWithoutMove);
            base.PerformMouseUp(sender, e, scrollPosition, parent);
            bool arrayClear = false;
            if (parent.DopList.Count != 0)
            {
                if (parent.DopList[0].GetMaxPoint().X > parent.FormWidth || parent.DopList[0].GetMaxPoint().Y > parent.FormHeight || parent.DopList[0].GetMinPoint().X < 0 || parent.DopList[0].GetMinPoint().Y < 0 || parent.DopList[0].AntiRoll(parent.DopList)) //Проверка вхождения нового положения фигуры в границы холста
                {
                    arrayClear = true;
                    parent.DopList.Clear();
                    parent.SelectedList[0].AddEditRectangles(parent.SelectedList, scrollPosition);
                }
                if (!arrayClear)
                {
                    if (parent.SnapGrid)
                    {
                        foreach (Figure f in parent.DopList)
                            f.AlignFigure(parent._parent.GridStep);
                    }
                    if (parent.FiguresList.Contains(parent.SelectedList[0]))
                        parent.FiguresList.Remove(parent.SelectedList[0]);
                    parent.SelectedList.Clear();
                    parent.FiguresList.Add(parent.DopList[0]);
                    parent.SelectedList.Add(parent.DopList[0]);
                    parent.SelectedList[0].AddEditRectangles(parent.SelectedList, scrollPosition);
                    parent.DopList.Clear();
                }
            }
            parent.Invalidate();
        }
        public override void PerformPaint(object sender, Graphics e, Point scrollPosition, Form2 parent)
        {
            base.PerformPaint(sender, e, scrollPosition, parent);
            if (parent.MousePressed && parent.DopList.Count!=0)
            {
                    Point offset = new Point(parent.EndPoint.X - parent.previousMousePosition.X, parent.EndPoint.Y - parent.previousMousePosition.Y);
                    parent.DopList[0].Edit(scrollPosition, e, parent.EndPoint, parent.DopList, offset, parent.SelectedList[0], parent.ClickWithoutMove);
                    parent.previousMousePosition.X = parent.EndPoint.X;
                    parent.previousMousePosition.Y = parent.EndPoint.Y;
            }
        }
    }
}
