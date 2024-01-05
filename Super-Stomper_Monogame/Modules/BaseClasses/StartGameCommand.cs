using Super_Stomper_Monogame.Screen;


namespace Super_Stomper_Monogame.Modules.BaseClasses
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
