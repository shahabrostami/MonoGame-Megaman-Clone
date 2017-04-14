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
    class PlayerAnimationBlink : BasePlayerAnimation
    {

        public PlayerAnimationBlink(Player player, SpriteSpec spriteSpec, AnimationSpec animation) :
            base(player, spriteSpec, animation)
        {
            cycles[1].frames = cycles[0].frames;
            cycles[1].ef = cycles[0].ef;
            cycles[1].sf = cycles[0].sf;
            cycles[1].velocity = cycles[0].velocity;
        }

        override public bool Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            
            if (currentFrame.ms > 0 && timeSinceLastFrame > currentFrame.ms)
            {
                timeSinceLastFrame -= currentFrame.ms;

                currentFrameIndex++;

                if (currentFrameIndex == currentCycle.ef + 1)
                    currentFrameIndex = currentCycle.sf;

                currentFrame = currentCycle.frames[currentFrameIndex];
                return true;
            }
            return false;
        }
                
        public override void updateOnAction(PlayerState pState, PlayerAction pAction)
        {
            if (player.direction == Direction.RIGHT)
            {
                direction = Direction.RIGHT;
                if (player.shooting)
                {
                    currentCycle = cycles[2];
                    currentFrame = currentCycle.frames[0];
                }
                else
                    currentCycle = cycles[0];
            }
            else if (player.direction == Direction.LEFT)
            {
                direction = Direction.RIGHT;
                if (player.shooting)
                {
                    currentCycle = cycles[3];
                    currentFrame = currentCycle.frames[0];
                }
                else
                    currentCycle = cycles[1];
            }
        }

        public override bool hasMovement()
        {
            return false;
        }

        public override void reset()
        {

        }
    }
}
