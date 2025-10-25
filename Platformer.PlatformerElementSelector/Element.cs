using Microsoft.Xna.Framework.Graphics;
using Platformer.Positioning;

namespace Platformer.PlatformerElementSelector
{
    public class Element
    {
        public Texture2D Texture { get; }
        public PlatformerRectangle Position { get; }
        public Element(Texture2D texture, PlatformerRectangle position)
        {
            Texture = texture;
            Position = position;
        }
    }
}