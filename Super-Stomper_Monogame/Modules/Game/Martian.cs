using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Super_Stomper_Monogame.Modules.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class Martian : IEnemy
    {
        public int direction;
        public Hitbox hitbox;
        public Physics physics;
        public Movement movement;

        private Sprite sprite;
        private Animation animation;


        private const int martianWidth = 32;
        private const int martianHeight = 32;
        private const int speed = 30;
        public Martian(ContentManager content, Vector2 position)
        {



            movement = new Movement(position);
            physics = new Physics() { dragScale = 0, velocity = direction * speed * Vector2.UnitX };
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\Enemy\Enemy1"), new Rectangle(0, 0, martianWidth, martianHeight), Vector2.Zero, position);
            animation = new Animation(sprite.texture, 0.25f, true, new int[] { 0, 1 }, sprite.sourceRect.Size);

        }
       

        public void Update(float deltaTime, Vector2 myHeroPosition)
        {


            // Update components
            movement.Update(deltaTime);
            physics.Update(deltaTime);
            animation.Update(deltaTime);
            
            // effect of velocity on position
            movement.deltaX += physics.desiredVelocity.X * deltaTime;
            movement.deltaY += physics.desiredVelocity.Y * deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.sourceRect = new Rectangle(2 * martianWidth, 0, martianWidth, martianHeight);
            sprite.Draw(spriteBatch, movement.position);
        }


    }
}
