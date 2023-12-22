﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Stomper_Monogame.Modules.BaseClasses;
using Super_Stomper_Monogame.Modules.Game;
using System;


namespace Super_Stomper_Monogame.Screen
{
    internal class GameHandler
    {
        private MyHero myHero;
        private MapLoader levelLoader;

        private enum GameState
        {
           StartMenu,
           LevelSelect,
           Playing
        }
        public static Vector2 cameraPosition;
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
        public GameHandler(ContentManager content, Windowbox windowbox)
        {

            this.windowbox = windowbox;
            this.game = new Game();

            this.content = content;
            selectColor = Color.Green;
            selected = 0;


            currentLevel = 1;
            font = content.Load<SpriteFont>(@"Fonts\Font");
            lastKeyboardState = Keyboard.GetState();
            cameraPosition = Vector2.Zero;

            gameState = GameState.StartMenu;


            myHero = new MyHero(content, new Vector2(20, 40));



        }
        //private GameWindow Window => game.Window;

        public void Reset()
        {
            selected = 0;
            cameraPosition = Vector2.Zero;
          
        }


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

                            levelLoader = new MapLoader(content, currentLevel);


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
                        //gameState = GameState.Playing;
                        currentLevel = selected + 1;
                       
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        selected = 0;
                        gameState = GameState.StartMenu;
                    }
                    break;
               case GameState.Playing:

                   
                    MyHero hero = levelLoader.myHero;
                    hero.Update(deltaTime);
                    foreach (Hitbox hitbox in levelLoader.colliders)
                    {
                        levelLoader.myHero.physics.Collision(hero.movement, hero.hitbox, hitbox);
                    }


                    // Clamp camera position to not go offscreen
                    cameraPosition.X = Math.Clamp(-hero.movement.position.X + Game1.designedResolutionWidth / 2, -levelLoader.levelMaxWidth + Game1.designedResolutionWidth, 0);
                    for (int i = 0; i < levelLoader.enemies.Count; i++)
                    {

                        Martian martian = (Martian)levelLoader.enemies[i];

                        martian.Update(deltaTime, hero.movement.position);
                    }

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
              
                  // myuhero
                    levelLoader.myHero.Draw(spriteBatch);

                    // tile 
                    foreach (Tile tile in levelLoader.tiles)
                        tile.Draw(spriteBatch);

                    // Draw the enemies
                    foreach (IEnemy enemy in levelLoader.enemies)
                        enemy.Draw(spriteBatch);


                    break;
            }
        }
        
    }
}
