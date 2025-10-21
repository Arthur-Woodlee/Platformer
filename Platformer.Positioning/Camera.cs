using System;
using Microsoft.Xna.Framework;

namespace Platformer.Positioning
{
    public class Camera
    {
        // A Camera points at a IRectangleSceneObject. A Camera follows a IRectangleSceneObject as it moves around the scene.
        // The focus box is contained within a lens. A IRectangleSceneObject is always contained in the focus box.
        public Rectangle Lens { get; private set; }
        public Rectangle FocusBox { get; private set; }
        private IRectangleSceneObject _subject;
        private PlatformerVector2 _endScene;
        public Camera(Rectangle lens, Rectangle focusBox, PlatformerVector2 endScene)
        {
            if (!lens.Contains(focusBox))
            {
                throw new Exception("Focus area must be contained within the Lens.");
            }
            Lens = lens;
            FocusBox = focusBox;
            _endScene = endScene;
        }
        public void Update()
        {
            if (_subject.GetPosition().Width > FocusBox.Width || _subject.GetPosition().Height > FocusBox.Height)
            {
                throw new Exception("Subject area must not exceed Focus Box Area.");
            }
            PlatformerRectangle positionToConvert = _subject.GetPosition();
            Rectangle position = new Rectangle(positionToConvert.X, positionToConvert.Y, positionToConvert.Width, positionToConvert.Height);
            if (!FocusBox.Contains(position))
            {
                if ((position.X + position.Width) > (FocusBox.X + FocusBox.Width))
                {
                    int cameraShiftX = (position.X + position.Width) - (FocusBox.X + FocusBox.Width);
                    ShiftLensPosition(cameraShiftX, 0);
                }
                if (position.X < FocusBox.X)
                {
                    int cameraShiftX = position.X - FocusBox.X;
                    ShiftLensPosition(cameraShiftX, 0);
                }
                if ((position.Y + position.Height) > (FocusBox.Y + FocusBox.Height))
                {
                    int cameraShiftY = (position.Y + position.Height) - (FocusBox.Y + FocusBox.Height);
                    ShiftLensPosition(0, cameraShiftY);
                }
                if (position.Y < FocusBox.Y)
                {
                    int cameraShiftY = position.Y - FocusBox.Y;
                    ShiftLensPosition(0, cameraShiftY);
                }
            }
        }
        private void ShiftLensPosition(int x, int y)
        {
            if ((FocusBox.X + FocusBox.Width) > (_endScene.X - (FocusBox.X - Lens.X)) || (FocusBox.Y + FocusBox.Height) > (_endScene.Y - (FocusBox.Y - Lens.Y)))
            {
                FocusBox = new Rectangle(FocusBox.X + x, FocusBox.Y + y, FocusBox.Width, FocusBox.Height);
            }
            else
            {
                Lens = new Rectangle(Lens.X + x, Lens.Y + y, Lens.Width, Lens.Height);
                FocusBox = new Rectangle(FocusBox.X + x, FocusBox.Y + y, FocusBox.Width, FocusBox.Height);
            }

        }
        public Rectangle GetDrawingPosition(PlatformerRectangle position)
        {
            int subjectLensPositionX;
            int subjectLensPositionY;
            PlatformerRectangle subjectPosition = _subject.GetPosition();
            if (Lens.X < 0)
            {
                subjectLensPositionX = subjectPosition.X;
            }
            else
            {
                subjectLensPositionX = subjectPosition.X - Lens.X;
            }
            if (Lens.Y < 0)
            {
                subjectLensPositionY = subjectPosition.Y;
            }
            else
            {
                subjectLensPositionY = subjectPosition.Y - Lens.Y;
            }
            int mapDistanceFromSubjectX = position.X - subjectPosition.X;
            int mapDistanceFromSubjectY = position.Y - subjectPosition.Y;

            int inputLensPositionX = subjectLensPositionX + mapDistanceFromSubjectX;
            int inputLensPositionY = subjectLensPositionY + mapDistanceFromSubjectY;

            return new Rectangle(inputLensPositionX, inputLensPositionY, position.Width, position.Height);
        }
        public Vector2 GetDrawingPosition(PlatformerVector2 point)
        {
            int subjectLensPositionX;
            int subjectLensPositionY;
            PlatformerRectangle subjectPosition = _subject.GetPosition();
            if (Lens.X < 0)
            {
                subjectLensPositionX = subjectPosition.X;
            }
            else
            {
                subjectLensPositionX = subjectPosition.X - Lens.X;
            }
            if (Lens.Y < 0)
            {
                subjectLensPositionY = subjectPosition.Y;
            }
            else
            {
                subjectLensPositionY = subjectPosition.Y - Lens.Y;
            }
            int mapDistanceFromSubjectX = point.X - subjectPosition.X;
            int mapDistanceFromSubjectY = point.Y - subjectPosition.Y;

            int inputLensPositionX = subjectLensPositionX + mapDistanceFromSubjectX;
            int inputLensPositionY = subjectLensPositionY + mapDistanceFromSubjectY;

            return new Vector2(inputLensPositionX, inputLensPositionY);
        }
        public void SetSubject(IRectangleSceneObject subject)
        {
            PlatformerRectangle position = subject.GetPosition();
            if (!FocusBox.Contains(new Rectangle(position.X, position.Y, position.Width, position.Height)))
            {
                throw new Exception("Subject must be contained within the Focus Area.");
            }
            if (position.Width > FocusBox.Width || position.Height > FocusBox.Height)
            {
                throw new Exception("Subject area must not exceed Focus Box Area.");
            }
            _subject = subject;
        }
    }
}