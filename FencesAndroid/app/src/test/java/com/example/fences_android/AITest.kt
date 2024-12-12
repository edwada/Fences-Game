package com.example.fences_android

import com.example.fences_android.core.AI
import com.example.fences_android.core.Turns
import org.junit.Test
import org.junit.Assert.*


class AITest {
    @Test
    fun CountMovesToFinishTest() {
        val game = Game(5, false);
        AssertMovesLeft(game, 4, 4);

        game.Play(7, 1);
        AssertMovesLeft(game, 3, 4);

        game.Play(5, 1);
        AssertMovesLeft(game, 4, 3);

        game.Play(5, 3);
        AssertMovesLeft(game, 3, 4);

        game.Play(6, 2);
        AssertMovesLeft(game, 3, 3);

        game.Play(7, 3);
        AssertMovesLeft(game, 2, 4);

        game.Play(3, 3);
        AssertMovesLeft(game, 3, 3);

        game.Play(4, 2);
        AssertMovesLeft(game, 2, 3);

        game.Play(3, 1);
        AssertMovesLeft(game, 3, 2);

        game.Play(7, 5);
        AssertMovesLeft(game, 3, 2);

        game.Play(3, 5);
        AssertMovesLeft(game, 4, 1);

        game.Play(1, 1);
        AssertMovesLeft(game, 4, 1);

        game.Play(1, 7);
        AssertMovesLeft(game, 5, 1);

        game.Play(1, 3);
        AssertMovesLeft(game, 5, 1);

        game.Play(3, 7);
        AssertMovesLeft(game, Int.MAX_VALUE, 0);
    }

    private fun AssertMovesLeft(game: Game, player1Moves: Int, player2Moves: Int) {
        assertEquals(player1Moves, AI.CountMovesToFinish(game.Board, Turns.Player1))
        assertEquals(player2Moves, AI.CountMovesToFinish(game.Board, Turns.Player2))
    }
}