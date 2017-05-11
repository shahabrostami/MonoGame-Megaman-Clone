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
    class PlayerAnimationFall : BasePlayerAnimation
    {
        public PlayerAnimationFall(Player player, SpriteSpec spriteSpec, AnimationSpec animation) :
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
                }

                currentFrame = currentCycle.frames[currentCycle.ef % currentFrameIndex];
            }

            player.updateLocation(currentCycle.velocity * (timeSinceLastFrame / 1000));
            return true;
        }
        
        public override void updateOnAction(PlayerStateAnimation pState, PlayerAction pAction)
        {
           
        }

        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {
            velocity = currentCycle.velocity / 4;
            velocity.Y *= -1;
            loopFinished = false;
        }
    }
}
