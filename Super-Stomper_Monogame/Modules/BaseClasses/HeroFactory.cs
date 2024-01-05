using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Super_Stomper_Monogame.Modules.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Stomper_Monogame.Modules.BaseClasses
{
    internal class HeroFactory
    {
        public MyHero createHero(ContentManager content, Vector2 position)
        {
            return new MyHero(content, position);

        }
    }
}
