using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Super_Stomper_Monogame.Modules.BaseClasses;

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class Coins
    {
        private Sprite sprite;

        private const int coinWidth = 16;
        private const int coinHeight = 16;

        public Coins(ContentManager content, Vector2 position)
        {
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\Coins\coin"), new Rectangle(0, coinHeight, coinWidth, coinHeight), Vector2.Zero, position);

        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            sprite.Draw(spriteBatch, sprite.position);
        }
    }
}
