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
        public Vector2 textureSize;
        Vector2 velocity;

        BaseEnemyAnimation animation;

        public Enemy()
        {
            this.hp = 100;
        }

        public bool isEnemyAlive()
        {
            if (hp < 0)
                return false;
            return true;
        }

        public void enemyHit()
        {
            hp -= 5;
        }

        public bool checkCollision(int x, int y, bool canDamage)
        {
            Console.WriteLine(position.X + "," + position.Y);
            if (x >= position.X + animation.textureOffset.X && x <= (position.X + animation.textureSize.X) &&
                y >= position.Y + animation.textureOffset.Y && y <= (position.Y + animation.textureSize.Y))
            {
                if(canDamage)
                    enemyHit();
                return true;
            }
            return false;
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
