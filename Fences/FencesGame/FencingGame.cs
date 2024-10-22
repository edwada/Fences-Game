using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FencesGame.Exceptions;

namespace FencesGame
{
    public class Game
    {
        private bool _ended;
        public Board Board;

        public Game(int size)
        {
            Board = new Board(size);
            
            FillPlayer1Dots(Board.Tiles);
            FillPlayer2Dots(Board.Tiles);
        }

        public Turns Turn { get; set; }

        private void FillPlayer2Dots(TileState[,] Board)
        {
            this.Board.EachPlayer2Dot((i, j) => Board[i, j] = TileState.Player2);
        }

        private void FillPlayer1Dots(TileState[,] Board)
        {
            this.Board.EachPlayer1Dot((i ,j) => Board[i,j] = TileState.Player1);
        }

        public void Play(int line, int col)
        {
            //Can't play on the board's edges
            if (line <= 0 || line >= Board.Tiles.GetLength(0) - 1 || col <= 0 || col >= Board.Tiles.GetLength(1) - 1)
                throw new InvalidMoveException();

            //Can't play on top of a used tile
            if (Board.Tiles[line, col] != TileState.Empty)
                throw new InvalidMoveException();

            //Can't play if the game has already ended
            if (this._ended)
                throw new InvalidMoveException();


            Board.Tiles[line, col] = this.Turn == Turns.Player1 ? TileState.Player1 : TileState.Player2;
            if (MoveWinsGame(line, col))
            {
                this._ended = true;
                AnnounceWinner(this.Turn);
            }

            Turn = Turn == Turns.Player1 ? Turns.Player2 : Turns.Player1;
        }

        private void AnnounceWinner(Turns turns)
        {
            if (this.Ended != null)
                this.Ended(this.Turn);
        }

        private bool MoveWinsGame(int line, int col)
        {
            bool[,] marks = new bool[Board.Tiles.GetLength(0), Board.Tiles.GetLength(1)];

            FloodFill(Board.Tiles, marks, line, col);

            if (Board.Tiles[line, col] == TileState.Player1)
                return CheckWinnerRows(marks);
            else
                return CheckWinnerCols(marks);
        }

        private bool CheckWinnerCols(bool[,] marks)
        {
            return AnyInCol(0, marks) && AnyInCol(Board.Tiles.GetLength(1) - 1, marks);
        }

        private bool CheckWinnerRows(bool[,] marks)
        {
            return AnyInRow(0, marks) && AnyInRow(Board.Tiles.GetLength(0) - 1, marks);
        }

        private bool AnyInRow(int row, bool[,] marks)
        {
            for(int i=0; i<Board.Tiles.GetLength(0); i++)
                if (marks[row, i])
                    return true;

            return false;
        }

        private bool AnyInCol(int col, bool[,] marks)
        {
            for (int i = 0; i < marks.GetLength(0); i++)
                if (marks[i, col])
                    return true;

            return false;
        }

        private void FloodFill(TileState[,] Board, bool[,] marks, int line, int col)
        {
            marks[line, col] = true;

            FloodIfEqualAndUnmarked(Board, marks, line+1, col, Board[line, col]);
            FloodIfEqualAndUnmarked(Board, marks, line - 1, col, Board[line, col]);
            FloodIfEqualAndUnmarked(Board, marks, line, col + 1, Board[line, col]);
            FloodIfEqualAndUnmarked(Board, marks, line, col - 1, Board[line, col]);
        }

        private void FloodIfEqualAndUnmarked(TileState[,] Board, bool[,] marks, int line, int col, TileState state)
        {
            if (line < 0 || line >= marks.GetLength(0))
                return;

            if (col < 0 || col >= marks.GetLength(1))
                return;

            if (marks[line, col])
                return;

            if (state != Board[line, col])
                return;

            FloodFill(Board, marks, line, col);
        }

        public event GameEndedHandler Ended;

        public delegate void GameEndedHandler(Turns winner);
    }
}
