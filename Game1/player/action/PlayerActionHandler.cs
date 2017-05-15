using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class PlayerActionHandler
    {
        private PlayerAction currentAction;
        private PlayerAction previousAction;
        private Player player;

        public PlayerActionHandler(Player player)
        {
            previousAction = PlayerAction.JUMP;
            currentAction = PlayerAction.STOP;
            this.player = player;
        }

        public PlayerAction Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.direction = Direction.RIGHT;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    currentAction = PlayerAction.JUMP;
                else
                    currentAction = PlayerAction.MOVE;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.direction = Direction.LEFT;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    currentAction = PlayerAction.JUMP;
                else
                    currentAction = PlayerAction.MOVE;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && !player.jumping)
            {
                currentAction = PlayerAction.JUMP;
            }
            else
            {
                currentAction = PlayerAction.STOP;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.O))
                player.shooting = true;
            else player.shooting = false;

            if (currentAction != previousAction)
            {
                Console.WriteLine("CurrentAction: " + currentAction.ToString() + " PreviousAction: " + previousAction.ToString());
                previousAction = currentAction;
            }

            if (currentAction == PlayerAction.JUMP)
                player.jumping = true;

            if (player.falling)
                currentAction = PlayerAction.FALL;

            if (player.damaged)
                currentAction = PlayerAction.HIT;

            return currentAction;
        }

        public PlayerAction getAction ()
        {
            return currentAction;
        }

    }
}
