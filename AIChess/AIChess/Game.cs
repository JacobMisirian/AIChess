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
            // TODO: if move is valid.
            x1--; x2--; y1--; y2--;

            PieceType type = Current.Tiles[(y1 * 8) + x1].Type;
            Current.Tiles[(y1 * 8) + x1] = new Piece(PieceColor.EMPTY, PieceType.EMPTY);
            Current.Tiles[(y2 * 8) + x2] = new Piece(PieceColor.WHITE, type);

            return true;
        }

        public void AIMakeMove() {
            Dictionary<int, Node> choices = new Dictionary<int, Node>();

            foreach (var child in Current.GetChildren(PieceColor.BLACK)) {
                int minimaxScore = minimax(child, true, Current.Depth + 3, int.MinValue, int.MaxValue);
                if (!choices.ContainsKey(minimaxScore)) {
                    choices.Add(minimaxScore, child);
                }
            }

            Current = choices[choices.Keys.Max()];
        }


        private int minimax(Node node, bool isBlackTurn, int targetDepth, int a, int b) {
            PieceColor color = isBlackTurn ? PieceColor.BLACK : PieceColor.WHITE;
            if (node.Depth == targetDepth) return node.GetHeuristic(PieceColor.BLACK);

            if (isBlackTurn) {
                int value = int.MinValue;
                foreach (Node child in node.GetChildren(color)) {
                    value = Math.Max(value, minimax(child, false, targetDepth, a, b));
                    a = Math.Max(a, value);
                    if (a >= b) break;
                }
                return value;
            } else {
                int value = int.MaxValue;
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
