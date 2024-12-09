using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FencesGame.UI
{
    public partial class GameChoice : UserControl
    {
        public event EventHandler playVsAI;
        public event EventHandler playVsHuman;

        public GameChoice()
        {
            InitializeComponent();
            vsAI.FlatButton();
            vsPlayer.FlatButton();
        }

        private void vsAI_Click(object sender, EventArgs e)
        {
            playVsAI(sender, e);
        }

        private void vsPlayer_Click(object sender, EventArgs e)
        {
            playVsHuman(sender, e);
        }
    }
}
