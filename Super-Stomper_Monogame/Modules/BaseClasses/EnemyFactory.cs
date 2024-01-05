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
    internal class EnemyFactory
    {
        public static IEnemy CreateEnemy(ContentManager content, Vector2 position, EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Martian:
                return new Martian(content, position);
                    break;
            case EnemyType.Ghost:
                return new Ghost(content, position);
                    break;
            case EnemyType.Fire:
                return new Fire(content, position);
                    break;
        }
            return null;
        }
    }
}
