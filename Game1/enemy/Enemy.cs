using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.enemy
{
    class Enemy : MovingObject
    {
        int hp;
        Vector2 position;
        Vector2 velocity;

        public Enemy()
        {
            this.hp = 100;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public string getDebugInfo()
        {
            throw new NotImplementedException();
        }

        public void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public bool updateLocation(Vector2 updateLocation)
        {
            throw new NotImplementedException();
        }
    }
}
