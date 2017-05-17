using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    class PlayerEventLanded : PlayerEvent
    {
        public PlayerEventLanded(Player player) :
            base (PlayerEventType.LANDED, player)
        {
        }

        public override PlayerAction Handle()
        {
            PlayerStates.FALL.animation.reset();
            player.grounded = true;
            return PlayerAction.LAND;
        }
    }
}
