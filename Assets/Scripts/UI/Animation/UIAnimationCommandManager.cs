using BlastGame.CommandSystem;

namespace BlastGame.UI.Animations
{
    public class UIAnimationCommandManager : CommandManagerBase
    {
        protected override void ProcessCommands()
        {
            if (_commands.Count <= 0)
            {
                _processing = false;
                return;
            }

            ICommand command = _commands.Dequeue();

            command.Execute(ProcessCommands);
        }

        public override void RegisterAndProcessCommand(ICommand command)
        {
            _commands.Enqueue(command);
            StartProcessingCommands();
        }

        public override void Dispose()
        {
        }
    }
}
