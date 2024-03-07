using BlastGame.ServiceManagement;
using System;

namespace BlastGame.CommandSystem
{
    public interface ICommandManager : IService, IDisposable
    {
        public void RegisterAndProcessCommand(ICommand command);
        public void StartProcessingCommands();
        public bool IsProcessing();
    }
}
