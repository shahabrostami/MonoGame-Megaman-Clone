using Game1.enemy;
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

        public static bool checkCollisions(int x, int y)
        {
            bool checkCollision = false;
            if (checkMapCollisions(x, y))
                checkCollision = true;

            if (checkEnemyCollisions(x, y))
                checkCollision = true;

            return checkCollision;

        }
        private static bool checkMapCollisions(int x, int y)
        {
            return Map.checkCollision(x, y);
        }

        private static bool checkEnemyCollisions(int x, int y)
        {
            return EnemyFactory.checkCollision(x, y);
        }
    }
}
