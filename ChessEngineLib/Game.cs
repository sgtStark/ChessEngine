using System.Linq;
using System.Collections.Generic;
using ChessEngineLib.ChessPieces;

namespace ChessEngineLib
{
    public class Game
    {
        #region Propertyt

        public Board Board { get; private set; }

        public GameState State { get; private set; }

        public PieceColor PlayerToMove { get; private set; }

        #endregion Propertyt

        #region Konstruktorit

        public Game(Board board)
        {
            Board = board;
            board.OnMove += Update;
            State = GameState.SetupMode;
            PlayerToMove = PieceColor.Empty;
        }

        #endregion Konstruktorit

        #region Julkiset metodit

        public void Start()
        {
            State = GameState.Normal;
            PlayerToMove = PieceColor.White;
        }

        #endregion Julkiset metodit

        #region Yksityiset metodit

        private void Update(object sender, MoveEventArgs e)
        {
            PlayerToMove = e.From.Color == PieceColor.White ? PieceColor.Black : PieceColor.White;

            if (IsKingChecked(PieceColor.White)
                || IsKingChecked(PieceColor.Black))
            {
                State = GameState.Check;
            }
            else
            {
                State = GameState.Normal;
            }
        }

        private bool IsKingChecked(PieceColor color)
        {
            var boolToReturn = false;
            var occupiedSquares = OccupiedSquares();
            var king = new King(color);

            var squareOccupiedByKing = occupiedSquares
                .SingleOrDefault(square => king.Equals(square.Occupier));

            var whiteOccupiedSquares = occupiedSquares
                .Where(square => square.Color.IsOppositeColor(color));

            foreach (var whiteOccupiedSquare in whiteOccupiedSquares)
            {
                if (Board.IsLegalMove(whiteOccupiedSquare, squareOccupiedByKing))
                {
                    boolToReturn = true;
                }
            }

            return boolToReturn;
        }

        private List<Square> OccupiedSquares()
        {
            var occupiedSquaresToReturn = new List<Square>();

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    var square = Board.GetPosition(i, j);

                    if (square.Occupier != null)
                    {
                        occupiedSquaresToReturn.Add(square);
                    }
                }
            }

            return occupiedSquaresToReturn;
        }

        #endregion Yksityiset metodit
    }
}