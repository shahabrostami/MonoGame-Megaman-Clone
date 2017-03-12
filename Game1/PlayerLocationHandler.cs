using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class PlayerLocationHandler
    {
        private Player player;
        private Vector2 moveRight = new Vector2(3, 0);
        private Vector2 moveLeft = new Vector2(-3, 0);

        public PlayerLocationHandler(Player player)
        {
            this.player = player;
        }

        public void Update(GameTime gameTime, PlayerState playerState, BasePlayerAnimation playerAnimation)
        {

            if (playerState == PlayerStates.RUN_RIGHT)
            {
                player.updateLocation(moveRight);
            }
            else if (playerState == PlayerStates.RUN_LEFT)
            {
                player.updateLocation(moveLeft);
            }
            else if (playerState == PlayerStates.JUMP_RIGHT)
                player.updateLocation(playerAnimation.updateLocation());

        }

    }
}
