using Xunit;
using Microsoft.Xna.Framework;
using Platformer.Positioning;

namespace Platformer.Tests
{
    using Xunit;
    using Microsoft.Xna.Framework;

    namespace Platformer.Tests
    {
        public class CameraTests
        {
            class MockRectangleSceneObject : IRectangleSceneObject
            {
                private PlatformerRectangle _position;

                public MockRectangleSceneObject(PlatformerRectangle position)
                {
                    _position = position;
                }

                public void SetPosition(PlatformerRectangle newPosition)
                {
                    _position = newPosition;
                }

                public PlatformerRectangle GetPosition() => _position;

                public void Draw() => throw new NotImplementedException();

                public void Update(ushort updateCycleLength, ushort cycleNumber) => throw new NotImplementedException();
            }

            private readonly Rectangle lens = new Rectangle(0, 0, 200, 200);
            private readonly Rectangle focusBox = new Rectangle(50, 50, 100, 100);
            private readonly PlatformerVector2 endScene = new PlatformerVector2(500, 500);

            [Fact]
            public void CameraTests_Constructor_InvalidFocusBox_ThrowsException()
            {
                var badFocus = new Rectangle(0, 0, 300, 300);
                Assert.Throws<Exception>(() => new Camera(lens, badFocus, endScene));
            }

            [Fact]
            public void CameraTests_SetSubject_OutsideFocusBox_ThrowsException()
            {
                var camera = new Camera(lens, focusBox, endScene);
                var subject = new MockRectangleSceneObject(new PlatformerRectangle(0, 0, 10, 10));
                Assert.Throws<Exception>(() => camera.SetSubject(subject));
            }

            [Fact]
            public void CameraTests_SetSubject_TooLargeForFocusBox_ThrowsException()
            {
                var camera = new Camera(lens, focusBox, endScene);
                var subject = new MockRectangleSceneObject(new PlatformerRectangle(50, 50, 150, 150));
                Assert.Throws<Exception>(() => camera.SetSubject(subject));
            }

            [Fact]
            public void CameraTests_SetSubject_ValidSubject_Succeeds()
            {
                var camera = new Camera(lens, focusBox, endScene);
                var subject = new MockRectangleSceneObject(new PlatformerRectangle(60, 60, 20, 20));
                camera.SetSubject(subject);
            }

            [Fact]
            public void CameraTests_Update_SubjectMovesRight_ShiftsLens()
            {
                var camera = new Camera(lens, focusBox, endScene);
                var subject = new MockRectangleSceneObject(new PlatformerRectangle(60, 60, 20, 20));
                camera.SetSubject(subject);

                subject.SetPosition(new PlatformerRectangle(160, 60, 20, 20)); // move right
                camera.Update();

                Assert.True(camera.Lens.X > lens.X); // lens should have shifted right
            }

            [Fact]
            public void CameraTests_GetDrawingPosition_Rectangle_ReturnsCorrectOffset()
            {
                var camera = new Camera(lens, focusBox, endScene);
                var subject = new MockRectangleSceneObject(new PlatformerRectangle(100, 100, 20, 20));
                camera.SetSubject(subject);

                var input = new PlatformerRectangle(120, 120, 10, 10);
                var result = camera.GetDrawingPosition(input);

                Console.WriteLine($"Actual Rectangle: {result}");
                Assert.Equal(new Rectangle(120, 120, 10, 10), result);
            }

            [Fact]
            public void CameraTests_GetDrawingPosition_Rectangle_ChangesAfterUpdate()
            {
                var camera = new Camera(lens, focusBox, endScene);
                var subject = new MockRectangleSceneObject(new PlatformerRectangle(60, 60, 20, 20));
                camera.SetSubject(subject);

                var input = new PlatformerRectangle(80, 80, 10, 10);
                var initialDrawing = camera.GetDrawingPosition(input);

                // Move subject outside focus box to trigger lens shift
                subject.SetPosition(new PlatformerRectangle(160, 160, 20, 20));
                camera.Update();

                var updatedDrawing = camera.GetDrawingPosition(input);

                Assert.NotEqual(initialDrawing, updatedDrawing);
            }

            [Fact]
            public void CameraTests_GetDrawingPosition_Vector2_ReturnsCorrectOffset()
            {
                var camera = new Camera(lens, focusBox, endScene);
                var subject = new MockRectangleSceneObject(new PlatformerRectangle(100, 100, 20, 20));
                camera.SetSubject(subject);

                var input = new PlatformerVector2(120, 120);
                var result = camera.GetDrawingPosition(input);

                Assert.Equal(new Vector2(120, 120), result);
            }
            [Fact]
            public void CameraTests_GetDrawingPosition_Vector2_ChangesAfterUpdate()
            {
                var camera = new Camera(lens, focusBox, endScene);
                var subject = new MockRectangleSceneObject(new PlatformerRectangle(60, 60, 20, 20));
                camera.SetSubject(subject);

                var input = new PlatformerVector2(80, 80);
                var initialDrawing = camera.GetDrawingPosition(input);

                // Move subject outside focus box to trigger lens shift
                subject.SetPosition(new PlatformerRectangle(160, 160, 20, 20));
                camera.Update();

                var updatedDrawing = camera.GetDrawingPosition(input);

                Assert.NotEqual(initialDrawing, updatedDrawing);
            }
        }
    }
}