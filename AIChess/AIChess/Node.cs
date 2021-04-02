using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIChess {
    public class Node {
        public Piece[] Tiles { get; private set; }
        public int Depth { get; private set; }

        public Piece PieceMoved { get; private set; }
        public (int, int) MovedTo { get; private set; }

        private static string[] BISHOP_MOVES = new string[] { "UP_LEFT", "UP_RIGHT", "DOWN_LEFT", "DOWN_RIGHT" };
        private static string[] ROOK_MOVES = new string[] { "UP", "DOWN", "LEFT", "RIGHT" };
        private static string[] QUEEN_MOVES = new string[] { "UP", "DOWN", "LEFT", "RIGHT", "UP_LEFT", "UP_RIGHT", "DOWN_LEFT", "DOWN_RIGHT" };
        private static double[,] pawnEvalBlack = new double[,]
        {
            {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            {0.5, 1.0, 1.0, -2.0, -2.0, 1.0, 1.0, 0.5},
            {0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5},
            {0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0},
            {0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5},
            {1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0},
            {5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0},
            {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0}
        };
        private static double[,] knightEvalBlack = new double[,]
        {
            {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0},
            {-4.0, -2.0,  0.0,  0.5,  0.5,  0.0, -2.0, -4.0},
            {-3.0,  0.5,  1.0,  1.5,  1.5,  1.0,  0.5, -3.0},
            {-3.0,  0.0,  1.5,  2.0,  2.0,  1.5,  0.0, -3.0},
            {-3.0,  0.5,  1.5,  2.0,  2.0,  1.5,  0.5, -3.0},
            {-3.0,  0.0,  1.0,  1.5,  1.5,  1.0,  0.0, -3.0},
            {-4.0, -2.0,  0.0,  0.0,  0.0,  0.0, -2.0, -4.0},
            {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0}
        };
        private static double[,] bishopEvalBlack = new double[,]
        {
            {-2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0},
            {-1.0,  0.5,  0.0,  0.0,  0.0,  0.0,  0.5, -1.0},
            {-1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0, -1.0},
            {-1.0,  0.0,  1.0,  1.0,  1.0,  1.0,  0.0, -1.0},
            {-1.0,  0.5,  0.5,  1.0,  1.0,  0.5,  0.5, -1.0},
            {-1.0,  0.0,  0.5,  1.0,  1.0,  0.5,  0.0, -1.0},
            {-1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
            {-2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0}
        };
        private static double[,] rookEvalBlack = new double[,]
        {
            {0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5},
            {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
        };
        private static double[,] queenEvalBlack = new double[,]
        {
            {-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0},
            {-1.0,  0.0,  0.0,  0.0,  0.0,  0.5,  0.0, -1.0},
            {-1.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.5, -1.0},
            {-0.5,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0,  0.0},
            {-0.5,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -0.5},
            {-1.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -1.0},
            {-1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
            {-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0}
        };
        private static double[,] kingEvalBlack = new double[,]
        {
            {2.0,  3.0,  1.0,  0.0,  0.0,  1.0,  3.0,  2.0},
            {2.0,  2.0,  0.0,  0.0,  0.0,  0.0,  2.0,  2.0},
            {-1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0},
            {-2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0}
        };

        public Node(Piece[] tiles) {
            Tiles = tiles;
            Depth = 0;
        }
        public Node(Node n) {
            Tiles = new Piece[n.Tiles.Length];
            Array.Copy(n.Tiles, Tiles, n.Tiles.Length);
            Depth = n.Depth + 1;
        }
        public Node(Node n, int x1, int y1, int x2, int y2) {
            Tiles = new Piece[n.Tiles.Length];
            Array.Copy(n.Tiles, Tiles, n.Tiles.Length);
            Depth = n.Depth + 1;

            PieceMoved = tile(x1, y1);
            MovedTo = (x2, y2);

            PieceType type = PieceMoved.Type;
            PieceColor color = PieceMoved.Color;

            tile(x1, y1, PieceColor.EMPTY, PieceType.EMPTY);
            tile(x2, y2, color, type);
        }

        public List<Node> GetChildren(PieceColor color, bool checkForCheck = true) {
            List<Node> children = new List<Node>();

            foreach (var coord in getCoordsOfFilledSpots(color)) {
                PieceType type = tile(coord.Item1, coord.Item2).Type;

                switch (type) {
                    case PieceType.PAWN:
                        generatePawnMoves(coord.Item1, coord.Item2, children);
                        break;
                    case PieceType.KING:
                        generateKingMoves(coord.Item1, coord.Item2, children);
                        break;
                    case PieceType.KNIGHT:
                        generateKnightMoves(coord.Item1, coord.Item2, children);
                        break;
                    case PieceType.BISHOP:
                        generateUnboundedPieceMoves(coord.Item1, coord.Item2, BISHOP_MOVES, children);
                        break;
                    case PieceType.ROOK:
                        generateUnboundedPieceMoves(coord.Item1, coord.Item2, ROOK_MOVES, children);
                        break;
                    case PieceType.QUEEN:
                        generateUnboundedPieceMoves(coord.Item1, coord.Item2, QUEEN_MOVES, children);
                        break;
                }
            }

            if (checkForCheck)
                children.RemoveAll((n) => n.isInCheck(color));


            return children;
        }

        private bool isInCheck(PieceColor us) {
            PieceColor them = us == PieceColor.BLACK ? PieceColor.WHITE : PieceColor.BLACK;
            List<Node> nextMoves = GetChildren(them, false);

            foreach (var move in nextMoves) {
                if (Array.Find(move.Tiles, (p) => p.Type == PieceType.KING && p.Color == us) == null) return true;
            }

            return false;
        }

        public double GetHeuristic(PieceColor color) {
            double blackValues = getPieceValues(PieceColor.BLACK);
            double whiteValues = getPieceValues(PieceColor.WHITE);

            if (color == PieceColor.BLACK) {
                return blackValues - whiteValues;
            } else {
                return whiteValues - blackValues;
            }
        }

        private double getPieceValues(PieceColor color) {
            double res = 0;
            foreach (var coord in getCoordsOfFilledSpots(color)) {
                switch (tile(coord.Item1, coord.Item2).Type) {
                    case PieceType.PAWN:
                        res += 10 + pawnEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.ROOK:
                        res += 50 + rookEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.BISHOP:
                        res += 30 + bishopEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.KNIGHT:
                        res += 30 + knightEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.KING:
                        res += 10000.0 + kingEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.QUEEN:
                        res += 90.0 + queenEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                }
            }

            return res;
        }

        private (int, int)[] getCoordsOfFilledSpots(PieceColor color) {
            List<(int, int)> res = new List<(int, int)>();

            for (int x = 1; x <= 8; x++)
                for (int y = 1; y <= 8; y++)
                    if (tile(x, y).Color == color)
                        res.Add((x, y));

            return res.ToArray();
        }

        private void generatePawnMoves(int x, int y, List<Node> children) {
            PieceColor us = tile(x, y).Color;
            PieceColor them = us == PieceColor.BLACK ? PieceColor.WHITE : PieceColor.BLACK;
            int ySign = us == PieceColor.BLACK ? 1 : -1; // Needed because pawns can only move in a single y direction
            bool canMoveTwo = us == PieceColor.BLACK ? y == 2 : y == 7;

            // MOVE UP
            if (this.tile(x, y + (ySign*1)).Type == PieceType.EMPTY) {
                children.Add(new Node(this, x, y, x, y + (ySign*1)));
            }

            // MOVE UP TWO
            if (canMoveTwo && this.tile(x, y + (ySign*1)).Type == PieceType.EMPTY && this.tile(x, y + (ySign*2)).Type == PieceType.EMPTY) {
                children.Add(new Node(this, x, y, x, y + (ySign*2)));
            }

            // MOVE LEFT DIAGONAL
            if (this.tile(x - 1, y + (ySign * 1)).Color == them) {
                children.Add(new Node(this, x, y, x - 1, y + (ySign * 1)));
            }

            // MOVE RIGHT DIAGONAL
            if (this.tile(x + 1, y + (ySign * 1)).Color == them) {
                children.Add(new Node(this, x, y, x + 1, y + (ySign * 1)));
            }
        }

        private void generateKingMoves(int x, int y, List<Node> children) {
            PieceColor us = tile(x, y).Color;
            PieceColor them = us == PieceColor.BLACK ? PieceColor.WHITE : PieceColor.BLACK;

            // MOVE UP
            if (this.tile(x, y + 1).Type == PieceType.EMPTY || this.tile(x, y + 1).Color == them) {
                children.Add(new Node(this, x, y, x, y + 1));
            }

            // MOVE DOWN
            if (this.tile(x, y - 1).Type == PieceType.EMPTY || this.tile(x, y - 1).Color == them) {
                children.Add(new Node(this, x, y, x, y - 1));
            }

            // MOVE LEFT
            if (this.tile(x - 1, y).Type == PieceType.EMPTY || this.tile(x - 1, y).Color == them) {
                children.Add(new Node(this, x, y, x - 1, y));
            }

            // MOVE RIGHT
            if (this.tile(x + 1, y).Type == PieceType.EMPTY || this.tile(x + 1, y).Color == them) {
                children.Add(new Node(this, x, y, x + 1, y));
            }

            // MOVE UP RIGHT
            if (this.tile(x + 1, y + 1).Type == PieceType.EMPTY || this.tile(x + 1, y + 1).Color == them) {
                children.Add(new Node(this, x, y, x + 1, y + 1));
            }

            // MOVE UP LEFT
            if (this.tile(x - 1, y + 1).Type == PieceType.EMPTY || this.tile(x - 1, y + 1).Color == them) {
                children.Add(new Node(this, x, y, x - 1, y + 1));
            }

            // MOVE DOWN RIGHT
            if (this.tile(x + 1, y - 1).Type == PieceType.EMPTY || this.tile(x + 1, y - 1).Color == them) {
                children.Add(new Node(this, x, y, x + 1, y - 1));
            }

            // MOVE DOWN LEFT
            if (this.tile(x - 1, y - 1).Type == PieceType.EMPTY || this.tile(x - 1, y - 1).Color == them) {
                children.Add(new Node(this, x, y, x - 1, y - 1));
            }
        }

        private void generateKnightMoves(int x, int y, List<Node> children) {
            PieceColor us = tile(x, y).Color;
            PieceColor them = us == PieceColor.BLACK ? PieceColor.WHITE : PieceColor.BLACK;

            // MOVE UP LEFT
            if (this.tile(x - 1, y + 2).Type == PieceType.EMPTY || this.tile(x - 1, y + 2).Color == them) {
                children.Add(new Node(this, x, y, x - 1, y + 2));
            }

            // MOVE UP RIGHT
            if (this.tile(x + 1, y + 2).Type == PieceType.EMPTY || this.tile(x + 1, y + 2).Color == them) {
                children.Add(new Node(this, x, y, x + 1, y + 2));
            }

            // MOVE LEFT UP
            if (this.tile(x - 2, y + 1).Type == PieceType.EMPTY || this.tile(x - 2, y + 1).Color == them) {
                children.Add(new Node(this, x, y, x - 2, y + 1));
            }

            // MOVE RIGHT UP
            if (this.tile(x + 2, y + 1).Type == PieceType.EMPTY || this.tile(x + 2, y + 1).Color == them) {
                children.Add(new Node(this, x, y, x + 2, y + 1));
            }

            // MOVE DOWN LEFT
            if (this.tile(x - 1, y - 2).Type == PieceType.EMPTY || this.tile(x - 1, y - 2).Color == them) {
                children.Add(new Node(this, x, y, x - 1, y - 2));
            }

            // MOVE DOWN RIGHT
            if (this.tile(x + 1, y - 2).Type == PieceType.EMPTY || this.tile(x + 1, y - 2).Color == them) {
                children.Add(new Node(this, x, y, x + 1, y - 2));
            }

            // MOVE LEFT DOWN
            if (this.tile(x - 2, y - 1).Type == PieceType.EMPTY || this.tile(x - 2, y -1).Color == them) {
                children.Add(new Node(this, x, y, x - 2, y - 1));
            }

            // MOVE RIGHT DOWN
            if (this.tile(x + 2, y - 1).Type == PieceType.EMPTY || this.tile(x + 2, y - 1).Color == them) {
                children.Add(new Node(this, x, y, x + 2, y - 1));
            }
        }
        

        // Used for ROOK, BISHOP, and QUEEN
        private void generateUnboundedPieceMoves(int x, int y, string[] directions, List<Node> children) {
            PieceType type = tile(x, y).Type;
            PieceColor us = tile(x, y).Color;
            PieceColor them = us == PieceColor.BLACK ? PieceColor.WHITE : PieceColor.BLACK;

            int originalX = x;
            int originalY = y;

            foreach (string dir in directions) {
                while (movingIter(dir, ref x, ref y)) {
                    if (this.tile(x, y).Type == PieceType.EMPTY) {
                        children.Add(new Node(this, originalX, originalY, x, y));
                    }

                    if (this.tile(x, y).Color == them) {
                        children.Add(new Node(this, originalX, originalY, x, y));
                        x = originalX;
                        y = originalY;
                        break;
                    }

                    if (this.tile(x, y).Color == us) {
                        x = originalX;
                        y = originalY;
                        break;
                    }
                }

                x = originalX;
                y = originalY;
            }
        }

        private bool movingIter(string direction, ref int x, ref int y) {
            switch (direction) {
                case "UP":
                    return ++y <= 8;
                case "DOWN":
                    return --y >= 1;
                case "LEFT":
                    return --x >= 1;
                case "RIGHT":
                    return ++x <= 8;
                case "UP_LEFT":
                    return ++y <= 8 && --x >= 1;
                case "UP_RIGHT":
                    return ++y <= 8 && ++x <= 8;
                case "DOWN_LEFT":
                    return --y >= 1 && --x >= 1;
                case "DOWN_RIGHT":
                    return --y >= 1 && ++x <= 8;
            }

            return false;
        }

        private Piece tile(int x, int y) {
            if (x > 8 || x < 1 || y > 8 || y < 1) {
                return new Piece(PieceColor.INVALID, PieceType.INVALID);
            }

            x -= 1;
            y -= 1;

            return Tiles[(y * 8) + x];
        }

        private void tile(int x, int y, PieceColor color, PieceType type) {
            x -= 1;
            y -= 1;

            Tiles[(y * 8) + x] = new Piece(color, type);
        }
    }
}
