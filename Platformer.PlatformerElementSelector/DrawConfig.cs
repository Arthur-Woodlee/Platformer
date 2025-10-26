using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.PlatformerElementSelector
{
    public class DrawConfig
    {
        public Color Color { get; }
        public Rectangle? SourceRectangle;
        public float? Rotation;
        public Vector2? Origin;
        public SpriteEffects? Effects;
        public float? LayerDepth;
        public DrawConfig(Color color, Rectangle? sourceRectangle, float? rotation, Vector2? origin, SpriteEffects? effects, float? layerDepth)
        {
            this.Color = color;
            this.SourceRectangle = sourceRectangle;
            this.Rotation = rotation;
            this.Origin = origin;
            this.Effects = effects;
            this.LayerDepth = layerDepth;
        }
    }
}