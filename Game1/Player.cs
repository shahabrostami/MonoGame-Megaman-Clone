using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MyObjects;

namespace Game1
{
    class Player
    {
        private PlayerStateMachine playerStateMachine;
        private PlayerActionHandler playerActionHandler;
        private PlayerState currentState;
        private PlayerAction playerAction;
        private bool shooting;
        public Vector2 location;

        public Player() {
            location.X = 0;
            location.Y = 426;
            playerActionHandler = new PlayerActionHandler(this);
            playerStateMachine = new PlayerStateMachine();
            currentState = PlayerStates.STAND_RIGHT;
        }
        
        public void updateLocation(Vector2 updateLocation)
        {
            Console.WriteLine("Update: (" + updateLocation.X + "),(" + updateLocation.Y + ")");
            location += updateLocation;
        }

        public bool isShooting()
        {
            return shooting;
        }

        public void setShooting(bool shoot)
        {
            this.shooting = shoot;
        }

        public void Load(Texture2D texture, Sprite sprite)
        {
            PlayerStates.Load(this, texture, sprite);
        }

        public void Update(GameTime gameTime)
        {

            playerAction = playerActionHandler.Update(gameTime);

            currentState = playerStateMachine.Update(playerAction);
            currentState.animation.updateOnAction(currentState, playerAction);

            currentState.animation.Update(gameTime);

            if (currentState.animation.isLoopFinished())
                playerStateMachine.revert();
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerStateMachine.currentState.animation.Draw(spriteBatch, location);
        }
    }
}
