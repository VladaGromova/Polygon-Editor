namespace GKProject1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuDeletion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuVertexDeletion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAddVertex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.krawędźPoziomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.krawędźPionowaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cofnijPoziomośćToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cofnijPionowośćToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.contextMenuDeletion.SuspendLayout();
            this.contextMenuVertexDeletion.SuspendLayout();
            this.contextMenuAddVertex.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(800, 450);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.canvas_Click);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // contextMenuDeletion
            // 
            this.contextMenuDeletion.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuDeletion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuDeletion.Name = "contextMenuDeletion";
            this.contextMenuDeletion.Size = new System.Drawing.Size(133, 36);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(132, 32);
            this.deleteToolStripMenuItem.Text = "delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // contextMenuVertexDeletion
            // 
            this.contextMenuVertexDeletion.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuVertexDeletion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteVertexToolStripMenuItem});
            this.contextMenuVertexDeletion.Name = "contextMenuStrip2";
            this.contextMenuVertexDeletion.Size = new System.Drawing.Size(185, 36);
            this.contextMenuVertexDeletion.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // deleteVertexToolStripMenuItem
            // 
            this.deleteVertexToolStripMenuItem.Name = "deleteVertexToolStripMenuItem";
            this.deleteVertexToolStripMenuItem.Size = new System.Drawing.Size(184, 32);
            this.deleteVertexToolStripMenuItem.Text = "delete vertex";
            this.deleteVertexToolStripMenuItem.Click += new System.EventHandler(this.deleteVertexToolStripMenuItem_Click);
            // 
            // contextMenuAddVertex
            // 
            this.contextMenuAddVertex.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuAddVertex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexToolStripMenuItem,
            this.krawędźPoziomaToolStripMenuItem,
            this.krawędźPionowaToolStripMenuItem,
            this.cofnijPoziomośćToolStripMenuItem,
            this.cofnijPionowośćToolStripMenuItem});
            this.contextMenuAddVertex.Name = "contextMenuAddVertex";
            this.contextMenuAddVertex.Size = new System.Drawing.Size(212, 164);
            // 
            // addVertexToolStripMenuItem
            // 
            this.addVertexToolStripMenuItem.Name = "addVertexToolStripMenuItem";
            this.addVertexToolStripMenuItem.Size = new System.Drawing.Size(211, 32);
            this.addVertexToolStripMenuItem.Text = "add vertex";
            this.addVertexToolStripMenuItem.Click += new System.EventHandler(this.addVertexToolStripMenuItem_Click);
            // 
            // krawędźPoziomaToolStripMenuItem
            // 
            this.krawędźPoziomaToolStripMenuItem.Name = "krawędźPoziomaToolStripMenuItem";
            this.krawędźPoziomaToolStripMenuItem.Size = new System.Drawing.Size(211, 32);
            this.krawędźPoziomaToolStripMenuItem.Text = "make horizontal";
            this.krawędźPoziomaToolStripMenuItem.Click += new System.EventHandler(this.krawędźPoziomaToolStripMenuItem_Click);
            // 
            // krawędźPionowaToolStripMenuItem
            // 
            this.krawędźPionowaToolStripMenuItem.Name = "krawędźPionowaToolStripMenuItem";
            this.krawędźPionowaToolStripMenuItem.Size = new System.Drawing.Size(211, 32);
            this.krawędźPionowaToolStripMenuItem.Text = "make vertical";
            this.krawędźPionowaToolStripMenuItem.Click += new System.EventHandler(this.krawędźPionowaToolStripMenuItem_Click);
            // 
            // cofnijPoziomośćToolStripMenuItem
            // 
            this.cofnijPoziomośćToolStripMenuItem.Name = "cofnijPoziomośćToolStripMenuItem";
            this.cofnijPoziomośćToolStripMenuItem.Size = new System.Drawing.Size(211, 32);
            this.cofnijPoziomośćToolStripMenuItem.Text = "no horizontal";
            this.cofnijPoziomośćToolStripMenuItem.Click += new System.EventHandler(this.cofnijPoziomośćToolStripMenuItem_Click);
            // 
            // cofnijPionowośćToolStripMenuItem
            // 
            this.cofnijPionowośćToolStripMenuItem.Name = "cofnijPionowośćToolStripMenuItem";
            this.cofnijPionowośćToolStripMenuItem.Size = new System.Drawing.Size(211, 32);
            this.cofnijPionowośćToolStripMenuItem.Text = "no vertical";
            this.cofnijPionowośćToolStripMenuItem.Click += new System.EventHandler(this.cofnijPionowośćToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 69);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Line drawing algorithm and offset";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(468, 29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 30);
            this.button2.TabIndex = 6;
            this.button2.Text = "SAVE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button2_MouseClick);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(396, 30);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(66, 29);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "WU";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(174, 30);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(218, 29);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Bresenham\'s algorithm";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 30);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(156, 29);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "library function";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBar1.Location = new System.Drawing.Point(0, 381);
            this.trackBar1.Maximum = 50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(800, 69);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Value = 3;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.contextMenuDeletion.ResumeLayout(false);
            this.contextMenuVertexDeletion.ResumeLayout(false);
            this.contextMenuAddVertex.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox;
        private ContextMenuStrip contextMenuDeletion;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ContextMenuStrip contextMenuVertexDeletion;
        private ToolStripMenuItem deleteVertexToolStripMenuItem;
        private ContextMenuStrip contextMenuAddVertex;
        private ToolStripMenuItem addVertexToolStripMenuItem;
        private ToolStripMenuItem krawędźPoziomaToolStripMenuItem;
        private ToolStripMenuItem krawędźPionowaToolStripMenuItem;
        private ToolStripMenuItem cofnijPoziomośćToolStripMenuItem;
        private ToolStripMenuItem cofnijPionowośćToolStripMenuItem;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private RadioButton radioButton3;
        private Button button2;
        private TrackBar trackBar1;
    }
}