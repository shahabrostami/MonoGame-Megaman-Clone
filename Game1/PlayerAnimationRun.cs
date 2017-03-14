using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    class PlayerAnimationRun : BasePlayerAnimation
    {
        Vector2 rightMoveUpdate = new Vector2(3, 0);
        Vector2 leftMoveUpdate = new Vector2(-3, 0);

        public PlayerAnimationRun(SpriteSpec spriteSpec, bool loopAnimation, int msPerFrame, SpriteLocation rightSprite, SpriteLocation leftSprite) :
            base(spriteSpec, loopAnimation, msPerFrame, rightSprite, leftSprite)
        {
            // Standard animation, likely to change...
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > msPerFrame)
            {
                timeSinceLastFrame -= msPerFrame;

                currentFrame++;
                if (currentFrame == currentSprite.eF)
                    currentFrame = currentSprite.sF;
            }
        }
        
        public override Vector2 updateLocation()
        {
            if (direction == Direction.RIGHT)
                return rightMoveUpdate;
            else return leftMoveUpdate;
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
