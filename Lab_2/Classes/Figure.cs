using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Lab_2
{
    [Serializable]
    abstract class Figure : ICloneable
    {
        internal List<Point> NotNormPointArray, FakeNotNormPointArray;
        public Point Point1, Point2, NotNormPoint1, NotNormPoint2;
        internal Color LineColor, BackgroundColor;
        internal bool BackgroundFill;
        internal int LineThickness;
        internal Font Font;
        internal string InnerText;
        internal Control[] controls = new Control[20];
        private protected Figure(Point point1, Point point2, Color lineColor, Color backgroundColor, bool backgroundFill, int lineThickness)
        {
            Point1 = new Point(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y));
            Point2 = new Point(Math.Max(point1.X, point2.X), Math.Max(point1.Y, point2.Y));
            NotNormPoint1 = point1;
            NotNormPoint2 = point2;
            LineColor = lineColor;
            BackgroundColor = backgroundColor;
            BackgroundFill = backgroundFill;
            LineThickness = lineThickness;
        }
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
        internal abstract void Draw(Graphics g, Point scrollPosition);
        internal abstract void DrawDash(Graphics g, Point scrollPosition);
        internal abstract void DrawHide(Graphics g, Point scrollPosition);
        internal bool Check(Graphics g, Point scrollPosition, Figure f)
        {
            Pen pen = new Pen(Color.Blue, 3) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            Point FigPoint1 = new Point(f.GetMinPoint().X + scrollPosition.X, f.GetMinPoint().Y + scrollPosition.Y);
            Point FigPoint2 = new Point(f.GetMaxPoint().X + scrollPosition.X, f.GetMaxPoint().Y + scrollPosition.Y);
            Rectangle fgr = new Rectangle(FigPoint1.X, FigPoint1.Y, FigPoint2.X - FigPoint1.X, FigPoint2.Y - FigPoint1.Y); //Чтобы использовать метод IntersectsWith преобразуем Figure в Rectangle обращаясь к значением её координат и проводим нормализацию
            Rectangle CheckRectangle = Rectangle.FromLTRB(Point1.X + scrollPosition.X, Point1.Y + scrollPosition.Y, Point2.X + scrollPosition.X, Point2.Y + scrollPosition.Y); //Преобразуем прямоугольную область выделения OurRectangle в Rectangle для того, чтобы использовать метод IntersectsWith 
            g.DrawRectangle(pen, CheckRectangle);
            if (CheckRectangle.IntersectsWith(fgr))
                g.DrawRectangle(pen, fgr);
            return CheckRectangle.IntersectsWith(fgr);
        }
        internal bool Check(Point scrollPosition, Point p)
        {
            Rectangle CheckRectangle = Rectangle.FromLTRB(GetMinPoint().X + scrollPosition.X, GetMinPoint().Y + scrollPosition.Y, GetMaxPoint().X + scrollPosition.X, GetMaxPoint().Y + scrollPosition.Y);
            Point p1 = new Point(p.X + scrollPosition.X, p.Y + scrollPosition.Y);
            return CheckRectangle.Contains(p1);
        }
        internal virtual void FigureMove(Point offset)
        {
            Point1 = new Point(Point1.X + offset.X, Point1.Y + offset.Y);
            Point2 = new Point(Point2.X + offset.X, Point2.Y + offset.Y);
        }
        internal virtual void FigureMoveToStart(Point offset)
        {
            Point1.X -= offset.X;
            Point1.Y -= offset.Y;
            Point2.X -= offset.X;
            Point2.Y -= offset.Y;
            NotNormPoint1.X -= offset.X;
            NotNormPoint1.Y -= offset.Y;
            NotNormPoint2.X -= offset.X;
            NotNormPoint2.Y -= offset.Y;
        }

        internal virtual void AlignFigure(int gridStep)
        {

            Point1.X = (int)Math.Round((double)Point1.X / gridStep) * gridStep;
            Point1.Y = (int)Math.Round((double)Point1.Y / gridStep) * gridStep;
            Point2.X = (int)Math.Round((double)Point2.X / gridStep) * gridStep;
            Point2.Y = (int)Math.Round((double)Point2.Y / gridStep) * gridStep;
            NotNormPoint1.X = (int)Math.Round((double)NotNormPoint1.X / gridStep) * gridStep;
            NotNormPoint1.Y = (int)Math.Round((double)NotNormPoint1.Y / gridStep) * gridStep;
            NotNormPoint2.X = (int)Math.Round((double)NotNormPoint2.X / gridStep) * gridStep;
            NotNormPoint2.Y = (int)Math.Round((double)NotNormPoint2.Y / gridStep) * gridStep;
        }
        internal virtual Point GetMinPoint()
        {
            return Point1;
        }
        internal virtual Point GetMaxPoint()
        {
            return Point2;
        }
        internal virtual Point GetMinPoint(List<Point> arrayPoint)
        {
            return new Point(0, 0);
        }
        internal virtual Point GetMaxPoint(List<Point> arrayPoint)
        {
            return new Point(0, 0);
        }
        internal virtual void Edit(Point scrollPosition, Graphics g, Point movePoint, List<Figure> rectangles, Point offset, Figure startFig, bool withoutMove)
        {
            if (!withoutMove && rectangles[0].Check(scrollPosition, movePoint) && !rectangles[1].Check(scrollPosition, movePoint) && !rectangles[2].Check(scrollPosition, movePoint) && !rectangles[3].Check(scrollPosition, movePoint) && !rectangles[4].Check(scrollPosition, movePoint) && !rectangles[5].Check(scrollPosition, movePoint) && !rectangles[6].Check(scrollPosition, movePoint) && !rectangles[7].Check(scrollPosition, movePoint) && !rectangles[8].Check(scrollPosition, movePoint))
                FigureMove(offset);
            else if (rectangles[1].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[1].Check(scrollPosition, movePoint)))
            {
                Point1 = new Point(movePoint.X, movePoint.Y);
                NotNormPoint1 = new Point(movePoint.X, movePoint.Y);
            }
            else if (rectangles[2].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[2].Check(scrollPosition, movePoint)))
            {
                Point2.X = movePoint.X;
                Point1.Y = movePoint.Y;
                NotNormPoint2.X = movePoint.X;
                NotNormPoint1.Y = movePoint.Y;
            }
            else if (rectangles[3].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[3].Check(scrollPosition, movePoint)))
            {
                Point1.X = movePoint.X;
                Point2.Y = movePoint.Y;
                NotNormPoint1.X = movePoint.X;
                NotNormPoint2.Y = movePoint.Y;
            }
            else if (rectangles[4].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[4].Check(scrollPosition, movePoint)))
            {
                Point2 = new Point(movePoint.X, movePoint.Y);
                NotNormPoint2 = new Point(movePoint.X, movePoint.Y);
            }
            else if (rectangles[5].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[5].Check(scrollPosition, movePoint)))
            {
                Point1 = new Point(movePoint.X, Point1.Y);
                NotNormPoint1 = new Point(movePoint.X, NotNormPoint1.Y);
            }
            else if (rectangles[6].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[6].Check(scrollPosition, movePoint)))
            {
                Point2 = new Point(movePoint.X, Point2.Y);
                NotNormPoint2 = new Point(movePoint.X, NotNormPoint2.Y);
            }
            else if (rectangles[7].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[7].Check(scrollPosition, movePoint)))
            {
                Point1 = new Point(Point1.X, movePoint.Y);
                NotNormPoint1 = new Point(NotNormPoint1.X, movePoint.Y);
            }
            else if (rectangles[8].Check(scrollPosition, movePoint) || (rectangles[0].Check(scrollPosition, movePoint) && rectangles[8].Check(scrollPosition, movePoint)))
            {
                Point2 = new Point(Point2.X, movePoint.Y);
                NotNormPoint2 = new Point(NotNormPoint2.X, movePoint.Y);
            }
            //
            int rectanglesCount = rectangles.Count;
            for (int i = 0; i < rectanglesCount - 1; i++) //Удаляем квадраты, чтобы добавить новые
                rectangles.Remove(rectangles[1]);
            AddEditRectangles(rectangles, scrollPosition);
        }
        internal virtual void AddEditRectangles(List<Figure> rectangles, Point scrollPosition)
        {
            rectangles.Add(new OurRectangle(new Point(Point1.X + 20, Point1.Y + 20), new Point(Point1.X - 20, Point1.Y - 20), Color.Black, Color.White, false, 1)); //LeftTop
            rectangles.Add(new OurRectangle(new Point(Point2.X + 20, Point1.Y + 20), new Point(Point2.X - 20, Point1.Y - 20), Color.Black, Color.White, false, 1)); //RigthTop
            rectangles.Add(new OurRectangle(new Point(Point1.X + 20, Point2.Y + 20), new Point(Point1.X - 20, Point2.Y - 20), Color.Black, Color.White, false, 1)); //LeftBottom
            rectangles.Add(new OurRectangle(new Point(Point2.X + 20, Point2.Y + 20), new Point(Point2.X - 20, Point2.Y - 20), Color.Black, Color.White, false, 1)); //RigthBottom
            rectangles.Add(new OurRectangle(new Point(Point1.X + 20, ((Point2.Y - Point1.Y) / 2) + Point1.Y + 20), new Point(Point1.X - 20, ((Point2.Y - Point1.Y) / 2) + Point1.Y - 20), Color.Black, Color.White, false, 1)); //LeftMiddle
            rectangles.Add(new OurRectangle(new Point(Point2.X + 20, ((Point2.Y - Point1.Y) / 2) + Point1.Y + 20), new Point(Point2.X - 20, ((Point2.Y - Point1.Y) / 2) + Point1.Y - 20), Color.Black, Color.White, false, 1)); //RigthMiddle
            rectangles.Add(new OurRectangle(new Point(((Point2.X - Point1.X) / 2) + Point1.X + 20, Point1.Y + 20), new Point(((Point2.X - Point1.X) / 2) + Point1.X - 20, Point1.Y - 20), Color.Black, Color.White, false, 1)); //TopMiddle
            rectangles.Add(new OurRectangle(new Point(((Point2.X - Point1.X) / 2) + Point1.X + 20, Point2.Y + 20), new Point(((Point2.X - Point1.X) / 2) + Point1.X - 20, Point2.Y - 20), Color.Black, Color.White, false, 1)); //BottomMiddle
        }
        internal virtual bool AntiRoll(List<Figure> rectangles)
        {
            if (rectangles[1].Point2.X < rectangles[4].Point1.X || rectangles[1].Point2.Y < rectangles[4].Point1.Y)
                return false;
            else
                return true;
        }
        internal virtual void TextEditing(Point scrollPosition, bool withoutMove)
        {

        }
        internal virtual Control[] GetControls()
        {   
            Label Label1X = new Label()
            {
                Text = "X1",
                Size = new Size(128, 22),
            };
            TextBox Text1X = new TextBox()
            {
                Text = Point1.X.ToString(),
                Size = new Size(128, 22),
            };
            Label Label1Y = new Label()
            {
                Text = "Y1",
                Size = new Size(128, 22),
            };
            TextBox Text1Y = new TextBox()
            {
                Text = Point1.Y.ToString(),
                Size = new Size(128, 22),
            };
            Label Label2X = new Label()
            {
                Text = "X2",
                Size = new Size(128, 22),
            };
            TextBox Text2X = new TextBox()
            {
                Text = Point2.X.ToString(),
                Size = new Size(128, 22),
            };
            Label Label2Y = new Label()
            {
                Text = "Y2",
                Size = new Size(128, 22),
            };
            TextBox Text2Y = new TextBox()
            {
                Text = Point2.Y.ToString(),
                Size = new Size(128, 22),
            };
            Label ThicknessLabel = new Label()
            {
                Text = "Толщина линии",
                Size = new Size(128, 22),
            };
            TextBox ThicknessText = new TextBox()
            {
                UseWaitCursor = false,
                ReadOnly = true,
                Text = LineThickness.ToString(),
                Size = new Size(128, 22),
            };
            ThicknessText.MouseClick += (object sender, MouseEventArgs e) =>
            {
                LineThicknessForm choiceThickness = new LineThicknessForm();
                DialogResult result = choiceThickness.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ThicknessText.Text = choiceThickness.LineThickness.ToString();
                }
            };
            Label LineColorLabel = new Label()
            {
                Text = "Цвет Линии",
                Size = new Size(128, 22),
            };
            TextBox ColorText = new TextBox()
            {
                UseWaitCursor = false,
                ReadOnly = true,
                Size = new Size(128,22),
                BackColor = LineColor,
            };
            ColorText.MouseClick += (object sender, MouseEventArgs e) =>
            {
                ColorDialog choiceColor = new ColorDialog();
                DialogResult result = choiceColor.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ColorText.BackColor = choiceColor.Color;
                }
            };
            if (BackgroundFill)
            {
                Label BackgroundColorLabel = new Label()
                {
                    Text = "Цвет Фона",
                    Size = new Size(126, 22),
                };
                TextBox BackgroundColorText = new TextBox()
                {
                    UseWaitCursor = false,
                    ReadOnly = true,
                    Size = new Size(126, 22),
                    BackColor = BackgroundColor,
                };
                BackgroundColorText.MouseClick += (object sender, MouseEventArgs e) =>
                {
                    ColorDialog choiceColor = new ColorDialog();
                    DialogResult result = choiceColor.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        BackgroundColorText.BackColor = choiceColor.Color;
                    }
                };
                controls[12] = BackgroundColorLabel;
                controls[13] = BackgroundColorText;
            }
            controls[0] = Label1X;
            controls[1] = Text1X;
            controls[2] = Label1Y;
            controls[3] = Text1Y;
            controls[4] = Label2X;
            controls[5] = Text2X;
            controls[6] = Label2Y;
            controls[7] = Text2Y;
            //
            controls[8] = ThicknessLabel;
            controls[9] = ThicknessText;
            controls[10] = LineColorLabel;
            controls[11] = ColorText;
            return controls;
        }
        internal virtual string GetFigureType()
        {
            return "Фигура";
        }
    }
}