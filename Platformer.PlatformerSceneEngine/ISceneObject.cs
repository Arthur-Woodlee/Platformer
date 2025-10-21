namespace Platformer.PlatformerSceneEngine
{
    public interface ISceneObject
    {
        void Update(ushort updateCycleLength, ushort cycleNumber);
        void Draw();
    }
}