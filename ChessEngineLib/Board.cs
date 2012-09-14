using System;

namespace ChessEngineLib
{
    using Exceptions;
    using ChessPieces;

    /// <summary>
    /// Luokka, joka kuvaa kaikki shakkilautaa koskevat toiminnot.
    /// </summary>
    public class Board
    {
        #region Vakiot

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

        #endregion Vakiot

        #region Sisäiset datajäsenet

        /// <summary>
        /// Ruudut ja niiden sisällön sisältävä matriisi.
        /// </summary>
        private readonly Square[,] _squareMatrix;

        public event EventHandler<MoveEventArgs> OnMove;

        #endregion Sisäiset datajäsenet

        #region Konstruktorit

        /// <summary>
        /// Oletusmuodostin
        /// </summary>
        public Board()
        {
            _squareMatrix = new Square[DEFAULT_NUMBER_OF_FILES, DEFAULT_NUMBER_OF_RANKS];
            Initialize();
        }

        #endregion Konstruktorit

        #region Julkiset metodit

        /// <summary>
        /// Järjestää shakkilaudan lähtökohtaan.
        /// </summary>
        public void Setup()
        {
            Initialize();
            InitializeChessPieces(PieceColor.White);
            InitializeChessPieces(PieceColor.Black);
        }

        /// <summary>
        /// Hakee ruudun sisällön shakkilaudalta.
        /// </summary>
        /// <param name="file">Ruudun sarakekomponentti.</param>
        /// <param name="rank">Ruudun rivikomponentti.</param>
        /// <returns>Ruudun tiedot kuvaava Square-olio.</returns>
        public Square GetPosition(int file, int rank)
        {
            if (AreOutsideBoardBoundaries(file, rank))
            {
                // TODO: Refactoroi niin että poikkeus on ChessEngineException-tyyppinen.
                throw new ArgumentOutOfRangeException("rank and file parameter values must be between 1 and 8");
            }

            return _squareMatrix[file, rank];
        }

        /// <summary>
        /// Asettaa ruudun shakkilaudalla.
        /// </summary>
        /// <param name="file">Ruudun sarakekomponentti.</param>
        /// <param name="rank">Ruudun rivikomponentti.</param>
        /// <param name="occupier">Ruudun miehittävä shakkinappula.</param>
        public void SetPosition(int file, int rank, ChessPiece occupier)
        {
            if (AreOutsideBoardBoundaries(file, rank))
            {
                // TODO: Refactoroi niin että poikkeus on ChessEngineException-tyyppinen.
                throw new ArgumentOutOfRangeException("rank and file parameter values must be between 1 and 8");
            }

            _squareMatrix[file, rank] = new Square(file, rank, occupier);
        }

        /// <summary>
        /// Tarkastaa onko siirto laillinen.
        /// </summary>
        /// <param name="origin">Siirron lähtöruutu.</param>
        /// <param name="destination">Siirron kohderuutu.</param>
        /// <returns>True, jos siirto on laillinen. False, jos siirto on laiton.</returns>
        public bool IsLegalMove(Square origin, Square destination)
        {
            var boolToReturn = false;
            var chessPiece = origin.Occupier;
            var movingStrategy = chessPiece != null ? chessPiece.GetMovingStrategy() : null;

            // Tarkastetaan shakkinappulan siirron laillisuus
            if (chessPiece != null
                && chessPiece.IsLegalMove(this, origin, destination))
            {
                boolToReturn = true;
            }
            // Tarkastetaan onko shakkinappulan siirto strategiaan liittyvä
            // erikois siirto, joka on laillinen.
            else if (movingStrategy != null
                && movingStrategy.IsSpecialMove(this, origin, destination))
            {
                boolToReturn = true;
            }

            return boolToReturn;
        }

        /// <summary>
        /// Tekee siirron shakkilaudalla.
        /// </summary>
        /// <param name="origin">Siirron lähtöruutu.</param>
        /// <param name="destination">Siirron kohderuutu.</param>
        public void Move(Square origin, Square destination)
        {
            // Jos siirto on laillinen asetetaan miehittäjä lähde ruudusta kohderuutuun
            // ja poistetaan miehittäjä lähtöruudusta.
            if (IsLegalMove(origin, destination))
            {
                var occupier = origin.Occupier;
                var movingStrategy = occupier.GetMovingStrategy();

                movingStrategy.Move(this, origin, destination);
                FireOnMoveEvent(new MoveEventArgs(origin, destination));
            }
            // Jos siirto oli laiton, nostetaan laittoman siirron poikkeus.
            else
            {
                // TODO: Lisää järkevät virheviestit ja testaa lisää ne testeihin!
                throw new IllegalMoveException("");
            }
        }

