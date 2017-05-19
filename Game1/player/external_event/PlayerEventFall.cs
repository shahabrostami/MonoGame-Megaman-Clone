using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    class PlayerEventFall : MovingObjectEvent
    {
        Player player;

        public PlayerEventFall(Player player) :
            base (EventType.FALLING, player)
        {
            this.player = player;
        }

        public override Action Handle()
        {
            return Action.FALL;
        }
    }
}
