using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Stomper_Monogame.Modules.BaseClasses;

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class MyHero
    {
        
        private Animation animation;
        private Sprite sprite;
        public Physics physics;
        public Movement movement;


        private Animation idleAnimation;
        private Animation runAnimation;

        private const int myHeroWidth = 32;
        private const int myHeroHeight = 32;
        private const int speed = 12;


        
    


        public MyHero(ContentManager content, Vector2 position)
        {
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\MyHero\MyHero"), new Rectangle(0, 0, myHeroWidth, myHeroHeight), Vector2.Zero, position);
             movement = new Movement(position);
            physics = new Physics() { dragScale = 0.05f };
            //frames
            idleAnimation = new Animation(sprite.texture, 0.1f, false, new int[] { 0 }, sprite.sourceRect.Size);
            runAnimation = new Animation(sprite.texture, 0.1f, true, new int[] { 0, 1, 2, 3 }, sprite.sourceRect.Size);


            //1
            animation = idleAnimation;
          



        }

        public void Update(float deltaTime)
        {


            movement.Update(deltaTime);
            physics.Update(deltaTime);

            animation.Update(deltaTime);
            // Move right
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                sprite.spriteEffects = SpriteEffects.None;
              
                physics.desiredVelocity.X += speed;
            }
            // Move left
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                sprite.spriteEffects = SpriteEffects.FlipHorizontally;
            
                physics.desiredVelocity.X -= speed;

            }
            // effect of velocity on position 
            movement.deltaX += physics.desiredVelocity.X * deltaTime;
            movement.deltaY += physics.desiredVelocity.Y * deltaTime;



        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.sourceRect = animation.GetCurrentFrame();
            sprite.Draw(spriteBatch, movement.position);
        }
    }
}
