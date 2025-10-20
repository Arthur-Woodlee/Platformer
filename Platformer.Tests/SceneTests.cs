using Xunit;
using Platformer.PlatformerSceneEngine;
using System.Collections.Generic;

namespace Platformer.Tests
{
    public class SceneTests
    {
        private class MockSceneObject : ISceneObject
        {
            public List<(ushort cycleLength, ushort cycleNumber)> Updates { get; } = new();
            public int DrawCalls { get; private set; } = 0;

            public void Update(ushort updateCycleLength, ushort cycleNumber)
            {
                Updates.Add((updateCycleLength, cycleNumber));
            }

            public void Draw()
            {
                DrawCalls++;
            }
        }

        [Fact]
        public void Scene_InitialisesWithCorrectIterations()
        {
            var scene = new Scene(5);
            Assert.NotNull(scene);
        }

        [Fact]
        public void Scene_AddsSceneObjectSuccessfully()
        {
            var scene = new Scene(3);
            var obj = new MockSceneObject();
            scene.AddSceneObject(obj);
            scene.UpdateScene();
            Assert.Equal(3, obj.Updates.Count);
        }

        [Fact]
        public void Scene_UpdatesEachObjectCorrectly()
        {
            var scene = new Scene(2);
            var obj1 = new MockSceneObject();
            scene.AddSceneObject(obj1);

            scene.UpdateScene();

            Assert.Equal(2, obj1.Updates.Count);
            Assert.Equal((ushort)2, obj1.Updates[0].cycleLength);
            Assert.Equal((ushort)1, obj1.Updates[0].cycleNumber);
            Assert.Equal((ushort)2, obj1.Updates[1].cycleNumber);
        }

        [Fact]
        public void Scene_DrawsEachObjectOnce()
        {
            var scene = new Scene(1);
            var obj1 = new MockSceneObject();
            var obj2 = new MockSceneObject();
            scene.AddSceneObject(obj1);
            scene.AddSceneObject(obj2);

            scene.DrawScene();

            Assert.Equal(1, obj1.DrawCalls);
            Assert.Equal(1, obj2.DrawCalls);
        }
    }
}

