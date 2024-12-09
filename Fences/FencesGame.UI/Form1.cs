using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FencesGame.UI
{
    public partial class Form1 : Form
    {
        private bool _lastGameType;
        private SoundPlayer lobbyMusic = new SoundPlayer("Lobby-time(chosic.com).wav");

        public Form1()
        {
            InitializeComponent();

            lobbyMusic.Play();
            btPlayAgain.FlatButton();
            btBackToMenu.FlatButton();
        }

        private void btPlayAgain_Click(object sender, EventArgs e)
        {
            lobbyMusic.Stop();
            fencingGameControl1.Restart(_lastGameType);
            HideEndGameButtons();
        }

        private void gameChoice1_playVsAI(object sender, EventArgs e)
        {
            fencingGameControl1.Restart(true);
            _lastGameType = true;
            gameChoice1.Visible = false;
            fencingGameControl1.Visible = true;
            HideEndGameButtons();
            lobbyMusic.Stop();
        }

        private void gameChoice1_playVsHuman(object sender, EventArgs e)
        {
            fencingGameControl1.Restart(false);
            _lastGameType = false;
            gameChoice1.Visible = false;
            fencingGameControl1.Visible = true;
            HideEndGameButtons();
            lobbyMusic.Stop();
        }

        private void fencingGameControl1_GameEnded(object sender, EventArgs e)
        {
            btPlayAgain.Visible = true;
            btBackToMenu.Visible = true;
            lobbyMusic.Play();
        }

        private void btBackToMenu_Click(object sender, EventArgs e)
        {
            fencingGameControl1.Visible = false;
            gameChoice1.Visible = true;
            HideEndGameButtons();
        }

        private void HideEndGameButtons() { 
            btPlayAgain.Visible = false;
            btBackToMenu.Visible = false;
        }
    }
}
