package com.example.fences_android.core

class Connection {
    var Direction: Orientation = Orientation.Vertical;
    var Color: TileState = TileState.Empty;
    var Row: Int = 0;
    var Column: Int = 0;

    constructor(row: Int, col:Int, color: TileState, direction: Orientation) {
        Row = row;
        Column = col;
        Color = color;
        Direction = direction;
    }

    override fun equals(other: Any?): Boolean {
        if (other == null) {
            return false
        }

        val o = other as Connection
        return Row == o.Row && Column == o.Column && Color == o.Color && Direction == o.Direction
    }
}