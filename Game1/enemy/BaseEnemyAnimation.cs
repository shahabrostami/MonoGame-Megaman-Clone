using Game1.enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class BaseEnemyAnimation : BaseAnimation
    {
        protected Enemy enemy;

        public BaseEnemyAnimation(Enemy enemy, SpriteSpec spriteSpec, AnimationSpec animation) :
            base(spriteSpec, animation)
        {
            this.enemy = enemy;
        }

        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {
            
        }

        public override bool Update(GameTime gameTime)
        {
            
        }
    }


}
