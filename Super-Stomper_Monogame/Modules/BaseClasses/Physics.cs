using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Super_Stomper_Monogame.Modules.BaseClasses
{
    internal class Physics
    {
        public Vector2 velocity;
        public Vector2 desiredVelocity;
        public float dragScale;

        public Physics()
        {
            velocity = Vector2.Zero;
            desiredVelocity = Vector2.Zero;
        }

        public void Update(float deltaTime)
        {
            
            desiredVelocity.X -= desiredVelocity.X * dragScale;
           
        }

    }
}
