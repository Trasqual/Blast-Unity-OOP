using System.Collections.Generic;

namespace BlastGame.CommandSystem
{
    public abstract class CommandManagerBase : ICommandManager
    {
        protected Queue<ICommand> _commands = new();

        protected bool _processing;

        public void StartProcessingCommands()
        {
            if (_processing)
            {
                return;
            }

            ProcessCommands();
            _processing = true;
        }

        protected virtual void ProcessCommands()
        {
            if (_commands.Count <= 0)
            {
                _processing = false;
                return;
            }

            ICommand command = _commands.Dequeue();
            command.Execute(ProcessCommands);
        }

        public virtual void RegisterAndProcessCommand(ICommand command)
        {
            _commands.Enqueue(command);
            StartProcessingCommands();
        }

        public bool IsProcessing() => _processing;

        public abstract void Dispose();
    }
}
