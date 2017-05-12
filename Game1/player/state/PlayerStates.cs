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
        public static PlayerStateAnimation RUN = new PlayerStateAnimation("RUN");
        public static PlayerStateAnimation JUMP = new PlayerStateAnimation("JUMP");
        public static PlayerStateAnimation STAND = new PlayerStateAnimation("STAND");
        public static PlayerStateAnimation FALL = new PlayerStateAnimation("FALL");
        public static PlayerStateAnimation HURT = new PlayerStateAnimation("HURT");

        public static void LoadContent(Player player, Texture2D texture, Sprite sprite)
        {
            SpriteSpec playerSpriteSpec = new SpriteSpec(texture, sprite.offset, sprite.rows, sprite.columns);

            BasePlayerAnimation stand = new PlayerAnimationBlink(player, playerSpriteSpec, sprite.animations[0]);
            BasePlayerAnimation run = new PlayerAnimationRun(player, playerSpriteSpec, sprite.animations[1]);
            BasePlayerAnimation jump = new PlayerAnimationJump(player, playerSpriteSpec, sprite.animations[2]);
            BasePlayerAnimation fall = new PlayerAnimationFall(player, playerSpriteSpec, sprite.animations[2]);
            BasePlayerAnimation hurt = new PlayerAnimationHurt(player, playerSpriteSpec, sprite.animations[3]);


            RUN.animation = run;
            JUMP.animation = jump;
            STAND.animation = stand;
            FALL.animation = fall;
            HURT.animation = hurt;
        }
    }
}
