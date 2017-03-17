using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    abstract class BasePlayerAnimation
    {
        private SpriteSpec spriteSpec;
        protected SpriteLocation rightSprite;
        protected SpriteLocation leftSprite;
        protected SpriteLocation currentSprite;
        protected Direction direction;
        protected bool loopFinished;
        protected bool loopAnimation;
        protected int currentFrame;
        protected int msPerFrame;
        protected int timeSinceLastFrame = 0;

        public BasePlayerAnimation(SpriteSpec spriteSpec, bool loopAnimation, int msPerFrame, SpriteLocation rightSprite, SpriteLocation leftSprite)
        {
            this.spriteSpec = spriteSpec;
            this.loopFinished = false;
            this.msPerFrame = msPerFrame;
            this.leftSprite = leftSprite;
            this.rightSprite = rightSprite;
            this.loopAnimation = loopAnimation;
            this.currentSprite = rightSprite;
            this.direction = Direction.RIGHT;
            currentFrame = rightSprite.sF;
        }

        public abstract void Update(GameTime gameTime);

        public abstract Vector2 updateLocation(PlayerState state);

        public abstract void updateAnimationCycle(PlayerState state);

        public abstract void reset();

        public abstract bool hasMovement();

        public void updateDirection(Direction direction)
        {
            this.direction = direction;
            if (direction == Direction.RIGHT)
                currentSprite = rightSprite;
            else
                currentSprite = leftSprite;
        }

        public bool isLoopFinished()
        {
            return loopFinished;
        }
        

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteSpec.Draw(spriteBatch, location, currentSprite.row, currentFrame);
        }
    }


}
