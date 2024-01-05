using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Super_Stomper_Monogame.Modules.BaseClasses;


namespace Super_Stomper_Monogame.Modules.Game
{
    internal class Martian : IEnemy
    {
       
        public Hitbox hitbox;
        public Physics physics;
        public Movement movement;
        private Sprite sprite;
        private Animation animation;

        public int direction;

        public bool stompedOn;
        public bool shouldBeRemoved;

        private float deadSince;


        public const int stompRepulsionForce = 150;

        private const int martianWidth = 32;
        private const int martianHeight = 32;
        private const int speed = 30;
        private const float vanishTimeAfterStompedOn = 0.6f;
        public Martian(ContentManager content, Vector2 position)
        {
            direction = -1;
            stompedOn = false;
            shouldBeRemoved = false;
            deadSince = 0;

            movement = new Movement(position);
            physics = new Physics() { dragScale = 0, velocity = direction * speed * Vector2.UnitX };
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\Enemies\AllEnemies"), new Rectangle(0, 0, martianWidth, martianHeight), Vector2.Zero, position);
            animation = new Animation(sprite.texture, 0.25f, true, new int[] { 0,1 }, sprite.sourceRect.Size);
            hitbox = new Hitbox(new Rectangle(7, 15, martianWidth - 7 * 2, martianHeight - 15), Vector2.Zero);

        }


        public void Update(float deltaTime, Vector2 myHeroPosition)
        {
            movement.Update(deltaTime);
            physics.Update(deltaTime);
            animation.Update(deltaTime);
            hitbox.Update(movement.position);

            if (!stompedOn)
                physics.desiredVelocity.X = speed * direction;
            else
                deadSince += deltaTime;

            if (deadSince > vanishTimeAfterStompedOn)
                shouldBeRemoved = true;

            movement.deltaX += physics.desiredVelocity.X * deltaTime;
            movement.deltaY += physics.desiredVelocity.Y * deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.sourceRect = stompedOn ? new Rectangle(2 * martianWidth, 0, martianWidth, martianHeight) : animation.GetCurrentFrame();
            sprite.Draw(spriteBatch, movement.position);
        }


    }
}
