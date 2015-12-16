using NUnit.Framework;

namespace Life.Tests
{
    [TestFixture]
    class UtilsTests
    {
        [Test]
        public void Utils_GenerateSeedData_Should_Return_Correct_Array_Bounds()
        {
            // Arrange

            // Act
            var seedData = Utils.GenerateSeedData(5, 5);

            // Assert
            Assert.AreEqual(4, seedData.GetUpperBound(0));
            Assert.AreEqual(4, seedData.GetUpperBound(1));
        }
    }
}
