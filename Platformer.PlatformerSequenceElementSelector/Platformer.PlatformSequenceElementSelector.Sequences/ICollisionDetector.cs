using Platformer.PlatformerSceneEngine;
using Platformer.Positioning;

namespace Platformer.PlatformerSequenceElementSelector.Sequences
{
    public interface ICollisionDetector
    {
        PlatformerRectangle DetectCollision(PlatformerRectangle boundingBox, ISceneObject inquirer);
    }
}