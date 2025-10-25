using Microsoft.Xna.Framework.Graphics;
using Platformer.PlatformerElementSelector;

namespace Platformer.PlatformerSequenceElementSelector.Sequences.ExecutionRules
{
    public class PlatformMoveSequenceConfig
    {
        public ElementSelectorSceneObject ElementSelectorSceneObject;
        public ushort SlowRate;
        public ICollisionDetector CollisionDetector;
        public IContactHandler ContactHandler;
        public ushort Increment;
        public ushort MaxSpeed;
        public Direction MoveDirection;
        public Texture2D[] Textures;
        public string SequenceId;
        public ushort InitialPixelsTravellingPerUpdate;
        public PlatformMoveSequenceConfig(ElementSelectorSceneObject elementSelectorSceneObject, ushort slowRate, ICollisionDetector collisionDetector, IContactHandler contactHandler, ushort increment, ushort maxSpeed, Direction moveDirection, Texture2D[] textures, string sequenceId, ushort initialPixelsTravellingPerUpdate)
        {
            this.ElementSelectorSceneObject = elementSelectorSceneObject;
            this.SlowRate = slowRate;
            this.CollisionDetector = collisionDetector;
            this.ContactHandler = contactHandler;
            this.Increment = increment;
            this.MaxSpeed = maxSpeed;
            this.MoveDirection = moveDirection;
            this.Textures = textures;
            this.SequenceId = sequenceId;
            this.InitialPixelsTravellingPerUpdate = initialPixelsTravellingPerUpdate;
        }
    }
}