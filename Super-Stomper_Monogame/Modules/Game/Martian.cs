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
       
        private Sprite sprite;
        private Animation animation;


        private const int martianWidth = 32;
        private const int martianHeight = 32;
        
        public Martian(ContentManager content, Vector2 position)
        {

           
            sprite = new Sprite(content.Load<Texture2D>(@"Spritesheets\Enemies\Enemy1"), new Rectangle(0, 0, martianWidth, martianHeight), Vector2.Zero, position);
            animation = new Animation(sprite.texture, 0.25f, true, new int[] { 0, 1 }, sprite.sourceRect.Size);

        }
       

        public void Update(float deltaTime, Vector2 myHeroPosition)
        {


            // Update components
        
            animation.Update(deltaTime);
            
          
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }


    }
}
