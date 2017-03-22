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
        private int ms;

        public PlayerAnimationRun(Player player, SpriteSpec spriteSpec, AnimationSpec animation) :
            base(player, spriteSpec, animation)
        {
            cycles[1].frames = cycles[0].frames;
            cycles[1].velocity = new Vector2(cycles[0].velocity.X * -1, cycles[0].velocity.Y);

            ms = animation.cycles[0].ms;
        }

        override public bool Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > ms)
            {
                timeSinceLastFrame -= ms;

                currentFrameIndex++;

                if (currentFrameIndex == currentCycle.ef + 1)
                    currentFrameIndex = currentCycle.sf;

                currentFrame = currentCycle.frames[currentCycle.ef % currentFrameIndex];
            }

            player.updateLocation(currentCycle.velocity * (timeSinceLastFrame/1000));
            return true;
        }

        public override void updateDirection(Direction direction)
        {

            this.direction = direction;
            if (direction == Direction.RIGHT)
                currentCycle = cycles[0];
            else
                currentCycle = cycles[1];
        }
        
        public override void updateOnAction(PlayerState pState, PlayerAction pAction)
        {

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
