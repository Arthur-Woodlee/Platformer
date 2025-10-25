using Platformer.Positioning;

namespace Platformer.PlatformerSequenceElementSelector.Sequences
{
    public static class RectangleUtility
    {
        public static PlatformerVector2 GetBottomRight(PlatformerRectangle rectangle)
        {
            return new PlatformerVector2(rectangle.X + rectangle.Width - 1, rectangle.Y + rectangle.Height - 1);
        }
        public static PlatformerVector2 GetBottomLeft(PlatformerRectangle rectangle)
        {
            return new PlatformerVector2(rectangle.X, rectangle.Y + rectangle.Height - 1);
        }
        public static PlatformerVector2 GetTopRight(PlatformerRectangle rectangle)
        {
            return new PlatformerVector2((rectangle.X + rectangle.Width), rectangle.Y);
        }
        public static bool Equal(PlatformerRectangle a, PlatformerRectangle b)
        {
            if (a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
