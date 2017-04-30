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

        public WalkerEnemyAnimation(SpriteSpec spriteSpec, AnimationSpec animationSpec) :
            base(spriteSpec, animationSpec)
        {
        }
    }
}
