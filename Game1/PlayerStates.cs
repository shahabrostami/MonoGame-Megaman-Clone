using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PlayerStates
    {
        public static PlayerState RUN_RIGHT = new PlayerState("RUN_RIGHT", Direction.RIGHT);
        public static PlayerState RUN_LEFT = new PlayerState("RUN_LEFT", Direction.LEFT);
        public static PlayerState JUMP_RIGHT = new PlayerState("JUMP_RIGHT", Direction.RIGHT);
        public static PlayerState JUMP_LEFT = new PlayerState("JUMP_LEFT", Direction.LEFT);
        public static PlayerState STAND_RIGHT = new PlayerState("STAND_RIGHT", Direction.RIGHT);
        public static PlayerState STAND_LEFT = new PlayerState("STAND_LEFT", Direction.LEFT);
    }
}
