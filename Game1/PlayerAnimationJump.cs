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
        private int animationCycleLength = 2;
        private int animationCycleIndex = 0;
        private Vector2[] animationCycle;
        private Vector2 noChange = new Vector2(0, 0);

        public PlayerAnimationJump(SpriteSpec spriteSpec, bool loopAnimation, int msPerFrame, SpriteLocation rightSprite, SpriteLocation leftSprite) :
            base(spriteSpec, loopAnimation, msPerFrame, rightSprite, leftSprite)
        {   
            animationCycle = new Vector2[animationCycleLength];
            animationCycle[0] = new Vector2(3, -8);
            animationCycle[1] = new Vector2(3,  8);
            loopFinished = false;
        }

        override public void Update(GameTime gameTime)
        {
            if (animationCycleIndex != animationCycleLength)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > msPerFrame)
                {
                    timeSinceLastFrame -= msPerFrame;
                    animationCycleIndex++;
                }
            }
            else
            {
                Console.WriteLine("cycleIndex " + animationCycleIndex + " / " + animationCycleLength);

                loopFinished = true;
            }
        }

        public override Vector2 updateLocation()
        {
            if (animationCycleIndex == animationCycleLength)
                return noChange;
            return animationCycle[animationCycleIndex];   
        }

        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {
            Console.WriteLine("RESET");
            timeSinceLastFrame = 0;
            animationCycleIndex = 0;
            loopFinished = false;
        }
    }
}
