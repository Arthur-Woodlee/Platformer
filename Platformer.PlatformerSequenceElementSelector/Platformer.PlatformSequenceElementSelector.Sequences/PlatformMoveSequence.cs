using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Platformer.PlatformerElementSelector;
using Platformer.Positioning;

namespace Platformer.PlatformerSequenceElementSelector.Sequences
{
    public class PlatformMoveSequence : ISequence
    {
        public ushort PixelsTravellingPerUpdate { get; private set; }
        public ushort SlowRate { get; private set; }
        private ICollisionDetector _collisionDetector;
        private IContactHandler _contactHandler;
        public ushort Increment { get; private set; }
        public ushort MaxSpeed { get; private set; }
        public Direction Direction { get; private set; }
        public TextureReel TextureReel { get ; private set; }
        private IRectangleSceneObject _sprite;

        private PlatformMoveSequence(ushort pixelsTravellingPerUpdate, ushort slowRate, ICollisionDetector collisionDetector, IContactHandler contactHandler, ushort increment, ushort maxSpeed, Direction direction, IRectangleSceneObject sprite, TextureReel textureReel)
        {
            if (pixelsTravellingPerUpdate > maxSpeed)
            {
                this.PixelsTravellingPerUpdate = maxSpeed;
            }
            else
            {
                this.PixelsTravellingPerUpdate = pixelsTravellingPerUpdate;
            }
            this._collisionDetector = collisionDetector;
            this._contactHandler = contactHandler;
            this.SlowRate = slowRate;
            this.Increment = increment;
            this.MaxSpeed = maxSpeed;
            this.Direction = direction;
            this._sprite = sprite;
            this.TextureReel = textureReel;
        }
        public static PlatformMoveSequence Create(ushort pixelsTravellingPerUpdate, ushort slowRate, ICollisionDetector collisionDetector, IContactHandler contactHandler, ushort increment, ushort maxSpeed, Direction direction, IRectangleSceneObject sprite, TextureReel textureReel)
        {
            PlatformerRectangle position = sprite.GetPosition();
            if (collisionDetector.DetectCollision(new PlatformerRectangle(position.X, position.Y + 1, position.Width, position.Height), sprite) == null)
            {
                return null;
            }
            else
            {
                return new PlatformMoveSequence(pixelsTravellingPerUpdate, slowRate, collisionDetector, contactHandler, increment, maxSpeed, direction, sprite, textureReel);
            }
        }
        public Element Continue(ushort updateCycleLength, ushort cycleNumber)
        {
            if (!OnPlatform(_sprite.GetPosition()))
            {
                return null;
            }
            if (PixelsTravellingPerUpdate == 0)
            {
                return null;
            }
            if ((cycleNumber % (updateCycleLength / PixelsTravellingPerUpdate)) == 0)
            {

                if (cycleNumber == updateCycleLength)
                {
                    ApplySlowRate();
                }
                if (Direction == Direction.right)
                {
                    PlatformerRectangle nextPosition = MoveRightSinglePixel(_sprite.GetPosition());
                    if (RectangleUtility.Equals(nextPosition, _sprite.GetPosition()))
                    {
                        return null;
                    }
                    else
                    {
                        return new Element(TextureReel.GetTexture(), nextPosition);
                    }
                }
                else
                {
                    PlatformerRectangle newPosition = MoveLeftSinglePixel(_sprite.GetPosition());

                    if (RectangleUtility.Equals(newPosition, _sprite.GetPosition()))
                    {
                        return null;
                    }
                    else
                    {
                        return new Element(TextureReel.GetTexture(), newPosition);
                    }
                }
            }
            else
            {
                if (cycleNumber == updateCycleLength)
                {
                    ApplySlowRate();
                }
                return new Element(TextureReel.Peek(), _sprite.GetPosition());
            }
        }
        private bool OnPlatform(PlatformerRectangle boundingBox)
        {
            if (_collisionDetector.DetectCollision(new PlatformerRectangle(boundingBox.X, boundingBox.Y + 1, boundingBox.Width, boundingBox.Height), _sprite) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private PlatformerRectangle MoveRightSinglePixel(PlatformerRectangle boundingBox)
        {
            if (_collisionDetector.DetectCollision(new PlatformerRectangle(boundingBox.X, boundingBox.Y, boundingBox.Width, boundingBox.Height), _sprite) != null)
            {
                return boundingBox;
            }
            LinkedList<Contact> contacts1 = _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X + 1, boundingBox.Y, boundingBox.Width, boundingBox.Height), _sprite, false);
            bool nextToAHill = false;
            foreach (Contact contact in contacts1)
            {
                foreach (string id in contact.SceneObjectID)
                {
                    if (id == "Hill")
                    {
                        nextToAHill = true;
                    }
                }
            }
            if (nextToAHill)
            {
                LinkedList<Contact> contacts2 = _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X + 1, boundingBox.Y - 1, boundingBox.Width, boundingBox.Height), _sprite, false);
                int count = 0;
                foreach (Contact contact in contacts2)
                {
                    if (contact.NewPositionAllowed == true)
                    {
                        count++;

                    }
                }
                if (count == contacts2.Count)
                {
                    // Can step up the hill
                    _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X + 1, boundingBox.Y - 1, boundingBox.Width, boundingBox.Height), _sprite, true);
                    return new PlatformerRectangle(boundingBox.X + 1, boundingBox.Y - 1, boundingBox.Width, boundingBox.Height);
                }
                else
                {
                    _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X + 1, boundingBox.Y, boundingBox.Width, boundingBox.Height), _sprite, true);
                    return boundingBox;
                }
            }
            else
            {
                // On a platform and not next to a hill
                int vacateCount = 0;
                foreach (Contact contact in contacts1)
                {
                    if (contact.NewPositionAllowed == true)
                    {
                        vacateCount++;
                    }
                }
                if (vacateCount == contacts1.Count)
                {
                    _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X + 1, boundingBox.Y, boundingBox.Width, boundingBox.Height), _sprite, true);


                    if (_collisionDetector.DetectCollision(new PlatformerRectangle(boundingBox.X + 1, boundingBox.Y + 1, boundingBox.Width, boundingBox.Height), _sprite) == null)
                    {
                        return new PlatformerRectangle(boundingBox.X + 1, boundingBox.Y + 1, boundingBox.Width, boundingBox.Height);
                    }
                    else
                    {
                        return new PlatformerRectangle(boundingBox.X + 1, boundingBox.Y, boundingBox.Width, boundingBox.Height);
                    }
                }
                else
                {
                    return boundingBox;
                }
            }
        }
        private PlatformerRectangle MoveLeftSinglePixel(PlatformerRectangle boundingBox)
        {
            if (_collisionDetector.DetectCollision(new PlatformerRectangle(boundingBox.X, boundingBox.Y, boundingBox.Width, boundingBox.Height), _sprite) != null)
            {
                return boundingBox;
            }
            if (boundingBox.X == 0)
            {
                return boundingBox;
            }
            LinkedList<Contact> contacts1 = _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X - 1, boundingBox.Y, boundingBox.Width, boundingBox.Height), _sprite, false);
            bool nextToAHill = false;
            foreach (Contact contact in contacts1)
            {
                foreach (string id in contact.SceneObjectID)
                {
                    if (id == "Hill")
                    {
                        nextToAHill = true;
                    }
                }
            }
            if (nextToAHill)
            {
                LinkedList<Contact> contacts2 = _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X - 1, boundingBox.Y - 1, boundingBox.Width, boundingBox.Height), _sprite, false);
                int count = 0;
                foreach (Contact contact in contacts2)
                {
                    if (contact.NewPositionAllowed == true)
                    {
                        count++;
                    }
                }
                if (count == contacts2.Count)
                {
                    // Can step up the hill
                    _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X - 1, boundingBox.Y - 1, boundingBox.Width, boundingBox.Height), _sprite, true);
                    return new PlatformerRectangle(boundingBox.X - 1, boundingBox.Y - 1, boundingBox.Width, boundingBox.Height);
                }
                else
                {
                    _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X - 1, boundingBox.Y, boundingBox.Width, boundingBox.Height), _sprite, true);
                    return boundingBox;
                }
            }
            else
            {
                // On a platform and not next to a hill
                int vacateCount = 0;
                foreach (Contact contact in contacts1)
                {
                    if (contact.NewPositionAllowed == true)
                    {
                        vacateCount++;
                    }
                }
                if (vacateCount == contacts1.Count)
                {
                    _contactHandler.MakeContact(new PlatformerRectangle(boundingBox.X - 1, boundingBox.Y, boundingBox.Width, boundingBox.Height), _sprite, true);

                    if (_collisionDetector.DetectCollision(new PlatformerRectangle(boundingBox.X - 1, boundingBox.Y + 1, boundingBox.Width, boundingBox.Height), _sprite) == null)
                    {
                        return new PlatformerRectangle(boundingBox.X - 1, boundingBox.Y + 1, boundingBox.Width, boundingBox.Height);
                    }
                    else
                    {
                        return new PlatformerRectangle(boundingBox.X - 1, boundingBox.Y, boundingBox.Width, boundingBox.Height);
                    }

                }
                else
                {
                    return boundingBox;
                }
            }
        }
        public Texture2D GetTexture()
        {
            if (TextureReel == null)
            {
                return null;
            }
            else
            {
                return TextureReel.GetTexture();
            }
        }
        private void ApplySlowRate()
        {
            if ((PixelsTravellingPerUpdate - SlowRate) < 0)
            {
                PixelsTravellingPerUpdate = 0;
            }
            else
            {
                PixelsTravellingPerUpdate = (ushort)(PixelsTravellingPerUpdate - SlowRate);
            }
        }
    }
}