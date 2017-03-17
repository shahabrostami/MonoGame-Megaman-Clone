using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    class PlayerAnimationJump : BasePlayerAnimation
    {
        private int animationCycleLength = 4;
        private int animationCycleIndex = 0;
        private AnimationCycle[] currentCycle;
        private AnimationCycle[] animationCycle;
        private AnimationCycle[] animationCycleNone;
        private Vector2 noChange = new Vector2(0, 0);

        public PlayerAnimationJump(SpriteSpec spriteSpec, bool loopAnimation, int msPerFrame, SpriteLocation rightSprite, SpriteLocation leftSprite) :
            base(spriteSpec, loopAnimation, msPerFrame, rightSprite, leftSprite)
        {   
            animationCycle = new AnimationCycle[animationCycleLength];
            animationCycle[0] = new AnimationCycle(3, -8, msPerFrame);
            animationCycle[1] = new AnimationCycle(3, -3, msPerFrame);
            animationCycle[2] = new AnimationCycle(3,  3, msPerFrame);
            animationCycle[3] = new AnimationCycle(3,  8, msPerFrame);
            animationCycleNone = new AnimationCycle[animationCycleLength];
            animationCycleNone[0] = new AnimationCycle(0, -8, msPerFrame);
            animationCycleNone[1] = new AnimationCycle(0, -3, msPerFrame);
            animationCycleNone[2] = new AnimationCycle(0, 3, msPerFrame);
            animationCycleNone[3] = new AnimationCycle(0, 8, msPerFrame);


            loopFinished = false;
        }

        override public void Update(GameTime gameTime)
        {
            if (animationCycleIndex < animationCycleLength)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > msPerFrame)
                {
                    timeSinceLastFrame -= msPerFrame;
                    animationCycleIndex++;
                }
            }
            else if (animationCycleIndex == animationCycleLength)
            {
                timeSinceLastFrame = 0;
                animationCycleIndex = 0;
                loopFinished = true;
            }
        }

        public override void updateAnimationCycle(PlayerState state)
        {
            if (state.action == PlayerAction.STOP)
                currentCycle = animationCycleNone;
            else
                currentCycle = animationCycle;
        }
        public override Vector2 updateLocation(PlayerState pState)
        {
            if (animationCycleIndex == animationCycleLength)
                return noChange;
            if (direction == Direction.RIGHT)
                return currentCycle[animationCycleIndex].getRightDisplacement();
            else
                return currentCycle[animationCycleIndex].getLeftDisplacement();
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
