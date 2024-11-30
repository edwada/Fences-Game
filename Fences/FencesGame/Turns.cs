using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencesGame
{
    public enum Turns
    {
        Player1,
        Player2
    }

    public static class TurnsExtensions
    {
        public static TileState ToTileState(this Turns turn) { 
            return turn == Turns.Player1 ? TileState.Player1 : TileState.Player2;
        }
    }
}
