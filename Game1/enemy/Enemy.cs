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
        public Vector2 position;
        Vector2 velocity;
        BaseEnemyAnimation animation;

        public Enemy()
        {
            this.hp = 100;
        }

        public void setAnimation(BaseEnemyAnimation enemyAnimation)
        {
            this.animation = enemyAnimation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, position);
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
            animation.Update(gameTime);   
        }

        public bool updateLocation(Vector2 updateLocation)
        {
            this.position += updateLocation;
            return true;
        }
    }
}
