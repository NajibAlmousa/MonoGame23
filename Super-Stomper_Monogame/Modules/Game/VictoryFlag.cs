using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Super_Stomper_Monogame.Modules.BaseClasses;



namespace Super_Stomper_Monogame.Modules.Game
{
    internal class VictoryFlag
    {
        private const int flagWidth = 60;
        private const int flagHeight = 60;
        // load victoryfalg tocthe map
        private Sprite sprite;
        private float yOffset;

        public VictoryFlag(ContentManager content, Vector2 position)
        {
            this.yOffset = 23;
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\Environment\flag"), new Rectangle(0, 0, flagWidth, flagHeight), Vector2.Zero, new Vector2(position.X, position.Y + yOffset));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, sprite.position);
        }
    }
}
