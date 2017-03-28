using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PlayerState
    {
        private String desc;
        public BasePlayerAnimation animation { get; set; }

        public PlayerState(String desc)
        {
            this.desc = desc;
        }
        public override String ToString()
        {
            return desc;
        }

    }
}
