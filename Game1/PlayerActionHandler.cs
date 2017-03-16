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

        public PlayerActionHandler()
        {
            previousAction = PlayerAction.JUMP;
            currentAction = PlayerAction.STOP;
        }

        public PlayerAction Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    currentAction = PlayerAction.JUMP;
                else
                    currentAction = PlayerAction.MOVE_RIGHT;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    currentAction = PlayerAction.JUMP;
                else
                    currentAction = PlayerAction.MOVE_LEFT;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.Space))
                currentAction = PlayerAction.JUMP;
            else
            {
                currentAction = PlayerAction.STOP;
            }

            if (currentAction != previousAction)
            {
                // Console.WriteLine("CurrentAction: " + currentAction.ToString() + " PreviousAction: " + previousAction.ToString());
                previousAction = currentAction;
            }
            return currentAction;
        }

        public PlayerAction getAction ()
        {
            return currentAction;
        }

    }
}
