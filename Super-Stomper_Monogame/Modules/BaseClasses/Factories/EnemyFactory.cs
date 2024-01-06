using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Super_Stomper_Monogame.Modules.BaseClasses.Enums;
using Super_Stomper_Monogame.Modules.BaseClasses.Interfaces;
using Super_Stomper_Monogame.Modules.Game;
using Super_Stomper_Monogame.Modules.Game.Enemies;


namespace Super_Stomper_Monogame.Modules.BaseClasses.Factories
{
    internal class EnemyFactory
    {
        public static IEnemy CreateEnemy(ContentManager content, Vector2 position, EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Martian:
                    return new Martian(content, position);
                case EnemyType.Ghost:
                    return new Ghost(content, position);
                case EnemyType.Fire:
                    return new Fire(content, position);
            }
            return null;
        }
    }
}
