using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    interface MovingObject
    {
        void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        bool updateLocation(Vector2 updateLocation);

        string getDebugInfo();
    }
}
