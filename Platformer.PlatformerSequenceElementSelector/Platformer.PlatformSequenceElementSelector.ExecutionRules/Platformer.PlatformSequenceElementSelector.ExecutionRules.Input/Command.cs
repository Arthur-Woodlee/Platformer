using System;

namespace Platformer.PlatformerSequenceElementSelector.Sequences.ExecutionRules.Input
{
    public abstract class Command
    {
        protected string _name;
        public Command(string name)
        {
            _name = name;
        }
        public string Name
        {
            get
            {
                return String.Copy(_name);
            }
        }
        public abstract void UpdateState();
        public abstract void PauseCommand(ushort period);
    }
}