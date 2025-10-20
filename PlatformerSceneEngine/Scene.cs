using System.Collections.Generic;

namespace Platformer.PlatformerSceneEngine
{
    public class Scene
    {
        private LinkedList<ISceneObject> _sceneObjects;
        private ushort _iterations { get; set; }

        public Scene(ushort interations)
        {
            _iterations = interations; // Iterations are the number of updates each scene object will have available to them when UpdateScene is called.
            _sceneObjects = new LinkedList<ISceneObject>();
        }
        public void UpdateScene()
        {
            for (ushort iteration = 1; iteration < _iterations + 1; iteration++)
            {
                foreach (ISceneObject sceneObject in _sceneObjects)
                {
                    sceneObject.Update(_iterations, iteration);
                }
            }
        }
        public void DrawScene()
        {
            foreach (ISceneObject sceneObject in _sceneObjects)
            {
                sceneObject.Draw();
            }
        }
        public void AddSceneObject(ISceneObject updateable)
        {
            _sceneObjects.AddFirst(updateable);
        }
    }
}
