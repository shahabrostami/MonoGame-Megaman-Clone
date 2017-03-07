using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    class Player
    {
        private PlayerSprite playerSprite;
        private Vector2 location;

        public Player() {
            location.X = 0;
            location.Y = 0;
        }

        public void Load(Texture2D texture)
        {
            playerSprite = new PlayerSprite(texture, 2, 6);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                location.X += 3;
                playerSprite.Update(gameTime, ActionEnum.MOVE_RIGHT);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                location.X -= 3;
                playerSprite.Update(gameTime, ActionEnum.MOVE_LEFT);
            }
            else
            {
                playerSprite.Update(gameTime, ActionEnum.NONE);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerSprite.Draw(spriteBatch, location);
        }
    }
}
