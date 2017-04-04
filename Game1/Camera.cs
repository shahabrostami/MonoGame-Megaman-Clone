using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Camera
    {
        private readonly Viewport viewport;

        private Vector2 Position;
        private float Rotation;
        private float Zoom;
        private Vector2 Origin;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;
        }

        public void Update(Player player, GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Home))
            {
                Position.Y -= 1.0F;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.End))
            {
                Position.Y += 1.0F;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Delete))
            {
                Position.X -= 1.0F;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                Position.X += 1.0F;
            }

            Position.X = player.location.X - Origin.X;
            Position.Y = player.location.Y - Origin.Y;
        }

        public Matrix getViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }
    }
}
