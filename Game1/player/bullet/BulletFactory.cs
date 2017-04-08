using Game1.player.bullet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class BulletFactory
    {
        Texture2D bulletTexture;
        protected Player player;
        protected float timeSinceLastFrame = 500;
        protected float delay = 100;
        protected static float screenWidth;
        protected List<Bullet> bullets = new List<Bullet>();

        public BulletFactory(Player player)
        {
            this.player = player;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            bulletTexture = new Texture2D(GraphicsDevice, 1, 1);
            bulletTexture.SetData(new[] { Color.White });
            screenWidth = GraphicsDevice.Viewport.Width / 2 + 20;
        }

        public void Update(bool isShooting, bool isJumping, GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > delay) { 

                if (isShooting) {
                    bullets.Add(new Bullet(isJumping, (player.location), player.getDirection()));
                    timeSinceLastFrame = 0;
                }
            }
            
            foreach (var bullet in bullets)
                bullet.Update(gameTime);

            var bulletsOffScreen = bullets.Where(i => IsOffScreen(i)).ToList();

            foreach (var bullet in bulletsOffScreen)
                bullets.Remove(bullet);

        }

        private Boolean IsOffScreen(Bullet bullet)
        {
            if (bullet.position.X <= player.location.X - screenWidth || bullet.position.X >= player.location.X + screenWidth)
                return true;
            return false;
        }

        public string GetDebugInfo()
        {
            return "Bullets: " + bullets.Count;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bullet in bullets)
            {
                bullet.Draw(spriteBatch, bulletTexture);
            }
        }
    }
}
