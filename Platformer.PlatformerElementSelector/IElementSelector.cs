namespace   Platformer.PlatformerElementSelector
{
    public interface IElementSelector
    {
        Element SelectElement(ushort updateCycleLength, ushort cycleNumber);
    }
}