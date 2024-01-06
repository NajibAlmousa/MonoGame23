using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Super_Stomper_Monogame.Modules.BaseClasses.Animations;
using Super_Stomper_Monogame.Modules.BaseClasses.GameHandlers;
using Super_Stomper_Monogame.Modules.BaseClasses.Interfaces;
using Super_Stomper_Monogame.Modules.BaseClasses.Sprites;

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class Ghost : IEnemy
    {
        public GhostFireBall fireBall;
        private ContentManager content;
        private Sprite sprite;
        private Animation animation;
        private Physics physics;
        private Movement movement;

        private const int ghostWidth = 32;
        private const int ghostHeight = 32;
        private const int ghostSpeed = 96;

        private float timer;

        private const float throwBallEvery = 3;
        public Ghost(ContentManager content, Vector2 position)
        {
            this.content = content;
            movement = new Movement(position);
            physics = new Physics();

            this.fireBall = null;

            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets/Enemies/Ghost"), new Rectangle(0, 0, ghostWidth, ghostHeight), Vector2.Zero);
            animation = new Animation(sprite.texture, 0.4f, true, new int[] { 0, 1,2,3 }, sprite.sourceRect.Size);
        }


        public void Update(float deltaTime, Vector2 myHeroPosition)
        {
            if (timer <= 0)
            {
                timer = throwBallEvery;
                animation.Reset(true);
            }

            if (timer == throwBallEvery)
            {
                fireBall = new GhostFireBall(content, movement.position);
                animation.Reset();
            }
            fireBall?.Update(deltaTime);
            timer -= deltaTime;

            physics.velocity.X = -System.Math.Sign(movement.position.X - myHeroPosition.X) * ghostSpeed;
            physics.Update(deltaTime);
            movement.deltaX += physics.velocity.X * deltaTime;

            movement.Update(deltaTime);
            animation.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (!animation.finished)
                sprite.sourceRect.X = animation.GetCurrentFrame().X;
            else
                sprite.sourceRect.X = 16 * ghostWidth;

            sprite.Draw(spriteBatch, movement.position);

            fireBall?.Draw(spriteBatch);
        }
    }
}
