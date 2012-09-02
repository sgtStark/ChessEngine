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

        private const int PAWN_CHAIN_RANK_FOR_WHITE = 2;
        private const int PAWN_CHAIN_RANK_FOR_BLACK = 7;

        private const int OFFICER_RANK_FOR_WHITE = 1;
        private const int OFFICER_RANK_FOR_BLACK = 8;

        #endregion Vakiot

        #region Sisäiset datajäsenet

        /// <summary>
        /// Ruudut ja niiden sisällön sisältävä matriisi.
        /// </summary>
        private Position[,] _positionMatrix;

        #endregion Sisäiset datajäsenet

        #region Konstruktorit

        /// <summary>
        /// Oletusmuodostin
        /// </summary>
        public Board()
        {
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
        /// <returns>Ruudun tiedot kuvaava Position-olio.</returns>
        public Position GetPosition(int file, int rank)
        {
            if (AreOutsideBoardBoundaries(file, rank))
            {
                // TODO: Refactoroi niin että poikkeus on ChessEngineException-tyyppinen.
                throw new ArgumentOutOfRangeException("rank and file parameter values must be between 1 and 8");
            }

            return _positionMatrix[file, rank];
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

            _positionMatrix[file, rank] = new Position(file, rank, occupier);
        }

        /// <summary>
        /// Tarkastaa onko siirto laillinen.
        /// </summary>
        /// <param name="origin">Siirron lähtöruutu.</param>
        /// <param name="destination">Siirron kohderuutu.</param>
        /// <returns>True, jos siirto on laillinen. False, jos siirto on laiton.</returns>
        public bool IsLegalMove(Position origin, Position destination)
        {
            var boolToReturn = false;
            var chessPiece = origin.Occupier;

            if (chessPiece != null)
            {
                boolToReturn = chessPiece.IsLegalMove(this, origin, destination);
            }

            return boolToReturn;
        }

        /// <summary>
        /// Tekee siirron shakkilaudalla.
        /// </summary>
        /// <param name="origin">Siirron lähtöruutu.</param>
        /// <param name="destination">Siirron kohderuutu.</param>
        public void Move(Position origin, Position destination)
        {
            // Jos siirto on laillinen asetetaan miehittäjä lähde ruudusta kohderuutuun
            // ja poistetaan miehittäjä lähderuudusta.
            if (IsLegalMove(origin, destination))
            {
                var occupier = origin.Occupier;
                occupier.MoveCount++;
                SetPosition(origin.File, origin.Rank, null);
                SetPosition(destination.File, destination.Rank, occupier);

                HandleEnPassant(origin, destination);
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
            bool boolToReturn = file % 2 == 0
                                && rank % 2 != 0;

            // Kun sarake ei ole jaollinen itsellään ja rivi on 
            // jaollinen itsellään on kyseessä vaalea ruutu
            if (file % 2 != 0
                && rank % 2 == 0)
            {
                boolToReturn = true;
            }

            return boolToReturn;
        }

        #endregion Julkiset metodit

        #region Yksityiset metodit

        /// <summary>
        /// Alustaa shakkilaudan tyhjäksi.
        /// </summary>
        private void Initialize()
        {
            _positionMatrix = new Position[DEFAULT_NUMBER_OF_FILES,DEFAULT_NUMBER_OF_RANKS];

            for (int file = NUMBER_OF_THE_FIRST_FILE; file < DEFAULT_NUMBER_OF_FILES; file++)
            {
                for (int rank = NUMBER_OF_THE_FIRST_RANK; rank < DEFAULT_NUMBER_OF_RANKS; rank++)
                {
                    _positionMatrix[file, rank] = new Position(file, rank);
                }
            }
        }

        /// <summary>
        /// Lisää shakkilaudalle nappulat aloitusruutuihin.
        /// </summary>
        /// <param name="pieceColor">Nappuloiden väri</param>
        private void InitializeChessPieces(PieceColor pieceColor)
        {
            var pawnChainRank = pieceColor == PieceColor.White ? PAWN_CHAIN_RANK_FOR_WHITE : PAWN_CHAIN_RANK_FOR_BLACK;
            var officerRank = pieceColor == PieceColor.White ? OFFICER_RANK_FOR_WHITE : OFFICER_RANK_FOR_BLACK;

            // sotilaat
            SetPosition(1, pawnChainRank, new Pawn(pieceColor));
            SetPosition(2, pawnChainRank, new Pawn(pieceColor));
            SetPosition(3, pawnChainRank, new Pawn(pieceColor));
            SetPosition(4, pawnChainRank, new Pawn(pieceColor));
            SetPosition(5, pawnChainRank, new Pawn(pieceColor));
            SetPosition(6, pawnChainRank, new Pawn(pieceColor));
            SetPosition(7, pawnChainRank, new Pawn(pieceColor));
            SetPosition(8, pawnChainRank, new Pawn(pieceColor));

            // tornit
            SetPosition(1, officerRank, new Rook(pieceColor));
            SetPosition(8, officerRank, new Rook(pieceColor));

            // hevoset
            SetPosition(2, officerRank, new Knight(pieceColor));
            SetPosition(7, officerRank, new Knight(pieceColor));

            // lähetit
            SetPosition(3, officerRank, new Bishop(pieceColor));
            SetPosition(6, officerRank, new Bishop(pieceColor));

            // Kuningattaret
            SetPosition(4, officerRank, new Queen(pieceColor));

            // Kuninkaat
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
            return file <= 0 || rank <= 0 || file >= 9 || rank >= 9;
        }

        /// <summary>
        /// Käsittelee En Passant-siirrot.
        /// </summary>
        /// <param name="origin">Siirron lähtöruutu.</param>
        /// <param name="destination">Siirron kohderuutu.</param>
        private void HandleEnPassant(Position origin, Position destination)
        {
            try
            {
                // Apumuuttujat, haetaan lähtöruudun miehittäjä ja En Passant -hyökkäyksen alainen
                // ruutu. Ruutu voi myös olla NULL, jos ollaan ensimmäisellä tai viimeisellä rivillä.
                var occupier = origin.Occupier;
                var enPassantPosition = GetPosition(destination.File,
                                                    origin.Color == PieceColor.White
                                                        ? destination.Rank - 1
                                                        : destination.Rank + 1);

                // Otetaan NULL mahdollisuus huomioon ja haetaan En Passant -ruudun miehittäjä, 
                // joka voi niin ikään olla NULL.
                var enPassantOccupier = enPassantPosition != null ? enPassantPosition.Occupier : null;

                // Tehdään tarkastukset: lähtöruudun ja En Passant -ruutujen miehittäjien pitää 
                // olla sotilaita sekä En Passant -miehittäjää saa olla siirretty vain kerran.
                if (occupier is Pawn
                    && enPassantOccupier is Pawn
                    && occupier.Color.IsOppositeColor(enPassantOccupier.Color)
                    && enPassantOccupier.MoveCount == 1)
                {
                    // Poistetaan En Passant-miehittäjä.
                    SetPosition(enPassantPosition.File, enPassantPosition.Rank, null);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            { /* Ei käsitellä millään tavalla koska siihen ei ole tarvetta.*/ }
        }

        #endregion Yksityiset metodit
    }
}