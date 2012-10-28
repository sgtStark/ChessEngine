using System;

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

        private const int PAWN_CHAIN_RANK_FOR_WHITE = 2;
        private const int PAWN_CHAIN_RANK_FOR_BLACK = 7;

        private const int OFFICER_RANK_FOR_WHITE = 1;
        private const int OFFICER_RANK_FOR_BLACK = 8;


        private readonly Square[,] _squareMatrix;

        public event EventHandler<MoveEventArgs> OnMove;

        public Board()
        {
            _squareMatrix = new Square[DEFAULT_NUMBER_OF_FILES, DEFAULT_NUMBER_OF_RANKS];
            Initialize();
        }

        private void Initialize()
        {
            for (int file = 0; file < DEFAULT_NUMBER_OF_FILES; file++)
            {
                for (int rank = 0; rank < DEFAULT_NUMBER_OF_RANKS; rank++)
                {
                    _squareMatrix[file, rank] = new Square(file, rank, new NullPiece());
                }
            }
        }

        public void Setup()
        {
            Initialize();
            InitializeChessPieces(PieceColor.White);
            InitializeChessPieces(PieceColor.Black);
        }

        private void InitializeChessPieces(PieceColor pieceColor)
        {
            // P‰‰tell‰‰n sotilaiden ja upseereiden rivinumerot
            var pawnChainRank = pieceColor == PieceColor.White ? PAWN_CHAIN_RANK_FOR_WHITE : PAWN_CHAIN_RANK_FOR_BLACK;
            var officerRank = pieceColor == PieceColor.White ? OFFICER_RANK_FOR_WHITE : OFFICER_RANK_FOR_BLACK;

            // Sotilaat
            SetPosition(1, pawnChainRank, new Pawn(this, pieceColor));
            SetPosition(2, pawnChainRank, new Pawn(this, pieceColor));
            SetPosition(3, pawnChainRank, new Pawn(this, pieceColor));
            SetPosition(4, pawnChainRank, new Pawn(this, pieceColor));
            SetPosition(5, pawnChainRank, new Pawn(this, pieceColor));
            SetPosition(6, pawnChainRank, new Pawn(this, pieceColor));
            SetPosition(7, pawnChainRank, new Pawn(this, pieceColor));
            SetPosition(8, pawnChainRank, new Pawn(this, pieceColor));

            // Tornit
            SetPosition(1, officerRank, new Rook(this, pieceColor));
            SetPosition(8, officerRank, new Rook(this, pieceColor));

            // Hevoset
            SetPosition(2, officerRank, new Knight(this, pieceColor));
            SetPosition(7, officerRank, new Knight(this, pieceColor));

            // L‰hetit
            SetPosition(3, officerRank, new Bishop(this, pieceColor));
            SetPosition(6, officerRank, new Bishop(this, pieceColor));

            // Kuningatar
            SetPosition(4, officerRank, new Queen(this, pieceColor));

            // Kuningas
            SetPosition(5, officerRank, new King(this, pieceColor));
        }

        public void Iterate(Action<Square> onIteration)
        {
            for (int file = NUMBER_OF_THE_FIRST_FILE; file < DEFAULT_NUMBER_OF_FILES; file++)
            {
                for (int rank = NUMBER_OF_THE_FIRST_RANK; rank < DEFAULT_NUMBER_OF_RANKS; rank++)
                {
                    onIteration.Invoke(_squareMatrix[file, rank]);
                }
            }
        }

        public Square GetPosition(int file, int rank)
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
            return new Square(file, rank, new NullPiece());
        }

        public void SetPosition(int file, int rank, ChessPiece occupier)
        {
            if (AreOutsideBoardBoundaries(file, rank)) return;

            _squareMatrix[file, rank] = new Square(file, rank, occupier);
        }

        public bool IsLegalMove(Square origin, Square destination)
        {
            var chessPiece = origin.Occupier;
            var movingStrategy = chessPiece.GetMovingStrategy();

            if (chessPiece.IsLegalMove(origin, destination)) return true;
            if (movingStrategy.IsSpecialMove(origin, destination)) return true;

            return false;
        }

        public void Move(Square origin, Square destination)
        {
            if (!IsLegalMove(origin, destination)) return;

            var occupier = origin.Occupier;
            var movingStrategy = occupier.GetMovingStrategy();

            movingStrategy.Move(origin, destination);
            FireOnMoveEvent(new MoveEventArgs(origin, destination));
        }

        public Board SimulatedMove(Square origin, Square destination)
        {
            var simulatedBoard = new Board();

            foreach (var square in _squareMatrix)
            {
                simulatedBoard.SetPosition(square.File, square.Rank, square.Occupier.Clone(simulatedBoard));
            }

            simulatedBoard.Move
                (
                    simulatedBoard.GetPosition(origin.File, origin.Rank),
                    simulatedBoard.GetPosition(destination.File, destination.Rank)
                );

            return simulatedBoard;
        }

        private void FireOnMoveEvent(MoveEventArgs eventArgs)
        {
            if (OnMove != null)
            {
                OnMove.Invoke(this, eventArgs);
            }
        }

        public bool IsLightSquare(int file, int rank)
        {
            // Kun sarake on jaollinen itsell‰‰n ja rivi ei ole 
            // jaollinen itsell‰‰n on kyseess‰ vaalea ruutu
            // Kun sarake ei ole jaollinen itsell‰‰n ja rivi on 
            // jaollinen itsell‰‰n on kyseess‰ vaalea ruutu
            bool boolToReturn = file % 2 == 0
                                && rank % 2 != 0
                                || file % 2 != 0
                                && rank % 2 == 0;

            return boolToReturn;
        }

        private bool Equals(Board other)
        {
            if (ReferenceEquals(this, other)) return true;

            var boolToReturn = true;

            foreach (var square in _squareMatrix)
            {
                if (!square.Equals(other.GetPosition(square.File, square.Rank)))
                {
                    boolToReturn = false;
                    break;
                }
            }

            return boolToReturn;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Board)) return false;
            return Equals((Board) obj);
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