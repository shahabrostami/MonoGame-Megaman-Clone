using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Game1.enemy
{
    class EnemyFactory
    {
        static List<EnemyWalker> enemyWalkers;

        public EnemyFactory()
        {
            enemyWalkers = new List<EnemyWalker>
                    {
                        new EnemyWalker(){},
                        new EnemyWalker(){},
                        new EnemyWalker(){},
                        new EnemyWalker(){},
                        new EnemyWalker(){}
                    };
        }

        internal static bool checkCollision(int x, int y)
        {
            bool hasHit = false;
            foreach (EnemyWalker enemyWalker in enemyWalkers)
            {
                if (enemyWalker.checkCollision(x, y))
                    hasHit = true;
            }

            return hasHit;
        }

        internal static void setEnemyMapping(TmxList<TmxObject> objects, int heightDiff)
        {
            // foreach (TmxObject tmxObj in objects)
            enemyWalkers[0].position = new Vector2((int)objects[0].X, (int)objects[0].Y - heightDiff);
        }

        public void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            Sprite enemySprite = Content.Load<Sprite[]>("spritetest")[1];
            Texture2D enemyTexture = Content.Load<Texture2D>(enemySprite.textureName);

            SpriteSpec spriteSpec = new SpriteSpec(enemyTexture, enemySprite.rows, enemySprite.columns);
            
            WalkerEnemyAnimation walkerAnimation = new WalkerEnemyAnimation(spriteSpec, enemySprite.animations);
            foreach (EnemyWalker enemyWalker in enemyWalkers)
                enemyWalker.setAnimation(walkerAnimation);
        }

        public void Update(GameTime gameTime)
        {
            List<Enemy> deadEnemies = new List<Enemy>();
            foreach (EnemyWalker enemyWalker in enemyWalkers)
            {
                enemyWalker.Update(gameTime);
                if (!enemyWalker.isEnemyAlive())
                    deadEnemies.Add(enemyWalker);
            }

            foreach (EnemyWalker deadEnemy in deadEnemies)
                enemyWalkers.Remove(deadEnemy);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // foreach (EnemyWalker enemyWalker in enemyWalkers)
            enemyWalkers[0].Draw(spriteBatch);
        }
    }
}
