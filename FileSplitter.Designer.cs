namespace FileSpitter
{
    partial class FileSplitter
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
            this.btn_Start = new System.Windows.Forms.Button();
            this.nud_sizeMB = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_sizeMB)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(181, 144);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 13;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // nud_sizeMB
            // 
            this.nud_sizeMB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nud_sizeMB.Location = new System.Drawing.Point(88, 78);
            this.nud_sizeMB.Name = "nud_sizeMB";
            this.nud_sizeMB.Size = new System.Drawing.Size(58, 19);
            this.nud_sizeMB.TabIndex = 14;
            this.nud_sizeMB.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "Size in MB";
            // 
            // FileSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 191);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nud_sizeMB);
            this.Controls.Add(this.btn_Start);
            this.Name = "FileSplitter";
            this.Text = "CSV Splitter";
            ((System.ComponentModel.ISupportInitialize)(this.nud_sizeMB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.NumericUpDown nud_sizeMB;
        private System.Windows.Forms.Label label1;
    }
}

