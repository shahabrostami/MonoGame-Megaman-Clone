using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MyObjects;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    class Player
    {
        // Player Related Objects
        private PlayerStateMachine playerStateMachine;
        private PlayerActionHandler playerActionHandler;
        private PlayerState playerState;
        private PlayerAction playerAction;
        private Direction playerDirection;
        private BulletFactory bulletFactory;

        // Player Related Flags
        private bool shooting;
        private bool jumping;

        // Player Location
        public Vector2 location;

        public Player() {
            location.X = 0;
            location.Y = 426;
            playerActionHandler = new PlayerActionHandler(this);
            playerStateMachine = new PlayerStateMachine();
            playerState = PlayerStates.STAND;
            bulletFactory = new BulletFactory();
            jumping = false;
            shooting = false;
        }
        public void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            Sprite playerSprite = Content.Load<Sprite[]>("spritetest")[0];
            Texture2D playerTexture = Content.Load<Texture2D>(playerSprite.textureName);
            PlayerStates.LoadContent(this, playerTexture, playerSprite);
            bulletFactory.LoadContent(GraphicsDevice);

        }

        public void Update(GameTime gameTime)
        {
            // Retrieve Player Action
            playerAction = playerActionHandler.Update(gameTime);

            // Update Player State
            playerState = playerStateMachine.Update(playerAction);
            
            // Update Player Animation
            playerState.animation.updateOnAction(playerState, playerAction);
            playerState.animation.Update(gameTime);
            if (playerState.animation.isLoopFinished())
                playerStateMachine.revert();

            // Update Bullets
            bulletFactory.Update(isShooting());

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerStateMachine.currentState.animation.Draw(spriteBatch, location);
        }

        public void updateLocation(Vector2 updateLocation)
        {
            // Console.WriteLine("Update: (" + updateLocation.X + "),(" + updateLocation.Y + ")");
            location += updateLocation;
        }

        public Direction getDirection()
        {
            return playerDirection;
        }

        public void setDirection(Direction dir)
        {
            this.playerDirection = dir;
        }

        public bool isShooting()
        {
            return shooting;
        }

        public void setShooting(bool shoot)
        {
            this.shooting = shoot;
        }

        public bool isJumping()
        {
            return jumping;
        }

        public void setJumping(bool jump)
        {
            this.jumping = jump;
        }

    }
}
