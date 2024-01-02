using Microsoft.Xna.Framework.Content;
using Super_Stomper_Monogame.Modules.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
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
            movement = new Movement(position);
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets/Enemies\fireBall"), new Rectangle(5, 0, ballSize, ballSize), Vector2.Zero);

            animation = new Animation(sprite.texture, 0.25f, true, new int[] { 0,1,3 }, new Point(ballSize));
            physics = new Physics() { affectedByGravity = true };
            hitbox = new Hitbox(new Rectangle(8, 14, ballSize - 8 * 2, ballSize - 14), Vector2.Zero);

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
            Console.WriteLine("Draw ball");
            sprite.sourceRect = animation.GetCurrentFrame();
            sprite.Draw(spriteBatch, movement.position);
        }

    }
}
