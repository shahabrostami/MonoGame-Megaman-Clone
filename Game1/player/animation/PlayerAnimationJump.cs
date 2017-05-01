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
        public PlayerAnimationJump(Player player, SpriteSpec spriteSpec, AnimationSpec animationSpec) :
            base(player, spriteSpec, animationSpec)
        {
        }

        override public bool Update(GameTime gameTime)
        {
            if (loopFinished)
                return false;
            
            timeSinceLastFrame = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (player.updateLocation(velocity * (timeSinceLastFrame * 10)))
                velocity += (gravity * (timeSinceLastFrame * 10));
            else
                velocity.Y = 0;

            if (!player.jumping)
            {
                velocity = currentCycle.velocity;
                loopFinished = true;
            }
            return true;
        }
        
        public override void updateOnAction(PlayerStateAnimation pState, PlayerAction pAction)
        {
            direction = player.direction;
            if (direction == Direction.RIGHT)
            {
                if (player.jumping)
                    updateCycle(cycles[2]);
                else updateCycle(cycles[0]);
            }
            else if (direction == Direction.LEFT)
            {
                if (player.shooting)
                    updateCycle(cycles[3]);
                else updateCycle(cycles[1]);
            }

            if (pAction == PlayerAction.STOP)
                velocity.X = 0;
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
