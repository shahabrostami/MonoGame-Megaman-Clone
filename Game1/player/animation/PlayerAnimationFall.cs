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
        public PlayerAnimationFall(Player player, SpriteSpec spriteSpec, AnimationSpec animation) :
            base(player, spriteSpec, animation)
        {
        }

        override public bool Update(GameTime gameTime)
        {
            if (!player.falling)
            {
                loopFinished = true;
                return false;
            }
            timeSinceLastFrame = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.updateLocation(velocity * (timeSinceLastFrame * 10));
            velocity += (gravity * (timeSinceLastFrame * 10));
            return true;
        }
        
        public override void updateOnAction(PlayerStateAnimation pState, PlayerAction pAction)
        {
            direction = player.direction;
            if (direction == Direction.RIGHT)
            {
                if (player.shooting)
                    updateCycle(cycles[2]);
                else updateCycle(cycles[0]);
            }
            else if (direction == Direction.LEFT)
            {
                if (player.shooting)
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
            velocity = currentCycle.velocity / 4;
            velocity.Y *= -1;
            loopFinished = false;
        }
    }
}
