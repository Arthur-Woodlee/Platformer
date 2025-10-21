using Platformer.PlatformerSceneEngine;

namespace Platformer.Positioning
{
    public interface IRectangleSceneObject : ISceneObject
    {
        PlatformerRectangle GetPosition();
    }
}