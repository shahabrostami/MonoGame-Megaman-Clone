using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    class PlayerAnimationBlink : BasePlayerAnimation
    {
        private int msPerFramesClosed;
        private int msPerFramesOpen;
        private Boolean eyesOpen = true;

        public PlayerAnimationBlink(Texture2D texture, int rows, int columns, int row, int startingFrame, int endFrame, int msPerFramesOpen, int msPerFramesClosed) : 
            base(texture, rows, columns, row, startingFrame, endFrame, msPerFramesOpen)
        {
            this.msPerFramesOpen = msPerFramesOpen;
            this.msPerFramesClosed = msPerFramesClosed;
        }

        override public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            
            if (timeSinceLastFrame > msPerFrame)
            {
                timeSinceLastFrame -= msPerFrame;

                currentFrame++;
                if (currentFrame == endFrame)
                    currentFrame = startingFrame;
                
                if (eyesOpen)
                    msPerFrame = msPerFramesClosed;
                else
                    msPerFrame = msPerFramesOpen;

                eyesOpen = !eyesOpen;
            }
            
        }
    }
}
