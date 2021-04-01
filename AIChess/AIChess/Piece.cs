namespace AIChess {
    public class Piece {
        public PieceColor Color { get; private set; }
        public PieceType Type { get; private set; }

        public Piece(PieceColor color, PieceType type) {
            Color = color;
            Type = type;
        }
    }

    public enum PieceColor {
        WHITE,
        BLACK,
        EMPTY,
        INVALID,
    }

    public enum PieceType {
        KING,
        QUEEN,
        ROOK,
        BISHOP,
        KNIGHT,
        PAWN,
        EMPTY,
        INVALID,
    }
}
