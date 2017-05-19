using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    class PlayerEventLanded : MovingObjectEvent
    {
        public PlayerEventLanded(MovingObject movingObj) :
            base (EventType.LANDED, movingObj)
        {
        }

        public override Action Handle()
        {
            return Action.LAND;
        }
    }
}
