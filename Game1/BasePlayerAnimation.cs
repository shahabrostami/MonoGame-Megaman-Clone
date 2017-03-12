using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    abstract class BasePlayerAnimation
    {
        private Texture2D Texture { get; set; }
        private int Rows { get; set; }
        private int Columns { get; set; }
        private int width;
        private int height;
        protected int currentFrame;
        protected int startingFrame;
        private int row;
        protected int endFrame;
        protected int timeSinceLastFrame = 0;
        protected int msPerFrame;

        public BasePlayerAnimation(Texture2D texture, int rows, int columns, int row, int startingFrame, int endFrame, int msPerFrame)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            width = Texture.Width / Columns;
            height = (Texture.Height / Rows);
            this.row = row;
            this.startingFrame = startingFrame;
            this.endFrame = endFrame;
            this.msPerFrame = msPerFrame;

            currentFrame = startingFrame;
        }

        public abstract void Update(GameTime gameTime);

        public abstract Vector2 updateLocation();

        public abstract void reset();

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int column = currentFrame;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();

        }
    }


}
