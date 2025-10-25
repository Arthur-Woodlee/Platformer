namespace Platformer.PlatformerSequenceElementSelector.Sequences.ExecutionRules.Input
{
    public class ControlManager
    {
        private Controls _userControls;
        public ControlManager(Controls userControls)
        {
            _userControls = userControls;
        }
        public bool GetBoolCommand(string command)
        {
            return _userControls.GetBoolCommand(command);
        }
        public void PauseCommand(string command, ushort period)
        {
            _userControls.PauseCommand(command, period);
        }
    }
}