using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player.bullet
{
    class Bullet
    {
        private Vector2 position;
        private static Vector2 velocityRight = new Vector2(500, 0);
        private static Vector2 velocityLeft = new Vector2(-500, 0);
        private static Vector2 rightDisplace = new Vector2(25, 14);
        private static Vector2 leftDisplace = new Vector2(25, 14);
        private static int jumpDisplace = -3;

        private Vector2 velocity;

        public Bullet(bool isJumping, Vector2 position, Direction direction)
        {
            this.position = position;
            if (direction == Direction.RIGHT)
            {
                this.velocity = velocityRight;
                this.position += rightDisplace;
            }
            else 
            {
                this.velocity = velocityLeft;
                this.position += leftDisplace;
            }

            if (isJumping)
                this.position.Y += jumpDisplace;

        }

        public void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D bulletTexture)
        {
            spriteBatch.Draw(bulletTexture, new Rectangle((int)position.X,(int)position.Y,2,2), Color.White);
        }
    }
}
