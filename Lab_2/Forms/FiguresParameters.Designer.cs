namespace Lab_2
{
    partial class FiguresParameters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("333", System.Windows.Forms.HorizontalAlignment.Left);
            this.ListViewFigures = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Figure = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartPoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Delete = new System.Windows.Forms.Button();
            this.Create = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.OneDown = new System.Windows.Forms.Button();
            this.OneUp = new System.Windows.Forms.Button();
            this.FullDown = new System.Windows.Forms.Button();
            this.FullUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListViewFigures
            // 
            this.ListViewFigures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Figure,
            this.StartPoint});
            this.ListViewFigures.FullRowSelect = true;
            this.ListViewFigures.GridLines = true;
            listViewGroup5.Header = "333";
            listViewGroup5.Name = "listViewGroup1";
            this.ListViewFigures.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup5});
            this.ListViewFigures.HideSelection = false;
            this.ListViewFigures.Location = new System.Drawing.Point(12, 12);
            this.ListViewFigures.MultiSelect = false;
            this.ListViewFigures.Name = "ListViewFigures";
            this.ListViewFigures.Size = new System.Drawing.Size(523, 400);
            this.ListViewFigures.TabIndex = 0;
            this.ListViewFigures.UseCompatibleStateImageBehavior = false;
            this.ListViewFigures.View = System.Windows.Forms.View.Details;
            this.ListViewFigures.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // ID
            // 
            this.ID.Tag = "ID";
            this.ID.Text = "ID";
            this.ID.Width = 30;
            // 
            // Figure
            // 
            this.Figure.Tag = "";
            this.Figure.Text = "Figure";
            this.Figure.Width = 237;
            // 
            // StartPoint
            // 
            this.StartPoint.Text = "StartPoint";
            this.StartPoint.Width = 247;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 418);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(523, 256);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.Tag = "";
            // 
            // Delete
            // 
            this.Delete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Delete.Location = new System.Drawing.Point(541, 510);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(100, 40);
            this.Delete.TabIndex = 14;
            this.Delete.Text = "Удалить";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Create
            // 
            this.Create.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Create.Location = new System.Drawing.Point(541, 418);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(100, 40);
            this.Create.TabIndex = 13;
            this.Create.Text = "Создать";
            this.Create.UseVisualStyleBackColor = true;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // Save
            // 
            this.Save.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Save.Location = new System.Drawing.Point(541, 464);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(100, 40);
            this.Save.TabIndex = 12;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Ok
            // 
            this.Ok.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Ok.Location = new System.Drawing.Point(541, 556);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(100, 40);
            this.Ok.TabIndex = 11;
            this.Ok.Text = "OK";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // OneDown
            // 
            this.OneDown.Location = new System.Drawing.Point(541, 104);
            this.OneDown.Name = "OneDown";
            this.OneDown.Size = new System.Drawing.Size(100, 40);
            this.OneDown.TabIndex = 18;
            this.OneDown.Text = "На 1 вниз";
            this.OneDown.UseVisualStyleBackColor = true;
            this.OneDown.Click += new System.EventHandler(this.OneDown_Click);
            // 
            // OneUp
            // 
            this.OneUp.Location = new System.Drawing.Point(541, 58);
            this.OneUp.Name = "OneUp";
            this.OneUp.Size = new System.Drawing.Size(100, 40);
            this.OneUp.TabIndex = 17;
            this.OneUp.Text = "На 1 вверх";
            this.OneUp.UseVisualStyleBackColor = true;
            this.OneUp.Click += new System.EventHandler(this.OneUp_Click);
            // 
            // FullDown
            // 
            this.FullDown.Location = new System.Drawing.Point(541, 150);
            this.FullDown.Name = "FullDown";
            this.FullDown.Size = new System.Drawing.Size(100, 40);
            this.FullDown.TabIndex = 16;
            this.FullDown.Text = "В конец";
            this.FullDown.UseVisualStyleBackColor = true;
            this.FullDown.Click += new System.EventHandler(this.FullDown_Click);
            // 
            // FullUp
            // 
            this.FullUp.Location = new System.Drawing.Point(541, 12);
            this.FullUp.Name = "FullUp";
            this.FullUp.Size = new System.Drawing.Size(100, 40);
            this.FullUp.TabIndex = 15;
            this.FullUp.Text = "В начало";
            this.FullUp.UseVisualStyleBackColor = true;
            this.FullUp.Click += new System.EventHandler(this.FullUp_Click);
            // 
            // FiguresParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 693);
            this.Controls.Add(this.OneDown);
            this.Controls.Add(this.OneUp);
            this.Controls.Add(this.FullDown);
            this.Controls.Add(this.FullUp);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Create);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ListViewFigures);
            this.Name = "FiguresParameters";
            this.Text = "FiguresEditList";
            this.Load += new System.EventHandler(this.FiguresEditList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListViewFigures;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Figure;
        private System.Windows.Forms.ColumnHeader StartPoint;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button Create;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button OneDown;
        private System.Windows.Forms.Button OneUp;
        private System.Windows.Forms.Button FullDown;
        private System.Windows.Forms.Button FullUp;
    }
}