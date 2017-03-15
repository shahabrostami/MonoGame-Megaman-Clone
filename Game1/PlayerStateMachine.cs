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
            currentState = PlayerStates.STAND_RIGHT;

            transitions = new Dictionary<PlayerStateTransition, PlayerState>
            {
                { new PlayerStateTransition(PlayerStates.STAND_RIGHT, PlayerAction.MOVE_RIGHT), PlayerStates.RUN_RIGHT },
                { new PlayerStateTransition(PlayerStates.STAND_RIGHT, PlayerAction.MOVE_LEFT), PlayerStates.RUN_LEFT},
                { new PlayerStateTransition(PlayerStates.STAND_RIGHT, PlayerAction.JUMP), PlayerStates.JUMP_UP_RIGHT},
                //{ new PlayerStateTransition(PlayerStates.STAND_RIGHT, PlayerAction.STOP), PlayerStates.STAND_RIGHT},
                { new PlayerStateTransition(PlayerStates.STAND_LEFT, PlayerAction.MOVE_RIGHT), PlayerStates.RUN_RIGHT },
                { new PlayerStateTransition(PlayerStates.STAND_LEFT, PlayerAction.MOVE_LEFT), PlayerStates.RUN_LEFT},
                { new PlayerStateTransition(PlayerStates.STAND_LEFT, PlayerAction.JUMP), PlayerStates.JUMP_UP_LEFT},
                //{ new PlayerStateTransition(PlayerStates.STAND_LEFT, PlayerAction.STOP), PlayerStates.STAND_LEFT},
                //{ new PlayerStateTransition(PlayerStates.RUN_RIGHT, PlayerAction.MOVE_RIGHT), PlayerStates.RUN_RIGHT },
                { new PlayerStateTransition(PlayerStates.RUN_RIGHT, PlayerAction.MOVE_LEFT), PlayerStates.RUN_LEFT},
                { new PlayerStateTransition(PlayerStates.RUN_RIGHT, PlayerAction.JUMP), PlayerStates.JUMP_RIGHT},
                { new PlayerStateTransition(PlayerStates.RUN_RIGHT, PlayerAction.STOP), PlayerStates.STAND_RIGHT},
                { new PlayerStateTransition(PlayerStates.RUN_LEFT, PlayerAction.MOVE_RIGHT), PlayerStates.RUN_RIGHT },
                //{ new PlayerStateTransition(PlayerStates.RUN_LEFT, PlayerAction.MOVE_LEFT), PlayerStates.RUN_LEFT},
                { new PlayerStateTransition(PlayerStates.RUN_LEFT, PlayerAction.JUMP), PlayerStates.JUMP_LEFT},
                { new PlayerStateTransition(PlayerStates.RUN_LEFT, PlayerAction.STOP), PlayerStates.STAND_LEFT},
                { new PlayerStateTransition(PlayerStates.JUMP_LEFT, PlayerAction.MOVE_RIGHT), PlayerStates.JUMP_RIGHT },
                //{ new PlayerStateTransition(PlayerStates.JUMP_LEFT, PlayerAction.MOVE_LEFT), PlayerStates.JUMP_LEFT},
                //{ new PlayerStateTransition(PlayerStates.JUMP_LEFT, PlayerAction.JUMP), PlayerStates.JUMP_LEFT},
                { new PlayerStateTransition(PlayerStates.JUMP_LEFT, PlayerAction.STOP), PlayerStates.JUMP_UP_LEFT},
                //{ new PlayerStateTransition(PlayerStates.JUMP_RIGHT, PlayerAction.MOVE_RIGHT), PlayerStates.JUMP_RIGHT },
                { new PlayerStateTransition(PlayerStates.JUMP_RIGHT, PlayerAction.MOVE_LEFT), PlayerStates.JUMP_LEFT},
                //{ new PlayerStateTransition(PlayerStates.JUMP_RIGHT, PlayerAction.JUMP), PlayerStates.JUMP_RIGHT},
                { new PlayerStateTransition(PlayerStates.JUMP_RIGHT, PlayerAction.STOP), PlayerStates.JUMP_UP_RIGHT},
                { new PlayerStateTransition(PlayerStates.JUMP_UP_LEFT, PlayerAction.MOVE_RIGHT), PlayerStates.JUMP_RIGHT },
                { new PlayerStateTransition(PlayerStates.JUMP_UP_LEFT, PlayerAction.MOVE_LEFT), PlayerStates.JUMP_LEFT},
               // { new PlayerStateTransition(PlayerStates.JUMP_UP_LEFT, PlayerAction.JUMP), PlayerStates.JUMP_UP_LEFT},
               // { new PlayerStateTransition(PlayerStates.JUMP_UP_LEFT, PlayerAction.STOP), PlayerStates.JUMP_UP_LEFT},
                { new PlayerStateTransition(PlayerStates.JUMP_UP_RIGHT, PlayerAction.MOVE_RIGHT), PlayerStates.JUMP_RIGHT },
                { new PlayerStateTransition(PlayerStates.JUMP_UP_RIGHT, PlayerAction.MOVE_LEFT), PlayerStates.JUMP_LEFT},
                //{ new PlayerStateTransition(PlayerStates.JUMP_UP_RIGHT, PlayerAction.JUMP), PlayerStates.JUMP_UP_RIGHT},
                //{ new PlayerStateTransition(PlayerStates.JUMP_UP_RIGHT, PlayerAction.STOP), PlayerStates.JUMP_UP_RIGHT},
            };
        }

        public PlayerState Update(PlayerAction playerAction)
        {
            PlayerStateTransition transition = new PlayerStateTransition(currentState, playerAction);
            PlayerState newState;
            Console.WriteLine("Transition: " + playerAction + " -> " + currentState);
            if (!transitions.TryGetValue(transition, out newState))
            {
                newState = currentState;
            }
            else
                currentState.animation.reset();

            if (currentState != newState)
                previousState = currentState;

            currentState = newState;

            return currentState;
        }
    }
}
