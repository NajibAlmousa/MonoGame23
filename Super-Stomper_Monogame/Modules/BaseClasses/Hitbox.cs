using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Stomper_Monogame.Modules.BaseClasses
{
    internal class Hitbox
    {
        public Rectangle rectangle;
        public Vector2 origin;

        private Rectangle originalRect;
        public Hitbox(Rectangle bounds, Vector2 origin)
        {
            this.origin = origin;
            rectangle = bounds;
            originalRect = rectangle;
        }

        public void Update(Vector2 position)
        {
            //hier moet ik updaten wanneer de hero move moet de hitbox meebewgen
        }


        
         public bool IsTouching(Hitbox hitbox)
        {
            return rectangle.Intersects(hitbox.rectangle);
        }
    }
}
