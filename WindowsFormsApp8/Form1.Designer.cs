namespace WindowsFormsApp8
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CircleButt = new System.Windows.Forms.ToolStripButton();
            this.RectButt = new System.Windows.Forms.ToolStripButton();
            this.LineButt = new System.Windows.Forms.ToolStripButton();
            this.TriangleButt = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.GroupButt = new System.Windows.Forms.ToolStripButton();
            this.UngroupButt = new System.Windows.Forms.ToolStripButton();
            this.SaveButt = new System.Windows.Forms.ToolStripButton();
            this.LoadButt = new System.Windows.Forms.ToolStripButton();
            this.UndoB = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.myPic = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myPic)).BeginInit();
            this.SuspendLayout();
            // 
            // CircleButt
            // 
            this.CircleButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CircleButt.Image = ((System.Drawing.Image)(resources.GetObject("CircleButt.Image")));
            this.CircleButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CircleButt.Name = "CircleButt";
            this.CircleButt.Size = new System.Drawing.Size(26, 26);
            this.CircleButt.Text = "Circle";
            this.CircleButt.Click += new System.EventHandler(this.CircleButt_Click);
            // 
            // RectButt
            // 
            this.RectButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RectButt.Image = ((System.Drawing.Image)(resources.GetObject("RectButt.Image")));
            this.RectButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RectButt.Name = "RectButt";
            this.RectButt.Size = new System.Drawing.Size(26, 26);
            this.RectButt.Text = "Rectangle";
            this.RectButt.Click += new System.EventHandler(this.RectButt_Click);
            // 
            // LineButt
            // 
            this.LineButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LineButt.Image = ((System.Drawing.Image)(resources.GetObject("LineButt.Image")));
            this.LineButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LineButt.Name = "LineButt";
            this.LineButt.Size = new System.Drawing.Size(26, 26);
            this.LineButt.Text = "Line";
            this.LineButt.Click += new System.EventHandler(this.LineButt_Click);
            // 
            // TriangleButt
            // 
            this.TriangleButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TriangleButt.Image = ((System.Drawing.Image)(resources.GetObject("TriangleButt.Image")));
            this.TriangleButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TriangleButt.Name = "TriangleButt";
            this.TriangleButt.Size = new System.Drawing.Size(26, 26);
            this.TriangleButt.Text = "Triangle";
            this.TriangleButt.Click += new System.EventHandler(this.TriangleButt_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CircleButt,
            this.RectButt,
            this.LineButt,
            this.TriangleButt,
            this.GroupButt,
            this.UngroupButt,
            this.SaveButt,
            this.LoadButt,
            this.UndoB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(754, 29);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // GroupButt
            // 
            this.GroupButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.GroupButt.Image = ((System.Drawing.Image)(resources.GetObject("GroupButt.Image")));
            this.GroupButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GroupButt.Name = "GroupButt";
            this.GroupButt.Size = new System.Drawing.Size(44, 26);
            this.GroupButt.Text = "Group";
            this.GroupButt.Click += new System.EventHandler(this.GroupButt_Click);
            // 
            // UngroupButt
            // 
            this.UngroupButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.UngroupButt.Image = ((System.Drawing.Image)(resources.GetObject("UngroupButt.Image")));
            this.UngroupButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UngroupButt.Name = "UngroupButt";
            this.UngroupButt.Size = new System.Drawing.Size(58, 26);
            this.UngroupButt.Text = "Ungroup";
            this.UngroupButt.Click += new System.EventHandler(this.UngroupButt_Click);
            // 
            // SaveButt
            // 
            this.SaveButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveButt.Image = ((System.Drawing.Image)(resources.GetObject("SaveButt.Image")));
            this.SaveButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButt.Name = "SaveButt";
            this.SaveButt.Size = new System.Drawing.Size(26, 26);
            this.SaveButt.Text = "Save";
            this.SaveButt.Click += new System.EventHandler(this.SaveButt_Click);
            // 
            // LoadButt
            // 
            this.LoadButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LoadButt.Image = ((System.Drawing.Image)(resources.GetObject("LoadButt.Image")));
            this.LoadButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoadButt.Name = "LoadButt";
            this.LoadButt.Size = new System.Drawing.Size(26, 26);
            this.LoadButt.Text = "Load";
            this.LoadButt.Click += new System.EventHandler(this.LoadButt_Click);
            // 
            // UndoB
            // 
            this.UndoB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoB.Enabled = false;
            this.UndoB.Image = ((System.Drawing.Image)(resources.GetObject("UndoB.Image")));
            this.UndoB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoB.Name = "UndoB";
            this.UndoB.Size = new System.Drawing.Size(26, 26);
            this.UndoB.Text = "UndoButt";
            this.UndoB.Click += new System.EventHandler(this.UndoB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(612, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // myPic
            // 
            this.myPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPic.Location = new System.Drawing.Point(0, 29);
            this.myPic.Name = "myPic";
            this.myPic.Size = new System.Drawing.Size(754, 555);
            this.myPic.TabIndex = 2;
            this.myPic.TabStop = false;
            this.myPic.Click += new System.EventHandler(this.myPic_Click);
            this.myPic.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.myPic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.myPic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.myPic_MouseMove);
            this.myPic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 584);
            this.Controls.Add(this.myPic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton CircleButt;
        private System.Windows.Forms.ToolStripButton RectButt;
        private System.Windows.Forms.ToolStripButton LineButt;
        private System.Windows.Forms.ToolStripButton TriangleButt;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox myPic;
        private System.Windows.Forms.ToolStripButton GroupButt;
        private System.Windows.Forms.ToolStripButton UngroupButt;
        private System.Windows.Forms.ToolStripButton SaveButt;
        private System.Windows.Forms.ToolStripButton LoadButt;
        private System.Windows.Forms.ToolStripButton UndoB;
    }
}

