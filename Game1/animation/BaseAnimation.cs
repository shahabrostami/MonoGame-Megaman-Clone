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
    abstract class BaseAnimation
    {
        protected readonly Vector2 gravity = new Vector2(0, 9.8f);
        AnimationSpec[] animationSpecs;

        private SpriteSpec spriteSpec;
        protected Direction direction;
        protected AnimationCycleSpec[] cycles;

        protected bool loopAnimation;
        protected bool loopFinished = false;

        protected AnimationCycleSpec currentCycle;
        protected AnimationFrameSpec currentFrame;
        protected int currentFrameIndex;
        protected float timeSinceLastFrame = 0;
        protected Vector2 velocity;

        public Vector2 textureOffset;
        public Vector2 textureSize;

        public BaseAnimation(SpriteSpec spriteSpec, AnimationSpec[] animationSpecs) : this(spriteSpec)
        {
            this.animationSpecs = animationSpecs;
            updateAnimationSpec(animationSpecs[0]);
        }

        public BaseAnimation(SpriteSpec spriteSpec, AnimationSpec animationSpec) : this(spriteSpec)
        {
            this.animationSpecs = new AnimationSpec[1];
            animationSpecs[0] = animationSpec;
            updateAnimationSpec(animationSpec);
        }

        public BaseAnimation(SpriteSpec spriteSpec)
        {
            this.spriteSpec = spriteSpec;
            this.loopFinished = false;
            this.textureSize = new Vector2(spriteSpec.width, spriteSpec.height);
            this.textureOffset = spriteSpec.textureOffset;
        }

        public void updateAnimationSpec(AnimationSpec animationSpec)
        {
            this.loopAnimation = animationSpec.loop;
            this.cycles = animationSpec.cycles;
            this.direction = Direction.RIGHT;
            this.currentCycle = cycles[0];
            if (cycles[0].frames != null)
                this.currentFrame = currentCycle.frames[0];
            else
            {
                currentCycle.frames = new AnimationFrameSpec[currentCycle.ef];
                this.currentFrame = currentCycle.frames[0];
            }
            this.currentFrameIndex = currentCycle.sf;
            reset();
        }

        public abstract bool Update(GameTime gameTime);

        public abstract void reset();

        public abstract bool hasMovement();

        public void updateCycle(AnimationCycleSpec cycle)
        {
            if (cycle != currentCycle)
            {
                currentCycle = cycle;
                if (currentFrameIndex > currentCycle.ef)
                    currentFrameIndex = currentCycle.ef;
                else if (currentFrameIndex < currentCycle.sf)
                    currentFrameIndex = currentCycle.sf;
            }
        }

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
