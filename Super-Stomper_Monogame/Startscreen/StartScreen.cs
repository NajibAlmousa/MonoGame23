using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Super_Stomper_Monogame.Startscreen
{
    internal class StartScreen
    {


        private enum GameState
        {
           StartMenu,
            LevelSelect,
            Playing,
        }
      

        private GameState gameState;
        private SpriteFont font;
        private int selected;
        private KeyboardState lastKeyboardState;





        private readonly ContentManager content;
        private readonly Color selectColor;



        private const string gameName = "Super Stomper";
        private const string startText = "Start";
        private const string levelSelectText = "Select Level";

        public StartScreen(ContentManager content)
        {

            this.content = content;
            selectColor = Color.Green;
            selected = 0;



            font = content.Load<SpriteFont>(@"Fonts\Font");
            lastKeyboardState = Keyboard.GetState();

            gameState = GameState.StartMenu;


        }

        public void Update(float deltaTime)
        {
            switch (gameState)
            {
                case GameState.StartMenu:


                    // Go up
                    if (Keyboard.GetState().IsKeyDown(Keys.Down) && !lastKeyboardState.IsKeyDown(Keys.Down))
                    {
                        selected++;
                        selected %= 2;

                       selected -= (selected / 2) * 2;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Up) && !lastKeyboardState.IsKeyDown(Keys.Up)) // Go Down
                    {
                        selected--;
                        selected %= 2;
                        selected = Math.Abs(selected);

                        selected += (selected / 2) * 2;
                    }
                    // Choose choice
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        // Start Game
                        if (selected == 0)
                        {
                            gameState = GameState.Playing;
                            
                        }
                        // Select Level
                        else if (selected == 1) // Select Level
                        {
                            selected = 0;
                            gameState = GameState.LevelSelect;
                        }
                    }

                    break;
            
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
            // Game name
            spriteBatch.DrawString(font, gameName, new Vector2(200, 20), Color.White, 0, font.MeasureString(gameName) / 2, 0.75f, SpriteEffects.None, 0);

            // Start Game

            spriteBatch.DrawString(font, startText, new Vector2(180, 120), selected == 0 ? selectColor : Color.White, 0, font.MeasureString(startText) / 2, 0.5f, SpriteEffects.None, 0);

            // Select level
            spriteBatch.DrawString(font, levelSelectText, new Vector2(180, 180), selected == 1 ? selectColor : Color.White, 0, font.MeasureString(levelSelectText) / 2, 0.5f, SpriteEffects.None, 0);
        }
    }
}
