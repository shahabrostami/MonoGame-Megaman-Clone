using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PlayerStates
    {
        public static PlayerState RUN_RIGHT = new PlayerState("RUN_RIGHT", Direction.RIGHT);
        public static PlayerState RUN_LEFT = new PlayerState("RUN_LEFT", Direction.LEFT);
        public static PlayerState JUMP_RIGHT = new PlayerState("JUMP_RIGHT", Direction.RIGHT);
        public static PlayerState JUMP_UP_RIGHT = new PlayerState("JUMP_UP_RIGHT", Direction.RIGHT);
        public static PlayerState JUMP_LEFT = new PlayerState("JUMP_LEFT", Direction.LEFT);
        public static PlayerState JUMP_UP_LEFT = new PlayerState("JUMP_UP_LEFT", Direction.LEFT);
        public static PlayerState STAND_RIGHT = new PlayerState("STAND_RIGHT", Direction.RIGHT);
        public static PlayerState STAND_LEFT = new PlayerState("STAND_LEFT", Direction.LEFT);

        private static BasePlayerAnimation run;
        private static BasePlayerAnimation stand;
        private static BasePlayerAnimation jump;

        public static void Load(Player player, Texture2D texture, int rows, int columns)
        {
            SpriteSpec playerSpriteSpec = new SpriteSpec(texture, rows, columns);

            run = new PlayerAnimationRun(playerSpriteSpec, true, 100,
                                            new SpriteLocation(Direction.RIGHT, 0, 3, 6),
                                            new SpriteLocation(Direction.LEFT, 1, 3, 6));

            stand = new PlayerAnimationBlink(playerSpriteSpec, true, 2000, 200,
                                                new SpriteLocation(Direction.RIGHT, 0, 0, 2),
                                                new SpriteLocation(Direction.LEFT, 1, 0, 2));

            jump = new PlayerAnimationJump(playerSpriteSpec, false, 100,
                                                new SpriteLocation(Direction.RIGHT, 0, 2, 3),
                                                new SpriteLocation(Direction.LEFT, 1, 2, 3));
            RUN_RIGHT.animation = run;
            RUN_LEFT.animation = run;
            JUMP_RIGHT.animation = jump;
            JUMP_LEFT.animation = jump;
            JUMP_UP_LEFT.animation = jump;
            JUMP_UP_RIGHT.animation = jump;
            STAND_RIGHT.animation = stand;
            STAND_LEFT.animation = stand;

        }
    }
}
