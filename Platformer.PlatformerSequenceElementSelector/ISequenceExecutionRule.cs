namespace Platformer.PlatformerSequenceElementSelector
{
    public interface ISequenceExecutionRule
    {
        ISequence Evaluate(ISequence current);
    }
}