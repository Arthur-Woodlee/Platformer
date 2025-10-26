using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.PlatformerSequenceElementSelector.Sequences.ExecutionRules.Input;
using System;

namespace Platformer.PlatformerSequenceElementSelector.Sequences.ExecutionRules
{
    public class PlatformMovementExecutionRule : ISequenceExecutionRule
    {
        private ControlManager _controlManager;
        private BoolCommand[] _requiredCommands;
        private ISequence _lastReturned;
        private PlatformMoveSequenceConfig _sequenceConfig;
        private Texture2D[] _textures;
        public PlatformMovementExecutionRule(PlatformMoveSequenceConfig platformMoveSequenceConfig)
        {
            this._sequenceConfig = platformMoveSequenceConfig;
            this._textures = LoadAssets(_sequenceConfig.Assets);
        }
        private Texture2D[] LoadAssets(String[] assetNames)
        {
            Texture2D[] textures = new Texture2D[assetNames.Length];
            for (int i = 0; i < assetNames.Length; i++)
            {
                textures[i] = _sequenceConfig.ContentManager.Load<Texture2D>(assetNames[i]);
            }
            return textures;
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
                    _lastReturned = PlatformMoveSequence.Create(_sequenceConfig.InitialPixelsTravellingPerUpdate, _sequenceConfig.SlowRate, _sequenceConfig.CollisionDetector, _sequenceConfig.ContactHandler, _sequenceConfig.Increment, _sequenceConfig.MaxSpeed, _sequenceConfig.MoveDirection, _sequenceConfig.ElementSelectorSceneObject, new TextureReel(_textures));
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