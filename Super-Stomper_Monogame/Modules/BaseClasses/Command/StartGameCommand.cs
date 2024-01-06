using Super_Stomper_Monogame.Modules.BaseClasses.GameHandlers;
using Super_Stomper_Monogame.Modules.BaseClasses.Interfaces;




namespace Super_Stomper_Monogame.Modules.BaseClasses.Command
{
    internal class StartCommand : ICommand
    {
        private readonly GameHandler gameHandler;

        public StartCommand(GameHandler gameHandler)
        {
            this.gameHandler = gameHandler;
        }

        public void Execute()
        {
            gameHandler.StartGame();
        }
    }

}
