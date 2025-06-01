using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    [Serializable]
    class OurEllipse : Figure
    {
        [NonSerialized] Pen pen;
        private Rectangle ClassRectangle = new Rectangle();
        internal OurEllipse(Point point1, Point point2, Color colorLine, Color backGroundColor, bool backGroundFill, int lineThickness) : base(point1, point2, colorLine, backGroundColor, backGroundFill, lineThickness)
        {
        }
        internal override void Draw(Graphics g, Point scrollPosition)
        {
            pen = new Pen(LineColor, LineThickness);
            ClassRectangle = Rectangle.FromLTRB(Point1.X + scrollPosition.X, Point1.Y + scrollPosition.Y, Point2.X + scrollPosition.X, Point2.Y + scrollPosition.Y);
            switch (BackgroundFill)
            {
                case true:
                    SolidBrush brush = new SolidBrush(BackgroundColor);
                    g.FillEllipse(brush, ClassRectangle);
                    g.DrawEllipse(pen, ClassRectangle);
                    break;
                default:
                    g.DrawEllipse(pen, ClassRectangle);
                    break;
            }
        }
        internal override void DrawDash(Graphics g, Point scrollPosition)
        {
            pen = new Pen(LineColor, LineThickness) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            ClassRectangle = Rectangle.FromLTRB(Point1.X + scrollPosition.X, Point1.Y + scrollPosition.Y, Point2.X + scrollPosition.X, Point2.Y + scrollPosition.Y);
            g.DrawEllipse(pen, ClassRectangle);
        }
        internal override void DrawHide(Graphics g, Point scrollPosition)
        {
            pen = new Pen(Color.White, LineThickness);
            ClassRectangle = Rectangle.FromLTRB(Point1.X + scrollPosition.X, Point1.Y + scrollPosition.Y, Point2.X + scrollPosition.X, Point2.Y + scrollPosition.Y);
            g.DrawEllipse(pen, ClassRectangle);
        }
        internal override string GetFigureType()
        {
            return "Эллипс";
        }
    }

}
