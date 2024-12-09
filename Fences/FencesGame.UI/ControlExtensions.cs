using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FencesGame.UI
{
    public static class ControlExtensions
    {
        public static void FlatButton(this Button button) 
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.BackColor = Color.Transparent;
            button.Cursor = Cursors.Hand;

            button.MouseEnter += (b, e) =>
            {
                button.Font = new Font(button.Font, FontStyle.Bold);
            };

            button.MouseLeave += (b, e) =>
            {
                button.Font = new Font(button.Font, FontStyle.Regular);
            };
        }
    }
}
