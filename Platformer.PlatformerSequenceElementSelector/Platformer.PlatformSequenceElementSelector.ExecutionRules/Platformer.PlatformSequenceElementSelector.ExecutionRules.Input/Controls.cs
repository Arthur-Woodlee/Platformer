using System.Collections.Generic;

namespace Platformer.PlatformerSequenceElementSelector.Sequences.ExecutionRules.Input
{
    public class Controls
    {
        private LinkedList<BoolCommand> _boolCommands;

        public Controls()
        {
            _boolCommands = new LinkedList<BoolCommand>();
        }
        public void SetBoolSensor(string commandName, ReadBoolSensor readBoolSensor)
        {
            foreach (BoolCommand bc in _boolCommands)
            {
                if (bc.Name == commandName)
                {
                    bc.SetSensor(readBoolSensor);
                }
            }
        }
        public bool AddBoolCommand(BoolCommand command)
        {
            foreach (BoolCommand c in _boolCommands)
            {
                if (command.Name.Equals(c.Name))
                {
                    return false; // A command with that name already exists
                }
            }
            _boolCommands.AddLast(command);
            return true;
        }
        public void ClearCommands()
        {
            _boolCommands = new LinkedList<BoolCommand>();

        }
        public void UpdateState()
        {
            foreach (BoolCommand c in _boolCommands)
            {
                c.UpdateState();
            }
        }
        public bool GetBoolCommand(string command)
        {
            foreach (BoolCommand c in _boolCommands)
            {
                if (c.Name == command)
                {
                    return c.GetCommand();
                }
            }
            return false;
        }
        public void PauseCommand(string command, ushort period)
        {
            foreach (BoolCommand bc in _boolCommands)
            {
                if (command == bc.Name)
                {
                    bc.PauseCommand(period);
                }
            }
        }
        public LinkedList<string> GetBoolCommands()
        {
            LinkedList<string> commands = new LinkedList<string>();
            foreach (BoolCommand bc in _boolCommands)
            {
                commands.AddLast(bc.Name);
            }
            return commands;
        }

    }
}