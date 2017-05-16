using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    class PlayerEventFallStop : PlayerEvent
    {
        public PlayerEventFallStop(Player player) :
            base (PlayerEventType.STOP_FALLING, player)
        {
        }

        public override PlayerAction Handle()
        {
            PlayerStates.FALL.animation.reset();
            return PlayerAction.LAND;
        }
    }
}
