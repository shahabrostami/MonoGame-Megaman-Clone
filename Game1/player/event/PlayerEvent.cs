using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.player
{
    abstract class PlayerEvent
    {
        PlayerEventType type;
        protected Player player;

        public PlayerEvent(PlayerEventType type, Player player)
        {
            this.player = player;
            this.type = type;
        }

        public abstract void Handle();
    }
}
