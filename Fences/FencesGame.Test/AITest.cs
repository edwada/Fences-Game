using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencesGame.Test
{
    [TestClass]
    public class AITest
    {
        [TestMethod]
        public void CountMovesToFinishTest()
        {
            Game game = new Game(5, false);
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
            AssertMovesLeft(game, int.MaxValue, 0);
        }

        private void AssertMovesLeft(Game game, int player1Moves, int player2Moves) {
            Assert.AreEqual(player1Moves, AI.CountMovesToFinish(game.Board, Turns.Player1));
            Assert.AreEqual(player2Moves, AI.CountMovesToFinish(game.Board, Turns.Player2));
        }
    }
}
