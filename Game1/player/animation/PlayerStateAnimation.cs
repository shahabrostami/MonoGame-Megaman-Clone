using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PlayerStateAnimation
    {
        private String desc;
        public BasePlayerAnimation animation { get; set; }

        public PlayerStateAnimation(String desc)
        {
            this.desc = desc;
        }
        public override String ToString()
        {
            return desc;
        }

    }
}
