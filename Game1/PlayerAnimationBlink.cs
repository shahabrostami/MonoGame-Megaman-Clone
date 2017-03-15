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

        public PlayerAnimationBlink(SpriteSpec spriteSpec, bool loopAnimation, int msPerFramesOpen, int msPerFramesClosed, SpriteLocation rightSprite, SpriteLocation leftSprite) :
            base(spriteSpec, loopAnimation, msPerFramesOpen, rightSprite, leftSprite)
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
                if (currentFrame == currentSprite.eF)
                    currentFrame = currentSprite.sF;
                
                if (eyesOpen)
                    msPerFrame = msPerFramesClosed;
                else
                    msPerFrame = msPerFramesOpen;

                eyesOpen = !eyesOpen;
            }
        }

        public override Vector2 updateLocation()
        {
            throw new NotImplementedException();
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
