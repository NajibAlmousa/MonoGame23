using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Stomper_Monogame.Startscreen
{
    internal class StartScreen
    {
        private SpriteFont font;
        private readonly ContentManager content;

        private const string gameName = "Super Stomper";
        private const string startText = "Start";
        private const string levelSelectText = "Select Level";

        public StartScreen(ContentManager content)
        {
            this.content = content;
            font = content.Load<SpriteFont>(@"Fonts\Font");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Game name
            spriteBatch.DrawString(font, gameName, new Vector2(180, 20), Color.Wheat);

            // Start Game
            spriteBatch.DrawString(font, startText, new Vector2(180, 120), Color.White);

            // Select level
            spriteBatch.DrawString(font, levelSelectText, new Vector2(180, 180), Color.White);
        }
    }
}
