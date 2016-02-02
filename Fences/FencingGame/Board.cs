using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingGame
{
    public class Board
    {
        public TileState[,] Tiles;

        public IEnumerable<Connection> Connections
        {
            get
            {
                List<Connection> result = new List<Connection>();
                
                EachPlayablePosition((i, j) =>
                {
                    if (Tiles[i, j] != TileState.Empty)
                    {
                        result.Add(GetConnection(i, j));
                    }
                });

                return result;
            }
        }

        public Connection GetConnection(int row, int col)
        {
            return new Connection
            {
                Collumn = col,
                Row = row,
                Color = Tiles[row, col],
                Direction = Tiles[row + 1, col] == Tiles[row, col] ? Orientation.Vertical : Orientation.Horizontal
            };
        }

        public Board(int size)
        {
            int boardSize = size * 2 - 1;
            Tiles = new TileState[boardSize, boardSize];
        }

        public void EachPlayer1Dot(Action<int, int> action)
        {
            for (int i = 0; i < Tiles.GetLength(0); i += 2)
            {
                for (int j = 1; j < Tiles.GetLength(1); j += 2)
                {
                    action(i, j);
                }
            }
        }

        public void EachPlayer2Dot(Action<int, int> action)
        {
            for (int i = 1; i < Tiles.GetLength(0); i += 2)
            {
                for (int j = 0; j < Tiles.GetLength(1); j += 2)
                {
                    action(i, j);
                }
            }
        }

        public void EachPlayablePosition(Action<int, int> action)
        {
            for (int i = 1; i < Tiles.GetLength(0)-1; i++)
            {
                for (int j = 1; j < Tiles.GetLength(1)-1; j++)
                {
                    if ((i + j)%2 == 0)
                    {
                        action(i, j);
                    }
                }
            }
        }
    }
}
