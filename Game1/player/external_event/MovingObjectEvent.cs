using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    abstract class MovingObjectEvent
    {
        EventType type;
        protected MovingObject movingObj;

        public MovingObjectEvent(EventType type, MovingObject movingObj)
        {
            this.movingObj = movingObj;
            this.type = type;
        }

        public abstract Action Handle();
    }
}
