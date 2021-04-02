using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIChess {
    public class Node {
        public Piece[] Tiles { get; private set; }
        public int Depth { get; private set; }

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

        public List<Node> GetChildren(PieceColor color) {
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



            return children;
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
                        res += 1.0 + pawnEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.ROOK:
                        res += 5.0 + rookEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.BISHOP:
                        res += 3.0 + bishopEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.KNIGHT:
                        res += 3.0 + knightEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.KING:
                        res += 1000.0 + kingEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
                        break;
                    case PieceType.QUEEN:
                        res += 9.0 + queenEvalBlack[coord.Item1 - 1, coord.Item2 - 1];
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
            Node up = new Node(this);
            if (up.tile(x, y + (ySign*1)).Type == PieceType.EMPTY) {
                up.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                up.tile(x, y + (ySign*1), us, PieceType.PAWN);
                children.Add(up);
            }

            // MOVE UP TWO
            Node upTwo = new Node(this);
            if (canMoveTwo && upTwo.tile(x, y + (ySign*1)).Type == PieceType.EMPTY && upTwo.tile(x, y + (ySign*2)).Type == PieceType.EMPTY) {
                upTwo.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                upTwo.tile(x, y + (ySign*2), us, PieceType.PAWN);
                children.Add(upTwo);
            }

            // MOVE LEFT DIAGONAL
            Node leftDiagonal = new Node(this);
            if (leftDiagonal.tile(x - 1, y + (ySign * 1)).Color == them) {
                leftDiagonal.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                leftDiagonal.tile(x - 1, y + (ySign * 1), us, PieceType.PAWN);
                children.Add(leftDiagonal);
            }

            // MOVE RIGHT DIAGONAL
            Node rightDiagonal = new Node(this);
            if (rightDiagonal.tile(x + 1, y + (ySign * 1)).Color == them) {
                rightDiagonal.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                rightDiagonal.tile(x + 1, y + (ySign * 1), us, PieceType.PAWN);
                children.Add(rightDiagonal);
            }
        }

        private void generateKingMoves(int x, int y, List<Node> children) {
            PieceColor us = tile(x, y).Color;
            PieceColor them = us == PieceColor.BLACK ? PieceColor.WHITE : PieceColor.BLACK;

            // MOVE UP
            Node up = new Node(this);
            if (up.tile(x, y + 1).Type == PieceType.EMPTY || up.tile(x, y + 1).Color == them) {
                up.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                up.tile(x, y + 1, us, PieceType.KING);
                children.Add(up);
            }

            // MOVE DOWN
            Node down = new Node(this);
            if (down.tile(x, y - 1).Type == PieceType.EMPTY || down.tile(x, y - 1).Color == them) {
                down.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                down.tile(x, y - 1, us, PieceType.KING);
                children.Add(down);
            }

            // MOVE LEFT
            Node left = new Node(this);
            if (left.tile(x - 1, y).Type == PieceType.EMPTY || left.tile(x - 1, y).Color == them) {
                left.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                left.tile(x - 1, y, us, PieceType.KING);
                children.Add(left);
            }

            // MOVE RIGHT
            Node right = new Node(this);
            if (right.tile(x + 1, y).Type == PieceType.EMPTY || left.tile(x + 1, y).Color == them) {
                right.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                right.tile(x + 1, y, us, PieceType.KING);
                children.Add(right);
            }

            // MOVE UP RIGHT
            Node upRight = new Node(this);
            if (upRight.tile(x + 1, y + 1).Type == PieceType.EMPTY || upRight.tile(x + 1, y + 1).Color == them) {
                upRight.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                upRight.tile(x + 1, y + 1, us, PieceType.KING);
                children.Add(upRight);
            }

            // MOVE UP LEFT
            Node upLeft = new Node(this);
            if (upLeft.tile(x - 1, y + 1).Type == PieceType.EMPTY || upLeft.tile(x - 1, y + 1).Color == them) {
                upLeft.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                upLeft.tile(x - 1, y + 1, us, PieceType.KING);
                children.Add(upLeft);
            }

            // MOVE DOWN RIGHT
            Node downRight = new Node(this);
            if (downRight.tile(x + 1, y - 1).Type == PieceType.EMPTY || downRight.tile(x + 1, y - 1).Color == them) {
                downRight.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                downRight.tile(x + 1, y - 1, us, PieceType.KING);
                children.Add(downRight);
            }

            // MOVE DOWN LEFT
            Node downLeft = new Node(this);
            if (downLeft.tile(x - 1, y - 1).Type == PieceType.EMPTY || downLeft.tile(x - 1, y - 1).Color == them) {
                downLeft.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                downLeft.tile(x - 1, y - 1, us, PieceType.KING);
                children.Add(downLeft);
            }
        }

        private void generateKnightMoves(int x, int y, List<Node> children) {
            PieceColor us = tile(x, y).Color;
            PieceColor them = us == PieceColor.BLACK ? PieceColor.WHITE : PieceColor.BLACK;

            // MOVE UP LEFT
            Node upLeft = new Node(this);
            if (upLeft.tile(x - 1, y + 2).Type == PieceType.EMPTY || upLeft.tile(x - 1, y + 2).Color == them) {
                upLeft.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                upLeft.tile(x - 1, y + 2, us, PieceType.KNIGHT);
                children.Add(upLeft);
            }

            // MOVE UP RIGHT
            Node upRight = new Node(this);
            if (upRight.tile(x + 1, y + 2).Type == PieceType.EMPTY || upRight.tile(x + 1, y + 2).Color == them) {
                upRight.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                upRight.tile(x + 1, y + 2, us, PieceType.KNIGHT);
                children.Add(upRight);
            }

            // MOVE LEFT UP
            Node leftUp = new Node(this);
            if (leftUp.tile(x - 2, y + 1).Type == PieceType.EMPTY || leftUp.tile(x - 2, y + 1).Color == them) {
                leftUp.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                leftUp.tile(x - 2, y + 1, us, PieceType.KNIGHT);
                children.Add(leftUp);
            }

            // MOVE RIGHT UP
            Node rightUp = new Node(this);
            if (rightUp.tile(x + 2, y + 1).Type == PieceType.EMPTY || rightUp.tile(x + 2, y + 1).Color == them) {
                rightUp.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                rightUp.tile(x + 2, y + 1, us, PieceType.KNIGHT);
                children.Add(rightUp);
            }

            // MOVE DOWN LEFT
            Node downLeft = new Node(this);
            if (downLeft.tile(x - 1, y - 2).Type == PieceType.EMPTY || downLeft.tile(x - 1, y - 2).Color == them) {
                downLeft.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                downLeft.tile(x - 1, y - 2, us, PieceType.KNIGHT);
                children.Add(downLeft);
            }

            // MOVE DOWN RIGHT
            Node downRight = new Node(this);
            if (downRight.tile(x + 1, y - 2).Type == PieceType.EMPTY || downRight.tile(x + 1, y - 2).Color == them) {
                downRight.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                downRight.tile(x + 1, y - 2, us, PieceType.KNIGHT);
                children.Add(downRight);
            }

            // MOVE LEFT DOWN
            Node leftDown = new Node(this);
            if (leftDown.tile(x - 2, y - 1).Type == PieceType.EMPTY || leftDown.tile(x - 2, y -1).Color == them) {
                leftDown.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                leftDown.tile(x - 2, y - 1, us, PieceType.KNIGHT);
                children.Add(leftDown);
            }

            // MOVE RIGHT DOWN
            Node rightDown = new Node(this);
            if (rightDown.tile(x + 2, y - 1).Type == PieceType.EMPTY || rightDown.tile(x + 2, y - 1).Color == them) {
                rightDown.tile(x, y, PieceColor.EMPTY, PieceType.EMPTY);
                rightDown.tile(x + 2, y - 1, us, PieceType.KNIGHT);
                children.Add(rightDown);
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
                Node child = new Node(this);
                while (movingIter(dir, ref x, ref y)) {
                    if (child.tile(x, y).Type == PieceType.EMPTY) {
                        child.tile(originalX, originalY, PieceColor.EMPTY, PieceType.EMPTY);
                        child.tile(x, y, us, type);
                        children.Add(child);
                        child = new Node(this);
                    }

                    if (child.tile(x, y).Color == them) {
                        child.tile(originalX, originalY, PieceColor.EMPTY, PieceType.EMPTY);
                        child.tile(x, y, us, type);
                        children.Add(child);
                        x = originalX;
                        y = originalY;
                        break;
                    }

                    if (child.tile(x, y).Color == us) {
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
