using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Game1.player;

namespace Game1
{
    class PlayerActionHandler
    {
        private Action currentAction;
        private Action previousAction;

        private Player player;

        // Player event list
        private List<MovingObjectEvent> playerEvents = new List<MovingObjectEvent>();

        public PlayerActionHandler(Player player)
        {
            previousAction = Action.JUMP;
            currentAction = Action.STOP;
            this.player = player;
        }

        public Action handlePlayerEvent()
        {
            int count = playerEvents.Count;
            Action action = Action.NONE;
            if (count > 0)
            {
                MovingObjectEvent pEvent = playerEvents[count - 1];
                playerEvents.RemoveAt(count - 1);
                action = pEvent.Handle();
            }
            return action;
        }
        
        public void addEvent(MovingObjectEvent pEvent)
        {
            playerEvents.Add(pEvent);
        }

        public Action Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.direction = Direction.RIGHT;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    currentAction = Action.JUMP;
                else
                    currentAction = Action.MOVE;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.direction = Direction.LEFT;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    currentAction = Action.JUMP;
                else
                    currentAction = Action.MOVE;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && !player.jumping)
            {
                currentAction = Action.JUMP;
            }
            else
            {
                currentAction = Action.STOP;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.O))
                player.shooting = true;
            else player.shooting = false;

            if (currentAction == Action.JUMP)
                player.jumping = true;

            Action playerEventAction = handlePlayerEvent();
            if (playerEventAction != Action.NONE)
                currentAction = playerEventAction;

            if (playerEventAction == Action.LAND)
            {
                PlayerStates.FALL.animation.reset();
                PlayerStates.JUMP.animation.reset();
                player.grounded = true;
            }
            else if (playerEventAction == Action.FALL)
                player.grounded = false;


            if (currentAction != previousAction)
            {
                Console.WriteLine("CurrentAction: " + currentAction.ToString() + " PreviousAction: " + previousAction.ToString());
                previousAction = currentAction;
            }
            return currentAction;
        }

        public Action getAction ()
        {
            return currentAction;
        }

    }
}
