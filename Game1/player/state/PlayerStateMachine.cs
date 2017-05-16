using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PlayerStateMachine
    {
        public PlayerStateAnimation currentState { get; set; }
        public PlayerStateAnimation previousState { get; set; }

        Dictionary<PlayerStateTransition, PlayerStateAnimation> transitions;

        public PlayerStateMachine()
        {
            currentState = PlayerStates.STAND;

           transitions = new Dictionary<PlayerStateTransition, PlayerStateAnimation>
            {
                { new PlayerStateTransition(PlayerStates.STAND, PlayerAction.MOVE), PlayerStates.RUN },
                { new PlayerStateTransition(PlayerStates.STAND, PlayerAction.JUMP), PlayerStates.JUMP},
                { new PlayerStateTransition(PlayerStates.STAND, PlayerAction.HIT), PlayerStates.DAMAGED},
                { new PlayerStateTransition(PlayerStates.RUN, PlayerAction.JUMP), PlayerStates.JUMP},
                { new PlayerStateTransition(PlayerStates.RUN, PlayerAction.STOP), PlayerStates.STAND},
                { new PlayerStateTransition(PlayerStates.RUN, PlayerAction.HIT), PlayerStates.DAMAGED},
                { new PlayerStateTransition(PlayerStates.RUN, PlayerAction.FALL), PlayerStates.FALL},
                { new PlayerStateTransition(PlayerStates.JUMP, PlayerAction.HIT), PlayerStates.DAMAGED},
                { new PlayerStateTransition(PlayerStates.FALL, PlayerAction.LAND), PlayerStates.STAND}
            };
        }

        public PlayerStateAnimation Update(PlayerAction playerAction)
        {
            PlayerStateTransition transition = new PlayerStateTransition(currentState, playerAction);
            PlayerStateAnimation newState;
            if (!transitions.TryGetValue(transition, out newState))
            {
                newState = currentState;
            }

            if (currentState != newState)
            {
                previousState = currentState;
            }

            currentState = newState;

            return currentState;
        }

        public void revert()
        {
            currentState.animation.reset();
            currentState = PlayerStates.STAND;
        }
    }
}
