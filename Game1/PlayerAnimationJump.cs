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
        private Vector2 noChange = new Vector2(0, 0);
        public PlayerAnimationJump(SpriteSpec spriteSpec, AnimationSpec animation) :
            base(spriteSpec, animation)
        {
        }

        override public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > currentFrame.ms)
            {
                timeSinceLastFrame -= currentFrame.ms;

                currentFrameIndex++;
                if (currentFrameIndex == currentCycle.ef)
                    currentFrameIndex = currentCycle.sf;

                currentFrame = currentCycle.frames[currentFrameIndex];
            }
        }

        /*
        public override void updateAnimationCycle(PlayerState state, PlayerAction pAction)
        {
            if (state.action == PlayerAction.STOP || pAction == PlayerAction.STOP)
                currentCycle = animationCycleNone;
            else
                currentCycle = animationCycle;
        }
        */
        

        public override Vector2 updateLocation(PlayerState pState)
        {
            /*
            if (animationCycleIndex == animationCycleLength)
                return noChange;
            if (direction == Direction.RIGHT)
                return currentCycle[animationCycleIndex].getRightDisplacement();
            else
                return currentCycle[animationCycleIndex].getLeftDisplacement();
                */
            return noChange;
        }

        public override void updateDirection(Direction direction)
        {
        }

        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {
            Console.WriteLine("RESET");
            loopFinished = false;
        }
    }
}
