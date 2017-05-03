using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class SpriteSpec
    {
        private Texture2D Texture { get; set; }
        public Vector2 textureOffset;
        private int Rows { get; set; }
        private int Columns { get; set; }
        public int width;
        public int height;

        public SpriteSpec(Texture2D texture, Vector2 offset, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            textureOffset = offset;
            width = Texture.Width / Columns;
            height = (Texture.Height / Rows);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int row, int column)
        {
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
