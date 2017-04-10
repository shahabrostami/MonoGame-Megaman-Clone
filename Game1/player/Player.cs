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
        private Map map;

        // Player Related Flags
        private bool shooting;
        private bool jumping;
        private bool falling;

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

            /*
            if (map.checkCollision(location.X + 7, location.Y + 34) &&
               map.checkCollision(location.X + 28, location.Y + 34) && !isJumping())
            {
                playerState = PlayerStates.FALL;
                setFalling(true);
            }
            else
            {
                setFalling(false);
            }
            */

            // Update Player Animation
            playerState.animation.updateOnAction(playerState, playerAction);
            playerState.animation.Update(gameTime);

            if (playerState.animation.isLoopFinished())
                playerStateMachine.revert();

            // Update Bullets
            bulletFactory.Update(isShooting(), isJumping(), gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerStateMachine.currentState.animation.Draw(spriteBatch, location);
            bulletFactory.Draw(spriteBatch);
        }

        public bool updateLocation(Vector2 updateLocation)
        {
            // Need to introduce snap to grid
            if((map.checkCollision(location.X + updateLocation.X + 7, location.Y + 4) &&
                    map.checkCollision(location.X + updateLocation.X + 7, location.Y + 27) &&
                    map.checkCollision(location.X + updateLocation.X + 28, location.Y + 27) &&
                    map.checkCollision(location.X + updateLocation.X + 28, location.Y + 4)))
            {
                location.X += updateLocation.X;
                location.X -= (location.X % 1);
            }

            if ((map.checkCollision(location.X + 7, location.Y + updateLocation.Y + 4) &&
                    map.checkCollision(location.X + 7, location.Y + updateLocation.Y + 27) &&
                    map.checkCollision(location.X + 28, location.Y + updateLocation.Y + 27) &&
                    map.checkCollision(location.X + 28, location.Y + updateLocation.Y + 4)))
            {
                location.Y += updateLocation.Y;
                location.Y -= (location.Y % 1);
            }
            else
            {
                if (updateLocation.Y < 0)
                {
                    if (!(map.checkCollision(location.X + 7, location.Y + updateLocation.Y + 4) &&
                        map.checkCollision(location.X + 28, location.Y + updateLocation.Y + 4)))
                    {
                        location.Y += updateLocation.Y;
                        location.Y -= location.Y % 16 - 4;
                    }
                }
                else if (updateLocation.Y > 0)
                {
                    if (!(map.checkCollision(location.X + 7, location.Y + updateLocation.Y + 27) &&
                        map.checkCollision(location.X + 28, location.Y + updateLocation.Y + 27)))
                    {
                        location.Y += updateLocation.Y;
                        location.Y -= (location.Y % 16) - 4;
                        setJumping(false);
                    }
                }
            }

            

           

           
            Console.WriteLine("Location:" + location.X + "," + location.Y);
            return true;
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

        public bool isFalling()
        {
            return falling;
        }

        public void setFalling(bool fall)
        {
            this.falling = fall;
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
