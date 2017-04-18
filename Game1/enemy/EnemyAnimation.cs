using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.enemy
{
    class EnemyAnimation
    {
        private String desc;
        private int type;
        public BaseAnimation animation { get; set; }

        public EnemyAnimation(int type, String desc)
        {
            this.type = type;
            this.desc = desc;
        }

        public int getType ()
        {
            return type;
        }
        public override String ToString()
        {
            return desc;
        }

    }
}
