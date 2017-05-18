using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    class PlayerEventDamaged : PlayerEvent
    {
        public PlayerEventDamaged(Player player) :
            base (PlayerEventType.DAMAGED, player)
        {
        }

        public override PlayerAction Handle()
        {
            return PlayerAction.HIT;
        }
    }
}
