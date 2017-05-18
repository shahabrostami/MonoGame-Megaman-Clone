using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    class PlayerEventFall : PlayerEvent
    {
        public PlayerEventFall(Player player) :
            base (PlayerEventType.FALLING, player)
        {
        }

        public override PlayerAction Handle()
        {
            player.grounded = false;
            return PlayerAction.FALL;
        }
    }
}
