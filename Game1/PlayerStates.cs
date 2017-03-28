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
        public static PlayerState RUN = new PlayerState("RUN_RIGHT");
        public static PlayerState JUMP = new PlayerState("RUN_LEFT");
        public static PlayerState STAND = new PlayerState("STAND_RIGHT");
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

            RUN.animation = run;
            JUMP.animation = jump;
            STAND.animation = stand;

        }
    }
}
