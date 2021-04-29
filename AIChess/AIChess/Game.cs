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
            List<Node> validMoves = Current.GetChildren(PieceColor.WHITE, removeBadChildren: false);
            Node requestedMove = new Node(Current, x1, y1, x2, y2);

            foreach (var move in validMoves) {
                if (move.PieceMoved.Type == requestedMove.PieceMoved.Type && move.MovedTo == requestedMove.MovedTo) {
                    Current = move;
                    return true;
                }
            }

            return false;
        }

        public bool AIMakeMove() {
            Dictionary<double, Node> choices = new Dictionary<double, Node>();

            foreach (var child in Current.GetChildren(PieceColor.BLACK)) {
                double minimaxScore = minimax(child, true, Current.Depth + 3, double.MinValue, double.MaxValue);
                if (!choices.ContainsKey(minimaxScore)) {
                    choices.Add(minimaxScore, child);
                }
            }

            if (choices.Count == 0) return false;
            Current = choices[choices.Keys.Max()];
            return true;
        }

        private double minimax(Node node, bool isBlackTurn, int targetDepth, double a, double b) {
            PieceColor color = isBlackTurn ? PieceColor.BLACK : PieceColor.WHITE;

            if (node.Depth == targetDepth) {
                if (node.IsCheckmate) { return double.MinValue; }
                return node.GetHeuristic(color);
            }

            var children = node.GetChildren(color);

            if (isBlackTurn) {
                double value = double.MinValue;
                foreach (Node child in children) {
                    value = Math.Max(value, minimax(child, false, targetDepth, a, b));
                    a = Math.Max(a, value);
                    if (a >= b) break;
                }
                return value;
            } else {
                if (node.IsCheckmate) {
                    return double.MaxValue - node.Depth;
                }
                double value = double.MaxValue - node.Depth;
                foreach (Node child in children) {
                    value = Math.Min(value, minimax(child, true, targetDepth, a, b));
                    b = Math.Min(b, value);
                    if (b <= a) break;
                }
                return value;
            }
        }

        private double minimaxExperimental(Node node, bool isBlackTurn, int targetDepth, double a, double b) {
            PieceColor color = isBlackTurn ? PieceColor.BLACK : PieceColor.WHITE;
            if (node.Depth == targetDepth) return node.GetHeuristic(color);

            if (isBlackTurn) {
                double sum = 0;
                var children = node.GetChildren(color);
                foreach (Node child in children) {
                    sum += minimax(child, false, targetDepth, a, b);
                }
                return sum / children.Count;
            } else {
                double sum = 0;
                var children = node.GetChildren(color);
                foreach (Node child in children) {
                    sum -= minimax(child, true, targetDepth, a, b);
                }
                return sum / children.Count;
            }
        }
    }
}
