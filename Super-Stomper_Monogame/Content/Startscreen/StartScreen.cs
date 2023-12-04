using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Super_Stomper_Monogame.Content.Startscreen
{
    internal class StartScreen
    {
        private const string gameName = "Super Stomper";
        private const string startText = "Start";
        private const string levelSelectText = "Select Level";

        public StartScreen()
        {
            
        }

        public void Drow (SpriteBatch spriteBatch)
        {
            // Game name
            spriteBatch.DrawString(gameName);

            // Start Game
            spriteBatch.DrawString(startText);

            // Select level
            spriteBatch.DrawString(levelSelectText);
           
        }
    }
}
