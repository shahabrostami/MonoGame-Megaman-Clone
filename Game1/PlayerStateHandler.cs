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
    class PlayerStateHandler
    {
        private PlayerState previousState;
        private PlayerState currentState;

        public PlayerStateHandler()
        {
            currentState = PlayerStates.STAND_RIGHT;
            previousState = PlayerStates.STAND_RIGHT;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    currentState = PlayerStates.JUMP_RIGHT;
                else
                    currentState = PlayerStates.RUN_RIGHT;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    currentState = PlayerStates.JUMP_LEFT;
                else
                    currentState = PlayerStates.RUN_LEFT;
            }
            else
            {
                if (currentState.getDirection() == Direction.RIGHT)
                    currentState = PlayerStates.STAND_RIGHT;
                else if (currentState.getDirection() == Direction.LEFT)
                    currentState = PlayerStates.STAND_LEFT;
            }

            if (currentState != previousState)
            {
                // Console.WriteLine("CurrentAction: " + currentState.ToString() + " PreviousAction: " + previousState.ToString());

                previousState = currentState;
            }
        }

        public PlayerState getState ()
        {
            return currentState;
        }

    }
}
