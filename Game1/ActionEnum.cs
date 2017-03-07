using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class ActionEnum
    {
        public static Action MOVE_RIGHT = new Action(100);
        public static Action MOVE_LEFT = new Action(100);
        public static Action MOVE_UP = new Action(100);
        public static Action MOVE_DOWN = new Action(100);
        public static Action NONE = new Action(500);
        public static Action NONE_BLINK = new Action(100);
        public static Action JUMP = new Action(500);
    }
}
