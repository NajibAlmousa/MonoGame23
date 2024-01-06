using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Super_Stomper_Monogame.Modules.BaseClasses.Animations;
using Super_Stomper_Monogame.Modules.BaseClasses.Sprites;
using Super_Stomper_Monogame.Modules.BaseClasses.GameHandlers;
namespace Super_Stomper_Monogame.Modules.Game
{
    internal class GhostFireBall
    {
        private Sprite sprite;
        private Animation animation;
        private Movement movement;
        private Physics physics;
        public Hitbox hitbox { private set; get; }


        private const int ballSize = 32;


        public GhostFireBall(ContentManager content, Vector2 position)
        {
           
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets/Enemies\fireBall"), new Rectangle(5, 0, ballSize, ballSize), Vector2.Zero);
            animation = new Animation(sprite.texture, 0.25f, true, new int[] { 0,1,3 }, new Point(ballSize));
            physics = new Physics() { affectedByGravity = true };
            hitbox = new Hitbox(new Rectangle(8, 14, ballSize - 8 * 2, ballSize - 14), Vector2.Zero);

            movement = new Movement(position);
        }
        public void Update(float deltaTime)
        {
            animation.Update(deltaTime);
            movement.Update(deltaTime);
            physics.Update(deltaTime);
            movement.deltaY += physics.desiredVelocity.Y * deltaTime;
            hitbox.Update(movement.position);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.sourceRect = animation.GetCurrentFrame();
            sprite.Draw(spriteBatch, movement.position);
        }

    }
}
