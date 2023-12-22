using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Stomper_Monogame.Modules.BaseClasses
{
    internal interface IEnemy
    {
        //Update logic for the enemy
        public void Update(float deltaTime, Vector2 myHeroPosition);

        // Draw logic for the enemy
        public void Draw(SpriteBatch spriteBatch);
    }
}
