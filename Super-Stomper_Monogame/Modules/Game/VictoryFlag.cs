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
      
        private Sprite sprite;
        private Animation animation;
        public Hitbox hitbox;

        private float yOffset;

        public VictoryFlag(ContentManager content, Vector2 position)
        {
            this.yOffset = 23;
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\Environment\flag"), new Rectangle(0, 0, flagWidth, flagHeight), Vector2.Zero, new Vector2(position.X, position.Y + yOffset));
            animation = new Animation(sprite.texture, 0.25f, true, new int[] { 0, 1, 2, 3 }, sprite.sourceRect.Size);
            hitbox = new Hitbox(new Rectangle((int)position.X + (2 * flagWidth / 7), (int)position.Y + (2 * flagHeight / 3) + (int)yOffset, flagWidth / 3, flagHeight / 2), Vector2.Zero);


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
