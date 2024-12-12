package com.example.fences_android.core

enum class Turns {
    Player1, Player2;

    fun ToTileState(): TileState {
        return if (this == Turns.Player1) TileState.Player1 else TileState.Player2;
    }
}