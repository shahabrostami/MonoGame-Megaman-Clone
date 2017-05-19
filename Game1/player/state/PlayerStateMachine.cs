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
                { new PlayerStateTransition(PlayerStates.STAND, Action.MOVE), PlayerStates.RUN },
                { new PlayerStateTransition(PlayerStates.STAND, Action.JUMP), PlayerStates.JUMP},
                { new PlayerStateTransition(PlayerStates.STAND, Action.HIT), PlayerStates.DAMAGED},
                { new PlayerStateTransition(PlayerStates.RUN, Action.JUMP), PlayerStates.JUMP},
                { new PlayerStateTransition(PlayerStates.RUN, Action.STOP), PlayerStates.STAND},
                { new PlayerStateTransition(PlayerStates.RUN, Action.HIT), PlayerStates.DAMAGED},
                { new PlayerStateTransition(PlayerStates.RUN, Action.FALL), PlayerStates.FALL},
                { new PlayerStateTransition(PlayerStates.JUMP, Action.HIT), PlayerStates.DAMAGED},
                { new PlayerStateTransition(PlayerStates.FALL, Action.LAND), PlayerStates.STAND},
                { new PlayerStateTransition(PlayerStates.FALL, Action.HIT), PlayerStates.DAMAGED}
            };
        }

        public PlayerStateAnimation Update(Action playerAction)
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
