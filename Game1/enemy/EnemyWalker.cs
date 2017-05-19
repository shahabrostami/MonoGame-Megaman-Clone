using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.enemy
{
    class EnemyWalker : Enemy
    {

        public EnemyWalker() : 
            base()
        {
        }

        public new void Update(GameTime gameTime)
        {
            enemyBound = new Rectangle(position.ToPoint(), animation.textureSize.ToPoint());
            animation.Update(gameTime);
        }
    }
}
