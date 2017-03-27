using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyObjects;

namespace Game1
{
    class PlayerAnimationJump : BasePlayerAnimation
    {
        public PlayerAnimationJump(Player player, SpriteSpec spriteSpec, AnimationSpec animation) :
            base(player, spriteSpec, animation)
        {

        }

        override public bool Update(GameTime gameTime)
        {
            if (loopFinished)
                return false;
            
            timeSinceLastFrame = (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity += (gravity * (timeSinceLastFrame*10));
            player.updateLocation(velocity * (timeSinceLastFrame*10));

            if (player.location.Y >= 426)
            {
                player.location.Y = 426;
                velocity = currentCycle.velocity;
                loopFinished = true;
            }
            return true;
        }
        
        public override void updateOnAction(PlayerState pState, PlayerAction pAction)
        {
            if (pState.action == PlayerAction.STOP || pAction == PlayerAction.STOP)
                velocity.X = 0;
            else if (direction == Direction.RIGHT && currentCycle != cycles[0])
            {
                currentCycle = cycles[0];
                velocity.X *= -1;
            }
            else if (direction == Direction.LEFT && currentCycle != cycles[1])
            {
                currentCycle = cycles[1];
                velocity.X *= -1;
            }
            else
                velocity.X = currentCycle.velocity.X;
        }

        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {
            velocity = currentCycle.velocity;
            loopFinished = false;
        }
    }
}
