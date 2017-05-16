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
        private PlayerAction currentAction;
        private PlayerAction previousAction;

        private Player player;

        // Player event list
        private List<PlayerEvent> playerEvents = new List<PlayerEvent>();

        public PlayerActionHandler(Player player)
        {
            previousAction = PlayerAction.JUMP;
            currentAction = PlayerAction.STOP;
            this.player = player;
        }

        public PlayerAction handlePlayerEvent()
        {
            int count = playerEvents.Count;
            PlayerAction action = PlayerAction.NONE;
            if (count > 0)
            {
                PlayerEvent pEvent = playerEvents[count - 1];
                playerEvents.RemoveAt(count - 1);
                action = pEvent.Handle();
            }
            return action;
        }
        
        public void addEvent(PlayerEvent pEvent)
        {
            playerEvents.Add(pEvent);
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

            if (currentAction == PlayerAction.JUMP)
                player.jumping = true;

            PlayerAction playerEventAction = handlePlayerEvent();
            if (playerEventAction != PlayerAction.NONE)
                currentAction = playerEventAction;

            if (currentAction != previousAction)
            {
                Console.WriteLine("CurrentAction: " + currentAction.ToString() + " PreviousAction: " + previousAction.ToString());
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
