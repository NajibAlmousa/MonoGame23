using Microsoft.Xna.Framework;


namespace Super_Stomper_Monogame.Modules.BaseClasses.GameHandlers
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
            rectangle = new Rectangle((position + (originalRect.Location.ToVector2() - origin)).ToPoint(), originalRect.Size.ToVector2().ToPoint());
        }

        public bool IsTouching(Hitbox hitbox)
        {
            return rectangle.Intersects(hitbox.rectangle);
        }
    }
}
