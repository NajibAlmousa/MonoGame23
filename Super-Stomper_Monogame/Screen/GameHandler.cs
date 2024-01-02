using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
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
            Playing,
            GameOver,
            Won
        }
        public static Vector2 cameraPosition;
        private int currentLevel;
        private GameState gameState;
        private SpriteFont font;
        private int selected;
        private KeyboardState lastKeyboardState;
        private readonly Game game;


        private readonly ScreenManager screenManager;
        private readonly ContentManager content;
        private readonly Color selectColor;



        private const string gameName = "Super Stomper";
        private const string startText = "Start";
        private const string levelSelectText = "Select Level";
        private const int numberOfLevels = 2;
        private const string gameOverText = "Game Over! :( ";
        private const string backToStartMenu = "Press Escape to Restart";
        private const string wonText = "Great job! You Won!";


        public GameHandler(ContentManager content, ScreenManager screenManager)
        {

            this.screenManager = screenManager ;
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
                        gameState = GameState.Playing;
                        currentLevel = selected + 1;
                        levelLoader = new MapLoader(content, selected + 1);

                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        selected = 0;
                        gameState = GameState.StartMenu;
                    }
                    break;
                case GameState.Playing:


                    MyHero hero = levelLoader.myHero;

                    // Game Over
                    if (hero.gameOver)
                    {
                        Reset();
                        gameState = GameState.GameOver;
                        break;
                    }

                    foreach (Hitbox hitbox in levelLoader.colliders)
                    {
                        if (hero.lives != 0)
                        {
                            levelLoader.myHero.physics.Collision(hero.movement, hero.hitbox, hitbox);
                        }
                            
                        // martian collision 
                        foreach (IEnemy martian in levelLoader.enemies)
                        {
                            if (martian is not Martian)
                                continue;
                            Martian thisMartian = (Martian)martian;

                            int collisionType = thisMartian.physics.Collision(thisMartian.movement, thisMartian.hitbox, hitbox);

                            if (collisionType == 1 || collisionType == 3)
                                thisMartian.direction *= -1;
                        }
                    }
                    //**/*/
                    hero.physics.velocity = hero.physics.desiredVelocity;

                    //Update flag animatie
                    levelLoader.victoryFlag.Update(deltaTime);

                    // Won the game
                    if (hero.hitbox.IsTouching(levelLoader.victoryFlag.hitbox))
                    {

                        Reset();
                        gameState = GameState.Won;
                        break;
                        
                    }

                    // Update martian physics
                    foreach (IEnemy martian in levelLoader.enemies)
                    {
                        if (martian is not Martian)
                            continue;

                        Martian thisMartian = (Martian)martian;
                        thisMartian.physics.velocity = thisMartian.physics.desiredVelocity;
                    }

                    //update hero
                    hero.Update(deltaTime);
                    // update martian
                    for (int i = 0; i < levelLoader.enemies.Count; i++)
                    {
                        if (levelLoader.enemies[i] is not Martian)
                            continue;

                        Martian thisMartian = (Martian)levelLoader.enemies[i];

                        levelLoader.enemies[i].Update(deltaTime, hero.movement.position);

                        // Remove martian after it has been stomped
                        if (thisMartian.shouldBeRemoved)
                        {
                            levelLoader.enemies.RemoveAt(i--);
                            continue;
                        }

                        // Stomp on martian 
                        int collisionType = thisMartian.physics.Collision(hero.movement, hero.hitbox, thisMartian.hitbox, true);

                        if (!thisMartian.stompedOn && (collisionType == 2 || collisionType == 3) && hero.physics.velocity.Y > 0 && hero.lives > 0)
                        {
                            thisMartian.stompedOn = true;
                            thisMartian.physics.desiredVelocity.X = 0;
                            hero.physics.desiredVelocity.Y = -Martian.stompRepulsionForce;
                        }
                        else if ((collisionType == 1 || (collisionType == 0 && hero.hitbox.IsTouching(thisMartian.hitbox))) && !thisMartian.stompedOn)
                        {

                            hero.hurtMyHero();
                        }

                    }

                    foreach (IEnemy ghost in levelLoader.enemies)
                    {
                        if (ghost is not Ghost)
                            continue;

                        ghost.Update(deltaTime, hero.movement.position);

                        // collision

                        if ((ghost as Ghost).fireBall != null && hero.hitbox.IsTouching((ghost as Ghost).fireBall.hitbox))
                            hero.hurtMyHero();

                    }
                    foreach (IEnemy fire in levelLoader.enemies)
                    {
                        if (fire is not Fire)
                            continue;

                        fire.Update(deltaTime, hero.movement.position);

                       
                    }




                    // Clamp camera position to not go offscreen
                    cameraPosition.X = Math.Clamp(-hero.movement.position.X + Game1.designedResolutionWidth / 2, -levelLoader.levelMaxWidth + Game1.designedResolutionWidth, 0);


                    // Collision with coins
                    for (int i = 0; i < levelLoader.coins.Count; i++)
                    {
                        levelLoader.coins[i].Update(deltaTime);

                        if (hero.hitbox.IsTouching(levelLoader.coins[i].hitbox))
                        {
                            levelLoader.coins.RemoveAt(i--);
                            continue;
                        }
                    }
                    break;
                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.StartMenu;
                        Reset();
                    }
                    break;
                case GameState.Won:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.StartMenu;
                        Reset();
                    }
                    break;
            }
            lastKeyboardState = Keyboard.GetState();
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //backgroundcolor
            // game.GraphicsDevice.Clear(Color.SkyBlue);

            
            switch (gameState)
            {

                case GameState.StartMenu:
                    Point windowSize = screenManager.GetScaledRect().Size;
                    // Game name
                    spriteBatch.DrawString(font, gameName, new Vector2(windowSize.X / 2, windowSize.Y * 0.25f), Color.White, 0, font.MeasureString(gameName) / 2, 0.75f, SpriteEffects.None, 0);

                    // Start Game

                    spriteBatch.DrawString(font, startText, new Vector2(windowSize.X / 2, windowSize.Y * 0.5f), selected == 0 ? selectColor : Color.White, 0, font.MeasureString(startText) / 2, 0.5f, SpriteEffects.None, 0);

                    // Select level
                    spriteBatch.DrawString(font, levelSelectText, new Vector2(windowSize.X / 2, windowSize.Y * 0.6f), selected == 1 ? selectColor : Color.White, 0, font.MeasureString(levelSelectText) / 2, 0.5f, SpriteEffects.None, 0);
                    break;
                case GameState.LevelSelect:
                    windowSize = screenManager.GetScaledRect().Size;

                    for (int i = 0; i < numberOfLevels; i++)
                        spriteBatch.DrawString(font, "Level " + (i + 1).ToString(), new Vector2(windowSize.X / 2, windowSize.Y * (0.2f + 0.1f * i)), selected == i ? selectColor : Color.White, 0, font.MeasureString("Level " + (i + 1).ToString()) / 2, 0.5f, SpriteEffects.None, 0);

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
                    // Draw coins
                    foreach (Coins coin in levelLoader.coins)
                        coin.Draw(spriteBatch);

                    // Draw the flag
                    levelLoader.victoryFlag.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    windowSize = screenManager.GetScaledRect().Size;

                    spriteBatch.DrawString(font, gameOverText, new Vector2(windowSize.X / 2, windowSize.Y * 0.3f), Color.White, 0, font.MeasureString(gameOverText) / 2, 0.8f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, backToStartMenu, new Vector2(windowSize.X / 2, windowSize.Y * 0.7f), Color.White , 0, font.MeasureString(backToStartMenu) / 2, 0.5f, SpriteEffects.None, 0);
                    break;
                case GameState.Won:
                    windowSize = screenManager.GetScaledRect().Size;

                    spriteBatch.DrawString(font, wonText, new Vector2(windowSize.X / 2, windowSize.Y * 0.3f), Color.White, 0, font.MeasureString(wonText) / 2, 0.5f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, backToStartMenu, new Vector2(windowSize.X / 2, windowSize.Y * 0.7f), Color.White, 0, font.MeasureString(backToStartMenu) / 2, 0.5f, SpriteEffects.None, 0);

                    break;
            }
        }

    }
}
