using System.Linq;
using System.Collections.Generic;

namespace ChessEngineLib
{
    using ChessPieces;

    public class Position
    {
        private const int NUMBER_OF_THE_FIRST_FILE = 1;
        private const int NUMBER_OF_THE_FIRST_RANK = 1;

        private const int NUMBER_OF_THE_LAST_FILE = 8;
        private const int NUMBER_OF_THE_LAST_RANK = 8;

        private readonly List<Square> _allSquares;

        public Position(List<Square> allSquares)
        {
            _allSquares = allSquares;
        }

        public bool WhiteKingIsInCheck()
        {
            return KingIsInCheck(SquareOccupiedByTheKingWith(PieceColor.White), SquaresOccupiedByPiecesWith(PieceColor.Black));
        }

        private Square SquareOccupiedByTheKingWith(PieceColor pieceColor)
        {
            return _allSquares.SingleOrDefault(square => square.Occupier is King &&
                                                         square.Color == pieceColor);
        }

        public IEnumerable<Square> SquaresOccupiedByPiecesWith(PieceColor pieceColor)
        {
            return _allSquares
                .Where(square => square.Color == pieceColor)
                .ToList();
        }

        private bool KingIsInCheck(Square squareOccupiedByTheKing, IEnumerable<Square> squaresOccupiedByTheOppositeColor)
        {
            if (squareOccupiedByTheKing == null || squaresOccupiedByTheOppositeColor == null) return false;
            var kingIsInCheck = false;

            foreach (var squareOccupiedByTheOppositeColor in squaresOccupiedByTheOppositeColor)
            {
                if (MoveIsLegal(squareOccupiedByTheOppositeColor, squareOccupiedByTheKing))
                {
                    kingIsInCheck = true;
                    break;
                }
            }

            return kingIsInCheck;
        }

        public bool MoveIsLegal(Square origin, Square destination)
        {
            if (AreOutsideBoardBoundaries(origin.File, origin.Rank)) return false;
            if (AreOutsideBoardBoundaries(destination.File, destination.Rank)) return false;

            var occupier = origin.Occupier;
            var movingStrategy = occupier.GetMovingStrategy();

            return occupier.IsLegalMove(origin, destination) ||
                   movingStrategy.IsSpecialMove(origin, destination);
        }

        private static bool AreOutsideBoardBoundaries(int file, int rank)
        {
            return file < NUMBER_OF_THE_FIRST_FILE || rank < NUMBER_OF_THE_FIRST_RANK
                || file > NUMBER_OF_THE_LAST_FILE || rank > NUMBER_OF_THE_LAST_RANK;
        }

        public bool BlackKingIsInCheck()
        {
            return KingIsInCheck(SquareOccupiedByTheKingWith(PieceColor.Black),
                                 SquaresOccupiedByPiecesWith(PieceColor.White));
        }

        public bool HasLegalMoves(PieceColor playerToMove)
        {
            var playerHasLegalMoves = false;
            var allSquares = new List<Square>();
            _allSquares.ForEach(allSquares.Add);

            foreach (var origin in SquaresOccupiedByPiecesWith(playerToMove))
            {
                allSquares.Remove(origin);
                playerHasLegalMoves = allSquares.Any(destination => KingIsReleasedBy(origin, destination));
                if (playerHasLegalMoves) break;
            }

            return playerHasLegalMoves;
        }

        private bool KingIsReleasedBy(Square origin, Square destination)
        {
            if (MoveIsIllegal(origin, destination)) return false;
            var simulatedPosition = SimulateMove(origin, destination);
            var squareOccupiedByTheKing = simulatedPosition.SquareOccupiedByTheKingWith(origin.Color);
            var oppositeColorPieces = simulatedPosition.SquaresOccupiedByPiecesWith(origin.Color.GetOppositeColor());
            return !simulatedPosition.KingIsInCheck(squareOccupiedByTheKing, oppositeColorPieces);
        }

        public bool MoveIsIllegal(Square origin, Square destination)
        {
            return !MoveIsLegal(origin, destination);
        }

        private Position SimulateMove(Square origin, Square destination)
        {
            var simulationBoard = new Board();

            foreach (var square in _allSquares)
                simulationBoard.SetSquare(square.File, square.Rank, square.Occupier.Clone(simulationBoard));

            simulationBoard.Move(simulationBoard.GetSquare(origin.File, origin.Rank), 
                                 simulationBoard.GetSquare(destination.File, destination.Rank));

            return simulationBoard.GetPosition();
        }
    }
}