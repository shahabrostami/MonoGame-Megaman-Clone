using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    class Player
    {
        private PlayerSprite playerSprite;
        private PlayerStateHandler playerStateHandler;
        private Vector2 location;

        public Player() {
            location.X = 0;
            location.Y = 400;
        }
        
        public void updateLocation(Vector2 updateLocation)
        {
            location.X += updateLocation.X;
            location.Y += updateLocation.Y;
        }

        public void Load(Texture2D texture)
        {
            playerStateHandler = new PlayerStateHandler();
            playerSprite = new PlayerSprite(this, texture, 2, 6);
        }

        public void Update(GameTime gameTime)
        {
            playerStateHandler.Update(gameTime);
            playerSprite.Update(gameTime, playerStateHandler.getState());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerSprite.Draw(spriteBatch, location);
        }
    }
}
