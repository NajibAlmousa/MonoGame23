using Super_Stomper_Monogame.Modules.Game;
using Super_Stomper_Monogame.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
