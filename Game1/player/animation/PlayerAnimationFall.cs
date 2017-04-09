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
    class PlayerAnimationFall : BasePlayerAnimation
    {
        float previousY;

        public PlayerAnimationFall(Player player, SpriteSpec spriteSpec, AnimationSpec animation) :
            base(player, spriteSpec, animation)
        {
            velocity.Y *= -1;
            reset();
        }

        override public bool Update(GameTime gameTime)
        {
            if (!player.isFalling())
            {
                return false;
            }
            
            timeSinceLastFrame = (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity += (gravity * (timeSinceLastFrame*10));
            if (!player.updateLocation(velocity * (timeSinceLastFrame * 10)) && velocity.Y > 0)
                previousY = player.location.Y;
            return true;
        }
        
        public override void updateOnAction(PlayerState pState, PlayerAction pAction)
        {
            direction = player.getDirection();
            if (direction == Direction.RIGHT)
            {
                if (player.isShooting())
                    updateCycle(cycles[2]);
                else updateCycle(cycles[0]);
            }
            else if (direction == Direction.LEFT)
            {
                if (player.isShooting())
                    updateCycle(cycles[3]);
                else updateCycle(cycles[1]);
            }

            if (pAction == PlayerAction.STOP)
                velocity.X = 0;
            else 
                velocity.X = currentCycle.velocity.X;
        }

        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {
            previousY = player.location.Y;
            velocity = currentCycle.velocity;
            velocity.Y *= -1;
            loopFinished = false;
        }
    }
}
