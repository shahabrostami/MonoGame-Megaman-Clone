using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    class AnimationCycle
    {
        private Vector2 rightDisplacement = new Vector2();
        private Vector2 leftDisplacement = new Vector2();
        private int msPerCycle;

        public AnimationCycle(int x, int y, int msPerCycle) {
            rightDisplacement.X = x;
            rightDisplacement.Y = y;
            leftDisplacement.X = x * -1;
            leftDisplacement.Y = y;
            this.msPerCycle = msPerCycle;
        }
        
        public Vector2 getRightDisplacement()
        {
            return rightDisplacement;
        }

        public Vector2 getLeftDisplacement()
        {
            return leftDisplacement;
        }
    }
}
