using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyObjects
{
    public class Sprite
    {
        public string textureName;
        public int rows;
        public int columns;
        public Vector2 offset;
        public Vector2 actualTextureSize;
        public AnimationSpec[] animations;
    }

    public class AnimationSpec
    {
        public string name;
        public bool loop;
        public AnimationCycleSpec[] cycles;
    }

    public class AnimationCycleSpec
    {
        public int row;
        [ContentSerializerAttribute(Optional = true)]
        public int sf;
        [ContentSerializerAttribute(Optional = true)]
        public int ef;
        [ContentSerializerAttribute(Optional = true)]
        public Vector2 velocity;
        [ContentSerializerAttribute(Optional = true)]
        public int ms;
        [ContentSerializerAttribute(Optional = true)]
        public AnimationFrameSpec[] frames;
    }
    
    public class AnimationFrameSpec
    {
        [ContentSerializerAttribute(Optional = true)]
        public Vector2 velocity;
        [ContentSerializerAttribute(Optional = true)]
        public int ms = 0;
    }
    

}
