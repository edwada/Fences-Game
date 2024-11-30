using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencesGame
{
    public class Board
    {
        public TileState[,] Tiles;
        private int _size;

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
            _size = size;
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

        /// <summary>
        /// Returns all playable tiles that are empty
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Position> GetAvailablePositions() {
            for (int i = 1; i < Tiles.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < Tiles.GetLength(1) - 1; j++)
                {
                    if ((i + j) % 2 == 0 && Tiles[i, j] == TileState.Empty)
                    {
                        yield return new Position(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the start positions for the given players, which is the top blue dots for Player 1 and the 
        /// left red dots for Player 2
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public IEnumerable<Position> GetStartPositions(Turns player)
        {
            if (player == Turns.Player1)
            {
                for (int j = 1; j < Tiles.GetLength(1); j += 2)
                {
                    yield return new Position(0, j);
                }
            }
            else
            {
                for (int i = 1; i < Tiles.GetLength(0); i += 2)
                {
                    yield return new Position(i, 0);
                }
            }
        }

        /// <summary>
        /// Returns the end positions for the given players, which is the bottom blue dots for Player 1 and the 
        /// right red dots for Player 2
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public IEnumerable<Position> GetEndPositions(Turns player)
        {
            if (player == Turns.Player1)
            {
                for (int j = 1; j < Tiles.GetLength(1); j += 2)
                {
                    yield return new Position(Tiles.GetLength(1)-1, j);
                }
            }
            else
            {
                for (int i = 1; i < Tiles.GetLength(0); i += 2)
                {
                    yield return new Position(i, Tiles.GetLength(0)-1);
                }
            }
        }

        /// <summary>
        /// Returns the neighbors of a given position, considering board boundaries
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public IEnumerable<Position> GetNeighbors(Position p) {
            return GetNeighborsUnfiltered(p).Where(IsValid);
        }

        /// <summary>
        /// Returns the neighbors of a given position, ignoring board boundaries
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private IEnumerable<Position> GetNeighborsUnfiltered(Position p) {
            yield return new Position(p.Row + 1, p.Col);
            yield return new Position(p.Row - 1, p.Col);
            yield return new Position(p.Row, p.Col + 1);
            yield return new Position(p.Row, p.Col - 1);
        }

        /// <summary>
        /// Returns true if a position is inside the board, false if it's not
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsValid(Position p) {
            return p.Row > 0 && p.Col > 0 && p.Row < Tiles.GetLength(0) & p.Col < Tiles.GetLength(1);   
        }

        public Board Clone() {
            var clone = new Board(_size);
            Array.Copy(Tiles, clone.Tiles, Tiles.Length);

            return clone;
        }
    }
}
