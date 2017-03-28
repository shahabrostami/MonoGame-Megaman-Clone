using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PlayerStateMachine
    {
        public PlayerState currentState { get; set; }
        public PlayerState previousState { get; set; }

        Dictionary<PlayerStateTransition, PlayerState> transitions;

        public PlayerStateMachine()
        {
            currentState = PlayerStates.STAND;

            transitions = new Dictionary<PlayerStateTransition, PlayerState>
            {
                { new PlayerStateTransition(PlayerStates.STAND, PlayerAction.MOVE_RIGHT), PlayerStates.RUN },
                { new PlayerStateTransition(PlayerStates.STAND, PlayerAction.MOVE_LEFT), PlayerStates.RUN},
                { new PlayerStateTransition(PlayerStates.STAND, PlayerAction.JUMP), PlayerStates.JUMP},
                { new PlayerStateTransition(PlayerStates.RUN, PlayerAction.JUMP), PlayerStates.JUMP},
                { new PlayerStateTransition(PlayerStates.RUN, PlayerAction.STOP), PlayerStates.STAND}
            };
        }

        public PlayerState Update(PlayerAction playerAction)
        {
            PlayerStateTransition transition = new PlayerStateTransition(currentState, playerAction);
            PlayerState newState;
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
