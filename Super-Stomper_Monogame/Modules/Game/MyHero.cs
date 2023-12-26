using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Stomper_Monogame.Modules.BaseClasses;
using Super_Stomper_Monogame.Screen;
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
        public bool gameOver;

        private Animation idleAnimation;
        private Animation runAnimation;
        private Animation jumpAnimation;
        private Animation deathAnimation;

        private Vector2 prevVelocity;

        private const int myHeroWidth = 32;
        private const int myHeroHeight = 32;
        private const int speed = 12;
        private const int maxSpeed = 150;
        private const int jumpForce = 170;


        //myhero lives
        public int lives;
        private const int heartWidth = 17;
        private const int heartHeight = 17;
        private const int startingLives = 3;
        private Sprite heartSprite;

        //IMMUNE
        public bool immune;
        private float immunityTimer;
        private const int immunityDuration = 2;
        //laten flickeren
        private const int flickerSpeed = 2;
        private float flickerTimer;

        private const int dieAfter = 2;
        private float deathTimer;
        






        public MyHero(ContentManager content, Vector2 position)
        {
            canJump = false;
            gameOver = false;

            movement = new Movement(position);
            hitbox = new Hitbox(new Rectangle(9, 17, myHeroWidth - 9 * 2, myHeroHeight - 17), Vector2.Zero);
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\MyHero\MyHero"), new Rectangle(0, 0, myHeroWidth, myHeroHeight), Vector2.Zero, position);

            physics = new Physics() { affectedByGravity = true, dragScale = 0.05f, fallingGravityScale = 1.5f };

            //frames
            idleAnimation = new Animation(sprite.texture, 0.1f, false, new int[] { 0 }, sprite.sourceRect.Size);
            runAnimation = new Animation(sprite.texture, 0.1f, true, new int[] { 0, 1, 2, 3 }, sprite.sourceRect.Size);
            jumpAnimation = new Animation(sprite.texture, 0.1f, false, new int[] { 3 }, sprite.sourceRect.Size);
            deathAnimation = new Animation(sprite.texture, 0.1f, false, new int[] { 4 }, sprite.sourceRect.Size);

            //myhero live
            heartSprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\MyHero\hart"), new Rectangle(0, 0, heartWidth, heartHeight), Vector2.Zero, new Vector2(3, 3));
            lives = startingLives;
            immune = false;
            immunityTimer = 0;

            flickerTimer = 0;
            deathTimer = 0;


            //1
            animation = idleAnimation;
            prevVelocity = physics.velocity;




        }
        public void hurtMyHero()
        {
            if (immune)
                return;

            lives = System.Math.Clamp(lives - 1, 0, lives);
            immune = true;
        }

        public void Update(float deltaTime)
        {
            // Immunity frames
            if (immune)
                immunityTimer += deltaTime;


            // Finished immunity frames
            if (immunityTimer > immunityDuration)
            {
                immune = false;
                immunityTimer = 0;
            }

            // dood
            if (lives <= 0)
            {
                deathTimer += deltaTime;
                if (deathTimer >= dieAfter)
                    gameOver = true;
            }

            // valen = dood
            if (movement.position.Y >= Game1.designedResolutionHeight)
                lives = 0;
                // bool isMoving = false; 
                Console.WriteLine("MyHero Update");

            movement.Update(deltaTime);
            physics.Update(deltaTime);

            hitbox.Update(movement.position);
            currentAnimation = idleAnimation;


            if (lives == 0)
            {

                if (animation != deathAnimation)
                {
                    animation = deathAnimation;
                   
                }

                movement.deltaY += physics.desiredVelocity.Y * deltaTime;

                return;
            }
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
            // Set the animation only if it's changed
            if (animation != currentAnimation)
            {
                animation = currentAnimation;
                animation.Reset();
            }
            //hero jump
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Console.WriteLine("Jumping");
                if (canJump)
                {
                    currentAnimation = jumpAnimation;
                    physics.desiredVelocity.Y -= jumpForce;
                    canJump = true;
                }
            }
           

            physics.desiredVelocity.X = System.Math.Clamp(physics.desiredVelocity.X, -maxSpeed, maxSpeed);

            // effect of velocity on position 
            movement.deltaX += physics.desiredVelocity.X * deltaTime;
            movement.deltaY += physics.desiredVelocity.Y * deltaTime;


            //jumping or falling
            if(physics.velocity.Y != 0 || prevVelocity.Y != physics.velocity.Y)
            {
               currentAnimation = jumpAnimation;
                canJump = false;
            }
            else if (physics.velocity == Vector2.Zero)
            {
                currentAnimation = idleAnimation;
                canJump = true;
            }
            else
            {
                currentAnimation = runAnimation;
                canJump = true;
            }

            animation.Update(deltaTime);
            prevVelocity = physics.velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (flickerTimer == flickerSpeed)
            {
                sprite.sourceRect = animation.GetCurrentFrame();
                sprite.Draw(spriteBatch, movement.position);
                flickerTimer = 0;
            }

            if (immunityTimer == 0)
                flickerTimer = flickerSpeed;
            else
                flickerTimer++;

            for (int i = 0; i < lives; i++)
                heartSprite.Draw(spriteBatch, heartSprite.position - GameHandler.cameraPosition + (heartWidth + 3) * Vector2.UnitX * i);

        }
    }
}