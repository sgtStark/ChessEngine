using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
    using ChessEngineLib;
    using ChessEngineLib.ChessPieces;

    /// <summary>
    /// Sisältää kaikki sotilas shakkinappulaan kohdistuvat yksikkötestit.
    /// HUOM! Perii yhteisestä ChessEngineTestBase-luokasta yleisiä setup-metodeita.
    /// </summary>
    /// <remarks>
    /// ======================
    ///     Muistiinpanot:
    /// ======================
    ///     Valkoisella sotilaalla katsotaan shakkilautaa alhaalta ylös ja oikealta vasemmalle.
    ///     Mustalla sotilaalla vasen ja oikea sekä eteen ja taakse vaihtavat paikkaa eli katso-
    ///     taan ylhäältä alas oikealta vasemmalle.
    /// 
    ///     IsLegalMove-metodia on suunnittelunäkökulmasta tarkoitettu käytettäväksi
    ///     niin että haetaan sijainnit samasta laudasta, jolta kysytään onko siirto laillinen.
    /// </remarks>
    [TestClass]
    public class PawnTests : ChessEngineTestBase
    {
        [TestInitialize]
        public void InitializationBeforeEveryTest()
        {
            InitializeBoard();
        }

        [TestMethod]
        public void Equals_TwoOppositeColorPawns_AreNotEqual()
        {
            Pawn pawn1 = new Pawn(new Board(), PieceColor.White);
            Pawn pawn2 = new Pawn(new Board(), PieceColor.Black);

            Assert.AreNotEqual(pawn1, pawn2);
        }

        [TestMethod]
        public void Equals_TwoSameColorPawns_AreEqual()
        {
            Pawn pawn1 = new Pawn(new Board(), PieceColor.White);
            Pawn pawn2 = new Pawn(new Board(), PieceColor.White);
            Pawn pawn3 = new Pawn(new Board(), PieceColor.Black);
            Pawn pawn4 = new Pawn(new Board(), PieceColor.Black);

            Assert.AreEqual(pawn1, pawn2);
            Assert.AreEqual(pawn3, pawn4);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnOneSquareForward_ReturnsTrue()
        {
            Board.SetSquare(1, 1, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(1, 1), GetSquare(1, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnOneSquareForward_ReturnsTrue()
        {
            Board.SetSquare(1, 7, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(1, 7), GetSquare(1, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesTwoSquaresForward_ReturnsFalse()
        {
            Board.SetSquare(1, 1, new Pawn(Board, PieceColor.White));

            bool result = IsLegalMove(GetSquare(1, 1), GetSquare(1, 3));

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void IsLegalMove_BlackPawnMovesTwoSquaresForward_ReturnsFalse()
        {
            Board.SetSquare(1, 8, new Pawn(Board, PieceColor.Black));

            bool result = IsLegalMove(GetSquare(1, 8), GetSquare(1, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesTwoSquaresForwardFromStartingRank_ReturnsTrue()
        {
            Board.SetSquare(1, 7, new Pawn(Board, PieceColor.Black));

            bool result = IsLegalMove(GetSquare(1, 7), GetSquare(1, 5));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesTwoSquaresForwardFromStartingRank_ReturnsTrue()
        {
            Board.SetSquare(1, 2, new Pawn(Board, PieceColor.White));

            bool result = IsLegalMove(GetSquare(1, 2), GetSquare(1, 4));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareDiagonallyToRight_ReturnsFalse()
        {
            Board.SetSquare(1, 1, new Pawn(Board, PieceColor.White));

            bool result = IsLegalMove(GetSquare(1, 1), GetSquare(2, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesOneSquareDiagonallyToRight_ReturnsFalse()
        {
            Board.SetSquare(4, 7, new Pawn(Board, PieceColor.Black));

            bool result = IsLegalMove(GetSquare(4, 7), GetSquare(3, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnAttacksOppositeColorPawnOneSquareForwardOnRightDiagonal_ReturnsTrue()
        {
            Board.SetSquare(1, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(2, 3, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(1, 2), GetSquare(2, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnAttacksOppositeColorPawnOneSquareForwardOnLeftDiagonal_ReturnsTrue()
        {
            Board.SetSquare(2, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(1, 3, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(2, 2), GetSquare(1, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnAttacksOppositeColorPawnOneSquareForwardOnRightDiagonal_ReturnsTrue()
        {
            Board.SetSquare(2, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(1, 6, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(2, 7), GetSquare(1, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnAttacksOppositeColorPawnOneSquareForwardOnLeftDiagonal_ReturnsTrue()
        {
            Board.SetSquare(1, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(2, 6, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(1, 7), GetSquare(2, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareToRight_ReturnsFalse()
        {
            Board.SetSquare(1, 1, new Pawn(Board, PieceColor.White));

            bool result = IsLegalMove(GetSquare(1, 1), GetSquare(2, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareBackward_ReturnsFalse()
        {
            Board.SetSquare(1, 3, new Pawn(Board, PieceColor.White));

            bool result = IsLegalMove(GetSquare(1, 3), GetSquare(1, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesOneSquareBackward_ReturnsFalse()
        {
            Board.SetSquare(1, 7, new Pawn(Board, PieceColor.Black));

            bool result = IsLegalMove(GetSquare(1, 7), GetSquare(1, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsFalse()
        {
            Board.SetSquare(1, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(1, 3, new Pawn(Board, PieceColor.Black));

            bool result = IsLegalMove(GetSquare(1, 2), GetSquare(1, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(1, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(1, 3, new Pawn(Board, PieceColor.White));

            bool result = IsLegalMove(GetSquare(1, 2), GetSquare(1, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsFalse()
        {
            Board.SetSquare(1, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(1, 6, new Pawn(Board, PieceColor.White));

            bool result = IsLegalMove(GetSquare(1, 7), GetSquare(1, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(1, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(1, 8, new Pawn(Board, PieceColor.Black));

            bool result = IsLegalMove(GetSquare(1, 7), GetSquare(1, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesTwoSquaresForwardWhileItsPathIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(1, 2, new Pawn(Board, PieceColor.White));
            Board.SetSquare(1, 3, new Pawn(Board, PieceColor.White));

            var result = IsLegalMove(GetSquare(1, 2), GetSquare(1, 4));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesTwoSquaresForwardWhileItsPathIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board.SetSquare(1, 7, new Pawn(Board, PieceColor.Black));
            Board.SetSquare(1, 6, new Pawn(Board, PieceColor.Black));

            var result = IsLegalMove(GetSquare(1, 7), GetSquare(1, 5));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMakesEnPassantAttackToTheLeft_ReturnsTrue()
        {
            Board.Setup();

            // Siirretään valkoinen sotilas hyökkäys asetelmaan
            Board.Move(GetSquare(4, 2), GetSquare(4, 4));
            Board.Move(GetSquare(4, 4), GetSquare(4, 5));

            // Siirretään musta sotilas kaksi ruutua eteenpäin mahdollistaen valkoiselle
            // En Passant hyökkäyksen ruutuun (3, 6)
            Board.Move(GetSquare(3, 7), GetSquare(3, 5));

            var result = IsLegalMove(GetSquare(4, 5), GetSquare(3, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMakesAttackToTheLeftToPositionWhichIsNotAnEnPassant_ReturnsFalse()
        {
            Board.Setup();

            // Siirretään valkoinen sotilas hyökkäys asetelmaan
            Board.Move(GetSquare(4, 2), GetSquare(4, 4));
            Board.Move(GetSquare(4, 4), GetSquare(4, 5));

            // Siirretään musta sotilas kaksi ruutua eteenpäin mahdollistaen valkoiselle
            // En Passant hyökkäyksen ruutuun (3, 6)
            Board.Move(GetSquare(3, 7), GetSquare(3, 6));
            Board.Move(GetSquare(3, 6), GetSquare(3, 5));

            var result = IsLegalMove(GetSquare(4, 5), GetSquare(3, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMakesEnPassantAttachToTheRight_ReturnsTrue()
        {
            Board.Setup();

            // Siirretään valkoinen sotilas hyökkäys asetelmaan
            Board.Move(GetSquare(4, 2), GetSquare(4, 4));
            Board.Move(GetSquare(4, 4), GetSquare(4, 5));

            // Siirretään musta sotilas kaksi ruutua eteenpäin mahdollistaen valkoiselle
            // En Passant hyökkäyksen ruutuun (5, 6)
            Board.Move(GetSquare(5, 7), GetSquare(5, 5));

            var result = IsLegalMove(GetSquare(4, 5), GetSquare(5, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMakesAttackToTheRightToPositionWhichIsNotAnEnPassant_ReturnsFalse()
        {
            Board.Setup();

            // Siirretään valkoinen sotilas hyökkäys asetelmaan
            Board.Move(GetSquare(4, 2), GetSquare(4, 4));
            Board.Move(GetSquare(4, 4), GetSquare(4, 5));

            // Siirretään musta sotilas kaksi ruutua eteenpäin mahdollistaen valkoiselle
            // En Passant hyökkäyksen ruutuun (3, 6)
            Board.Move(GetSquare(5, 7), GetSquare(5, 6));
            Board.Move(GetSquare(5, 6), GetSquare(5, 5));

            var result = IsLegalMove(GetSquare(4, 5), GetSquare(5, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMakesEnPassantAttackToTheLeft_ReturnsTrue()
        {
            Board.Setup();

            // Siirretään musta sotilas hyökkäys asetelmaan
            Board.Move(GetSquare(4, 7), GetSquare(4, 5));
            Board.Move(GetSquare(4, 5), GetSquare(4, 4));

            // Siirretään valkoinen sotilas kaksi ruutua eteenpäin mahdollistaen mustalle
            // En Passant hyökkäyksen ruutuun (5, 3)
            Board.Move(GetSquare(5, 2), GetSquare(5, 4));

            var result = IsLegalMove(GetSquare(4, 4), GetSquare(5, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMakesAttackToTheLeftToPositionWhichIsNotAnEnPassant_ReturnsFalse()
        {
            Board.Setup();

            // Siirretään musta sotilas hyökkäys asetelmaan
            Board.Move(GetSquare(4, 7), GetSquare(4, 5));
            Board.Move(GetSquare(4, 5), GetSquare(4, 4));

            // Siirretään valkoinen sotilas kaksi ruutua eteenpäin mahdollistaen mustalle
            // En Passant hyökkäyksen ruutuun (5, 3)
            Board.Move(GetSquare(5, 2), GetSquare(5, 3));
            Board.Move(GetSquare(5, 3), GetSquare(5, 4));

            var result = IsLegalMove(GetSquare(4, 4), GetSquare(5, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMakesEnPassantAttachToTheRight_ReturnsTrue()
        {
            Board.Setup();

            // Siirretään musta sotilas hyökkäys asetelmaan
            Board.Move(GetSquare(4, 7), GetSquare(4, 5));
            Board.Move(GetSquare(4, 5), GetSquare(4, 4));

            // Siirretään valkoinen sotilas kaksi ruutua eteenpäin mahdollistaen mustalle
            // En Passant hyökkäyksen ruutuun (3, 3)
            Board.Move(GetSquare(3, 2), GetSquare(3, 4));

            var result = IsLegalMove(GetSquare(4, 4), GetSquare(3, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMakesAttackToTheRightToPositionWhichIsNotAnEnPassant_ReturnsFalse()
        {
            Board.Setup();

            // Siirretään musta sotilas hyökkäys asetelmaan
            Board.Move(GetSquare(4, 7), GetSquare(4, 5));
            Board.Move(GetSquare(4, 5), GetSquare(4, 4));

            // Siirretään valkoinen sotilas kaksi ruutua eteenpäin mahdollistaen mustalle
            // En Passant hyökkäyksen ruutuun (3, 3)
            Board.Move(GetSquare(3, 2), GetSquare(3, 3));
            Board.Move(GetSquare(3, 3), GetSquare(3, 4));

            var result = IsLegalMove(GetSquare(4, 4), GetSquare(3, 3));

            Assert.IsFalse(result);
        }
    }
}