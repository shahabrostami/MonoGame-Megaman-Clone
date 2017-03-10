using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    class PlayerAnimation : BasePlayerAnimation
    {
        public PlayerAnimation(Texture2D texture, int rows, int columns, int row, int startingFrame, int endFrame, int msPerFrame) : 
            base(texture, rows, columns, row, startingFrame, endFrame, msPerFrame)
        {
            // Standard animation, likely to change...
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > msPerFrame)
            {
                timeSinceLastFrame -= msPerFrame;

                currentFrame++;
                if (currentFrame == endFrame)
                    currentFrame = startingFrame;
            }
        }

}
}
