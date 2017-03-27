using Microsoft.Xna.Framework.Graphics;
using MyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PlayerStates
    {
        public static PlayerState RUN_RIGHT = new PlayerState("RUN_RIGHT", Direction.RIGHT, PlayerAction.MOVE_RIGHT);
        public static PlayerState RUN_LEFT = new PlayerState("RUN_LEFT", Direction.LEFT, PlayerAction.MOVE_LEFT);
        public static PlayerState JUMP_RIGHT = new PlayerState("JUMP_RIGHT", Direction.RIGHT, PlayerAction.JUMP);
        public static PlayerState JUMP_LEFT = new PlayerState("JUMP_LEFT", Direction.LEFT, PlayerAction.JUMP);
        public static PlayerState STAND_RIGHT = new PlayerState("STAND_RIGHT", Direction.RIGHT, PlayerAction.STOP);
        public static PlayerState STAND_LEFT = new PlayerState("STAND_LEFT", Direction.LEFT, PlayerAction.STOP);
        /* public static PlayerState STAND_SHOOT_RIGHT = new PlayerState("STAND_RIGHT", Direction.RIGHT, PlayerAction.SHOOT);
        public static PlayerState STAND_SHOOT_LEFT = new PlayerState("STAND_LEFT", Direction.LEFT, PlayerAction.SHOOT);
        public static PlayerState RUN_SHOOT_RIGHT = new PlayerState("STAND_RIGHT", Direction.RIGHT, PlayerAction.SHOOT);
        public static PlayerState RUN_SHOOT_LEFT = new PlayerState("STAND_LEFT", Direction.LEFT, PlayerAction.SHOOT);
        public static PlayerState JUMP_SHOOT_RIGHT = new PlayerState("STAND_RIGHT", Direction.RIGHT, PlayerAction.SHOOT);
        public static PlayerState JUMP_SHOOT_LEFT = new PlayerState("STAND_LEFT", Direction.LEFT, PlayerAction.SHOOT);
        */

        private static BasePlayerAnimation run;
        private static BasePlayerAnimation stand;
        private static BasePlayerAnimation jump;

        public static void Load(Player player, Texture2D texture, Sprite sprite)
        {
            SpriteSpec playerSpriteSpec = new SpriteSpec(texture, sprite.rows, sprite.columns);

            stand = new PlayerAnimationBlink(player, playerSpriteSpec, sprite.animations[0]);

            run = new PlayerAnimationRun(player, playerSpriteSpec, sprite.animations[1]);
            
            jump = new PlayerAnimationJump(player, playerSpriteSpec, sprite.animations[2]);

            RUN_RIGHT.animation = run;
            RUN_LEFT.animation = run;
            JUMP_RIGHT.animation = jump;
            JUMP_LEFT.animation = jump;
            STAND_RIGHT.animation = stand;
            STAND_LEFT.animation = stand;

        }
    }
}
