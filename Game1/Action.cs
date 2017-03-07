using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
   class Action
    {
        private int frameRate;

        public Action(int frameRate)
        {
            this.frameRate = frameRate;
        }
        
        public int getFrameRate()
        {
            return frameRate;
        }
    }
}
