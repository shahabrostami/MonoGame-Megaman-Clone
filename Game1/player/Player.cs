using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MyObjects;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    class Player : MovingObject
    {
        // Player Related Objects
        private PlayerStateMachine playerStateMachine;
        private PlayerActionHandler playerActionHandler;
        private PlayerStateAnimation playerState;
        private PlayerAction playerAction;
        private BulletFactory bulletFactory;
        private Map map;
        public Direction direction { get; set; }

        // Player Related Flags
        public bool shooting { get; set; }
        public bool jumping { get; set; }
        public bool falling { get; set; }
        private Vector2 playerTextureOffset = new Vector2(4, 6);

        // Player Location
        public Vector2 position;

        public Player() {
            bulletFactory = new BulletFactory(this);
            playerActionHandler = new PlayerActionHandler(this);
            playerStateMachine = new PlayerStateMachine();
            playerState = PlayerStates.STAND;
            jumping = false;
            falling = false;
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

            if (falling)
                playerState = PlayerStates.FALL;

            // Update Player Animation
            playerState.animation.updateOnAction(playerState, playerAction);
            playerState.animation.Update(gameTime);

            if (playerState.animation.isLoopFinished())
                playerStateMachine.revert();

            // Update Bullets
            bulletFactory.Update(shooting, jumping, gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerStateMachine.currentState.animation.Draw(spriteBatch, position);
            bulletFactory.Draw(spriteBatch);
        }

        public bool updateLocation(Vector2 updateLocation)
        {
            if (updateLocation.X == 0 && updateLocation.Y == 0)
                return false;

            float newX = (position.X + updateLocation.X);
            float newY = (position.Y + updateLocation.Y);
            newX = (int)Math.Round(newX, 0);
            newY = (int)Math.Round(newY, 0);
            Rectangle newBound = new Rectangle((int) (newX + playerTextureOffset.X), (int) (newY + playerTextureOffset.Y), 20, 23);

            position = map.checkPlayerCollisions(newBound, updateLocation);

            position.X -= playerTextureOffset.X;
            position.Y -= playerTextureOffset.Y;
            return true;
        }

        public void setFalling(bool fall)
        {
            if(fall == false)
                PlayerStates.FALL.animation.reset();
            this.falling = fall;
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
