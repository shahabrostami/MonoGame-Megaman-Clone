using Game1.enemy;
using Game1.player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Game1
{
    class CollisionHandler
    {
        public static void checkAllCollisions(Map map, Player player, EnemyFactory enemyFactory)
        {
            if (checkPlayerEnemyCollisions((int)player.position.X, (int)player.position.Y))
                player.addEvent(new PlayerEventDamaged(player));

        }

        public static bool checkPlayerEnemyCollisions(int x, int y)
        {
            bool checkCollision = false;
            if (checkEnemyCollision(x, y, false))
                checkCollision = true;
            return checkCollision;
        }

        public static bool checkBulletCollisions(int x, int y)
        {
            bool checkCollision = false;
            if (checkMapCollision(x, y))
                checkCollision = true;

            if (checkEnemyCollision(x, y, true))
                checkCollision = true;

            return checkCollision;

        }
        private static bool checkMapCollision(int x, int y)
        {
            return Map.checkCollision(x, y);
        }

        private static bool checkEnemyCollision(int x, int y, bool canDamage)
        {
            return EnemyFactory.checkCollision(x, y, canDamage);
        }
    }
}
