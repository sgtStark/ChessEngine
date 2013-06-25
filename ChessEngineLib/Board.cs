using System;
using System.Collections.Generic;

namespace ChessEngineLib
{
    using ChessPieces;

    public class Board : ICloneable
    {
        private const int DEFAULT_NUMBER_OF_FILES = 9;
        private const int DEFAULT_NUMBER_OF_RANKS = 9;

        private const int NUMBER_OF_THE_FIRST_FILE = 1;
        private const int NUMBER_OF_THE_FIRST_RANK = 1;

        private const int NUMBER_OF_THE_LAST_FILE = 8;
        private const int NUMBER_OF_THE_LAST_RANK = 8;

        private readonly Square[,] _squareMatrix;
        private readonly List<BoardObserver> _boardObservers;

        public Board()
        {
            _squareMatrix = new Square[DEFAULT_NUMBER_OF_FILES, DEFAULT_NUMBER_OF_RANKS];
            _boardObservers = new List<BoardObserver>();
            Initialize();
        }

        private void Initialize()
        {
            for (int file = 0; file < DEFAULT_NUMBER_OF_FILES; file++)
            {
                for (int rank = 0; rank < DEFAULT_NUMBER_OF_RANKS; rank++)
                {
                    _squareMatrix[file, rank] = new Square(file, rank, new NullPiece(this));
                }
            }
        }

        public void Attach(BoardObserver boardObserver)
        {
            _boardObservers.Add(boardObserver);
        }

        public void Detach(BoardObserver boardObserver)
        {
            _boardObservers.Remove(boardObserver);
        }

        public void Setup()
        {
            Initialize();
            InitializeChessPieces(PieceColor.White);
            InitializeChessPieces(PieceColor.Black);
        }

        private void InitializeChessPieces(PieceColor pieceColor)
        {
            // Pawns
            SetSquare(1, pieceColor.PawnChainRank(), new Pawn(this, pieceColor));
            SetSquare(2, pieceColor.PawnChainRank(), new Pawn(this, pieceColor));
            SetSquare(3, pieceColor.PawnChainRank(), new Pawn(this, pieceColor));
            SetSquare(4, pieceColor.PawnChainRank(), new Pawn(this, pieceColor));
            SetSquare(5, pieceColor.PawnChainRank(), new Pawn(this, pieceColor));
            SetSquare(6, pieceColor.PawnChainRank(), new Pawn(this, pieceColor));
            SetSquare(7, pieceColor.PawnChainRank(), new Pawn(this, pieceColor));
            SetSquare(8, pieceColor.PawnChainRank(), new Pawn(this, pieceColor));

            // Officers
            SetSquare(1, pieceColor.OfficerRank(), new Rook(this, pieceColor));
            SetSquare(8, pieceColor.OfficerRank(), new Rook(this, pieceColor));
            SetSquare(2, pieceColor.OfficerRank(), new Knight(this, pieceColor));
            SetSquare(7, pieceColor.OfficerRank(), new Knight(this, pieceColor));
            SetSquare(3, pieceColor.OfficerRank(), new Bishop(this, pieceColor));
            SetSquare(6, pieceColor.OfficerRank(), new Bishop(this, pieceColor));
            SetSquare(4, pieceColor.OfficerRank(), new Queen(this, pieceColor));
            SetSquare(5, pieceColor.OfficerRank(), new King(this, pieceColor));
        }

        public Position GetPosition()
        {
            var allSquares = new List<Square>();

            foreach (var square in _squareMatrix)
                allSquares.Add(square);

            return new Position(allSquares);
        }

        public Square GetSquare(int file, int rank)
        {
            if (AreOutsideBoardBoundaries(file, rank)) return CreateNullSquare(file, rank);

            return _squareMatrix[file, rank];
        }

        private static bool AreOutsideBoardBoundaries(int file, int rank)
        {
            return file < NUMBER_OF_THE_FIRST_FILE || rank < NUMBER_OF_THE_FIRST_RANK
                || file > NUMBER_OF_THE_LAST_FILE || rank > NUMBER_OF_THE_LAST_RANK;
        }

        private Square CreateNullSquare(int file, int rank)
        {
            return new Square(file, rank, new NullPiece(this));
        }

        public void SetSquare(int file, int rank, ChessPiece occupier)
        {
            if (AreOutsideBoardBoundaries(file, rank)) return;

            _squareMatrix[file, rank] = new Square(file, rank, occupier);
        }

        public void Move(Square origin, Square destination)
        {
            var currentPosition = GetPosition();
            if (currentPosition.MoveIsIllegal(origin, destination)) return;

            var occupier = origin.Occupier;
            var movingStrategy = occupier.GetMovingStrategy();

            movingStrategy.Move(origin, destination);
            NotifyBoardObservers(origin, destination);
        }

        private void NotifyBoardObservers(Square origin, Square destination)
        {
            var newPosition = GetPosition();
            _boardObservers.ForEach(boardObserver => boardObserver.OnMove(origin, destination, newPosition));
        }

        public bool IsLightSquare(int file, int rank)
        {
            return file % 2 == 0 &&
                   rank % 2 != 0 ||
                   file % 2 != 0 &&
                   rank % 2 == 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Board)) return false;
            return Equals((Board) obj);
        }

        private bool Equals(Board other)
        {
            if (ReferenceEquals(this, other)) return true;

            var boolToReturn = true;

            foreach (var square in _squareMatrix)
            {
                if (!square.Equals(other.GetSquare(square.File, square.Rank)))
                {
                    boolToReturn = false;
                    break;
                }
            }

            return boolToReturn;
        }

        public override int GetHashCode()
        {
            return (_squareMatrix != null ? _squareMatrix.GetHashCode() : 0);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}