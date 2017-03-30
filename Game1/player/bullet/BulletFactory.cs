using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class BulletFactory
    {
        Texture2D bullet;

        public BulletFactory()
        {

        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            bullet = new Texture2D(GraphicsDevice, 1, 1);
            bullet.SetData(new[] { Color.White });
        }

        public void Update(bool isShooting)
        {

        }

        public void Draw()
        {

        }
    }
}
