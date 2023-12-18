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
        // springen
        public Vector2 velocity;
        public Vector2 desiredVelocity;
        public float dragScale;
        public bool affectedByGravity;
        public float fallingGravityScale;
        public float risingGravityScale;

        private const float gravityConstant = 1;
        public Physics()
        {
            velocity = Vector2.Zero;
            desiredVelocity = Vector2.Zero;
            affectedByGravity = true;
            fallingGravityScale = 1;
            risingGravityScale = 1;
            dragScale = 1;

        }

        public void Update(float deltaTime)
        {
            
            desiredVelocity.X -= desiredVelocity.X * dragScale;
            //desiredVelocity.Y += gravityConstant * (desiredVelocity.Y >= 0 ? fallingGravityScale : risingGravityScale);
            desiredVelocity.X = desiredVelocity.X >= 0 ? (float)System.Math.Floor(desiredVelocity.X) : (float)System.Math.Ceiling(desiredVelocity.X);
        }


        public int Collision(Movement movement, Hitbox myHitbox, Hitbox otherHitbox, bool justDetection = false)
        {
            // collision type 
            // 0: no collision, 1: horizontal, 2: vertical, 3: horizontal & vertical
            int collisionType = 0;
            float originalDeltaX = movement.deltaX;
            float originalDeltaY = movement.deltaY;
            Rectangle originalRect = myHitbox.rectangle;
            myHitbox.rectangle.Offset(
                movement.deltaX < 0 ? (int)System.Math.Floor(movement.deltaX) : (int)System.Math.Ceiling(movement.deltaX),
                movement.deltaY < 0 ? (int)System.Math.Floor(movement.deltaY) : (int)System.Math.Ceiling(movement.deltaY)
            );
            Rectangle offsetedRectangle = myHitbox.rectangle;
            return collisionType;
           
        }

    }
}
