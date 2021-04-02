using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIChess {
    public class Game {
        public Node Current { get; private set; }

        public Game() {
            Current = new Node(new Piece[] { 
                new Piece(PieceColor.BLACK, PieceType.ROOK), new Piece(PieceColor.BLACK, PieceType.KNIGHT), new Piece(PieceColor.BLACK, PieceType.BISHOP), new Piece(PieceColor.BLACK, PieceType.QUEEN), new Piece(PieceColor.BLACK, PieceType.KING), new Piece(PieceColor.BLACK, PieceType.BISHOP), new Piece(PieceColor.BLACK, PieceType.KNIGHT), new Piece(PieceColor.BLACK, PieceType.ROOK),
                new Piece(PieceColor.BLACK, PieceType.PAWN), new Piece(PieceColor.BLACK, PieceType.PAWN),new Piece(PieceColor.BLACK, PieceType.PAWN), new Piece(PieceColor.BLACK, PieceType.PAWN), new Piece(PieceColor.BLACK, PieceType.PAWN), new Piece(PieceColor.BLACK, PieceType.PAWN), new Piece(PieceColor.BLACK, PieceType.PAWN), new Piece(PieceColor.BLACK, PieceType.PAWN),
                new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY),
                new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY),
                new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY),
                new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY), new Piece(PieceColor.EMPTY, PieceType.EMPTY),
                new Piece(PieceColor.WHITE, PieceType.PAWN), new Piece(PieceColor.WHITE, PieceType.PAWN), new Piece(PieceColor.WHITE, PieceType.PAWN), new Piece(PieceColor.WHITE, PieceType.PAWN), new Piece(PieceColor.WHITE, PieceType.PAWN), new Piece(PieceColor.WHITE, PieceType.PAWN), new Piece(PieceColor.WHITE, PieceType.PAWN), new Piece(PieceColor.WHITE, PieceType.PAWN),
                new Piece(PieceColor.WHITE, PieceType.ROOK), new Piece(PieceColor.WHITE, PieceType.KNIGHT), new Piece(PieceColor.WHITE, PieceType.BISHOP), new Piece(PieceColor.WHITE, PieceType.QUEEN), new Piece(PieceColor.WHITE, PieceType.KING), new Piece(PieceColor.WHITE, PieceType.BISHOP), new Piece(PieceColor.WHITE, PieceType.KNIGHT), new Piece(PieceColor.WHITE, PieceType.ROOK),
            });
        }

        public bool HumanMakeMove(int x1, int y1, int x2, int y2) {
            List<Node> validMoves = Current.GetChildren(PieceColor.WHITE);
            Node requestedMove = new Node(Current, x1, y1, x2, y2);

            foreach (var move in validMoves) {
                if (move.PieceMoved.Type == requestedMove.PieceMoved.Type && move.MovedTo == requestedMove.MovedTo) {
                    Current = move;
                    return true;
                }
            }

            return false; ;
        }

        public void AIMakeMove() {
            Dictionary<double, Node> choices = new Dictionary<double, Node>();

            foreach (var child in Current.GetChildren(PieceColor.BLACK)) {
                double minimaxScore = minimax(child, true, Current.Depth + 3, int.MinValue, int.MaxValue);
                if (!choices.ContainsKey(minimaxScore)) {
                    choices.Add(minimaxScore, child);
                }
            }

            Current = choices[choices.Keys.Max()];
        }


        private double minimax(Node node, bool isBlackTurn, int targetDepth, double a, double b) {
            PieceColor color = isBlackTurn ? PieceColor.BLACK : PieceColor.WHITE;
            if (node.Depth == targetDepth) return node.GetHeuristic(PieceColor.BLACK);

            if (isBlackTurn) {
                double value = double.MinValue;
                foreach (Node child in node.GetChildren(color)) {
                    value = Math.Max(value, minimax(child, false, targetDepth, a, b));
                    a = Math.Max(a, value);
                    if (a >= b) break;
                }
                return value;
            } else {
                double value = double.MaxValue;
                foreach (Node child in node.GetChildren(color)) {
                    value = Math.Min(value, minimax(child, true, targetDepth, a, b));
                    b = Math.Min(b, value);
                    if (b <= a) break;
                }
                return value;
            }
        }
    }
}
