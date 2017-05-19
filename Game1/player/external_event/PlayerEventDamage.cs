using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    class PlayerEventDamaged : MovingObjectEvent
    {
        public PlayerEventDamaged(Player player) :
            base (EventType.DAMAGED, player)
        {
        }

        public override Action Handle()
        {
            return Action.HIT;
        }
    }
}
