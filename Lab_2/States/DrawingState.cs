using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Net;


namespace Lab_2
{
    internal class DrawingState : AbstractState, IState
    {
        public override void PerformMouseDown(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            if (e.Button == MouseButtons.Left && e.X < parent.FormWidth && e.Y < parent.FormHeight && e.X > 0 && e.Y > 0)
            {
                base.PerformMouseDown(sender, e, scrollPosition, parent);
                parent.CurvePoints = new List<Point>();
                parent.SelectedList.Clear();
            }
        }
        public override void PerformMouseMove(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            base.PerformMouseMove(sender, e, scrollPosition, parent);
            if (parent.MousePressed && e.X < parent.FormWidth && e.Y < parent.FormHeight && parent.StartPoint.X < parent.FormWidth && parent.StartPoint.Y < parent.FormHeight)
            {
                switch (parent.CurrentFigure)
                {
                    case "Прямоугольник":
                        parent.Fig = new OurRectangle(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness);//
                        break;
                    case "Эллипс":
                        parent.Fig = new OurEllipse(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness);//
                        break;
                    case "Прямая линия":
                        parent.Fig = new OurLine(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness);//
                        break;
                    case "Кривая линия":
                        parent.CurvePoints.Add(new Point(e.X - scrollPosition.X, e.Y - scrollPosition.Y));
                        parent.Fig = new OurCurve(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness, parent.CurvePoints);//
                        break;
                    case "Текст":
                        parent.Fig = new OurText(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness, parent._parent.PFont, parent);//
                        break;
                }
            }
            parent.Invalidate();
        }
        public override void PerformMouseUp(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent)
        {
            base.PerformMouseUp(sender, e, scrollPosition, parent);
            if (e.Button == MouseButtons.Left && e.X < parent.FormWidth && e.Y < parent.FormHeight && e.X > 0 && e.Y > 0)
            {
                switch (parent.CurrentFigure)
                {
                    case "Прямоугольник":
                        parent.Fig = new OurRectangle(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness);//
                        break;
                    case "Эллипс":
                        parent.Fig = new OurEllipse(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness);//
                        break;
                    case "Прямая линия":
                        parent.Fig = new OurLine(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness);//
                        break;
                    case "Кривая линия":
                        parent.Fig = new OurCurve(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness, parent.CurvePoints);//
                        break;
                    case "Текст":
                        parent.Fig = new OurText(parent.StartPoint, parent.EndPoint, parent._parent.PLineColor, parent._parent.PBackgroundColor, parent._parent.PBackgroundFill, parent._parent.PLineThickness, parent._parent.PFont, parent);
                        parent.Fig.Draw(parent.G, parent.AutoScrollPosition);
                        break;
                }
                if (parent.SnapGrid)
                    parent.Fig.AlignFigure(parent._parent.GridStep);
                parent.FiguresList.Add(parent.Fig);
            }
        }
        public override void PerformPaint(object sender, Graphics e, Point scrollPosition, Form2 parent)
        {
            base.PerformPaint(sender, e, scrollPosition, parent);
            if (parent.MousePressed)
            {
                if (parent.SnapGrid && parent.CurrentFigure != "Кривая линия")
                    parent.Fig.AlignFigure(parent._parent.GridStep);
                parent.Fig.DrawDash(e, scrollPosition);
            }
        }
    }
}
