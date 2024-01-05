using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Super_Stomper_Monogame.Modules.Game;


namespace Super_Stomper_Monogame.Modules.BaseClasses
{
    internal class HeroFactory
    {
        public static MyHero createHero(ContentManager content, Vector2 position)
        {
            return new MyHero(content, position);

        }
    }
}
