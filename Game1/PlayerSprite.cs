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
        private BasePlayerAnimation run;
        private BasePlayerAnimation stand;
        private BasePlayerAnimation jump;
       
        public PlayerSprite(Player player, Texture2D texture, int rows, int columns)
        {
            this.player = player;
            playerLocationHandler = new PlayerLocationHandler(player);
            SpriteSpec playerSpriteSpec = new SpriteSpec(texture, rows, columns);

            run = new PlayerAnimationRun(playerSpriteSpec, true, 100, 
                                            new SpriteLocation(Direction.RIGHT, 0, 3, 6), 
                                            new SpriteLocation(Direction.LEFT, 1, 3, 6));

            stand = new PlayerAnimationBlink(playerSpriteSpec, true, 2000, 200, 
                                                new SpriteLocation(Direction.RIGHT, 0, 0, 2), 
                                                new SpriteLocation(Direction.LEFT, 1, 0, 2));

            jump = new PlayerAnimationJump(playerSpriteSpec, false, 100, 
                                                new SpriteLocation(Direction.RIGHT, 0, 2, 3), 
                                                new SpriteLocation(Direction.LEFT, 1, 2, 3));
            currentAnimation = stand;
            previousAnimation = run;
        }

        public void Update(GameTime gameTime, PlayerState playerState)
        {
            currentAnimation.updateDirection(playerState.getDirection());

            if (currentAnimation.isLoopFinished())
            {
                if (playerState == PlayerStates.RUN_RIGHT)
                {
                    currentAnimation = run;
                }
                else if (playerState == PlayerStates.RUN_LEFT)
                {
                    currentAnimation = run;
                }
                else if (playerState == PlayerStates.JUMP_RIGHT)
                {
                    currentAnimation = jump;
                }
                else if (playerState == PlayerStates.JUMP_LEFT)
                {
                    currentAnimation = jump;
                }
                else if (playerState == PlayerStates.STAND_RIGHT)
                {
                    currentAnimation = stand;
                }
                else if (playerState == PlayerStates.STAND_LEFT)
                {
                    currentAnimation = stand;
                }

                if (previousAnimation != currentAnimation)
                {
                    Console.WriteLine("currentAnimation: " + currentAnimation.ToString() + " previousAnimation: " + previousAnimation.ToString());
                    previousAnimation = currentAnimation;
                    previousAnimation.reset();
                }
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
