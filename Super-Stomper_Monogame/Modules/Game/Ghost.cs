using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Super_Stomper_Monogame.Modules.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class Ghost : IEnemy
    {
        private ContentManager content;
        private Sprite sprite;
        private Animation animation;
        private Physics physics;
        private Movement movement;


        private const int ghostWidth = 32;
        private const int ghostHeight = 32;
        private const int ghostSpeed = 85;
        public Ghost(ContentManager content, Vector2 position)
        {

            this.content = content;
            movement = new Movement(position);
            physics = new Physics();

            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets/Enemies/Ghost"), new Rectangle(0, 0, ghostWidth, ghostHeight), Vector2.Zero);
            animation = new Animation(sprite.texture, 0.5f, true, new int[] { 0, 1, 2, 3, 5 }, sprite.sourceRect.Size);

        }


        public void Update(float deltaTime, Vector2 myHeroPosition)
        {

            physics.velocity.X = -System.Math.Sign(movement.position.X - myHeroPosition.X) * ghostSpeed;
            physics.Update(deltaTime);
            movement.deltaX += physics.velocity.X * deltaTime;

            movement.Update(deltaTime);
            animation.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, movement.position);
        }
    }
}
