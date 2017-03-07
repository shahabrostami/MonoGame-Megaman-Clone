using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    class PlayerAnimation : BasePlayerAnimation
    {
        private Texture2D Texture { get; set; }
        private int Rows { get; set; }
        private int Columns { get; set; }
        private int width;
        private int height;
        private int currentFrame;
        private int startingFrame;
        private int row;
        private int endFrame;
        private int timeSinceLastFrame = 0;
        private int msPerFrame;

        public PlayerAnimation(Texture2D texture, int rows, int columns, int row, int startingFrame, int endFrame, int msPerFrame)
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

        override public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > msPerFrame)
            {
                timeSinceLastFrame -= msPerFrame;

                currentFrame++;
                if (currentFrame == endFrame)
                    currentFrame = startingFrame;
            }
            
        }

        override public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int column = currentFrame;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            Console.WriteLine("width" + width + " height" + height + " row" + row + " locationx" + location.X + " locationy" + location.Y);
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

    }
}
