using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Game1
{
    class Map
    {
        int width, height, heightDiff;
        int screenWidthCap, screenHeightCap;
        int tileWidth, tileHeight, mapWidth, mapHeight;
        int tiles;
        Texture2D textureTileset;
        TmxTileset tmxTileset;
        TmxLayer background, wall, collision;
        Player player;

        public Map(int tiles, Player player)
        {
            this.tiles = tiles;
            this.player = player;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            width = GraphicsDevice.Viewport.Bounds.Width;
            height = GraphicsDevice.Viewport.Bounds.Height;
            screenWidthCap = (width / 2) + 20;
            screenHeightCap = (height / 2) + 20;
            TmxMap map = new TmxMap("Content/testmap3.tmx");
            var version = map.Version;
            tmxTileset = map.Tilesets["KITileset"];
            background = map.Layers[0];
            wall = map.Layers[1];
            collision = map.Layers[2];
            tileWidth = tmxTileset.TileWidth;
            tileHeight = tmxTileset.TileHeight;
            mapWidth = map.Width;
            mapHeight = map.Height;

            textureTileset = Content.Load<Texture2D>(map.Tilesets[0].Name);
            heightDiff = (map.Height*tileHeight) - height;
            
            player.location = new Vector2((int)map.ObjectGroups[0].Objects[0].X, (int)map.ObjectGroups[0].Objects[0].Y - heightDiff);
        }


        public void Update()
        {
        }


        public void checkCollision()
        {
            int xTile = (int)player.location.X / tileWidth -1;
            int yTile = (height -(int)player.location.Y) / tileHeight -1;

            int tile = yTile * ((int)tmxTileset.Columns-1) + xTile;

            TmxLayerTile collisionTile = collision.Tiles[tile];
            if (collisionTile.Gid == 211)
                Console.WriteLine("true");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float capPosY = player.location.Y - screenHeightCap;
            float capNegY = player.location.Y + screenHeightCap;
            float capPosX = player.location.X + screenWidthCap;
            float capNegX = player.location.X - screenWidthCap;
            checkCollision();

            foreach (TmxLayerTile tile in background.Tiles)
            {
                int tileNumber = tile.Gid;
                int column = (tileNumber % (int)tmxTileset.Columns) - 1;
                int row = ((int)tileNumber / (int)tmxTileset.Columns);
                int x = tile.X * tileWidth;
                int y = (tile.Y * tileHeight) - heightDiff;
                if (tileNumber != 0 && (x > capNegX) && (x < capPosX) && (y > capPosY) && (y < capNegY))
                    spriteBatch.Draw(textureTileset, new Rectangle(x, y, tileWidth, tileHeight), new Rectangle(column * tileWidth, row * tileHeight, tileWidth, tileHeight), Color.White);
            }


            foreach (TmxLayerTile tile in wall.Tiles)
            {
                int tileNumber = tile.Gid;
                int column = (tileNumber % (int)tmxTileset.Columns) - 1;
                int row = ((int)tileNumber / (int)tmxTileset.Columns);
                int x = tile.X * tileWidth;
                int y = (tile.Y * tileHeight) - heightDiff;
                if (tileNumber != 0 && (x > capNegX) && (x < capPosX) && (y > capPosY) && (y < capNegY))
                    spriteBatch.Draw(textureTileset, new Rectangle(x, y, tileWidth, tileHeight), new Rectangle(column * tileWidth, row * tileHeight, tileWidth, tileHeight), Color.White);
            }
        }

        public string GetDebugInfo()
        {
            int xTile = ( (int)player.location.X + 15) / tileWidth;
            int yTile = ( (int)player.location.Y + 15 + heightDiff) / tileHeight;
            int tile = yTile * ((int)mapWidth) + xTile;
            

            TmxLayerTile collisionTile = collision.Tiles[tile];
            return "Map: (" + xTile + "," + yTile + ") - " + tile + ":" + collisionTile.Gid;
        }

    }
}
