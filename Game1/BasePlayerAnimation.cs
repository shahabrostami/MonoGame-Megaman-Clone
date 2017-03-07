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
        private int row = 0;

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
