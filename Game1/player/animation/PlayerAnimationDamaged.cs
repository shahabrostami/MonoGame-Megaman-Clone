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
    class PlayerAnimationHurt : BasePlayerAnimation
    {
        bool initialDirection = false;

        public PlayerAnimationHurt(Player player, SpriteSpec spriteSpec, AnimationSpec animation) :
            base(player, spriteSpec, animation)
        {
        }

        override public bool Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > currentCycle.ms)
            {
                timeSinceLastFrame -= currentCycle.ms;
                currentFrameIndex++;

                if (currentFrameIndex > currentCycle.ef)
                {
                    loopFinished = true;
                    return false;
                }
            }

            player.updateLocation(currentCycle.velocity * (timeSinceLastFrame / 1000));
            return true;
        }
        
        public override void updateOnAction(PlayerStateAnimation pState, Action pAction)
        {
            if (!initialDirection)
            {
                direction = player.direction;
                if (direction == Direction.RIGHT)
                    updateCycle(cycles[0]);
                else if (direction == Direction.LEFT)
                    updateCycle(cycles[1]);
                initialDirection = true;
            }
        }

        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {
            initialDirection = false;
            velocity = currentCycle.velocity;
            currentFrameIndex = 0;
            loopFinished = false;
        }
    }
}
