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
        Rectangle enemyBound;
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

        public bool checkCollision(Rectangle collisionRect, bool canDamage)
        {
            Rectangle intersect = Rectangle.Intersect(collisionRect, enemyBound);
            if (intersect.IsEmpty)
                return false;
            return true;
        }

        public bool checkCollision(int x, int y, bool canDamage)
        {
            if (x >= position.X + animation.textureOffset.X && x <= (position.X - animation.textureOffset.X + animation.textureSize.X) &&
                y >= position.Y + animation.textureOffset.Y && y <= (position.Y - animation.textureOffset.Y + animation.textureSize.Y))
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
            enemyBound = new Rectangle(position.ToPoint(), animation.textureSize.ToPoint());
            animation.Update(gameTime);   
        }

        public bool updateLocation(Vector2 updateLocation)
        {
            this.position += updateLocation;
            return true;
        }
    }
}
