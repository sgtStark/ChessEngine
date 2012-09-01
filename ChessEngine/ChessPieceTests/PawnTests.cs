using ChessEngineLib;
using ChessEngineLib.ChessPieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests.ChessPieceTests
{
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
        #region Equals

        [TestMethod]
        public void Equals_TwoOppositeColorPawns_AreNotEqual()
        {
            Pawn pawn1 = new Pawn(PieceColor.White);
            Pawn pawn2 = new Pawn(PieceColor.Black);

            Assert.AreNotEqual(pawn1, pawn2);
        }

        [TestMethod]
        public void Equals_TwoSameColorPawns_AreEqual()
        {
            Pawn pawn1 = new Pawn(PieceColor.White);
            Pawn pawn2 = new Pawn(PieceColor.White);
            Pawn pawn3 = new Pawn(PieceColor.Black);
            Pawn pawn4 = new Pawn(PieceColor.Black);

            Assert.AreEqual(pawn1, pawn2);
            Assert.AreEqual(pawn3, pawn4);
        }

        #endregion Equals

        #region Yhden ruudun siirtojen laillisuuden tarkastus testit

        [TestMethod]
        public void IsLegalMove_WhitePawnOneSquareForward_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(1, 2));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnOneSquareForward_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 7, new Pawn(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 7), board.GetPosition(1, 6));

            Assert.IsTrue(result);
        }

        #endregion Yhden ruudun siirtojen laillisuuden tarkastus testit

        #region Kahden ruudun siirtojen laillisuuden testit

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesTwoSquaresForward_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Pawn(PieceColor.White));

            bool result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(1, 3));

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void IsLegalMove_BlackPawnMovesTwoSquaresForward_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 8, new Pawn(PieceColor.Black));

            bool result = board.IsLegalMove(board.GetPosition(1, 8), board.GetPosition(1, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesTwoSquaresForwardFromStartingRank_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 7, new Pawn(PieceColor.Black));

            bool result = board.IsLegalMove(board.GetPosition(1, 7), board.GetPosition(1, 5));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesTwoSquaresForwardFromStartingRank_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 2, new Pawn(PieceColor.White));

            bool result = board.IsLegalMove(board.GetPosition(1, 2), board.GetPosition(1, 4));

            Assert.IsTrue(result);
        }

        #endregion Kahden ruudun siirtojen laillisuuden testit

        #region Viistosiirtojen laillisuuden testit

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareDiagonallyToRight_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Pawn(PieceColor.White));

            bool result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(2, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesOneSquareDiagonallyToRight_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(4, 7, new Pawn(PieceColor.Black));

            bool result = board.IsLegalMove(board.GetPosition(4, 7), board.GetPosition(3, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnAttacksOppositeColorPawnOneSquareForwardOnRightDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 2, new Pawn(PieceColor.White));
            board.SetPosition(2, 3, new Pawn(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 2), board.GetPosition(2, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnAttacksOppositeColorPawnOneSquareForwardOnLeftDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 2, new Pawn(PieceColor.White));
            board.SetPosition(1, 3, new Pawn(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(2, 2), board.GetPosition(1, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnAttacksOppositeColorPawnOneSquareForwardOnRightDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(2, 7, new Pawn(PieceColor.Black));
            board.SetPosition(1, 6, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(2, 7), board.GetPosition(1, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnAttacksOppositeColorPawnOneSquareForwardOnLeftDiagonal_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 7, new Pawn(PieceColor.Black));
            board.SetPosition(2, 6, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 7), board.GetPosition(2, 6));

            Assert.IsTrue(result);
        }

        #endregion Viistosiirtojen laillisuuden testit

        #region Laittomien ja muiden virheellisten siirtojen testit

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareToRight_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 1, new Pawn(PieceColor.White));

            bool result = board.IsLegalMove(board.GetPosition(1, 1), board.GetPosition(2, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareBackward_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 3, new Pawn(PieceColor.White));

            bool result = board.IsLegalMove(board.GetPosition(1, 3), board.GetPosition(1, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesOneSquareBackward_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 7, new Pawn(PieceColor.Black));

            bool result = board.IsLegalMove(board.GetPosition(1, 7), board.GetPosition(1, 8));

            Assert.IsFalse(result);
        }

        #endregion Laittomien ja muiden virheellisten siirtojen testit

        #region Siirrot miehitetyille ruuduille ja siirrot joiden polku on miehitetty

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 2, new Pawn(PieceColor.White));
            board.SetPosition(1, 3, new Pawn(PieceColor.Black));

            bool result = board.IsLegalMove(board.GetPosition(1, 2), board.GetPosition(1, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 2, new Pawn(PieceColor.White));
            board.SetPosition(1, 3, new Pawn(PieceColor.White));

            bool result = board.IsLegalMove(board.GetPosition(1, 2), board.GetPosition(1, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesOneSquareForwardToPositionWhichIsOccupiedByOppositeColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 7, new Pawn(PieceColor.Black));
            board.SetPosition(1, 6, new Pawn(PieceColor.White));

            bool result = board.IsLegalMove(board.GetPosition(1, 7), board.GetPosition(1, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesOneSquareForwardToPositionWhichIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 7, new Pawn(PieceColor.Black));
            board.SetPosition(1, 8, new Pawn(PieceColor.Black));

            bool result = board.IsLegalMove(board.GetPosition(1, 7), board.GetPosition(1, 8));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMovesTwoSquaresForwardWhileItsPathIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 2, new Pawn(PieceColor.White));
            board.SetPosition(1, 3, new Pawn(PieceColor.White));

            var result = board.IsLegalMove(board.GetPosition(1, 2), board.GetPosition(1, 4));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMovesTwoSquaresForwardWhileItsPathIsOccupiedBySameColorPiece_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.SetPosition(1, 7, new Pawn(PieceColor.Black));
            board.SetPosition(1, 6, new Pawn(PieceColor.Black));

            var result = board.IsLegalMove(board.GetPosition(1, 7), board.GetPosition(1, 5));

            Assert.IsFalse(result);
        }

        #endregion Siirrot miehitetyille ruuduille ja siirrot joiden polku on miehitetty

        #region En Passant siirtojen testit

        [TestMethod]
        public void IsLegalMove_WhitePawnMakesEnPassantAttackToTheLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            // Siirretään valkoinen sotilas hyökkäys asetelmaan
            board.Move(board.GetPosition(4, 2), board.GetPosition(4, 4));
            board.Move(board.GetPosition(4, 4), board.GetPosition(4, 5));

            // Siirretään musta sotilas kaksi ruutua eteenpäin mahdollistaen valkoiselle
            // En Passant hyökkäyksen ruutuun (3, 6)
            board.Move(board.GetPosition(3, 7), board.GetPosition(3, 5));

            var result = board.IsLegalMove(board.GetPosition(4, 5), board.GetPosition(3, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMakesAttackToTheLeftToPositionWhichIsNotAnEnPassant_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            // Siirretään valkoinen sotilas hyökkäys asetelmaan
            board.Move(board.GetPosition(4, 2), board.GetPosition(4, 4));
            board.Move(board.GetPosition(4, 4), board.GetPosition(4, 5));

            // Siirretään musta sotilas kaksi ruutua eteenpäin mahdollistaen valkoiselle
            // En Passant hyökkäyksen ruutuun (3, 6)
            board.Move(board.GetPosition(3, 7), board.GetPosition(3, 6));
            board.Move(board.GetPosition(3, 6), board.GetPosition(3, 5));

            var result = board.IsLegalMove(board.GetPosition(4, 5), board.GetPosition(3, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMakesEnPassantAttachToTheRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            // Siirretään valkoinen sotilas hyökkäys asetelmaan
            board.Move(board.GetPosition(4, 2), board.GetPosition(4, 4));
            board.Move(board.GetPosition(4, 4), board.GetPosition(4, 5));

            // Siirretään musta sotilas kaksi ruutua eteenpäin mahdollistaen valkoiselle
            // En Passant hyökkäyksen ruutuun (5, 6)
            board.Move(board.GetPosition(5, 7), board.GetPosition(5, 5));

            var result = board.IsLegalMove(board.GetPosition(4, 5), board.GetPosition(5, 6));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_WhitePawnMakesAttackToTheRightToPositionWhichIsNotAnEnPassant_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            // Siirretään valkoinen sotilas hyökkäys asetelmaan
            board.Move(board.GetPosition(4, 2), board.GetPosition(4, 4));
            board.Move(board.GetPosition(4, 4), board.GetPosition(4, 5));

            // Siirretään musta sotilas kaksi ruutua eteenpäin mahdollistaen valkoiselle
            // En Passant hyökkäyksen ruutuun (3, 6)
            board.Move(board.GetPosition(5, 7), board.GetPosition(5, 6));
            board.Move(board.GetPosition(5, 6), board.GetPosition(5, 5));

            var result = board.IsLegalMove(board.GetPosition(4, 5), board.GetPosition(5, 6));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMakesEnPassantAttackToTheLeft_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            // Siirretään musta sotilas hyökkäys asetelmaan
            board.Move(board.GetPosition(4, 7), board.GetPosition(4, 5));
            board.Move(board.GetPosition(4, 5), board.GetPosition(4, 4));

            // Siirretään valkoinen sotilas kaksi ruutua eteenpäin mahdollistaen mustalle
            // En Passant hyökkäyksen ruutuun (5, 3)
            board.Move(board.GetPosition(5, 2), board.GetPosition(5, 4));

            var result = board.IsLegalMove(board.GetPosition(4, 4), board.GetPosition(5, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMakesAttackToTheLeftToPositionWhichIsNotAnEnPassant_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            // Siirretään musta sotilas hyökkäys asetelmaan
            board.Move(board.GetPosition(4, 7), board.GetPosition(4, 5));
            board.Move(board.GetPosition(4, 5), board.GetPosition(4, 4));

            // Siirretään valkoinen sotilas kaksi ruutua eteenpäin mahdollistaen mustalle
            // En Passant hyökkäyksen ruutuun (5, 3)
            board.Move(board.GetPosition(5, 2), board.GetPosition(5, 3));
            board.Move(board.GetPosition(5, 3), board.GetPosition(5, 4));

            var result = board.IsLegalMove(board.GetPosition(4, 4), board.GetPosition(5, 3));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMakesEnPassantAttachToTheRight_ReturnsTrue()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            // Siirretään musta sotilas hyökkäys asetelmaan
            board.Move(board.GetPosition(4, 7), board.GetPosition(4, 5));
            board.Move(board.GetPosition(4, 5), board.GetPosition(4, 4));

            // Siirretään valkoinen sotilas kaksi ruutua eteenpäin mahdollistaen mustalle
            // En Passant hyökkäyksen ruutuun (3, 3)
            board.Move(board.GetPosition(3, 2), board.GetPosition(3, 4));

            var result = board.IsLegalMove(board.GetPosition(4, 4), board.GetPosition(3, 3));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLegalMove_BlackPawnMakesAttackToTheRightToPositionWhichIsNotAnEnPassant_ReturnsFalse()
        {
            Board board = CreateEmptyBoard();
            board.Setup();

            // Siirretään musta sotilas hyökkäys asetelmaan
            board.Move(board.GetPosition(4, 7), board.GetPosition(4, 5));
            board.Move(board.GetPosition(4, 5), board.GetPosition(4, 4));

            // Siirretään valkoinen sotilas kaksi ruutua eteenpäin mahdollistaen mustalle
            // En Passant hyökkäyksen ruutuun (3, 3)
            board.Move(board.GetPosition(3, 2), board.GetPosition(3, 3));
            board.Move(board.GetPosition(3, 3), board.GetPosition(3, 4));

            var result = board.IsLegalMove(board.GetPosition(4, 4), board.GetPosition(3, 3));

            Assert.IsFalse(result);
        }

        #endregion En Passant siirtojen testit
    }
}