using Platformer.PlatformerElementSelector;

namespace Platformer.PlatformerSequenceElementSelector
{
    public class SequenceElementSelector : IElementSelector
    {
        private ISequence _currentSequence;
        private readonly ISequenceExecutionRule[] _rules;
        public SequenceElementSelector(ISequence initialSequence, ISequenceExecutionRule[] rules)
        {
            this._currentSequence = initialSequence;
            this._rules = rules;
        }
        public Element SelectElement(ushort updateCycleLength, ushort cycleNumber)
        {
            ISequence seq = _currentSequence;
            foreach (ISequenceExecutionRule rule in _rules)
            {
                seq = rule.Evaluate(_currentSequence);
                if (seq != null)
                {
                    _currentSequence = seq;
                }
            }
            return _currentSequence == null ? null : _currentSequence.Continue(updateCycleLength, cycleNumber);
        }
    }
}