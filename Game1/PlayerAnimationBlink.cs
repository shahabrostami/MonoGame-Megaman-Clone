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

        public PlayerAnimationBlink(SpriteSpec spriteSpec, AnimationSpec animation) :
            base(spriteSpec, animation)
        {
            cycles[1].frames = cycles[0].frames;
            cycles[1].ef = cycles[0].ef;
            cycles[1].sf = cycles[0].sf;
            cycles[1].dis = cycles[0].dis;
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

                currentFrame = currentCycle.frames[currentFrameIndex];
            }
        }

        public override Vector2 updateLocation(PlayerState pState)
        {
            throw new NotImplementedException();
        }

        
        public override void updateOnAction(PlayerState pState, PlayerAction pAction)
        {

        }

        public override void updateDirection(Direction direction)
        {

            this.direction = direction;
            if (direction == Direction.RIGHT)
                currentCycle = cycles[0];
            else
                currentCycle = cycles[1];
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
