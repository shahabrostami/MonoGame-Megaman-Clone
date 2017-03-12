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
        private Vector2[] animationCycle;
        private Vector2 noChange = new Vector2(0, 0);

        public PlayerAnimationJump(Texture2D texture, int rows, int columns, int row, int startingFrame, int endFrame, int msPerFrame) : 
            base(texture, rows, columns, row, startingFrame, endFrame, msPerFrame)
        {
            animationCycle = new Vector2[animationCycleLength];
            animationCycle[0] = new Vector2(3, -5);
            animationCycle[1] = new Vector2(3, -5);
            animationCycle[2] = new Vector2(3, 5);
            animationCycle[3] = new Vector2(3, 5);
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
        }

        public override Vector2 updateLocation()
        {
            if (animationCycleIndex == animationCycleLength)
                return noChange;
            return animationCycle[animationCycleIndex];   
        }

        public override void reset()
        {
            Console.WriteLine("RESET");
            timeSinceLastFrame = 0;
            animationCycleIndex = 0;
        }
    }
}
