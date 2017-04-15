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
    class TileRect
    {
        public int tileId { get; set; }
        public Rectangle rectangle { get; set; }

    public TileRect(int tileId, Rectangle rectangle)
        {
            this.tileId = tileId;
            this.rectangle = rectangle;
        }
    }

    class Map
    {
        int width, height, heightDiff;
        int screenWidthCap, screenHeightCap;
        int tileWidth, tileHeight, mapWidth, mapHeight;
        int tiles;
        Texture2D textureTileset;
        TmxTileset tmxTileset;
        TmxLayerTile collisionTile;
        TmxLayer background, wall, collision;
        Player player;
        static Map inst;
        private int collisionTileId = 211;
        private int emptyTileId = 0;

        public Map(int tiles, Player player)
        {
            this.tiles = tiles;
            this.player = player;
            inst = this;
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
            heightDiff = (map.Height * tileHeight) - height;

            player.location = new Vector2((int)map.ObjectGroups[0].Objects[0].X, (int)map.ObjectGroups[0].Objects[0].Y - heightDiff);
            player.setMap(this);
        }


        public void Update()
        {
        }

        public TileRect getCollisionRectangle(Rectangle player, int x, int y)
        {
            TileRect tileRect = getTileRectangle(x, y);
            if (tileRect.rectangle != Rectangle.Empty)
            {
                Rectangle intersect = Rectangle.Intersect(player, tileRect.rectangle);
                return new TileRect(tileRect.tileId, intersect);
            }
            return new TileRect(tileRect.tileId, tileRect.rectangle);
        }

        public void checkFalling(Rectangle playerBound, Vector2 updateLocation)
        {
            int check = playerBound.Left;
            bool falling = true;
            if (updateLocation.Y == 0)
            {
                while (check < playerBound.Right)
                {
                    TileRect collisionRect = getCollisionRectangle(playerBound, check, playerBound.Bottom);

                    if (collisionRect.rectangle.IsEmpty && collisionRect.tileId != emptyTileId)
                        falling = false;

                    check += 16;
                    if (check < playerBound.Right)
                        check = playerBound.Right;
                }
                player.falling = falling;
                // Console.WriteLine("falling = true");
                // Console.WriteLine("falling = false");
            }
        }

        public Rectangle checkVertCollision(Rectangle playerBound, int checkY, int mult)
        {
            int check = playerBound.Left;
            while (check < playerBound.Right)
            {
                TileRect tileRect = getCollisionRectangle(playerBound, check, checkY);
                if (!tileRect.rectangle.IsEmpty && tileRect.tileId == collisionTileId)
                {
                    playerBound.Offset(0, tileRect.rectangle.Height*mult);
                    if (mult == -1)
                    {
                        player.falling = false;
                        player.jumping = false;
                    }
                }
                check += tileHeight;
                if (check > playerBound.Right)
                    check = playerBound.Right;
            }
            return playerBound;
        }

        public Rectangle checkHorizCollision(Rectangle playerBound, int checkX, int mult)
        {
            // Check Below
            int check = playerBound.Top;
            while (check < playerBound.Bottom)
            {
                TileRect tileRect = getCollisionRectangle(playerBound, checkX, check);
                if (!tileRect.rectangle.IsEmpty && tileRect.tileId == collisionTileId)
                    playerBound.Offset(tileRect.rectangle.Width * mult, 0);
                check += tileWidth;
                if (check > playerBound.Bottom)
                    check = playerBound.Bottom;
            }
            return playerBound;
        }

        public Vector2 checkCollisions(Rectangle playerBound, Vector2 updateLocation)
        {
            checkFalling(playerBound, updateLocation);
            Rectangle horizBound = playerBound;
            Rectangle vertBound = playerBound;
            int xDiff = 0;
            int yDiff = 0;

            if (updateLocation.X > 0) {
                horizBound = checkHorizCollision(playerBound, playerBound.Right, -1);
                xDiff = horizBound.X - playerBound.X;
            }
            else if (updateLocation.X < 0) {
                horizBound = checkHorizCollision(playerBound, playerBound.Left, 1);
                xDiff = playerBound.X - horizBound.X;
            }

            if (updateLocation.Y < 0) {
                vertBound = checkVertCollision(playerBound, playerBound.Top, 1);
                yDiff = playerBound.Y - vertBound.Y;
            }
            else if (updateLocation.Y > 0) {
                vertBound = checkVertCollision(playerBound, playerBound.Bottom, -1);
                yDiff = vertBound.Y - playerBound.Y;
            }

            if (xDiff < yDiff)
                return horizBound.Location.ToVector2();
            return vertBound.Location.ToVector2();
            
        }

        public TileRect getTileRectangle(int x, int y)
        {
            int xTile = ((int)x) / tileWidth;
            int yTile = ((int)y + heightDiff) / tileHeight;
            int tile = yTile * ((int)mapWidth) + xTile;
            TmxLayerTile tmxTile = collision.Tiles[tile];
            return new TileRect(tmxTile.Gid, new Rectangle(tmxTile.X * tileWidth, tmxTile.Y * tileHeight - heightDiff, tileWidth, tileHeight));
        }

        public bool checkCollision(float x, float y)
        {
            int xTile = ((int)x) / tileWidth;
            int yTile = ((int)y + heightDiff) / tileHeight;
            int tile = yTile * ((int)mapWidth) + xTile;


            collisionTile = collision.Tiles[tile];
            if (collisionTile.Gid == 211)
               return false;
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float capPosY = player.location.Y - screenHeightCap;
            float capNegY = player.location.Y + screenHeightCap;
            float capPosX = player.location.X + screenWidthCap;
            float capNegX = player.location.X - screenWidthCap;

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

        public static Map getInstance()
        {
            return inst;
        }
    }
}
