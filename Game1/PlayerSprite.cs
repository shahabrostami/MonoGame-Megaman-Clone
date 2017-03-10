using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    class PlayerSprite
    {
        public Texture2D Texture { get; set; }
        private Action previousActionEnum = ActionEnum.MOVE_RIGHT;
        private BasePlayerAnimation currentAnimation;
        private BasePlayerAnimation walkRight;
        private BasePlayerAnimation walkLeft;
        private BasePlayerAnimation standRight;
        private BasePlayerAnimation standLeft;

        public PlayerSprite(Texture2D texture, int rows, int columns)
        {
            walkRight = new PlayerAnimation(texture, rows, columns, 0, 3, 6, 100);
            walkLeft = new PlayerAnimation(texture, rows, columns, 1, 3, 6, 100);
            standRight = new PlayerAnimationBlink(texture, rows, columns, 0, 0, 2, 2000, 200);
            standLeft = new PlayerAnimationBlink(texture, rows, columns, 1, 0, 2, 2000, 200);
            currentAnimation = standRight;
        }

        public void Update(GameTime gameTime, Action actionEnum)
        {

            if (actionEnum == ActionEnum.MOVE_RIGHT)
            {
                currentAnimation = walkRight;
            }
            else if (actionEnum == ActionEnum.MOVE_LEFT)
            {
                currentAnimation = walkLeft;
            }
            else if (actionEnum == ActionEnum.NONE)
            {
                if (previousActionEnum == ActionEnum.MOVE_RIGHT)
                {
                    currentAnimation = standRight;
                }
                else if (previousActionEnum == ActionEnum.MOVE_LEFT)
                {
                    currentAnimation = standLeft;
                }
            }
            currentAnimation.Update(gameTime);
            previousActionEnum = actionEnum;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            currentAnimation.Draw(spriteBatch, location);
        }

    }
}
