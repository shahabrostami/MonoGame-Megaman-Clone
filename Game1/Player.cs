using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    class Player
    {
        private PlayerStateMachine playerStateMachine;
        private PlayerActionHandler playerActionHandler;
        private PlayerState currentState;
        private PlayerAction playerAction;
        private Vector2 location;

        public Player() {
            location.X = 0;
            location.Y = 400;
            playerActionHandler = new PlayerActionHandler();
            playerStateMachine = new PlayerStateMachine();
            currentState = PlayerStates.STAND_RIGHT;
        }
        
        public void updateLocation(Vector2 updateLocation)
        {
            Console.WriteLine("Update: (" + updateLocation.X + "),(" + updateLocation.Y + ")");
            location.X += updateLocation.X;
            location.Y += updateLocation.Y;
        }

        public void Load(Texture2D texture)
        {
            PlayerStates.Load(this, texture, 2, 6);
        }

        public void Update(GameTime gameTime)
        {

            playerAction = playerActionHandler.Update(gameTime);

            currentState = playerStateMachine.Update(playerAction);

            currentState.animation.updateDirection(currentState.getDirection());

            currentState.animation.Update(gameTime);

            if (currentState.animation.hasMovement())
                updateLocation(currentState.animation.updateLocation(currentState));

            if (currentState.animation.isLoopFinished())
                playerStateMachine.revert();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerStateMachine.currentState.animation.Draw(spriteBatch, location);
        }
    }
}
