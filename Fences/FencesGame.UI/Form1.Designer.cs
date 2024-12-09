namespace FencesGame.UI
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
            btPlayAgain = new Button();
            fencingGameControl1 = new FencingGameControl();
            gameChoice1 = new GameChoice();
            btBackToMenu = new Button();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btPlayAgain
            // 
            btPlayAgain.Font = new Font("Comic Sans MS", 9F);
            btPlayAgain.ForeColor = Color.DarkRed;
            btPlayAgain.Location = new Point(0, 0);
            btPlayAgain.Margin = new Padding(4, 3, 4, 3);
            btPlayAgain.Name = "btPlayAgain";
            btPlayAgain.Size = new Size(100, 27);
            btPlayAgain.TabIndex = 1;
            btPlayAgain.Text = "Play Again";
            btPlayAgain.UseVisualStyleBackColor = true;
            btPlayAgain.Visible = false;
            btPlayAgain.Click += btPlayAgain_Click;
            // 
            // fencingGameControl1
            // 
            fencingGameControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            fencingGameControl1.BackColor = Color.Transparent;
            fencingGameControl1.Location = new Point(14, 50);
            fencingGameControl1.Margin = new Padding(5, 3, 5, 3);
            fencingGameControl1.Name = "fencingGameControl1";
            fencingGameControl1.Size = new Size(433, 315);
            fencingGameControl1.TabIndex = 0;
            fencingGameControl1.Visible = false;
            fencingGameControl1.GameEnded += fencingGameControl1_GameEnded;
            // 
            // gameChoice1
            // 
            gameChoice1.BackgroundImage = Properties.Resources.grass_low_res_darker;
            gameChoice1.Dock = DockStyle.Fill;
            gameChoice1.Location = new Point(0, 0);
            gameChoice1.Name = "gameChoice1";
            gameChoice1.Size = new Size(461, 378);
            gameChoice1.TabIndex = 2;
            gameChoice1.playVsAI += gameChoice1_playVsAI;
            gameChoice1.playVsHuman += gameChoice1_playVsHuman;
            // 
            // btBackToMenu
            // 
            btBackToMenu.Font = new Font("Comic Sans MS", 9F);
            btBackToMenu.ForeColor = Color.DarkRed;
            btBackToMenu.Location = new Point(108, 0);
            btBackToMenu.Margin = new Padding(4, 3, 4, 3);
            btBackToMenu.Name = "btBackToMenu";
            btBackToMenu.Size = new Size(100, 27);
            btBackToMenu.TabIndex = 3;
            btBackToMenu.Text = "Back To Menu";
            btBackToMenu.UseVisualStyleBackColor = true;
            btBackToMenu.Visible = false;
            btBackToMenu.Click += btBackToMenu_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(btBackToMenu);
            panel1.Controls.Add(btPlayAgain);
            panel1.Location = new Point(126, 18);
            panel1.Name = "panel1";
            panel1.Size = new Size(209, 26);
            panel1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.grass_low_res_darker;
            ClientSize = new Size(461, 378);
            Controls.Add(panel1);
            Controls.Add(fencingGameControl1);
            Controls.Add(gameChoice1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Fences";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FencingGameControl fencingGameControl1;
        private System.Windows.Forms.Button btPlayAgain;
        private GameChoice gameChoice1;
        private Button btBackToMenu;
        private Panel panel1;
    }
}

