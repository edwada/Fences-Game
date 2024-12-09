namespace FencesGame.UI
{
    partial class GameChoice
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            vsAI = new Button();
            vsPlayer = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // vsAI
            // 
            vsAI.BackColor = Color.Transparent;
            vsAI.Cursor = Cursors.Hand;
            vsAI.FlatAppearance.BorderSize = 0;
            vsAI.FlatAppearance.MouseDownBackColor = Color.Transparent;
            vsAI.FlatAppearance.MouseOverBackColor = Color.Transparent;
            vsAI.FlatStyle = FlatStyle.Flat;
            vsAI.Font = new Font("Comic Sans MS", 14F);
            vsAI.ForeColor = Color.DarkRed;
            vsAI.Location = new Point(149, 157);
            vsAI.Name = "vsAI";
            vsAI.Size = new Size(179, 33);
            vsAI.TabIndex = 0;
            vsAI.Text = "1 Player vs AI";
            vsAI.UseVisualStyleBackColor = false;
            vsAI.Click += vsAI_Click;
            // 
            // vsPlayer
            // 
            vsPlayer.BackColor = Color.Transparent;
            vsPlayer.Cursor = Cursors.Hand;
            vsPlayer.FlatAppearance.BorderSize = 0;
            vsPlayer.FlatAppearance.MouseDownBackColor = Color.Transparent;
            vsPlayer.FlatAppearance.MouseOverBackColor = Color.Transparent;
            vsPlayer.FlatStyle = FlatStyle.Flat;
            vsPlayer.Font = new Font("Comic Sans MS", 14F);
            vsPlayer.ForeColor = Color.DarkRed;
            vsPlayer.Location = new Point(149, 196);
            vsPlayer.Name = "vsPlayer";
            vsPlayer.Size = new Size(179, 33);
            vsPlayer.TabIndex = 1;
            vsPlayer.Text = "2 Player Game";
            vsPlayer.UseVisualStyleBackColor = false;
            vsPlayer.Click += vsPlayer_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Comic Sans MS", 40F);
            label1.ForeColor = Color.DarkRed;
            label1.Location = new Point(135, 43);
            label1.Name = "label1";
            label1.Size = new Size(207, 76);
            label1.TabIndex = 2;
            label1.Text = "Fences";
            // 
            // GameChoice
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(vsPlayer);
            Controls.Add(vsAI);
            Name = "GameChoice";
            Size = new Size(471, 284);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button vsAI;
        private Button vsPlayer;
        private Label label1;
    }
}
