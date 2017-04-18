using Microsoft.Xna.Framework.Graphics;
using MyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.enemy
{
    class EnemyTypes
    {
        private static EnemyFactory enemyFactory;
        public static EnemyAnimation WALKER = new EnemyAnimation(1, "WALKER");

        public static void LoadContent(EnemyFactory enemyFactory, Texture2D texture, Sprite sprite)
        {
            SpriteSpec spriteSpec = new SpriteSpec(texture, sprite.rows, sprite.columns);
            EnemyTypes.enemyFactory = enemyFactory;

            Enemy enemy = new Enemy();
            BaseEnemyAnimation walkerAnimation = new BaseEnemyAnimation(enemy, spriteSpec, sprite.animations[0]);

            WALKER.animation = walkerAnimation;
        }
    }
}
