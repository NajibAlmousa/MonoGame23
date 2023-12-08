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

        public bool canJump;


        private Animation idleAnimation;
        private Animation runAnimation;
        private Animation jumpAnimation;

        private Vector2 prevVelocity;

        private const int myHeroWidth = 32;
        private const int myHeroHeight = 32;
        private const int speed = 12;
        private const int maxSpeed = 150;
        private const int jumpForce = 40;





        public MyHero(ContentManager content, Vector2 position)
        {
            canJump = false;

            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\MyHero\MyHero"), new Rectangle(0, 0, myHeroWidth, myHeroHeight), Vector2.Zero, position);
            movement = new Movement(position);
            physics = new Physics() { dragScale = 0.05f };

            //frames
            idleAnimation = new Animation(sprite.texture, 0.1f, false, new int[] { 0 }, sprite.sourceRect.Size);
            runAnimation = new Animation(sprite.texture, 0.1f, true, new int[] { 0, 1, 2,3 }, sprite.sourceRect.Size);
            jumpAnimation = new Animation(sprite.texture, 0.1f, false, new int[] { 3 }, sprite.sourceRect.Size);



            //1
            animation = idleAnimation;
            prevVelocity = physics.velocity;




        }

        public void Update(float deltaTime)
        {
           // bool isMoving = false; 

             movement.Update(deltaTime);
             physics.Update(deltaTime);

          
            animation.Update(deltaTime);

            // hero move
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                sprite.spriteEffects = SpriteEffects.None;
              
                physics.desiredVelocity.X += speed;
               // isMoving = true; 
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                sprite.spriteEffects = SpriteEffects.FlipHorizontally;
            
                physics.desiredVelocity.X -= speed;
                //isMoving = true;

            }
            


            /*if (!isMoving)
            {
                animation = idleAnimation;
                animation.Reset(); 
            }
            else
            {
                
                animation = runAnimation;
            }*/

            //hero jump
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (canJump)
                {
                    animation = jumpAnimation;
                    physics.desiredVelocity.Y -= jumpForce;
                    canJump = false;
                }
            }

            physics.desiredVelocity.X = System.Math.Clamp(physics.desiredVelocity.X, -maxSpeed, maxSpeed);

            // effect of velocity on position 
            movement.deltaX += physics.desiredVelocity.X * deltaTime;
            movement.deltaY += physics.desiredVelocity.Y * deltaTime;

            //jumping or falling
            if (physics.velocity.Y != 0 || prevVelocity.Y != physics.velocity.Y)
            {
                animation = jumpAnimation;
                canJump = false;
            }
            else if (physics.velocity == Vector2.Zero) 
            {
                animation = idleAnimation;
                animation.Reset();
                canJump = true;
            }
            else
            {
                animation = runAnimation;
                canJump = true;
            }

            prevVelocity = physics.velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.sourceRect = animation.GetCurrentFrame();
            sprite.Draw(spriteBatch, movement.position);
        }
    }
}
