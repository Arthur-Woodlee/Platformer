namespace Platformer.PlatformerSequenceElementSelector.Sequences.ExecutionRules.Input
{
    public delegate bool ReadBoolSensor();

    public class BoolCommand : Command
    {
        protected ReadBoolSensor _boolSensor;
        bool _state;
        private ushort _pauseCounter;

        public BoolCommand(string name) : base(name)
        {
            _pauseCounter = 0;
            _state = false;
        }
        public override void PauseCommand(ushort period)
        {
            _pauseCounter = period;
        }
        public override void UpdateState()
        {
            if (_pauseCounter != 0)
            {
                _pauseCounter--;
                _state = false;
                return;
            }
            if (_boolSensor())
            {
                _state = true;
            }
            else
            {
                _state = false;
            }
        }
        public bool GetCommand()
        {
            return _state;
        }
        public void SetSensor(ReadBoolSensor boolSensor)
        {
            _boolSensor = boolSensor;
        }
    }
}