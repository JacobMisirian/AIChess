using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIChess {
    public partial class Board : Form {
        private const string BLACK_PAWN = "../../BlackPawn.png";
        private const string BLACK_ROOK = "../../BlackRook.png";
        private const string BLACK_BISHOP = "../../BlackBishop.png";
        private const string BLACK_KNIGHT = "../../BlackKnight.png";
        private const string BLACK_QUEEN = "../../BlackQueen.png";
        private const string BLACK_KING = "../../BlackKing.png";

        private const string WHITE_PAWN = "../../WhitePawn.png";
        private const string WHITE_ROOK = "../../WhiteRook.png";
        private const string WHITE_BISHOP = "../../WhiteBishop.png";
        private const string WHITE_KNIGHT = "../../WhiteKnight.png";
        private const string WHITE_QUEEN = "../../WhiteQueen.png";
        private const string WHITE_KING = "../../WhiteKing.png";

        private const string EMPTY = "../../Empty.jpg";
        private const string EMPTY_BLACK = "../../EmptyBlack.png";

        private const int SELECT_BORDER_SIZE = 10;

        private PictureBox[] pictureBoxes;
        private Game game;
        public Board() {
            InitializeComponent();

            pictureBoxes = new PictureBox[64];

            int width = 80;
            int height = 80;
            int distance = 5;
            int startX = 10;
            int startY = 10;

            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Left = startX + (x * height + distance);
                    pictureBox.Top = startY + (y * width + distance);
                    pictureBox.Width = width;
                    pictureBox.Height = height;
                    int tmpX = x;
                    int tmpY = y;
                    pictureBox.Click += (s, e) => { selectSquare(tmpX + 1, tmpY + 1); };

                    Controls.Add(pictureBox);
                    pictureBoxes[y * 8 + x] = pictureBox;
                }
            }

            game = new Game();
            refreshBoxes();
        }

        private void refreshBoxes() {
            bool color = false;
            for (int i = 0; i < pictureBoxes.Length; i++) {
                if (game.Current.Tiles[i].Color == PieceColor.WHITE && game.Current.Tiles[i].Type == PieceType.PAWN)
                    pictureBoxes[i].Load(WHITE_PAWN);
                else if (game.Current.Tiles[i].Color == PieceColor.WHITE && game.Current.Tiles[i].Type == PieceType.ROOK)
                    pictureBoxes[i].Load(WHITE_ROOK);
                else if (game.Current.Tiles[i].Color == PieceColor.WHITE && game.Current.Tiles[i].Type == PieceType.BISHOP)
                    pictureBoxes[i].Load(WHITE_BISHOP);
                else if (game.Current.Tiles[i].Color == PieceColor.WHITE && game.Current.Tiles[i].Type == PieceType.KNIGHT)
                    pictureBoxes[i].Load(WHITE_KNIGHT);
                else if (game.Current.Tiles[i].Color == PieceColor.WHITE && game.Current.Tiles[i].Type == PieceType.QUEEN)
                    pictureBoxes[i].Load(WHITE_QUEEN);
                else if (game.Current.Tiles[i].Color == PieceColor.WHITE && game.Current.Tiles[i].Type == PieceType.KING)
                    pictureBoxes[i].Load(WHITE_KING);
                else if (game.Current.Tiles[i].Color == PieceColor.BLACK && game.Current.Tiles[i].Type == PieceType.PAWN)
                    pictureBoxes[i].Load(BLACK_PAWN);
                else if (game.Current.Tiles[i].Color == PieceColor.BLACK && game.Current.Tiles[i].Type == PieceType.ROOK)
                    pictureBoxes[i].Load(BLACK_ROOK);
                else if (game.Current.Tiles[i].Color == PieceColor.BLACK && game.Current.Tiles[i].Type == PieceType.BISHOP)
                    pictureBoxes[i].Load(BLACK_BISHOP);
                else if (game.Current.Tiles[i].Color == PieceColor.BLACK && game.Current.Tiles[i].Type == PieceType.KNIGHT)
                    pictureBoxes[i].Load(BLACK_KNIGHT);
                else if (game.Current.Tiles[i].Color == PieceColor.BLACK && game.Current.Tiles[i].Type == PieceType.QUEEN)
                    pictureBoxes[i].Load(BLACK_QUEEN);
                else if (game.Current.Tiles[i].Color == PieceColor.BLACK && game.Current.Tiles[i].Type == PieceType.KING)
                    pictureBoxes[i].Load(BLACK_KING);
                else if (color == true)
                    pictureBoxes[i].Load(EMPTY_BLACK);
                else
                    pictureBoxes[i].Load(EMPTY);
                if (!(i > 1 && (i + 1) % 8 == 0))
                    color = !color;
            }
        }

        private int selectedX = -1;
        private int selectedY = -1;
        private void selectSquare(int x, int y) {
            if (selectedX == -1 && selectedY == -1 && game.Current.Tiles[(y - 1) * 8 + x - 1].Color != PieceColor.WHITE) {
                MessageBox.Show("Select one of your own tiles!");
            } else if (selectedX == -1 && selectedY == -1) {
                selectedX = x;
                selectedY = y;

                var validMoves = game.Current.GetChildren(PieceColor.WHITE, removeBadChildren: false).Select((n) => n.MovedFrom == (selectedX, selectedY) ? n : null);
                bool existsValidMove = false;
                foreach (var validMove in validMoves) {
                    if (validMove == null) continue;
                    existsValidMove = true;
                    int validX = validMove.MovedTo.Item1 - 1;
                    int validY = validMove.MovedTo.Item2 - 1;
                    var pictureBox = pictureBoxes[(validY * 8) + validX];
                    ControlPaint.DrawBorder(pictureBox.CreateGraphics(), pictureBox.ClientRectangle,
                                  Color.Red, SELECT_BORDER_SIZE, ButtonBorderStyle.Inset,
                                  Color.Red, SELECT_BORDER_SIZE, ButtonBorderStyle.Inset,
                                  Color.Red, SELECT_BORDER_SIZE, ButtonBorderStyle.Inset,
                                  Color.Red, SELECT_BORDER_SIZE, ButtonBorderStyle.Inset);
                }

                if (!existsValidMove) {
                    selectedX = -1;
                    selectedY = -1;
                }

            } else {
                if (!game.HumanMakeMove(selectedX, selectedY, x, y)) {
                    MessageBox.Show("INVALID MOVE!");
                    selectedX = -1;
                    selectedY = -1;
                    refreshBoxes();
                    return;
                }

                if (!game.AIMakeMove()) {
                    MessageBox.Show("Checkmate!");
                }
                refreshBoxes();

                selectedX = -1;
                selectedY = -1;
            }
        }

        private void Board_Load(object sender, EventArgs e) {

        }
    }
}
