using Microsoft.Xna.Framework.Content;
using Super_Stomper_Monogame.Modules.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Super_Stomper_Monogame.Modules.Game
{
    internal class GhostFireBall
    {
        private Sprite sprite;
        private Animation animation;

        private const int ballSize = 32;


        public GhostFireBall(ContentManager content, Vector2 position)
        {
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheet/Enemies\ghost-FireBall"), new Rectangle(17, 0, ballSize, ballSize), Vector2.Zero);

            animation = new Animation(sprite.texture, 0.2f, true, new int[] { 2, 3, 4, 5 }, new Point(ballSize));
        }


        public void Update(float deltaTime)
        {
            animation.Update(deltaTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.sourceRect = animation.GetCurrentFrame();
        }

    }
}
