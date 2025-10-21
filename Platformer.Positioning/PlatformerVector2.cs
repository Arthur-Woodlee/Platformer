using System;

namespace Platformer.Positioning
{
    public class PlatformerVector2
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public PlatformerVector2(int x, int y)
        {
            if (x < 0 || y < 0) throw new ArgumentOutOfRangeException("The arguments 'x' and 'y' must be non-negative.");
            if (x > int.MaxValue || y > int.MaxValue) throw new ArgumentOutOfRangeException("The arguments 'x' and 'y' cannot be larger than int.MaxValue.");

            X = x;
            Y = y;
        }
    }
}