using System.Collections.Generic;

namespace ChessEngineLib
{
    using ChessPieces;

    public class Game
    {
        private Square _whiteKingsSquare;
        private Square _blackKingsSquare;

        private readonly List<Square> _occupiedSquares;
        private readonly List<Square> _squaresOccupiedByOpponent;

        private Square _currentColorKingSquare;

        private PieceColor _currentColor;


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

            _whiteKingsSquare = null;
            _blackKingsSquare = null;

            _occupiedSquares = new List<Square>();
            _squaresOccupiedByOpponent = new List<Square>();
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
            RefreshOccupiedSquares();
            _currentColor = color;
            Board.Iterate(KingSearchHandler);
            Board.Iterate(OpponentSquareHandler);

            foreach (var square in _squaresOccupiedByOpponent)
            {
                if (Board.IsLegalMove(square, _currentColorKingSquare))
                {
                    boolToReturn = true;
                }
            }

            return boolToReturn;
        }

        private void OpponentSquareHandler(Square square)
        {
            if (_currentColor.IsOppositeColor(square.Color))
                _squaresOccupiedByOpponent.Add(square);
        }

        private void KingSearchHandler(Square square)
        {
            if (IsOccupied(square)
                && square.Occupier is King
                && square.Color == _currentColor)
            {
                _currentColorKingSquare = square;
            }
        }

        private void RefreshOccupiedSquares()
        {
            _occupiedSquares.Clear();
            Board.Iterate(OnIterationHandler);
        }

        private void OnIterationHandler(Square square)
        {
            if (IsOccupied(square))
                _occupiedSquares.Add(square);
        }

        private bool IsOccupied(Square square)
        {
            return square.Occupier != null;
        }

        #endregion Yksityiset metodit
    }
}