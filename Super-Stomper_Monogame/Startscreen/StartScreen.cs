using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Stomper_Monogame.Modules.BaseClasses;
using Super_Stomper_Monogame.Modules.Game;
using System;

namespace Super_Stomper_Monogame.Startscreen
{
    internal class StartScreen
    {
        private MyHero myHero;

        private enum GameState
        {
           StartMenu,
           LevelSelect,
           Playing,
        }

        private int currentLevel;
        private GameState gameState;
        private SpriteFont font;
        private int selected;
        private KeyboardState lastKeyboardState;

        private readonly Game game;




        private readonly Windowbox windowbox;
        private readonly ContentManager content;
        private readonly Color selectColor;



        private const string gameName = "Super Stomper";
        private const string startText = "Start";
        private const string levelSelectText = "Select Level";
        private const int numberOfLevels = 2;
        public StartScreen(ContentManager content, Windowbox windowbox)
        {

            this.windowbox = windowbox;
            this.game = game;

            this.content = content;
            selectColor = Color.Green;
            selected = 0;


            currentLevel = 1;
            font = content.Load<SpriteFont>(@"Fonts\Font");
            lastKeyboardState = Keyboard.GetState();

            gameState = GameState.StartMenu;


            myHero = new MyHero(content, new Vector2(200, 300));  



        }
        private GameWindow Window => game.Window;

        public void Update(float deltaTime)
        {
            switch (gameState)
            {
                case GameState.StartMenu:
                    currentLevel = 1;

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
                        else if (selected == 1) 
                        {
                            selected = 0;
                            gameState = GameState.LevelSelect;
                        }
                    }

                    break;
                case GameState.LevelSelect:
                    if (Keyboard.GetState().IsKeyDown(Keys.Down) && !lastKeyboardState.IsKeyDown(Keys.Down))
                    {
                        selected++;
                        selected %= numberOfLevels;

                        selected -= (selected / numberOfLevels) * numberOfLevels;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Up) && !lastKeyboardState.IsKeyDown(Keys.Up))
                    {
                        selected = selected > 0 ? selected - 1 : numberOfLevels - 1;
                        selected %= numberOfLevels;
                        selected = Math.Abs(selected);

                        selected += (selected / numberOfLevels) * numberOfLevels;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !lastKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        // Select level
                        //hier later aanpassen
                      // gameState = GameState.Playing;
                        currentLevel = selected + 1;
                       
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        selected = 0;
                        gameState = GameState.StartMenu;
                    }
                    break;
               case GameState.Playing:
                    myHero.Update(deltaTime);
                    break;

            }
            lastKeyboardState = Keyboard.GetState();
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //backgroundcolor
          // game.GraphicsDevice.Clear(Color.SkyBlue);

            Point windowSize = windowbox.GetScaledRect().Size;
            switch (gameState)
            {

                case GameState.StartMenu:
                    // Game name
                    spriteBatch.DrawString(font, gameName, new Vector2(windowSize.X /2, windowSize.Y * 0.25f), Color.White, 0, font.MeasureString(gameName) / 2, 0.75f, SpriteEffects.None, 0);

                    // Start Game

                    spriteBatch.DrawString(font, startText, new Vector2(windowSize.X/2, windowSize.Y * 0.5f), selected == 0 ? selectColor : Color.White, 0, font.MeasureString(startText) / 2, 0.5f, SpriteEffects.None, 0);

                    // Select level
                    spriteBatch.DrawString(font, levelSelectText, new Vector2(windowSize.X/2, windowSize.Y * 0.6f), selected == 1 ? selectColor : Color.White, 0, font.MeasureString(levelSelectText) / 2, 0.5f, SpriteEffects.None, 0);
                    break;
                case GameState.LevelSelect:
                   

                    for (int i = 0; i < numberOfLevels; i++)
                        spriteBatch.DrawString(font, "Level " + (i + 1).ToString(), new Vector2(220, 350 * (0.4f + 0.1f * i)), selected == i ? selectColor : Color.White, 0, font.MeasureString("Level " + (i + 1).ToString()) / 2, 0.5f, SpriteEffects.None, 0);

                    break;
                case GameState.Playing:
                    myHero.Draw(spriteBatch);



                    break;
            }
        }
    }
}
