using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PlayerStateTransition
    {
        PlayerStateAnimation state;
        PlayerAction action;

        public PlayerStateTransition(PlayerStateAnimation state, PlayerAction action)
        {
            this.state = state;
            this.action = action;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * state.GetHashCode() + 31 * action.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            PlayerStateTransition other = obj as PlayerStateTransition;
            return other != null && this.state == other.state && this.action == other.action;
        }

    }
}
