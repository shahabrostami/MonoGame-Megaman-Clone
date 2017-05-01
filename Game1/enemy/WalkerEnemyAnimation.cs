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
    class WalkerEnemyAnimation : BaseEnemyAnimation
    {
        private int ms;

        public WalkerEnemyAnimation(SpriteSpec spriteSpec, AnimationSpec animationSpec) :
            base(spriteSpec, animationSpec)
        {
            ms = animationSpec.cycles[0].ms;
        }

        override public bool Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (ms > 0 && timeSinceLastFrame > ms)
            {
                timeSinceLastFrame -= ms;

                currentFrameIndex++;

                if (currentFrameIndex == currentCycle.ef + 1)
                    currentFrameIndex = currentCycle.sf;

                return true;
            }
            return false;
        }
    }
}
