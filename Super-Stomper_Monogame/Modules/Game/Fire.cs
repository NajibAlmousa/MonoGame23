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

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class Fire : IEnemy
    {
        private Sprite sprite;
        private Animation animation;
        private Movement movement;

        private const int fireWidth = 32;
        private const int fireHeight = 32;
        public Fire(ContentManager content, Vector2 position)
        {
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\Enemies\AllEnemies"), new Rectangle(7 * fireWidth, 0, fireWidth, fireHeight), Vector2.Zero, position);
            animation = new Animation(sprite.texture, 0.25f, true, new int[] { 6, 7, 8, 9, 10, 11, 12, 13 }, sprite.sourceRect.Size);
            movement = new Movement(position);
        }


        public void Update(float deltaTime, Vector2 myHeroPosition)
        {
            movement.Update(deltaTime);
            animation.Update(deltaTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.sourceRect = animation.GetCurrentFrame();
            sprite.Draw(spriteBatch, movement.position);
        }
    }
}
