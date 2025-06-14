﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{

    internal interface IState
    {
        void PerformMouseDown(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent);
        void PerformMouseMove(object sender, MouseEventArgs e,Point scrollPosition, Form2 parent);
        void PerformMouseUp(object sender, MouseEventArgs e, Point scrollPosition, Form2 parent);
        void PerformPaint(object sender, Graphics e, Point scrollPosition, Form2 parent);
    }
}
