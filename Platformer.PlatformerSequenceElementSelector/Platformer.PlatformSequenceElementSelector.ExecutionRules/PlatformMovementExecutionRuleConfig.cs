using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.PlatformerElementSelector;
using System;

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
        public String[] Assets;
        public string SequenceId;
        public ushort InitialPixelsTravellingPerUpdate;
        public ContentManager ContentManager;
        public PlatformMoveSequenceConfig(ElementSelectorSceneObject elementSelectorSceneObject, ushort slowRate, ICollisionDetector collisionDetector, IContactHandler contactHandler, ushort increment, ushort maxSpeed, Direction moveDirection, String[] assets, string sequenceId, ushort initialPixelsTravellingPerUpdate, ContentManager contentManager)
        {
            this.ElementSelectorSceneObject = elementSelectorSceneObject;
            this.SlowRate = slowRate;
            this.CollisionDetector = collisionDetector;
            this.ContactHandler = contactHandler;
            this.Increment = increment;
            this.MaxSpeed = maxSpeed;
            this.MoveDirection = moveDirection;
            this.Assets = assets;
            this.SequenceId = sequenceId;
            this.InitialPixelsTravellingPerUpdate = initialPixelsTravellingPerUpdate;
            this.ContentManager = contentManager;
            
        }
    }
}