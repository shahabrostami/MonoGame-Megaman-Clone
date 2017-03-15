using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PlayerState
    {
        private Direction direction;
        private String desc;
        public BasePlayerAnimation animation { get;  set; }

        public PlayerState(String desc, Direction direction)
        {
            this.desc = desc;
            this.direction = direction;
        }
        
        public Direction getDirection()
        {
            return direction;
        }

        public override String ToString()
        {
            return desc;
        }

    }
}
