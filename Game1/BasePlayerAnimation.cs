using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyObjects;
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
        protected Direction direction;
        protected AnimationCycleSpec[] cycles;

        protected bool loopAnimation;
        protected bool loopFinished;

        protected AnimationCycleSpec currentCycle;
        protected AnimationFrameSpec currentFrame;
        protected int currentFrameIndex;
        protected int timeSinceLastFrame = 0;

        public BasePlayerAnimation(SpriteSpec spriteSpec, AnimationSpec animation)
        {
            this.spriteSpec = spriteSpec;
            this.loopFinished = false;
            this.loopAnimation = animation.loop;
            this.cycles = animation.cycles;
            this.direction = Direction.RIGHT;
            this.currentCycle = cycles[0];
            this.currentFrame = currentCycle.frames[0];
            this.currentFrameIndex = currentCycle.sf;
        }

        public abstract bool Update(GameTime gameTime);

        public abstract Vector2 updateLocation(PlayerState state);

        public abstract void reset();

        public abstract bool hasMovement();

        public abstract void updateDirection(Direction direction);

        public abstract void updateOnAction(PlayerState pState, PlayerAction pAction);

        public bool isLoopFinished()
        {
            return loopFinished;
        }
        

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteSpec.Draw(spriteBatch, location, currentCycle.row, currentFrameIndex);
        }
    }


}
