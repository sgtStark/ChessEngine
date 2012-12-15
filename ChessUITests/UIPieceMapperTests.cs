using System.Windows.Media;
using System.Windows.Shapes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessEngineTests
{
    [TestClass]
    public class UIPieceMapperTests
    {
        [TestMethod]
        public void Rectangle_EqualsWorksForComplexRectangleObject_ReturnsTrue()
        {
            var rectangle1 = new Rectangle();
            rectangle1.Name = "Nimi";

            var rectangle2 = new Rectangle();
            rectangle2.Name = "Nimi";

            Transform renderTransform1 = new TranslateTransform(){ X = 1};
            var renderTransform2 = new TranslateTransform(){ X = 1};

            Assert.AreEqual(rectangle1.Name, rectangle2.Name);
            Assert.AreEqual(renderTransform1, renderTransform2.X);
        }
    }
}
