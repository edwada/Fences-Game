namespace FencingGame.UI
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
            this.btRestart = new System.Windows.Forms.Button();
            this.fencingGameControl1 = new FencingGame.UI.FencingGameControl();
            this.SuspendLayout();
            // 
            // btRestart
            // 
            this.btRestart.Location = new System.Drawing.Point(13, 13);
            this.btRestart.Name = "btRestart";
            this.btRestart.Size = new System.Drawing.Size(75, 23);
            this.btRestart.TabIndex = 1;
            this.btRestart.Text = "Restart";
            this.btRestart.UseVisualStyleBackColor = true;
            this.btRestart.Click += new System.EventHandler(this.btRestart_Click);
            // 
            // fencingGameControl1
            // 
            this.fencingGameControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fencingGameControl1.BackColor = System.Drawing.Color.White;
            this.fencingGameControl1.Location = new System.Drawing.Point(12, 43);
            this.fencingGameControl1.Name = "fencingGameControl1";
            this.fencingGameControl1.Size = new System.Drawing.Size(371, 273);
            this.fencingGameControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 328);
            this.Controls.Add(this.btRestart);
            this.Controls.Add(this.fencingGameControl1);
            this.Name = "Form1";
            this.Text = "Fences";
            this.ResumeLayout(false);

        }

        #endregion

        private FencingGameControl fencingGameControl1;
        private System.Windows.Forms.Button btRestart;
    }
}

