using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Lab_2
{
    [Serializable]
    class OurText : Figure
    {
        [NonSerialized] Pen pen;
        [NonSerialized] internal Form2 Parent;
        [NonSerialized] internal bool txtbool;
        private Rectangle ClassRectangle = new Rectangle();
        internal OurText(Point point1, Point point2, Color colorLine, Color backGroundColor, bool backGroundFill, int lineThickness, Font font, Form2 parent) : base(point1, point2, colorLine, backGroundColor, backGroundFill, lineThickness)
        {
            Parent = parent;
            Font = font;
            txtbool = true;
        }
        internal override void Draw(Graphics g, Point scrollPosition)
        {
            if (Parent.SnapGrid)
                AlignFigure(Parent._parent.GridStep);
            Point TextPoint1 = new Point(Point1.X + scrollPosition.X, Point1.Y + scrollPosition.Y);
            Point TextPoint2 = new Point(Point2.X + scrollPosition.X, Point2.Y + scrollPosition.Y);
            if (txtbool)
            {
                TextBox textBox = new TextBox()
                {
                    Location = TextPoint1,
                    Size = new Size(TextPoint2.X - TextPoint1.X, TextPoint2.Y - TextPoint1.Y),
                    Parent = Parent,
                    Multiline = true,
                    Font = Font,
                    ForeColor = LineColor,
                };
                textBox.KeyDown += new KeyEventHandler(Click);
                textBox.Focus();
                textBox.Show();
                txtbool = false;
            }
            else
            {
                Brush brush = new SolidBrush(LineColor);
                g.DrawString(InnerText, Font, brush, Point1);
            }
        }
        public void Click(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                InnerText = textBox.Text;
                textBox.Dispose();
            }
        }
        internal override void DrawDash(Graphics g, Point scrollPosition)
        {
            //Point TextPoint1 = new Point(Point1.X + scrollPosition.X, Point1.Y + scrollPosition.Y);
            Brush brush = new SolidBrush(LineColor);
            pen = new Pen(LineColor, LineThickness) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            ClassRectangle = Rectangle.FromLTRB(Point1.X + scrollPosition.X, Point1.Y + scrollPosition.Y, Point2.X + scrollPosition.X, Point2.Y + scrollPosition.Y);
            g.DrawRectangle(pen, ClassRectangle);
            g.DrawString(InnerText, Font, brush, Point1);
        }
        internal override void DrawHide(Graphics g, Point scrollPosition)
        {
            pen = new Pen(LineColor, LineThickness) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            ClassRectangle = Rectangle.FromLTRB(Point1.X + scrollPosition.X, Point1.Y + scrollPosition.Y, Point2.X + scrollPosition.X, Point2.Y + scrollPosition.Y);
            g.DrawRectangle(pen, ClassRectangle);
        }
        internal override void Edit(Point scrollPosition, Graphics g, Point movePoint, List<Figure> rectangles, Point offset, Figure startFig, bool withoutMove)
        {
            base.Edit(scrollPosition, g, movePoint, rectangles, offset, startFig, withoutMove);
        }
        internal override void TextEditing(Point scrollPosition, bool withoutMove)
        {
            if (withoutMove)
            {
                Point TextPoint1 = new Point(Point1.X + scrollPosition.X, Point1.Y + scrollPosition.Y);
                Point TextPoint2 = new Point(Point2.X + scrollPosition.X, Point2.Y + scrollPosition.Y);

                TextBox textBox1 = new TextBox()
                {
                    Location = TextPoint1,
                    Size = new Size(TextPoint2.X - TextPoint1.X, TextPoint2.Y - TextPoint1.Y),
                    Parent = Parent,
                    Multiline = true,
                    Font = this.Font,
                    ForeColor = LineColor,
                };
                textBox1.KeyDown += new KeyEventHandler(Click);
                textBox1.Focus();
                textBox1.Show();
            }
        }
        internal override Control[] GetControls()
        {
            base.GetControls();
            Label LabelFont = new Label()
            {
                Text = "Шрифт",
                Size = new Size(126, 22),
            };
            TextBox TextFont = new TextBox()
            {
                Text = Font.FontFamily.ToString() + "," + Font.Size.ToString(),
                Tag = Font,
                Size = new Size(126, 22),
            };
            TextFont.MouseClick += (object sender, MouseEventArgs e) =>
            {
                FontDialog choiceFont = new FontDialog();
                DialogResult result = choiceFont.ShowDialog();
                if (result == DialogResult.OK)
                {
                    TextFont.Tag = choiceFont.Font;
                    TextFont.Text = choiceFont.Font.FontFamily.ToString()+","+ choiceFont.Font.Size.ToString();
                    //TextFont.Text = choiceFont.ToString();
                }
            };
            Label LabelInnerText = new Label()
            {
                Text = "Текст",
                Size = new Size(126, 22),
            };
            TextBox TextInnerText = new TextBox()
            {
                Text = InnerText,
                Size = new Size(126, 22),
            };
            controls[14] = LabelFont;
            controls[15] = TextFont;
            controls[16] = LabelInnerText;
            controls[17] = TextInnerText;

            return controls;
        }
        internal override string GetFigureType()
        {
            return "Текст";
        }

    }
}
