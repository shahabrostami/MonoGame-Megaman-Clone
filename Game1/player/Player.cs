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
        private BulletFactory bulletFactory;
        private Map map;
        public Direction direction { get; set; }

        // Player Related Flags
        public bool shooting { get; set; }
        public bool jumping { get; set; }
        public bool falling { get; set; }
        private Vector2 playerTextureOffset = new Vector2(4, 6);

        // Player Location
        public Vector2 location;

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
            playerStateMachine.currentState.animation.Draw(spriteBatch, location);
            bulletFactory.Draw(spriteBatch);
        }

        public bool updateLocation(Vector2 updateLocation)
        {
            if (updateLocation.X == 0 && updateLocation.Y == 0)
                return false;

            float newX = (location.X + updateLocation.X);
            float newY = (location.Y + updateLocation.Y);
            newX -= newX % 1;
            newY -= newY % 1;
            Rectangle newBound = new Rectangle((int) (newX + playerTextureOffset.X), (int) (newY + playerTextureOffset.Y-1), 20, 23);
            location = map.checkCollisions(newBound, updateLocation);
            location.X -= playerTextureOffset.X;
            location.Y -= playerTextureOffset.Y - 1;
            return true;
        }

        public void setMap(Map map)
        {
            this.map = map;
        }

        public string getDebugInfo()
        {
            return "Player: (" + (int)location.X + "," + (int)location.Y + ")" + "\n" + bulletFactory.GetDebugInfo();
        }
    }
}
