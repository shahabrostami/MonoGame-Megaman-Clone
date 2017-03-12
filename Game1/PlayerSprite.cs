using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Game1.PlayerStateHandler;

namespace Game1
{
    class PlayerSprite
    {
        public Texture2D Texture { get; set; }
        private Player player;
        private PlayerLocationHandler playerLocationHandler;
        private BasePlayerAnimation currentAnimation;
        private BasePlayerAnimation previousAnimation;
        private BasePlayerAnimation walkRight;
        private BasePlayerAnimation walkLeft;
        private BasePlayerAnimation standRight;
        private BasePlayerAnimation standLeft;
        private BasePlayerAnimation jumpRight;
        private BasePlayerAnimation jumpLeft;
       
        public PlayerSprite(Player player, Texture2D texture, int rows, int columns)
        {
            this.player = player;
            playerLocationHandler = new PlayerLocationHandler(player);
            walkRight = new PlayerAnimation(texture, rows, columns, 0, 3, 6, 100);
            walkLeft = new PlayerAnimation(texture, rows, columns, 1, 3, 6, 100);
            standRight = new PlayerAnimationBlink(texture, rows, columns, 0, 0, 2, 2000, 200);
            standLeft = new PlayerAnimationBlink(texture, rows, columns, 1, 0, 2, 2000, 200);
            jumpRight = new PlayerAnimationJump(texture, rows, columns, 0, 2, 3, 100);
            jumpLeft = new PlayerAnimationJump(texture, rows, columns, 1, 2, 3, 100);
            currentAnimation = standRight;
            previousAnimation = standLeft;
        }

        public void Update(GameTime gameTime, PlayerState playerState)
        {
            if (playerState == PlayerStates.RUN_RIGHT)
            {
                currentAnimation = walkRight;
            }
            else if (playerState == PlayerStates.RUN_LEFT)
            {
                currentAnimation = walkLeft;
            }
            else if (playerState == PlayerStates.JUMP_RIGHT)
            {
                currentAnimation = jumpRight;
            }
            else if (playerState == PlayerStates.JUMP_LEFT)
            {
                currentAnimation = jumpLeft;
            }
            else if (playerState == PlayerStates.STAND_RIGHT)
            {
                currentAnimation = standRight;
            }
            else if (playerState == PlayerStates.STAND_LEFT)
            {
                currentAnimation = standLeft;
            }

            if (previousAnimation != currentAnimation)
            { 
                Console.WriteLine("currentAnimation: " + currentAnimation.ToString() + " previousAnimation: " + previousAnimation.ToString());
                previousAnimation = currentAnimation;
                previousAnimation.reset();
            }

            currentAnimation.Update(gameTime);
            playerLocationHandler.Update(gameTime, playerState, currentAnimation);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            currentAnimation.Draw(spriteBatch, location);
        }

    }
}
