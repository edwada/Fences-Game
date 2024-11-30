using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencesGame
{
    public class AI
    {
        public static Position GetNextMove(Board board, Turns turn) {
            var possibleMoves = board.GetAvailablePositions();

            var evaluations = possibleMoves.Select(m => (m, EvaluateMove(board, m, turn)));

            var bestEval = turn == Turns.Player1 ? evaluations.Max(e => e.Item2) : evaluations.Min(e => e.Item2);

            var bestMoves = evaluations.Where(e => e.Item2 == bestEval).ToList();
            
            var random = new Random();
            return bestMoves[random.Next(bestMoves.Count)].Item1;
        }

        /// <summary>
        /// Creates a copy of the board, plays the move on the board, and returns a tuple of the move
        /// with the result of the evaluation
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        private static int EvaluateMove(Board board, Position move, Turns turn) {
            var copy = board.Clone();
            copy.Tiles[move.Row, move.Col] = turn.ToTileState();

            return Evaluate(copy);
        }

        /// <summary>
        /// Evaluates a given position on the board, positive evaluation means advantage to player 1 
        /// and negative to player 2, 0 means the position is equal
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static int Evaluate(Board board) {
            return CountMovesToFinish(board, Turns.Player2) - CountMovesToFinish(board, Turns.Player1);
        }

        /// <summary>
        /// Returns the number of moves the given player would need to win, assuming no moves from the other player
        /// </summary>
        /// <param name="board"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static int CountMovesToFinish(Board board, Turns player) {
            var playerTile = player == Turns.Player1 ? TileState.Player1 : TileState.Player2;
            var startPositions = board.GetStartPositions(player);
            int[,] counts = new int[board.Tiles.GetLength(0), board.Tiles.GetLength(1)];
            bool[,] visitMarks = new bool[board.Tiles.GetLength(0), board.Tiles.GetLength(1)];

            foreach (var p in startPositions)
            {
                visitMarks[p.Row, p.Col] = true;
            }

            //Included the priority on the element so that it can be dequeued with it later
            var visitQueue = new PriorityQueue<(Position, int), int>();
            visitQueue.EnqueueRange(startPositions.Select(p => ((p, 0), 0)));

            while (visitQueue.Count > 0) { 
                var next = visitQueue.Dequeue();

                visitQueue.EnqueueRange(CountingFloodfillVisit(board, counts, visitMarks, next.Item1, next.Item2, playerTile));
            }

            var visitedEndPositions = board.GetEndPositions(player).Where(p => visitMarks[p.Row, p.Col]).ToList();

            return visitedEndPositions.Any() ? visitedEndPositions.Select(p => counts[p.Row, p.Col]).Min() : int.MaxValue;
        }

        /// <summary>
        /// Visits a position, updating the count and visit marks for that position and returning the neighboring 
        /// unvisited positions
        /// </summary>
        /// <param name="board"></param>
        /// <param name="counts"></param>
        /// <param name="visitMarks"></param>
        /// <param name="p"></param>
        /// <param name="currentCount"></param>
        /// <returns></returns>
        private static IEnumerable<((Position, int), int)> CountingFloodfillVisit(Board board, int[,] counts, bool[,] visitMarks, Position p, int currentCount, TileState player) {
            //if not, either we have not visited the position, or we found a shorter path, perform the visit
            counts[p.Row, p.Col] = currentCount;
            visitMarks[p.Row, p.Col] = true;

            var unvisited = board.GetNeighbors(p).Where(n => !visitMarks[n.Row, n.Col]).ToList();

            var empty = unvisited.Where(n => board.Tiles[n.Row, n.Col] == TileState.Empty).ToList();
            var players = unvisited.Where(n => board.Tiles[n.Row, n.Col] == player).ToList();

            var result = empty.Select(n => ((n, currentCount + 1), currentCount + 1)).ToList();
            result.AddRange(players.Select(n => ((n, currentCount), currentCount)));

            return result;
        }
    }
}
