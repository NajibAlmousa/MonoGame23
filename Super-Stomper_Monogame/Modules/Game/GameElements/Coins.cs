using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Super_Stomper_Monogame.Modules.BaseClasses.Animations;
using Super_Stomper_Monogame.Modules.BaseClasses.GameHandlers;
using Super_Stomper_Monogame.Modules.BaseClasses.Sprites;


namespace Super_Stomper_Monogame.Modules.Game.GameElements
{
    internal class Coins
    {
        private Sprite sprite;
        private Animation animation;
        public Hitbox hitbox;


        private const int coinWidth = 16;
        private const int coinHeight = 16;

        public Coins(ContentManager content, Vector2 position)
        {
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\Coins\coins"), new Rectangle(0, coinHeight, coinWidth, coinHeight), Vector2.Zero, position);
            animation = new Animation(sprite.texture, 0.25f, true, new int[] { 0, 1, 2, 3, 4 }, sprite.sourceRect.Size);
            hitbox = new Hitbox(new Rectangle((int)position.X, (int)position.Y, coinWidth, coinHeight), Vector2.Zero);
        }

        public void Update(float deltaTime)
        {
            animation.Update(deltaTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.sourceRect = animation.GetCurrentFrame();
            sprite.Draw(spriteBatch, sprite.position);
        }
    }
}
