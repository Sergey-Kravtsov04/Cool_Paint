using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    [Serializable]
    internal class OurLine : Figure
    {
        [NonSerialized] Pen pen;
        internal OurLine(Point point1, Point point2, Color colorLine, Color backGroundColor, bool backGroundFill, int lineThickness) : base(point1, point2, colorLine, backGroundColor, backGroundFill, lineThickness)
        {
        }
        internal override void Draw(Graphics g, Point scrollPosition)
        {
            pen = new Pen(LineColor, LineThickness);
            g.DrawLine(pen, NotNormPoint1.X + scrollPosition.X, NotNormPoint1.Y + scrollPosition.Y, NotNormPoint2.X + scrollPosition.X, NotNormPoint2.Y + scrollPosition.Y);
        }
        internal override void DrawDash(Graphics g, Point scrollPosition)
        {
            pen = new Pen(LineColor, LineThickness) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            g.DrawLine(pen, NotNormPoint1.X + scrollPosition.X, NotNormPoint1.Y + scrollPosition.Y, NotNormPoint2.X + scrollPosition.X, NotNormPoint2.Y + scrollPosition.Y);
        }
        internal override void DrawHide(Graphics g, Point scrollPosition)
        {
            pen = new Pen(Color.White, LineThickness);
            g.DrawLine(pen, NotNormPoint1.X + scrollPosition.X, NotNormPoint1.Y + scrollPosition.Y, NotNormPoint2.X + scrollPosition.X, NotNormPoint2.Y + scrollPosition.Y);
        }
        internal override void FigureMove(Point offset)
        {
            NotNormPoint1 = new Point(NotNormPoint1.X + offset.X, NotNormPoint1.Y + offset.Y);
            NotNormPoint2 = new Point(NotNormPoint2.X + offset.X, NotNormPoint2.Y + offset.Y);
            Point1 = new Point(Point1.X + offset.X, Point1.Y + offset.Y);
            Point2 = new Point(Point2.X + offset.X, Point2.Y + offset.Y);
        }
        internal Point GetMinFLIM() //For Line In Move
        {
            if (NotNormPoint1.X < NotNormPoint2.X && NotNormPoint1.Y < NotNormPoint2.Y)
                return NotNormPoint1;
            else if (NotNormPoint1.X < NotNormPoint2.X && NotNormPoint1.Y > NotNormPoint2.Y)
                return new Point(NotNormPoint1.X, NotNormPoint2.Y);
            else if (NotNormPoint1.X > NotNormPoint2.X && NotNormPoint1.Y < NotNormPoint2.Y)
                return new Point(NotNormPoint2.X, NotNormPoint1.Y);
            else if (NotNormPoint1.X > NotNormPoint2.X && NotNormPoint1.Y > NotNormPoint2.Y)
                return NotNormPoint2;
            else
                return Point1;
        }
        internal Point GetMaxFLIM()
        {
            if (NotNormPoint1.X > NotNormPoint2.X && NotNormPoint1.Y > NotNormPoint2.Y)
                return NotNormPoint1;
            else if (NotNormPoint1.X > NotNormPoint2.X && NotNormPoint1.Y < NotNormPoint2.Y)
                return new Point(NotNormPoint1.X, NotNormPoint2.Y);
            else if (NotNormPoint1.Y > NotNormPoint2.Y && NotNormPoint1.X < NotNormPoint2.X)
                return new Point(NotNormPoint2.X, NotNormPoint1.Y);
            else if (NotNormPoint2.X > NotNormPoint1.X && NotNormPoint2.Y > NotNormPoint1.Y)
                return NotNormPoint2;
            else
                return Point2;
        }
        internal override void Edit(Point scrollPosition, Graphics g, Point movePoint, List<Figure> rectangles, Point offset, Figure startFig, bool withoutMove)
        {
            Point min = new Point(GetMinPoint().X, GetMinPoint().Y);
            Point max = new Point(GetMaxPoint().X, GetMaxPoint().Y);
            int whereOffset = 0;
            if (!withoutMove && rectangles[0].Check(scrollPosition, movePoint) && !rectangles[1].Check(scrollPosition, movePoint) && !rectangles[2].Check(scrollPosition, movePoint) && !rectangles[3].Check(scrollPosition, movePoint) && !rectangles[4].Check(scrollPosition, movePoint) && !rectangles[5].Check(scrollPosition, movePoint) && !rectangles[6].Check(scrollPosition, movePoint) && !rectangles[7].Check(scrollPosition, movePoint) && !rectangles[8].Check(scrollPosition, movePoint))
                FigureMove(offset);
            else if (rectangles[1].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[1].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, startFig.GetMaxPoint().Y);
                min = new Point(movePoint.X, movePoint.Y);
                Point1 = new Point(movePoint.X, movePoint.Y);
                whereOffset = 1;
            }
            else if (rectangles[2].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[2].Check(scrollPosition, movePoint)))
            {
                max = new Point(movePoint.X, startFig.GetMaxPoint().Y);
                min = new Point(startFig.GetMinPoint().X, movePoint.Y);
                Point2.X = movePoint.X;
                Point1.Y = movePoint.Y;
                whereOffset = 2;

            }
            else if (rectangles[3].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[3].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, movePoint.Y);
                min = new Point(movePoint.X, startFig.GetMinPoint().Y);
                Point1.X = movePoint.X;
                Point2.Y = movePoint.Y;
                whereOffset = 3;
            }
            else if (rectangles[4].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[4].Check(scrollPosition, movePoint)))
            {
                max = new Point(movePoint.X, movePoint.Y);
                min = new Point(startFig.GetMinPoint().X, startFig.GetMinPoint().Y);
                Point2 = new Point(movePoint.X, movePoint.Y);
                whereOffset = 4;
            }
            else if (rectangles[5].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[5].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, startFig.GetMaxPoint().Y);
                min = new Point(movePoint.X, startFig.GetMinPoint().Y);
                Point1 = new Point(movePoint.X, Point1.Y);
                whereOffset = 5;
            }
            else if (rectangles[6].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[6].Check(scrollPosition, movePoint)))
            {
                max = new Point(movePoint.X, startFig.GetMaxPoint().Y);
                min = new Point(startFig.GetMinPoint().X, startFig.GetMinPoint().Y);
                Point2 = new Point(movePoint.X, Point2.Y);
                whereOffset = 6;
            }
            else if (rectangles[7].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[7].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, startFig.GetMaxPoint().Y);
                min = new Point(startFig.GetMinPoint().X, movePoint.Y);
                Point1 = new Point(Point1.X, movePoint.Y);
                whereOffset = 7;
            }
            else if (rectangles[8].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[8].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, movePoint.Y);
                min = new Point(startFig.GetMinPoint().X, startFig.GetMinPoint().Y);
                Point2 = new Point(Point2.X, movePoint.Y);
                whereOffset = 8;
            }
            double Xcoef = (max.X - min.X) / (double)(startFig.GetMaxPoint().X - startFig.GetMinPoint().X);
            double Ycoef = (max.Y - min.Y) / (double)(startFig.GetMaxPoint().Y - startFig.GetMinPoint().Y);
            EditLine(startFig, Xcoef, Ycoef, whereOffset);
            //
            int rectanglesCount = rectangles.Count;
            for (int i = 0; i < rectanglesCount - 1; i++) //Удаляем квадраты, чтобы добавить новые
                rectangles.Remove(rectangles[1]);
            AddEditRectangles(rectangles, scrollPosition);
        }
        internal void EditLine(Figure start, double X_coef, double Y_coef, int where)
        {
            if (X_coef != 1 || Y_coef != 1)
            {
                int ScaleOffsetX = 0;
                int ScaleOffsetY = 0;
                NotNormPoint1 = new Point((int)(start.NotNormPoint1.X * X_coef), (int)(start.NotNormPoint1.Y * Y_coef));
                NotNormPoint2 = new Point((int)(start.NotNormPoint2.X * X_coef), (int)(start.NotNormPoint2.Y * Y_coef));
                switch (where)
                {
                    case 1:
                        ScaleOffsetX = GetMaxFLIM().X - start.GetMaxPoint().X;
                        ScaleOffsetY = GetMaxFLIM().Y - start.GetMaxPoint().Y;
                        break;
                    case 2:
                        ScaleOffsetX = GetMinFLIM().X - start.GetMinPoint().X;
                        ScaleOffsetY = GetMaxFLIM().Y - start.GetMaxPoint().Y;
                        break;
                    case 3:
                        ScaleOffsetX = GetMaxFLIM().X - start.GetMaxPoint().X;
                        ScaleOffsetY = GetMinFLIM().Y - start.GetMinPoint().Y;
                        break;
                    case 4:
                        ScaleOffsetX = GetMinFLIM().X - start.GetMinPoint().X;
                        ScaleOffsetY = GetMinFLIM().Y - start.GetMinPoint().Y;
                        break;
                    case 5:
                        ScaleOffsetX = GetMaxFLIM().X - start.GetMaxPoint().X;
                        break;
                    case 6:
                        ScaleOffsetX = GetMinFLIM().X - start.GetMinPoint().X;
                        break;
                    case 7:
                        ScaleOffsetY = GetMaxFLIM().Y - start.GetMaxPoint().Y;
                        break;
                    case 8:
                        ScaleOffsetY = GetMinFLIM().Y - start.GetMinPoint().Y;
                        break;
                }
                NotNormPoint1 = new Point(NotNormPoint1.X - ScaleOffsetX, NotNormPoint1.Y - ScaleOffsetY);
                NotNormPoint2 = new Point(NotNormPoint2.X - ScaleOffsetX, NotNormPoint2.Y - ScaleOffsetY);
            }
        }
        internal override string GetFigureType()
        {
            return "Прямая линия";
        }
    }
}
