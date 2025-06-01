using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    internal abstract class AbstractState : IState
    {
        public virtual void PerformMouseDown(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            parent.StartPoint = new Point(e.Location.X - scrollPosition.X, e.Location.Y - scrollPosition.Y);
            parent.previousMousePosition = parent.StartPoint;
            parent._parent = (Form1)parent.ParentForm;
            parent.CurrentFigure = parent._parent.PCurrentFigure;
            parent.MousePressed = true;
            parent.ClickWithoutMove = true;

        }
        public virtual void PerformMouseMove(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            parent.ClickWithoutMove = false;
            parent._parent.MouseCoordinateStatus(e.Location);
            parent.EndPoint = new Point(e.X - scrollPosition.X, e.Y - scrollPosition.Y);
        }
        public virtual void PerformMouseUp(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            parent.MousePressed = false;
            if (e.Button == MouseButtons.Left && e.X < parent.FormWidth && e.Y < parent.FormHeight && e.X > 0 && e.Y > 0)
            {
                parent.Changes = true;
            }
            parent.Invalidate();
        }
        public virtual void PerformPaint(object sender, Graphics e, Point scrollPosition, Form2 parent)
        {
            e.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, parent.FormWidth, parent.FormHeight));
            foreach (Figure f in parent.GridList)
                f.Draw(e, scrollPosition);
            foreach (Figure f in parent.FiguresList)
            {
                if (!parent.SelectedList.Contains(f))
                    f.Draw(e, scrollPosition);
            }
            //
            foreach (Figure f in parent.SelectedList)
                f.DrawDash(e, scrollPosition);
            foreach (Figure f in parent.DopList)
                f.DrawDash(e, scrollPosition);
            //
        }
    }
}
