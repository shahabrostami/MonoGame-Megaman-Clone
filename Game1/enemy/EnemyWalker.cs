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
            this.textureSize = new Vector2(24, 24);
        }
    }
}
