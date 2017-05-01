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

        public BaseEnemyAnimation(SpriteSpec spriteSpec, AnimationSpec[] animationSpecs) :
            base(spriteSpec, animationSpecs)
        {
        }

        public void setEnemy(Enemy enemy)
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
            return true;
        }
    }


}
