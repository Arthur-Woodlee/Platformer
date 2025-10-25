using Platformer.PlatformerElementSelector;
using Platformer.PlatformerSequenceElementSelector.Sequences;

namespace Platformer.PlatformerSequenceElementSelector
{
    public interface ISequence
    {
        Element Continue(ushort updateCycleLength, ushort cycleNumber);
        ushort PixelsTravellingPerUpdate { get; set; }
        TextureReel TextureReel { get; set; }
    }
}