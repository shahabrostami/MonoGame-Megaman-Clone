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
    class PlayerAnimationRun : BasePlayerAnimation
    {

        public PlayerAnimationRun(SpriteSpec spriteSpec, AnimationSpec animation) :
            base(spriteSpec, animation)
        {
            cycles[1].frames = cycles[0].frames;
        }

        override public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > currentFrame.ms)
            {
                timeSinceLastFrame -= currentFrame.ms;

                currentFrameIndex++;

                if (currentFrameIndex == currentCycle.ef + 1)
                    currentFrameIndex = currentCycle.sf;

                currentFrame = currentCycle.frames[currentCycle.ef % currentFrameIndex];
            }
        }

        public override void updateDirection(Direction direction)
        {

            this.direction = direction;
            if (direction == Direction.RIGHT)
                currentCycle = cycles[0];
            else
                currentCycle = cycles[1];
        }

        public override Vector2 updateLocation(PlayerState pState)
        {
            return currentCycle.dis;
        }

        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {

        }
    }
}
