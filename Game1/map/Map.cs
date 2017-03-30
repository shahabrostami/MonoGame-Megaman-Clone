using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Map
    {
        int[,] mapTiles;
        int width, height;
        int tiles;
        Texture2D whiteTile;
        Texture2D blackTile;

        public Map(int tiles)
        {
            this.tiles = tiles;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            width = GraphicsDevice.Viewport.Bounds.Width / tiles;
            height = GraphicsDevice.Viewport.Bounds.Height / tiles;

            mapTiles = new int[20, 20];

            for (int i = 0; i < tiles; i++)
                for (int j = 0; j < tiles; j++)
                    if (i == 19)
                        mapTiles[i, j] = 1;
                    else
                        mapTiles[i, j] = 0;

            whiteTile = new Texture2D(GraphicsDevice, 1, 1);
            blackTile = new Texture2D(GraphicsDevice, 1, 1);
            whiteTile.SetData(new[] { Color.White });
            blackTile.SetData(new[] { Color.Black });
        }
        

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            for (int k = 0; k < mapTiles.GetLength(0); k++)
                for (int l = 0; l < mapTiles.GetLength(1); l++) {
                    if (mapTiles[k, l] == 1)
                        spriteBatch.Draw(whiteTile, new Rectangle(l* width, k * height, width, height), Color.Chocolate);
                    else if (mapTiles[k, l] == 0)
                        spriteBatch.Draw(blackTile, new Rectangle(l* width, k * height, width, height), Color.Chocolate);

                }
            spriteBatch.End();

        }

    }
}
