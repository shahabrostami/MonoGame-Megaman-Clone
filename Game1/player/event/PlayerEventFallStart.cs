using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    class PlayerEventFallStart : PlayerEvent
    {
        public PlayerEventFallStart(Player player) :
            base (PlayerEventType.START_FALLING, player)
        {
        }

        public override PlayerAction Handle()
        {
            return PlayerAction.FALL;
        }
    }
}
