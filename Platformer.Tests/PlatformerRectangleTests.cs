using Platformer.Positioning;
using Microsoft.Xna.Framework;

namespace Platformer.Tests
{
    public class PlatformerRectangleTests
    {
        [Fact]
        public void PlatformerRectangle_Constructor_ValidInput_SetsPropertiesCorrectly()
        {
            var rect = new PlatformerRectangle(10, 20, 30, 40);
            Assert.Equal(10, rect.X);
            Assert.Equal(20, rect.Y);
            Assert.Equal(30, rect.Width);
            Assert.Equal(40, rect.Height);
        }

        [Theory]
        [InlineData(-1, 0, 10, 10)]
        [InlineData(0, -1, 10, 10)]
        [InlineData(0, 0, -10, 10)]
        [InlineData(0, 0, 10, -10)]
        public void PlatformerRectangle_Constructor_NegativeInput_ThrowsArgumentOutOfRange(int x, int y, int w, int h)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PlatformerRectangle(x, y, w, h));
        }

        [Fact]
        public void PlatformerRectangle_Contains_RectangleInside_ReturnsTrue()
        {
            var outer = new PlatformerRectangle(0, 0, 100, 100);
            var inner = new Rectangle(10, 10, 20, 20);
            Assert.True(outer.Contains(inner));
        }

        [Fact]
        public void PlatformerRectangle_Contains_PointInside_ReturnsTrue()
        {
            var rect = new PlatformerRectangle(0, 0, 100, 100);
            Assert.True(rect.Contains(50, 50));
        }

        [Fact]
        public void PlatformerRectangle_Contains_PointOutside_ReturnsFalse()
        {
            var rect = new PlatformerRectangle(0, 0, 100, 100);
            Assert.False(rect.Contains(150, 150));
        }

        [Fact]
        public void PlatformerRectangle_Intersects_OverlappingRectangle_ReturnsTrue()
        {
            var a = new PlatformerRectangle(0, 0, 100, 100);
            var b = new Rectangle(50, 50, 100, 100);
            Assert.True(a.Intersects(b));
        }

        [Fact]
        public void PlatformerRectangle_Intersects_NonOverlappingRectangle_ReturnsFalse()
        {
            var a = new PlatformerRectangle(0, 0, 100, 100);
            var b = new Rectangle(200, 200, 50, 50);
            Assert.False(a.Intersects(b));
        }
    }
}