        /// <summary>
        /// Tarkastaa onko kyseinen ruutu vaalea pohjainen.
        /// </summary>
        /// <param name="file">Ruudun ilmaiseva sarakekomponentti.</param>
        /// <param name="rank">Ruudun ilmaiseva rivikomponentti.</param>
        /// <returns>True, jos ruutu on vaalea. False, jos ruutu on tumma.</returns>
        public bool IsLightSquare(int file, int rank)
        {
            // Kun sarake on jaollinen itsellään ja rivi ei ole 
            // jaollinen itsellään on kyseessä vaalea ruutu
            // Kun sarake ei ole jaollinen itsellään ja rivi on 
            // jaollinen itsellään on kyseessä vaalea ruutu
            bool boolToReturn = file % 2 == 0
                                && rank % 2 != 0
                                || file % 2 != 0
                                && rank % 2 == 0;

            return boolToReturn;
        }

        /// <summary>
        /// Determines whether the specified Board is equal to the current Board.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private bool Equals(Board other)
        {
            bool boolToReturn = ReferenceEquals(this, other);

            foreach (var position in _squareMatrix)
            {
                if (position == null) continue;

                var otherPosition = other != null ? other.GetPosition(position.File, position.Rank) : null;
                
                if (position.Equals(otherPosition))
                {
                    boolToReturn = true;
                }
                else
                {
                    boolToReturn = false;
                    break;
                }
            }

            return boolToReturn;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Board)) return false;
            return Equals((Board) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return (_squareMatrix != null ? _squareMatrix.GetHashCode() : 0);
        }

        #endregion Julkiset metodit

        #region Yksityiset metodit

        /// <summary>
        /// Alustaa shakkilaudan tyhjäksi.
        /// </summary>
        private void Initialize()
        {
            for (int file = NUMBER_OF_THE_FIRST_FILE; file < DEFAULT_NUMBER_OF_FILES; file++)
            {
                for (int rank = NUMBER_OF_THE_FIRST_RANK; rank < DEFAULT_NUMBER_OF_RANKS; rank++)
                {
                    _squareMatrix[file, rank] = new Square(file, rank);
                }
            }
        }

        /// <summary>
        /// Lisää shakkilaudalle nappulat aloitusruutuihin.
        /// </summary>
        /// <param name="pieceColor">Nappuloiden väri</param>
        private void InitializeChessPieces(PieceColor pieceColor)
        {
            // Päätellään sotilaiden ja upseereiden rivinumerot
            var pawnChainRank = pieceColor == PieceColor.White ? PAWN_CHAIN_RANK_FOR_WHITE : PAWN_CHAIN_RANK_FOR_BLACK;
            var officerRank = pieceColor == PieceColor.White ? OFFICER_RANK_FOR_WHITE : OFFICER_RANK_FOR_BLACK;

            // Sotilaat
            SetPosition(1, pawnChainRank, new Pawn(pieceColor));
            SetPosition(2, pawnChainRank, new Pawn(pieceColor));
            SetPosition(3, pawnChainRank, new Pawn(pieceColor));
            SetPosition(4, pawnChainRank, new Pawn(pieceColor));
            SetPosition(5, pawnChainRank, new Pawn(pieceColor));
            SetPosition(6, pawnChainRank, new Pawn(pieceColor));
            SetPosition(7, pawnChainRank, new Pawn(pieceColor));
            SetPosition(8, pawnChainRank, new Pawn(pieceColor));

            // Tornit
            SetPosition(1, officerRank, new Rook(pieceColor));
            SetPosition(8, officerRank, new Rook(pieceColor));

            // Hevoset
            SetPosition(2, officerRank, new Knight(pieceColor));
            SetPosition(7, officerRank, new Knight(pieceColor));

            // Lähetit
            SetPosition(3, officerRank, new Bishop(pieceColor));
            SetPosition(6, officerRank, new Bishop(pieceColor));

            // Kuningatar
            SetPosition(4, officerRank, new Queen(pieceColor));

            // Kuningas
            SetPosition(5, officerRank, new King(pieceColor));
        }

        /// <summary>
        /// Tarkastaa ovatko sarake ja rivi parametrit shakkilauden rajojen ulkopuolella.
        /// </summary>
        /// <param name="file">Sarake</param>
        /// <param name="rank">Rivi</param>
        /// <returns>True, jos ovat ulkopuolella. False, jos ovat sisäpuolella.</returns>
        private static bool AreOutsideBoardBoundaries(int file, int rank)
        {
            return file < NUMBER_OF_THE_FIRST_FILE || rank < NUMBER_OF_THE_FIRST_RANK
                || file > NUMBER_OF_THE_LAST_FILE || rank > NUMBER_OF_THE_LAST_RANK;
        }

        /// <summary>
        /// Laukaisee OnMove-eventin.
        /// </summary>
        /// <param name="eventArgs">Tiedot siirrosta.</param>
        private void FireOnMoveEvent(MoveEventArgs eventArgs)
        {
            if (OnMove != null)
            {
                OnMove.Invoke(this, eventArgs);
            }
        }

        #endregion Yksityiset metodit
    }
}