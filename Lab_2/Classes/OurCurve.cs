using Lab_2.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    [Serializable]
    internal class OurCurve : Figure
    {
        [NonSerialized] Pen pen;
        internal List<Point> TrueArray;
        internal OurCurve(Point point1, Point point2, Color colorLine, Color backGroundColor, bool backGroundFill, int lineThickness, List<Point> notNormPointArray) : base(point1, point2, colorLine, backGroundColor, backGroundFill, lineThickness)
        {
            NotNormPointArray = notNormPointArray;
            FakeNotNormPointArray = new List<Point>();
            foreach (Point p in notNormPointArray)
                FakeNotNormPointArray.Add(new Point(p.X, p.Y));
        }
        public override object Clone() => new OurCurve(Point1, Point2, LineColor, BackgroundColor, BackgroundFill, LineThickness, new List<Point>(FakeNotNormPointArray));
        internal override void Draw(Graphics g, Point scrollPosition)
        {
            TrueArray = new List<Point>();
            pen = new Pen(LineColor, LineThickness);
            foreach (Point point in NotNormPointArray)
                TrueArray.Add(new Point(point.X + scrollPosition.X, point.Y + scrollPosition.Y));
            if (TrueArray.Count > 1)
                g.DrawCurve(pen, TrueArray.ToArray());
        }
        internal override void DrawDash(Graphics g, Point scrollPosition)
        {
            TrueArray = new List<Point>();
            pen = new Pen(LineColor, LineThickness) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            foreach (Point point in NotNormPointArray)
                TrueArray.Add(new Point(point.X + scrollPosition.X, point.Y + scrollPosition.Y));
            if (TrueArray.Count > 1)
                g.DrawCurve(pen, TrueArray.ToArray());
        }
        internal override void DrawHide(Graphics g, Point scrollPosition)
        {
            TrueArray = new List<Point>();
            pen = new Pen(Color.White, LineThickness) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            foreach (Point point in NotNormPointArray)
                TrueArray.Add(new Point(point.X + scrollPosition.X, point.Y + scrollPosition.Y));
            if (TrueArray.Count > 1)
                g.DrawCurve(pen, TrueArray.ToArray());
        }
        internal override void FigureMove(Point offset)
        {
            NotNormPoint1 = new Point(NotNormPoint1.X + offset.X, NotNormPoint1.Y + offset.Y);
            NotNormPoint2 = new Point(NotNormPoint2.X + offset.X, NotNormPoint2.Y + offset.Y);
            Point1 = new Point(Point1.X + offset.X, Point1.Y + offset.Y);
            Point2 = new Point(Point2.X + offset.X, Point2.Y + offset.Y);
            for (int i = 0; i < NotNormPointArray.Count || i < FakeNotNormPointArray.Count; i++)
            {
                FakeNotNormPointArray[i] = new Point(FakeNotNormPointArray[i].X + offset.X, FakeNotNormPointArray[i].Y + offset.Y); //Перемещаем доп лист
                NotNormPointArray[i] = new Point(NotNormPointArray[i].X + offset.X, NotNormPointArray[i].Y + offset.Y); // перемещаем селект лист
            }
        }
        internal override void FigureMoveToStart(Point offset)
        {
            NotNormPoint1 = new Point(NotNormPoint1.X - offset.X, NotNormPoint1.Y - offset.Y);
            NotNormPoint2 = new Point(NotNormPoint2.X - offset.X, NotNormPoint2.Y - offset.Y);
            Point1 = new Point(Point1.X - offset.X, Point1.Y - offset.Y);
            Point2 = new Point(Point2.X - offset.X, Point2.Y - offset.Y);
            for (int i = 0; i < NotNormPointArray.Count || i < FakeNotNormPointArray.Count; i++)
            {
                FakeNotNormPointArray[i] = new Point(FakeNotNormPointArray[i].X - offset.X, FakeNotNormPointArray[i].Y - offset.Y); //Перемещаем доп лист
                NotNormPointArray[i] = new Point(NotNormPointArray[i].X - offset.X, NotNormPointArray[i].Y - offset.Y); // перемещаем селект лист
            }
        }
        
        
        internal override Point GetMinPoint()
        {
            if (NotNormPointArray.Count > 0)
            {
                int x_min = NotNormPointArray[0].X;
                int y_min = NotNormPointArray[0].Y;
                foreach (Point p in NotNormPointArray)
                {
                    if (x_min > p.X)
                        x_min = p.X;
                    if (y_min > p.Y)
                        y_min = p.Y;
                }
                Point minCoordinate = new Point(x_min, y_min);
                return minCoordinate;
            }
            else
            {
                return new Point(0, 0);
            }
        }
        internal override Point GetMaxPoint()
        {
            if (NotNormPointArray.Count > 0)
            {
                int x_max = NotNormPointArray[0].X;
                int y_max = NotNormPointArray[0].Y;
                foreach (Point p in NotNormPointArray)
                {
                    if (x_max < p.X)
                        x_max = p.X;
                    if (y_max < p.Y)
                        y_max = p.Y;
                }
                Point maxCoordinate = new Point(x_max, y_max);
                return maxCoordinate;
            }
            else
            {
                return new Point(0, 0);
            }
        }
        internal override Point GetMinPoint(List<Point> arrayPoint)
        {
            if (arrayPoint.Count > 0)
            {
                int x_min = arrayPoint[0].X;
                int y_min = arrayPoint[0].Y;
                foreach (Point p in arrayPoint)
                {
                    if (x_min > p.X)
                        x_min = p.X;
                    if (y_min > p.Y)
                        y_min = p.Y;
                }
                Point minCoordinate = new Point(x_min, y_min);
                return minCoordinate;
            }
            else
            {
                return new Point(0, 0);
            }
        }
        internal override Point GetMaxPoint(List<Point> arrayPoint)
        {
            if (arrayPoint.Count > 0)
            {
                int x_max = arrayPoint[0].X;
                int y_max = arrayPoint[0].Y;
                foreach (Point p in arrayPoint)
                {
                    if (x_max < p.X)
                        x_max = p.X;
                    if (y_max < p.Y)
                        y_max = p.Y;
                }
                Point maxCoordinate = new Point(x_max, y_max);
                return maxCoordinate;
            }
            else
            {
                return new Point(0, 0);
            }
        }
        internal override void AlignFigure(int gridStep)
        {
            int lastNum = NotNormPointArray.Count - 1;
            Point FPoint = new Point(NotNormPointArray[0].X, NotNormPointArray[0].Y);
            Point LPoint = new Point(NotNormPointArray[lastNum].X, NotNormPointArray[lastNum].Y);
            Point FRPoint = new Point((int)Math.Round((double)FPoint.X / gridStep) * gridStep, (int)Math.Round((double)FPoint.Y / gridStep) * gridStep);
            Point LRPoint = new Point((int)Math.Round((double)LPoint.X / gridStep) * gridStep, (int)Math.Round((double)LPoint.Y / gridStep) * gridStep);
            double Xcoef = (double)(LRPoint.X - FRPoint.X) / (LPoint.X - FPoint.X);
            double Ycoef = (double)(LRPoint.Y - FRPoint.Y) / (LPoint.Y - FPoint.Y);
            if (LRPoint.X == FRPoint.X)
                Xcoef = 1;
            if (LRPoint.Y == FRPoint.Y)
                Ycoef = 1;
            int OX = FakeNotNormPointArray[0].X;
            int OY = FakeNotNormPointArray[0].Y;
            for (int i = 0; i < NotNormPointArray.Count || i < FakeNotNormPointArray.Count; i++)
            {
                FakeNotNormPointArray[i] = new Point(FakeNotNormPointArray[i].X - OX, FakeNotNormPointArray[i].Y - OY);
                NotNormPointArray[i] = new Point(NotNormPointArray[i].X - OX, NotNormPointArray[i].Y - OY);
            }
            for (int i = 0; i < NotNormPointArray.Count || i < FakeNotNormPointArray.Count; i++)
            {
                FakeNotNormPointArray[i] = new Point((int)(FakeNotNormPointArray[i].X * Xcoef), (int)(FakeNotNormPointArray[i].Y * Ycoef));
                NotNormPointArray[i] = new Point((int)(NotNormPointArray[i].X * Xcoef), (int)(NotNormPointArray[i].Y * Ycoef));
            }
            OX = FRPoint.X;
            OY = FRPoint.Y;
            for (int i = 0; i < NotNormPointArray.Count || i < FakeNotNormPointArray.Count; i++)
            {
                FakeNotNormPointArray[i] = new Point(FakeNotNormPointArray[i].X + OX, FakeNotNormPointArray[i].Y + OY);
                NotNormPointArray[i] = new Point(NotNormPointArray[i].X + OX, NotNormPointArray[i].Y + OY);
            }
            for (int i = 0; i < NotNormPointArray.Count || i < FakeNotNormPointArray.Count; i++)
            {
                if (FakeNotNormPointArray[i].X < 0)
                    FakeNotNormPointArray[i] = new Point(0, FakeNotNormPointArray[i].Y);
                if (FakeNotNormPointArray[i].Y < 0)
                    FakeNotNormPointArray[i] = new Point(FakeNotNormPointArray[i].X, 0);
                if (NotNormPointArray[i].X < 0)
                    NotNormPointArray[i] = new Point(0, NotNormPointArray[i].Y);
                if (NotNormPointArray[i].Y < 0)
                    NotNormPointArray[i] = new Point(NotNormPointArray[i].X, 0);
            }
        }
        internal override void AddEditRectangles(List<Figure> rectangles, Point scrollPosition)
        {
            rectangles.Add(new OurRectangle(new Point(GetMinPoint().X + 20, GetMinPoint().Y + 20), new Point(GetMinPoint().X - 20, GetMinPoint().Y - 20), Color.Black, Color.White, false, 1));
            rectangles.Add(new OurRectangle(new Point(GetMaxPoint().X + 20, GetMinPoint().Y + 20), new Point(GetMaxPoint().X - 20, GetMinPoint().Y - 20), Color.Black, Color.White, false, 1));
            rectangles.Add(new OurRectangle(new Point(GetMinPoint().X + 20, GetMaxPoint().Y + 20), new Point(GetMinPoint().X - 20, GetMaxPoint().Y - 20), Color.Black, Color.White, false, 1));
            rectangles.Add(new OurRectangle(new Point(GetMaxPoint().X + 20, GetMaxPoint().Y + 20), new Point(GetMaxPoint().X - 20, GetMaxPoint().Y - 20), Color.Black, Color.White, false, 1));
            rectangles.Add(new OurRectangle(new Point(GetMinPoint().X + 20, ((GetMaxPoint().Y - GetMinPoint().Y) / 2) + GetMinPoint().Y + 20), new Point(GetMinPoint().X - 20, ((GetMaxPoint().Y - GetMinPoint().Y) / 2) + GetMinPoint().Y - 20), Color.Black, Color.White, false, 1));
            rectangles.Add(new OurRectangle(new Point(GetMaxPoint().X + 20, ((GetMaxPoint().Y - GetMinPoint().Y) / 2) + GetMinPoint().Y + 20), new Point(GetMaxPoint().X - 20, ((GetMaxPoint().Y - GetMinPoint().Y) / 2) + GetMinPoint().Y - 20), Color.Black, Color.White, false, 1));
            rectangles.Add(new OurRectangle(new Point(((GetMaxPoint().X - GetMinPoint().X) / 2) + GetMinPoint().X + 20, GetMinPoint().Y + 20), new Point(((GetMaxPoint().X - GetMinPoint().X) / 2) + GetMinPoint().X - 20, GetMinPoint().Y - 20), Color.Black, Color.White, false, 1));
            rectangles.Add(new OurRectangle(new Point(((GetMaxPoint().X - GetMinPoint().X) / 2) + GetMinPoint().X + 20, GetMaxPoint().Y + 20), new Point(((GetMaxPoint().X - GetMinPoint().X) / 2) + GetMinPoint().X - 20, GetMaxPoint().Y - 20), Color.Black, Color.White, false, 1));
        }
        internal override void Edit(Point scrollPosition, Graphics g, Point movePoint, List<Figure> rectangles, Point offset, Figure startFig, bool withoutMove)
        {
            Point min = new Point(GetMinPoint().X, GetMinPoint().Y);
            Point max = new Point(GetMaxPoint().X, GetMaxPoint().Y);
            int whereOffset = 0;
            if (rectangles[0].Check(scrollPosition, movePoint) && !rectangles[1].Check(scrollPosition, movePoint) && !rectangles[2].Check(scrollPosition, movePoint) && !rectangles[3].Check(scrollPosition, movePoint) && !rectangles[4].Check(scrollPosition, movePoint) && !rectangles[5].Check(scrollPosition, movePoint) && !rectangles[6].Check(scrollPosition, movePoint) && !rectangles[7].Check(scrollPosition, movePoint) && !rectangles[8].Check(scrollPosition, movePoint))
                FigureMove(offset); //Не попадает в квадраты, т е двигается
            else if (rectangles[1].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[1].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, startFig.GetMaxPoint().Y); // 
                min = new Point(movePoint.X, movePoint.Y);
                whereOffset = 1;
            }
            else if (rectangles[2].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[2].Check(scrollPosition, movePoint)))
            {
                max = new Point(movePoint.X, startFig.GetMaxPoint().Y);
                min = new Point(startFig.GetMinPoint().X, movePoint.Y);
                whereOffset = 2;
            }
            else if (rectangles[3].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[3].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, movePoint.Y);
                min = new Point(movePoint.X, startFig.GetMinPoint().Y);
                whereOffset = 3;
            }
            else if (rectangles[4].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[4].Check(scrollPosition, movePoint)))
            {
                max = new Point(movePoint.X, movePoint.Y);
                min = new Point(startFig.GetMinPoint().X, startFig.GetMinPoint().Y);
                whereOffset = 4;
            }
            else if (rectangles[5].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[5].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, startFig.GetMaxPoint().Y);
                min = new Point(movePoint.X, startFig.GetMinPoint().Y);
                whereOffset = 5;
            }
            else if (rectangles[6].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[6].Check(scrollPosition, movePoint)))
            {
                max = new Point(movePoint.X, startFig.GetMaxPoint().Y);
                min = new Point(startFig.GetMinPoint().X, startFig.GetMinPoint().Y);
                whereOffset = 6;
            }
            else if (rectangles[7].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[7].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, startFig.GetMaxPoint().Y);
                min = new Point(startFig.GetMinPoint().X, movePoint.Y);
                whereOffset = 7;
            }
            else if (rectangles[8].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[8].Check(scrollPosition, movePoint)))
            {
                max = new Point(startFig.GetMaxPoint().X, movePoint.Y);
                min = new Point(startFig.GetMinPoint().X, startFig.GetMinPoint().Y);
                whereOffset = 8;
            }
            double Xcoef = (max.X - min.X) / (double)(GetMaxPoint(startFig.NotNormPointArray).X - GetMinPoint(startFig.NotNormPointArray).X);
            double Ycoef = (max.Y - min.Y) / (double)(GetMaxPoint(startFig.NotNormPointArray).Y - GetMinPoint(startFig.NotNormPointArray).Y);
            EditCurve(startFig, Xcoef, Ycoef, movePoint, whereOffset);
            //
            int rectanglesCount = rectangles.Count;
            for (int i = 0; i < rectanglesCount - 1; i++) //Удаляем квадраты, чтобы добавить новые
                rectangles.Remove(rectangles[1]);
            //rectangles.Clear();
            AddEditRectangles(rectangles, scrollPosition);
        }
        internal void EditCurve(Figure start, double X_coef, double Y_coef, Point move, int where)
        {
            if (X_coef != 1 || Y_coef != 1)
            {
                int scaleOffsetX = 0;
                int scaleOffsetY = 0;
                List<Point> startArray = start.NotNormPointArray;
                for (int i = 0; i < NotNormPointArray.Count; i++)
                {
                    FakeNotNormPointArray[i] = new Point((int)(startArray[i].X * (double)X_coef), (int)(startArray[i].Y * (double)Y_coef));
                    NotNormPointArray[i] = new Point((int)(startArray[i].X * (double)X_coef), (int)(startArray[i].Y * (double)Y_coef));
                }
                //Прижимаем
                switch (where)
                {
                    case 1:
                        scaleOffsetX = GetMinPoint().X - move.X;
                        scaleOffsetY = GetMinPoint().Y - move.Y;
                        break;
                    case 2:
                        scaleOffsetX = GetMaxPoint().X - move.X;
                        scaleOffsetY = GetMinPoint().Y - move.Y;
                        break;
                    case 3:
                        scaleOffsetX = GetMinPoint().X - move.X;
                        scaleOffsetY = GetMaxPoint().Y - move.Y;
                        break;
                    case 4:
                        scaleOffsetX = GetMaxPoint().X - move.X;
                        scaleOffsetY = GetMaxPoint().Y - move.Y;
                        break;
                    case 5:
                        scaleOffsetX = GetMinPoint().X - move.X;
                        break;
                    case 6:
                        scaleOffsetX = GetMaxPoint().X - move.X;
                        break;
                    case 7:
                        scaleOffsetY = GetMinPoint().Y - move.Y;
                        break;
                    case 8:
                        scaleOffsetY = GetMaxPoint().Y - move.Y;
                        break;
                }
                for (int i = 0; i < NotNormPointArray.Count; i++)
                {
                    FakeNotNormPointArray[i] = new Point(FakeNotNormPointArray[i].X - scaleOffsetX, FakeNotNormPointArray[i].Y - scaleOffsetY);
                    NotNormPointArray[i] = new Point(NotNormPointArray[i].X - scaleOffsetX, NotNormPointArray[i].Y - scaleOffsetY);
                }
            }
        }
        internal override Control[] GetControls()
        {
            base.GetControls();
            Label LabelPointArray = new Label()
            {
                Text = "Массив из точек",
                Size = new Size(126, 22),
            };

            Point[] array = new Point[NotNormPointArray.Count];
            Button TextPointArray = new Button()
            {
                Tag = "",
                Text = "Показать",
                Size = new Size(126, 60),
            };
            for (int i = 0; i < NotNormPointArray.Count; i++)
            {
                TextPointArray.Tag += NotNormPointArray[i].X +";"+ NotNormPointArray[i].Y + ",\r\n";
            }
            TextPointArray.MouseClick += (object sender, MouseEventArgs e) =>
            {
                PointArrayEditor forma = new PointArrayEditor(TextPointArray.Tag.ToString(),TextPointArray);
                DialogResult result = forma.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //TextPointArray.Text= forma.Text;
                }
            };
            //MessageBox.Show(TextPointArray.Text);
            //MessageBox.Show(TextPointArray.Text.Split(';', ',')[2]);
            controls[18] = LabelPointArray;
            controls[19] = TextPointArray;
            return controls;
        }
        internal override string GetFigureType()
        {
            return "Кривая линия";
        }
    }
}
