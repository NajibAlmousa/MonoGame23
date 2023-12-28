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

        private const int ghostWidth = 32;
        private const int ghostHeight = 32;
        public Ghost(ContentManager content, Vector2 position)
        {
            this.content = content;
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets/Enemies/Ghost"), new Rectangle(0, 0, ghostWidth, ghostHeight), Vector2.Zero);
        }
       

        public void Update(float deltaTime, Vector2 myHeroPosition)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
