using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class SpriteLocation
    {
        public Direction direction;
        public int row;
        public int sF;
        public int eF;

        public SpriteLocation(Direction direction, int row, int sF, int eF)
        {
            this.direction = direction;
            this.row = row;
            this.sF = sF;
            this.eF = eF;
        }

    }
}
