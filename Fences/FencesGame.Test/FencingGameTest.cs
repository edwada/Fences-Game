using System;
using System.Collections.Generic;
using System.Linq;
using FencesGame;
using FencesGame.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FencesGame.Test
{
    [TestClass]
    public class FencingGameTest
    {
        [TestMethod]
        public void ShouldInitializeTheBoard()
        {
            Game game = new Game(5);

            Assert.AreEqual(TileState.Empty, game.Board.Tiles[0, 0]);
            Assert.AreEqual(TileState.Player2, game.Board.Tiles[1, 0]);
            Assert.AreEqual(TileState.Player1, game.Board.Tiles[0, 1]);
            Assert.AreEqual(TileState.Player1, game.Board.Tiles[2, 1]);
            Assert.AreEqual(TileState.Empty, game.Board.Tiles[1, 1]);
            Assert.AreEqual(TileState.Empty, game.Board.Tiles[2, 0]);

            game.Turn = Turns.Player1;
        }

        [TestMethod]
        public void ShouldUpdateTileWhenPlayed()
        {
            Game game = new Game(5);

            game.Play(1, 1);
            Assert.AreEqual(TileState.Player1, game.Board.Tiles[1, 1]);

            Assert.AreEqual(Turns.Player2, game.Turn);
            game.Play(2, 2);
            Assert.AreEqual(TileState.Player2, game.Board.Tiles[2, 2]);
            Assert.AreEqual(Turns.Player1, game.Turn);
        }

        [TestMethod]
        public void ShouldNotAllowToPlayOnNonEmptyTile()
        {
            Game game = new Game(5);

            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(1, 0));
            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(0, 1));
            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(2, 1));

            game.Play(1, 1);
            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(1, 1));
        }

        [TestMethod]
        public void ShouldNotAllowToPlayOnEdgeTilesOrOutOfTheBoard()
        {
            Game game = new Game(5);

            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(0, 0));
            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(2, 0));
            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(8, 0));
            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(0, 8));
            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(8, 8));
            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(-3, 4));
            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(15, 2));
        }

        [TestMethod]
        public void ShouldAnnounceWinnerAndAvoidFurtherPlays()
        {
            Game game = new Game(3);

            game.Play(1, 1);
            game.Play(3, 1);
            game.Play(3, 3);
            game.Play(2, 2);

            bool annoucedWinner = false;
            game.Ended += winner =>
            {
                Assert.AreEqual(Turns.Player1, winner);
                annoucedWinner = true;
            };

            game.Play(1, 3);
            Assert.IsTrue(annoucedWinner);


            game = new Game(3);

            game.Play(1, 1);
            game.Play(3, 1);
            game.Play(2, 2);
            
            annoucedWinner = false;
            game.Ended += winner =>
            {
                Assert.AreEqual(Turns.Player2, winner);
                annoucedWinner = true;
            };

            game.Play(3, 3);
            Assert.IsTrue(annoucedWinner);

            TestHelper.AssertThrows<InvalidMoveException>(() => game.Play(1, 3));
        }

        [TestMethod]
        public void ShouldUpdateConnectionsCollection()
        {
            Game game = new Game(3);

            game.Play(1, 1);
            game.Play(3, 1);
            game.Play(3, 3);
            game.Play(2, 2);

            CollectionAssert.AreEqual(new List<Connection>(){
                new Connection()
                {
                    Row = 1,
                    Collumn = 1,
                    Color = TileState.Player1,
                    Direction = Orientation.Vertical
                }, new Connection()
                {
                    Row = 2,
                    Collumn = 2,
                    Color = TileState.Player2,
                    Direction = Orientation.Vertical
                }, new Connection()
                {
                    Row = 3,
                    Collumn = 1,
                    Color = TileState.Player2,
                    Direction = Orientation.Horizontal
                    
                }, new Connection()
                {
                    Row = 3,
                    Collumn = 3,
                    Color = TileState.Player1,
                    Direction = Orientation.Vertical
                }
                }, game.Board.Connections.ToList());
            
        }
    }
}
