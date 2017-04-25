using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.enemy
{
    class EnemyFactory
    {
        public EnemyFactory()
        {
        }

        public void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            Sprite enemySprite = Content.Load<Sprite[]>("spritetest")[1];
            Texture2D enemyTexture = Content.Load<Texture2D>(enemySprite.textureName);

            SpriteSpec spriteSpec = new SpriteSpec(enemyTexture, enemySprite.rows, enemySprite.columns);

            EnemyWalker enemyWalker1 = new EnemyWalker();
            WalkerEnemyAnimation walkerAnimation = new WalkerEnemyAnimation(enemyWalker1, spriteSpec, enemySprite.animations[0]);
        }
    }
}
