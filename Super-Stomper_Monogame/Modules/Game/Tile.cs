using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Super_Stomper_Monogame.Modules.BaseClasses;

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class Tile
    {
        private Sprite sprite;

        public const int tileWidth = 16;
        public const int tileHeight = 16;

        public Tile(Texture2D texture, Rectangle sourceRect, Vector2 position)
        {
            sprite = new Sprite(texture, sourceRect, Vector2.Zero, position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, sprite.position);
        }
    }
}
