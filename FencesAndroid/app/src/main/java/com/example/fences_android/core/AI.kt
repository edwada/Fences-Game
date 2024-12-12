package com.example.fences_android.core

import java.util.PriorityQueue
import kotlin.random.Random

class AI {
    companion object {
        fun GetNextMove(board: Board, turn: Turns) : Position {
            val possibleMoves = board.GetAvailablePositions()

            val evaluations = possibleMoves.map { Pair(it, EvaluateMove(board, it, turn))}
            val evalNumbers = evaluations.map { it.second }

            val bestEval = if (turn == Turns.Player1) evalNumbers.max() else evalNumbers.min()

            val bestMoves = evaluations.filter { it.second == bestEval }.toList()

            return bestMoves[Random.nextInt(bestMoves.size)].first
        }

        fun EvaluateMove(board: Board, move: Position, turn: Turns) : Int {
            val copy = board.Clone()
            copy.Tiles[move.Row][move.Col] = turn.ToTileState()

            return Evaluate(copy)
        }

        fun Evaluate(board: Board) : Int {
            return CountMovesToFinish(board, Turns.Player2) - CountMovesToFinish(board, Turns.Player1)
        }

        fun CountMovesToFinish(board: Board, player: Turns) : Int {
            val playerTile = player.ToTileState()
            val startPositions = board.GetStartPositions(player)
            val counts: Array<Array<Int>> = Array(board.Tiles.size) { Array(board.Tiles[0].size) { 0 } }
            val visitMarks: Array<Array<Boolean>> = Array(board.Tiles.size) { Array(board.Tiles[0].size) { false } }

            for (p in startPositions) {
                visitMarks[p.Row][p.Col] = true
            }

            val test = Pair("one", 2)


            val comparator = Comparator.comparing { p:Pair<Position, Int> -> p.second }
            val visitQueue: PriorityQueue<Pair<Position, Int>> = PriorityQueue(comparator)
            visitQueue.addAll(startPositions.map { Pair(it, 0) })

            while (visitQueue.count() > 0) {
                val next = visitQueue.remove()

                visitQueue.addAll(CountingFloodfillVisit(board, counts, visitMarks, next.first, next.second, playerTile));
            }

            var visitedEndPositions = board.GetEndPositions(player).filter { visitMarks[it.Row][it.Col] }.toList()
            return if (visitedEndPositions.any()) visitedEndPositions.map { counts[it.Row][it.Col] }.min() else Int.MAX_VALUE
        }

        fun CountingFloodfillVisit(board: Board, counts: Array<Array<Int>>, visitMarks: Array<Array<Boolean>>,
                                   p: Position, currentCount:Int, player: TileState): Iterable<Pair<Position, Int>> {
            counts[p.Row][p.Col] = currentCount
            visitMarks[p.Row][p.Col] = true

            val unvisited = board.GetNeighbors(p).filter{ !visitMarks[it.Row][it.Col] }.toList()

            val empty = unvisited.filter{ board.Tiles[it.Row][it.Col] == TileState.Empty }.toList()
            val players = unvisited.filter{ board.Tiles[it.Row][it.Col] == player }.toList()

            val result = empty.map{ Pair(it, currentCount + 1) }

            return result.union(players.map{ Pair(it, currentCount) }).toList()
        }
    }
}