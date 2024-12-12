package com.example.fences_android.core

import java.lang.Thread.yield


class Board(size: Int) {
    var Tiles: Array<Array<TileState>>;
    private var _size: Int = size;

    val Connections: ArrayList<Connection>
        get() {
            val result = ArrayList<Connection>();

            EachPlayablePosition { i, j ->
                if (Tiles[i][j] != TileState.Empty) {
                    result.add(GetConnection(i, j));
                }
            };

            return result;
        }

    fun GetConnection(row: Int, col: Int): Connection {
        val result = Connection(row, col, Tiles[row][col], if (Tiles[row + 1][col] == Tiles[row][col]) Orientation.Vertical else Orientation.Horizontal);

        return result;
    }

    init {
        val boardSize = size * 2 - 1;
        Tiles = Array<Array<TileState>>(boardSize) {
            Array<TileState>(
                boardSize
            ) { TileState.Empty }
        };
    }

    fun EachPlayer1Dot(action: (Int, Int) -> Unit) {
        for (i in 0..Tiles.size - 1 step 2) {
            for (j in 1..Tiles[0].size - 1 step 2) {
                action(i, j)
            }
        }
    }

    fun EachPlayer2Dot(action: (Int, Int) -> Unit) {
        for (i in 1..Tiles.size - 1 step 2) {
            for (j in 0..Tiles[0].size - 1 step 2) {
                action(i, j)
            }
        }
    }

    fun EachPlayablePosition(action: (Int, Int) -> Unit) {
        for (i in 1..Tiles.size - 2) {
            for (j in 1..Tiles[0].size - 2) {
                if ((i + j)%2 == 0)
                {
                    action(i, j)
                }
            }
        }
    }

    fun GetAvailablePositions(): ArrayList<Position> {
        val result = ArrayList<Position>();

        for (i in 1..Tiles.size - 2) {
            for (j in 1..Tiles[0].size - 2) {
                if ((i + j) % 2 == 0 && Tiles[i][j] == TileState.Empty) {
                    result.add(Position(i, j));
                }
            }
        }

        return result;
    }

    fun GetStartPositions(player: Turns): ArrayList<Position> {
        val result = ArrayList<Position>();
        if (player == Turns.Player1) {
            for (j in 1..Tiles[0].size - 1 step 2) {
                result.add(Position(0, j));
            }
        } else {
            for (i in 1..Tiles.size - 1 step 2) {
                result.add(Position(i, 0));
            }
        }
        return result;
    }

    fun GetEndPositions(player: Turns): ArrayList<Position> {
        val result = ArrayList<Position>();
        if (player == Turns.Player1) {
            for (j in 1..Tiles[0].size - 1 step 2) {
                result.add(Position(Tiles[0].size - 1, j));
            }
        } else {
            for (i in 1..Tiles.size - 1 step 2) {
                result.add(Position(i, Tiles[0].size - 1));
            }
        }
        return result;
    }

    fun GetNeighbors(p: Position) : List<Position> {
        return GetNeighborsUnfiltered(p).filter { pos -> IsValid(pos) };
    }

    fun GetNeighborsUnfiltered(p: Position): Array<Position> {
        return arrayOf(
            Position(p.Row + 1, p.Col),
            Position(p.Row - 1, p.Col),
            Position(p.Row, p.Col + 1),
            Position(p.Row, p.Col - 1)
        )
    }

    fun IsValid(p: Position) : Boolean {
        return p.Row > 0 && p.Col > 0 && p.Row < Tiles.size && p.Col < Tiles[0].size;
    }

    fun Clone(): Board {
        val clone = Board(_size);

        clone.Tiles = Array(_size) { clone.Tiles[it].clone() }

        return clone;
    }
}