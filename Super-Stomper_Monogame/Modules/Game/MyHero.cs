using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Stomper_Monogame.Modules.BaseClasses;
using System;

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class MyHero
    {
        public Hitbox hitbox;
        private Animation animation;
        private Animation currentAnimation;
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
        private const int jumpForce = 85;





        public MyHero(ContentManager content, Vector2 position)
        {
            canJump = false;

           movement = new Movement(position);
            hitbox = new Hitbox(new Rectangle(9, 17, myHeroWidth - 9 * 2, myHeroHeight - 17), Vector2.Zero);
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\MyHero\MyHero"), new Rectangle(0, 0, myHeroWidth, myHeroHeight), Vector2.Zero, position);

           physics = new Physics() { affectedByGravity = true, dragScale = 0.05f, fallingGravityScale = 1.5f };

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
            Console.WriteLine("MyHero Update");

            movement.Update(deltaTime);
            physics.Update(deltaTime);

            hitbox.Update(movement.position);
            currentAnimation = idleAnimation;

            // hero move
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Console.WriteLine("Moving right");
                physics.desiredVelocity.X += speed;

                sprite.spriteEffects = SpriteEffects.None;
                // isMoving = true; 
                currentAnimation = runAnimation;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Console.WriteLine("Moving left");

                physics.desiredVelocity.X -= speed;

                sprite.spriteEffects = SpriteEffects.FlipHorizontally;

                //isMoving = true;
                currentAnimation = runAnimation;

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
                Console.WriteLine("Jumping");
                if (canJump)
                {
                    currentAnimation = jumpAnimation;
                    physics.desiredVelocity.Y -= jumpForce;
                   // canJump = true;
                }
            }
            // Set the animation only if it's changed
            if (animation != currentAnimation)
            {
                animation = currentAnimation;
                animation.Reset();
            }

            physics.desiredVelocity.X = System.Math.Clamp(physics.desiredVelocity.X, -maxSpeed, maxSpeed);

            // effect of velocity on position 
            movement.deltaX += physics.desiredVelocity.X * deltaTime;
            movement.deltaY += physics.desiredVelocity.Y * deltaTime;

            //jumping or falling
            
           
            animation.Update(deltaTime);
            prevVelocity = physics.velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            sprite.sourceRect = animation.GetCurrentFrame();
            sprite.Draw(spriteBatch, movement.position);
        }
    }
}
