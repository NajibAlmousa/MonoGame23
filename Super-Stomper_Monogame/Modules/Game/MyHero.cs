using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Stomper_Monogame.Modules.BaseClasses;

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class MyHero
    {
        private const int myHeroWidth = 32;
        private const int myHeroHeight = 32;

        private Sprite sprite;

        Vector2 direction = Vector2.Zero;
        

        public MyHero(ContentManager content, Vector2 position)
        {
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\MyHero\MyHero"), new Rectangle(0, 0, myHeroWidth, myHeroHeight), Vector2.Zero, position);
        }

        public void Update(float deltaTime)
        {
            
            direction = Vector2.Zero;

            // Move right
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                sprite.spriteEffects = SpriteEffects.None;
                direction.X += 1;
            }
            // Move left
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                sprite.spriteEffects = SpriteEffects.FlipHorizontally;
                direction.X -= 1;
            }

           

          
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Vector2(20, 30));
        }
    }
}
