using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Stomper_Monogame.Modules.BaseClasses.Interfaces
{
    internal interface IEnemy
    {
        public void Update(float deltaTime, Vector2 myHeroPosition);
        public void Draw(SpriteBatch spriteBatch);
    }
}
