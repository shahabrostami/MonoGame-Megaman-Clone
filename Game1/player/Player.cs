using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MyObjects;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Game1.player;

namespace Game1
{
    class Player : MovingObject
    {

        // Player Related Objects
        private PlayerStateMachine playerStateMachine;
        private PlayerActionHandler playerActionHandler;
        public PlayerStateAnimation playerState;
        private PlayerAction playerAction;
        private BulletFactory bulletFactory;
        private Map map;
        public Direction direction { get; set; }

        // Player Related Flags
        public bool shooting { get; set; }
        public bool jumping { get; set; }
        public bool grounded { get; set; }
        private Vector2 playerTextureOffset;
        private Vector2 playerTextureSize;

        // Player Location
        public Vector2 position;
        public Rectangle playerBound;

        public Player() {
            bulletFactory = new BulletFactory(this);
            playerActionHandler = new PlayerActionHandler(this);
            playerStateMachine = new PlayerStateMachine();
            playerState = PlayerStates.STAND;
            jumping = false;
            shooting = false;
            grounded = true;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            Sprite playerSprite = Content.Load<Sprite[]>("spritetest")[0];
            Texture2D playerTexture = Content.Load<Texture2D>(playerSprite.textureName);
            PlayerStates.LoadContent(this, playerTexture, playerSprite);
            playerTextureOffset = playerSprite.offset;
            playerTextureSize = playerSprite.actualTextureSize;
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
            bulletFactory.Update(shooting, jumping, gameTime);
        }

        public bool onGround()
        {
            return grounded && !jumping;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerStateMachine.currentState.animation.Draw(spriteBatch, position);
            bulletFactory.Draw(spriteBatch);
        }

        public void addEvent(PlayerEvent pEvent) {
            playerActionHandler.addEvent(pEvent);
        }

        public Rectangle getPlayerBound()
        {
            return playerBound;
        }

        public Rectangle getPlayerBound(float x, float y)
        {
            return new Rectangle((int)(x + playerTextureOffset.X), (int)(y + playerTextureOffset.Y), (int) playerTextureSize.X, (int)playerTextureSize.Y); 
        }

        public bool updateLocation(Vector2 updateLocation)
        {
            if (updateLocation.X == 0 && updateLocation.Y == 0)
                return false;

            float newX = (position.X + updateLocation.X);
            float newY = (position.Y + updateLocation.Y);
            newX = (int)Math.Round(newX, 0);
            newY = (int)Math.Round(newY, 0);
            Rectangle newBound = getPlayerBound(newX, newY);

            position = map.checkPlayerCollisions(newBound, updateLocation);
            
            position.X -= playerTextureOffset.X;
            position.Y -= playerTextureOffset.Y;
            playerBound = newBound;
            return true;
        }
        
        public void setMap(Map map)
        {
            this.map = map;
        }

        public string getDebugInfo()
        {
            return String.Format("Player: ({0},{1}) \nAction: {2}\nState: {3} \n{4}", (int)position.X, (int)position.Y, playerAction, playerState, bulletFactory.GetDebugInfo());
        }
    }
}
