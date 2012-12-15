using System.Collections.Generic;

namespace ChessEngineLib.GameStateDetection
{
    using ChessPieces;

    public class CheckStateDetector
    {
        private readonly List<Square> _squaresOccupiedByOpponent;

        private PieceColor _currentColor;
        private Square _currentColorKingSquare;

        public CheckStateDetector()
        {
            _currentColor = PieceColor.Empty;
            _squaresOccupiedByOpponent = new List<Square>();
        }

        public bool KingIsChecked(Board board, PieceColor color)
        {
            var boolToReturn = false;

            _currentColor = color;
            _squaresOccupiedByOpponent.Clear();

            board.Iterate(KingSearchHandler);
            board.Iterate(OpponentSquareHandler);

            foreach (var square in _squaresOccupiedByOpponent)
            {
                if (board.IsLegalMove(square, _currentColorKingSquare))
                {
                    boolToReturn = true;
                }
            }

            return boolToReturn;
        }

        private void KingSearchHandler(Square square)
        {
            if (square.Color == _currentColor && square.Occupier is King)
                _currentColorKingSquare = square;
        }

        private void OpponentSquareHandler(Square square)
        {
            if (_currentColor.IsOppositeColor(square.Color))
                _squaresOccupiedByOpponent.Add(square);
        }
    }
}
