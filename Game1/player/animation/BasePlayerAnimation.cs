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
    abstract class BasePlayerAnimation : BaseAnimation
    {
        protected Player player;

        public BasePlayerAnimation(Player player, SpriteSpec spriteSpec, AnimationSpec animation) :
            base(spriteSpec, animation)
        {
            this.player = player;
        }

        public abstract void updateOnAction(PlayerStateAnimation pState, PlayerAction pAction);
    }


}
