using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Stomper_Monogame.Modules.BaseClasses;
using Super_Stomper_Monogame.Modules.Game;
using Super_Stomper_Monogame.Startscreen;

namespace Super_Stomper_Monogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //screenMenu
        StartScreen StartScreen;
        private Windowbox windowbox;

        //schermvergroten
        public const int designedResolutionWidth = 320;
        public const int designedResolutionHeight = 256;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _graphics.PreferredBackBufferWidth = designedResolutionWidth * 3;
            _graphics.PreferredBackBufferHeight = designedResolutionHeight * 3;
            _graphics.ApplyChanges();

            windowbox = new Windowbox(this, designedResolutionWidth, designedResolutionHeight);
            StartScreen = new StartScreen(Content, windowbox);


        }

        protected override void Update(GameTime gameTime)
        {
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();*/

            // TODO: Add your update logic here
            StartScreen.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
         
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            //GraphicsDevice.Clear(Color.SkyBlue);


            // TODO: Add your drawing code here
           /* _spriteBatch.Begin();
            StartScreen.Draw(_spriteBatch);
            _spriteBatch.End();*/
            windowbox.Draw(_spriteBatch, StartScreen.Draw, samplerState: SamplerState.PointClamp);

            base.Draw(gameTime);
        }
    }
}
