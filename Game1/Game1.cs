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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprite playerSprite = Content.Load<Sprite[]>("spritetest")[0];
            Texture2D playerTexture = Content.Load<Texture2D>(playerSprite.textureName);
            player.Load(playerTexture, playerSprite);

            map = new Map(GraphicsDevice, this.GraphicsDevice.Viewport.Bounds.Width, this.GraphicsDevice.Viewport.Bounds.Height);
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}