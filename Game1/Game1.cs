using Game1.enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyObjects;

namespace Game1
{
    public class Game1 : Game
    {
        // Graphic Objects
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game Objects
        Player player;
        Map map;
        Camera camera;
        EnemyFactory enemyFactory;

        // Font Objects
        SpriteFont Arial;
        Vector2 DebugPos;

        string debugText;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player();
            enemyFactory = new EnemyFactory();
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
            //Font
            Arial = Content.Load<SpriteFont>("Arial");
            DebugPos = new Vector2(80, 50);
            //Game
            map.LoadContent(GraphicsDevice, Content);
            player.LoadContent(GraphicsDevice, Content);
            enemyFactory.LoadContent(GraphicsDevice, Content);

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
            enemyFactory.Update(gameTime);
            debugText = player.getDebugInfo();
            debugText = debugText + "\nMouse: (" + Mouse.GetState().X + "," + Mouse.GetState().Y + ")";
            debugText = debugText + "\n" + map.GetDebugInfo();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            var updateMatrix = camera.getViewMatrix();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: updateMatrix);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemyFactory.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin();
            Vector2 FontOrigin = Arial.MeasureString(debugText) / 2;
            spriteBatch.DrawString(Arial, debugText, DebugPos, Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}