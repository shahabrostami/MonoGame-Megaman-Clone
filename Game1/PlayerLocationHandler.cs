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

        public PlayerLocationHandler(Player player)
        {
            this.player = player;
        }

        public void Update(GameTime gameTime, PlayerState playerState, BasePlayerAnimation playerAnimation)
        {
            if(playerAnimation.hasMovement())
                player.updateLocation(playerAnimation.updateLocation());
        }
    }
}
