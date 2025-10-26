using Platformer.PlatformerSequenceElementSelector.Sequences.ExecutionRules.Input;

namespace Platformer.PlatformerSequenceElementSelector.Sequences.ExecutionRules
{
    public class PlatformMovementExecutionRule : ISequenceExecutionRule
    {
        private ControlManager _controlManager;
        private BoolCommand[] _requiredCommands;
        private ISequence _lastReturned;
        private PlatformMoveSequenceConfig _sequenceConfig;
        public PlatformMovementExecutionRule(PlatformMoveSequenceConfig platformMoveSequenceConfig)
        {
            this._sequenceConfig = platformMoveSequenceConfig;
        }
        public ISequence Evaluate(ISequence current)
        {
            if (VerifyMove())
            {
                if (current == _lastReturned && _lastReturned != null)
                {
                    _lastReturned = PlatformMoveSequence.Create((ushort)(current.PixelsTravellingPerUpdate + _sequenceConfig.SlowRate + _sequenceConfig.Increment), _sequenceConfig.SlowRate, _sequenceConfig.CollisionDetector, _sequenceConfig.ContactHandler, _sequenceConfig.Increment, _sequenceConfig.MaxSpeed, _sequenceConfig.MoveDirection, _sequenceConfig.ElementSelectorSceneObject, current.TextureReel);
                    return _lastReturned;
                }
                else
                {
                    _lastReturned = PlatformMoveSequence.Create(_sequenceConfig.InitialPixelsTravellingPerUpdate, _sequenceConfig.SlowRate, _sequenceConfig.CollisionDetector, _sequenceConfig.ContactHandler, _sequenceConfig.Increment, _sequenceConfig.MaxSpeed, _sequenceConfig.MoveDirection, _sequenceConfig.ElementSelectorSceneObject, new TextureReel(_sequenceConfig.Textures));
                    return _lastReturned;
                }
            }
            return null;
        }
        private bool VerifyMove()
        {
            int requiredCommandsCount = 0;
            foreach (BoolCommand boolCommand in this._requiredCommands)
            {
                if (this._controlManager.GetBoolCommand(boolCommand.Name))
                {
                    requiredCommandsCount++;
                }
            }
            if (requiredCommandsCount != this._requiredCommands.Length)
            {
                return false;
            }
            return true;
        }
    }
}