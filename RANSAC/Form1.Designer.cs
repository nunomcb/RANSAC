namespace RANSAC
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLoad2 = new System.Windows.Forms.Button();
            this.btnLoad1 = new System.Windows.Forms.Button();
            this.btnDetect = new System.Windows.Forms.Button();
            this.btnAnnulus = new System.Windows.Forms.Button();
            this.btnClearSet = new System.Windows.Forms.Button();
            this.btnHull = new System.Windows.Forms.Button();
            this.btnFPVD = new System.Windows.Forms.Button();
            this.btnCPVD = new System.Windows.Forms.Button();
            this.btnDelaunay = new System.Windows.Forms.Button();
            this.btnClearLines = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(6, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(716, 520);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // btnLoad2
            // 
            this.btnLoad2.Location = new System.Drawing.Point(6, 560);
            this.btnLoad2.Name = "btnLoad2";
            this.btnLoad2.Size = new System.Drawing.Size(94, 23);
            this.btnLoad2.TabIndex = 1;
            this.btnLoad2.Text = "Load Data Set 2";
            this.btnLoad2.UseVisualStyleBackColor = true;
            this.btnLoad2.Click += new System.EventHandler(this.btnLoad2_Click);
            // 
            // btnLoad1
            // 
            this.btnLoad1.Location = new System.Drawing.Point(6, 531);
            this.btnLoad1.Name = "btnLoad1";
            this.btnLoad1.Size = new System.Drawing.Size(94, 23);
            this.btnLoad1.TabIndex = 2;
            this.btnLoad1.Text = "Load Data Set 1";
            this.btnLoad1.UseVisualStyleBackColor = true;
            this.btnLoad1.Click += new System.EventHandler(this.btnLoad1_Click);
            // 
            // btnDetect
            // 
            this.btnDetect.Enabled = false;
            this.btnDetect.Location = new System.Drawing.Point(590, 560);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(132, 23);
            this.btnDetect.TabIndex = 3;
            this.btnDetect.Text = "Detect Circles";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // btnAnnulus
            // 
            this.btnAnnulus.Enabled = false;
            this.btnAnnulus.Location = new System.Drawing.Point(452, 560);
            this.btnAnnulus.Name = "btnAnnulus";
            this.btnAnnulus.Size = new System.Drawing.Size(132, 23);
            this.btnAnnulus.TabIndex = 4;
            this.btnAnnulus.Text = "Smallest Width Annulus";
            this.btnAnnulus.UseVisualStyleBackColor = true;
            this.btnAnnulus.Click += new System.EventHandler(this.btnAnnulus_Click);
            // 
            // btnClearSet
            // 
            this.btnClearSet.Location = new System.Drawing.Point(6, 589);
            this.btnClearSet.Name = "btnClearSet";
            this.btnClearSet.Size = new System.Drawing.Size(94, 23);
            this.btnClearSet.TabIndex = 5;
            this.btnClearSet.Text = "Clear Data Set";
            this.btnClearSet.UseVisualStyleBackColor = true;
            this.btnClearSet.Click += new System.EventHandler(this.btnClearSet_Click);
            // 
            // btnHull
            // 
            this.btnHull.Enabled = false;
            this.btnHull.Location = new System.Drawing.Point(590, 531);
            this.btnHull.Name = "btnHull";
            this.btnHull.Size = new System.Drawing.Size(132, 23);
            this.btnHull.TabIndex = 6;
            this.btnHull.Text = "Convex Hull";
            this.btnHull.UseVisualStyleBackColor = true;
            this.btnHull.Click += new System.EventHandler(this.btnHull_Click);
            // 
            // btnFPVD
            // 
            this.btnFPVD.Enabled = false;
            this.btnFPVD.Location = new System.Drawing.Point(314, 531);
            this.btnFPVD.Name = "btnFPVD";
            this.btnFPVD.Size = new System.Drawing.Size(132, 23);
            this.btnFPVD.TabIndex = 7;
            this.btnFPVD.Text = "FP Voronoi";
            this.btnFPVD.UseVisualStyleBackColor = true;
            this.btnFPVD.Click += new System.EventHandler(this.btnFPVD_Click);
            // 
            // btnCPVD
            // 
            this.btnCPVD.Enabled = false;
            this.btnCPVD.Location = new System.Drawing.Point(314, 560);
            this.btnCPVD.Name = "btnCPVD";
            this.btnCPVD.Size = new System.Drawing.Size(132, 23);
            this.btnCPVD.TabIndex = 8;
            this.btnCPVD.Text = "CP Voronoi";
            this.btnCPVD.UseVisualStyleBackColor = true;
            this.btnCPVD.Click += new System.EventHandler(this.btnCPVD_Click);
            // 
            // btnDelaunay
            // 
            this.btnDelaunay.Enabled = false;
            this.btnDelaunay.Location = new System.Drawing.Point(452, 531);
            this.btnDelaunay.Name = "btnDelaunay";
            this.btnDelaunay.Size = new System.Drawing.Size(132, 23);
            this.btnDelaunay.TabIndex = 9;
            this.btnDelaunay.Text = "Delaunay";
            this.btnDelaunay.UseVisualStyleBackColor = true;
            this.btnDelaunay.Click += new System.EventHandler(this.btnDelaunay_Click);
            // 
            // btnClearLines
            // 
            this.btnClearLines.Location = new System.Drawing.Point(176, 531);
            this.btnClearLines.Name = "btnClearLines";
            this.btnClearLines.Size = new System.Drawing.Size(132, 23);
            this.btnClearLines.TabIndex = 10;
            this.btnClearLines.Text = "Clear Lines";
            this.btnClearLines.UseVisualStyleBackColor = true;
            this.btnClearLines.Click += new System.EventHandler(this.btnClearLines_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(330, 589);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(116, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "10";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(468, 589);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(116, 20);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "10";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(606, 589);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(116, 20);
            this.textBox3.TabIndex = 13;
            this.textBox3.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(311, 592);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "T:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(452, 592);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "I:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(590, 592);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "M:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 560);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 39);
            this.label4.TabIndex = 17;
            this.label4.Text = "T - Threshold\r\n I - Iterations\r\nM - Minimum Inliers";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 613);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnClearLines);
            this.Controls.Add(this.btnDelaunay);
            this.Controls.Add(this.btnCPVD);
            this.Controls.Add(this.btnFPVD);
            this.Controls.Add(this.btnHull);
            this.Controls.Add(this.btnClearSet);
            this.Controls.Add(this.btnAnnulus);
            this.Controls.Add(this.btnDetect);
            this.Controls.Add(this.btnLoad1);
            this.Controls.Add(this.btnLoad2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "RANSAC";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLoad2;
        private System.Windows.Forms.Button btnLoad1;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.Button btnAnnulus;
        private System.Windows.Forms.Button btnClearSet;
        private System.Windows.Forms.Button btnHull;
        private System.Windows.Forms.Button btnFPVD;
        private System.Windows.Forms.Button btnCPVD;
        private System.Windows.Forms.Button btnDelaunay;
        private System.Windows.Forms.Button btnClearLines;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

    }
}