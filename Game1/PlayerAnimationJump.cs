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
        private int animationCycleIndex = 0;
        private int animationCycleMs = 0;
        public PlayerAnimationJump(SpriteSpec spriteSpec, AnimationSpec animation) :
            base(spriteSpec, animation)
        {
            int frameLength = cycles[0].frames.Length;
            cycles[1].frames = new AnimationFrameSpec[frameLength];
            cycles[2].frames = new AnimationFrameSpec[frameLength];
            for (int i = 0; i < frameLength; i++)
            {
                float X = cycles[0].frames[i].dis.X;
                float Y = cycles[0].frames[i].dis.Y;
                int ms = cycles[0].frames[i].ms;
                cycles[1].frames[i] = new AnimationFrameSpec();
                cycles[1].frames[i].dis = new Vector2(X * -1, Y);
                cycles[1].frames[i].ms = ms;
                cycles[2].frames[i] = new AnimationFrameSpec();
                cycles[2].frames[i].dis = new Vector2(X * 0, Y);
                cycles[2].frames[i].ms = ms;
            }
            animationCycleMs = currentFrame.ms;
        }

        override public bool Update(GameTime gameTime)
        {
            if (loopFinished)
                return false;

            timeSinceLastFrame = gameTime.ElapsedGameTime.Milliseconds;
            if (animationCycleMs <= 0)
            {
                animationCycleIndex++;
                if(animationCycleIndex == currentCycle.frames.Length)
                {
                    loopFinished = true;
                    return false;
                }
                currentFrame = currentCycle.frames[animationCycleIndex];
                animationCycleMs = currentFrame.ms;
            }
            animationCycleMs -= timeSinceLastFrame;
            return true;
        }
        
        public override void updateOnAction(PlayerState pState, PlayerAction pAction)
        {
            if (pState.action == PlayerAction.STOP || pAction == PlayerAction.STOP)
                currentCycle = cycles[2];
        }

        public override Vector2 updateLocation(PlayerState pState)
        {
            return timeSinceLastFrame * (currentFrame.dis / currentFrame.ms); ;
        }

        public override void updateDirection(Direction direction)
        {
            if (direction == Direction.RIGHT)
                currentCycle = cycles[0];
            else
                currentCycle = cycles[1];
            currentFrame = currentCycle.frames[animationCycleIndex];
        }

        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {
            animationCycleIndex = 0;
            currentFrame = currentCycle.frames[animationCycleIndex];
            animationCycleMs = currentFrame.ms;
            loopFinished = false;
        }
    }
}
