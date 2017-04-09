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
        public static PlayerState RUN = new PlayerState("RUN");
        public static PlayerState JUMP = new PlayerState("JUMP");
        public static PlayerState STAND = new PlayerState("STAND");
        public static PlayerState FALL = new PlayerState("FALL");

        public static void LoadContent(Player player, Texture2D texture, Sprite sprite)
        {
            SpriteSpec playerSpriteSpec = new SpriteSpec(texture, sprite.rows, sprite.columns);

            BasePlayerAnimation stand = new PlayerAnimationBlink(player, playerSpriteSpec, sprite.animations[0]);

            BasePlayerAnimation run = new PlayerAnimationRun(player, playerSpriteSpec, sprite.animations[1]);

            BasePlayerAnimation jump = new PlayerAnimationJump(player, playerSpriteSpec, sprite.animations[2]);

            BasePlayerAnimation fall = new PlayerAnimationFall(player, playerSpriteSpec, sprite.animations[2]);

            RUN.animation = run;
            JUMP.animation = jump;
            STAND.animation = stand;
            FALL.animation = fall;
        }
    }
}
