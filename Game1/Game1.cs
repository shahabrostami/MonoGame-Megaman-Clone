using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyObjects;

namespace Game1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Map map;
        Camera camera;
        SpriteFont Arial;
        Vector2 DebugPos;
        string debugText;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player();
            map = new Map(20, player);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            camera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Arial = Content.Load<SpriteFont>("Arial");
            map.LoadContent(GraphicsDevice, Content);
            player.LoadContent(GraphicsDevice, Content);
            DebugPos = new Vector2(100,20);

        }

        protected override void UnloadContent()
        {
            //texture.Dispose(); <-- Only directly loaded
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.
                Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            camera.Update(player, gameTime);
            debugText = player.getDebugInfo();
            debugText = debugText + "\nMouse: (" + Mouse.GetState().X + "," + Mouse.GetState().Y + ")";
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            var updateMatrix = camera.getViewMatrix();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: updateMatrix);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin();
            
            Vector2 FontOrigin = Arial.MeasureString(debugText) / 2;
            spriteBatch.DrawString(Arial, debugText, DebugPos, Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}