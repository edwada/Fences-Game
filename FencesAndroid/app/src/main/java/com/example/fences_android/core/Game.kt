package com.example.fences_android.core

import kotlin.random.Random

class Game(size: Int, VsAi: Boolean) {
    var HasEnded: Boolean = false
    var Board: Board
    private var _AIPlayer: Turns? = null;
    val Ended = Event<Turns>()

    init {
        Board = Board(size)

        FillPlayer1Dots(Board.Tiles)
        FillPlayer2Dots(Board.Tiles)

        if (VsAi) {
            _AIPlayer = if (Random.Default.nextInt() % 2 == 0) Turns.Player1 else Turns.Player2
            if (_AIPlayer == Turns.Player1) {
                PlayAIMove();
            }
        }
    }

    var Turn: Turns = Turns.Player1;

    private fun FillPlayer2Dots(board: Array<Array<TileState>>) {
        this.Board.EachPlayer2Dot { i, j -> board[i][j] = TileState.Player2 }
    }

    private fun FillPlayer1Dots(board: Array<Array<TileState>>) {
        this.Board.EachPlayer1Dot { i, j -> board[i][j] = TileState.Player1 }
    }

    fun Play(line: Int, col: Int) {
        //Can't play on the board's edges
        if (line <= 0 || line >= Board.Tiles.size - 1 || col <= 0 || col >= Board.Tiles[0].size - 1) {
            throw InvalidMoveException();
        }

        //Can't play on top of a used tile
        if (Board.Tiles[line][col] != TileState.Empty) {
            throw InvalidMoveException()
        }

        //Can't play if the game has already ended
        if (HasEnded)
            throw InvalidMoveException()


        Board.Tiles[line][col] =
            if (this.Turn == Turns.Player1) TileState.Player1 else TileState.Player2;
        if (MoveWinsGame(line, col)) {
            HasEnded = true
            AnnounceWinner()
        }

        Turn = if (Turn == Turns.Player1) Turns.Player2 else Turns.Player1;

        if (_AIPlayer != null && _AIPlayer == Turn && !HasEnded) {
            PlayAIMove()
        }
    }

    private fun PlayAIMove() {
        val aiMove = AI.GetNextMove(Board, Turn);
        Play(aiMove.Row, aiMove.Col)
    }

    private fun AnnounceWinner() {
        Ended.Raise(Turn)
    }

    private fun MoveWinsGame(line: Int, col: Int): Boolean {
        var marks: Array<Array<Boolean>> =
            Array(Board.Tiles.size) { Array(Board.Tiles[0].size) { false } }

        FloodFill(Board.Tiles, marks, line, col)

        if (Board.Tiles[line][col] == TileState.Player1) {
            return CheckWinnerRows(marks)
        } else {
            return CheckWinnerCols(marks)
        }
    }

    private fun CheckWinnerCols(marks: Array<Array<Boolean>>): Boolean {
        return AnyInCol(0, marks) && AnyInCol(Board.Tiles[0].size - 1, marks)
    }

    private fun CheckWinnerRows(marks: Array<Array<Boolean>>): Boolean {
        return AnyInRow(0, marks) && AnyInRow(Board.Tiles.size - 1, marks)
    }

    private fun AnyInRow(row: Int, marks: Array<Array<Boolean>>): Boolean {
        for (i in 0..Board.Tiles.size - 1) {
            if (marks[row][i]) {
                return true
            }
        }
        return false
    }

    private fun AnyInCol(col: Int, marks: Array<Array<Boolean>>) : Boolean {
        for (i in 0..Board.Tiles[0].size - 1) {
            if (marks[i][col]) {
                return true
            }
        }
        return false
    }

    private fun FloodFill(board: Array<Array<TileState>>, marks: Array<Array<Boolean>>, line: Int, col: Int) {
        marks[line][col] = true;
        // Flood right
        FloodIfEqualAndUnmarked(board, marks, line+1, col, board[line][col]);
        // Flood left
        FloodIfEqualAndUnmarked(board, marks, line - 1, col, board[line][col]);
        // Flood down
        FloodIfEqualAndUnmarked(board, marks, line, col + 1, board[line][col]);
        // Flood up
        FloodIfEqualAndUnmarked(board, marks, line, col - 1, board[line][col]);
    }

    private fun FloodIfEqualAndUnmarked(board: Array<Array<TileState>>, marks: Array<Array<Boolean>>, line: Int, col: Int, state: TileState){
        if (line < 0 || line >= marks.size)
            return;

        if (col < 0 || col >= marks[0].size)
            return;

        if (marks[line][col])
            return;

        if (state != board[line][col])
            return;

        FloodFill(board, marks, line, col);
    }
}