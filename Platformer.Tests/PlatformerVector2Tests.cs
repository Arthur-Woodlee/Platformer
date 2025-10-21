using Platformer.Positioning;

namespace Platformer.Tests
{
    public class PlatformerVector2Tests
    {
        [Fact]
        public void PlatformerVector2_Constructor_ValidInput_SetsPropertiesCorrectly()
        {
            var vec = new PlatformerVector2(10, 20);
            Assert.Equal(10, vec.X);
            Assert.Equal(20, vec.Y);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-5, -5)]
        public void PlatformerVector2_Constructor_NegativeInput_ThrowsArgumentOutOfRange(int x, int y)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PlatformerVector2(x, y));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(int.MaxValue, int.MaxValue)]
        public void PlatformerVector2_Constructor_EdgeCases_SetsPropertiesCorrectly(int x, int y)
        {
            var vec = new PlatformerVector2(x, y);
            Assert.Equal(x, vec.X);
            Assert.Equal(y, vec.Y);
        }
    }
}