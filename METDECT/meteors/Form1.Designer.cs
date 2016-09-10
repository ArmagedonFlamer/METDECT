namespace meteors
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.OpenF = new System.Windows.Forms.Button();
            this.SaveF = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.minObjS = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.minStarS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numDil = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // OpenF
            // 
            this.OpenF.Location = new System.Drawing.Point(27, 22);
            this.OpenF.Name = "OpenF";
            this.OpenF.Size = new System.Drawing.Size(75, 23);
            this.OpenF.TabIndex = 0;
            this.OpenF.Text = "Root Folder";
            this.OpenF.UseVisualStyleBackColor = true;
            this.OpenF.Click += new System.EventHandler(this.OpenF_Click);
            // 
            // SaveF
            // 
            this.SaveF.Location = new System.Drawing.Point(27, 66);
            this.SaveF.Name = "SaveF";
            this.SaveF.Size = new System.Drawing.Size(75, 23);
            this.SaveF.TabIndex = 1;
            this.SaveF.Text = "Save";
            this.SaveF.UseVisualStyleBackColor = true;
            this.SaveF.Click += new System.EventHandler(this.SaveF_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(25, 356);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 2;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(283, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 117);
            this.label2.MaximumSize = new System.Drawing.Size(250, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Minimum size of the object to be taken into account when calculating diameter";
            // 
            // minObjS
            // 
            this.minObjS.ForeColor = System.Drawing.Color.SlateGray;
            this.minObjS.Location = new System.Drawing.Point(27, 164);
            this.minObjS.Name = "minObjS";
            this.minObjS.Size = new System.Drawing.Size(100, 20);
            this.minObjS.TabIndex = 5;
            this.minObjS.Text = "200";
            this.minObjS.Enter += new System.EventHandler(this.minObjS_Enter);
            this.minObjS.Leave += new System.EventHandler(this.minObjS_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Minimum star size";
            // 
            // minStarS
            // 
            this.minStarS.ForeColor = System.Drawing.Color.SlateGray;
            this.minStarS.Location = new System.Drawing.Point(25, 231);
            this.minStarS.Name = "minStarS";
            this.minStarS.Size = new System.Drawing.Size(100, 20);
            this.minStarS.TabIndex = 7;
            this.minStarS.Text = "20";
            this.minStarS.Enter += new System.EventHandler(this.minStarS_Enter);
            this.minStarS.Leave += new System.EventHandler(this.minStarS_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Number of times dilatation will be applied";
            // 
            // numDil
            // 
            this.numDil.ForeColor = System.Drawing.Color.SlateGray;
            this.numDil.Location = new System.Drawing.Point(27, 308);
            this.numDil.Name = "numDil";
            this.numDil.Size = new System.Drawing.Size(100, 20);
            this.numDil.TabIndex = 9;
            this.numDil.Text = "50";
            this.numDil.Enter += new System.EventHandler(this.numDil_Enter);
            this.numDil.Leave += new System.EventHandler(this.numDil_Leave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(343, 391);
            this.Controls.Add(this.numDil);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.minStarS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.minObjS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.SaveF);
            this.Controls.Add(this.OpenF);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.Text = "METDECT";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button OpenF;
        private System.Windows.Forms.Button SaveF;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox minObjS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox minStarS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numDil;
    }
}

