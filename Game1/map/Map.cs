using Game1.enemy;
using Game1.player;
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
        static int width, height, heightDiff;
        static int screenWidthCap, screenHeightCap;
        static int tileWidth, tileHeight, mapWidth, mapHeight;
        static TmxLayer background, wall, collision;
        int tiles;
        Texture2D textureTileset;
        TmxTileset tmxTileset;
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

            player.position = new Vector2((int)map.ObjectGroups[0].Objects[0].X, (int)map.ObjectGroups[0].Objects[0].Y - heightDiff);
            player.setMap(this);
            
            EnemyFactory.setEnemyMapping(map.ObjectGroups[1].Objects, heightDiff);
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

                    check += tileWidth;
                    if (check > playerBound.Right)
                        check = playerBound.Right;
                }

                if (falling)
                    player.addEvent(new PlayerEventFallStart(player));

            }
        }

        public Rectangle checkVertCollision(Rectangle playerBound, int checkY, int mult)
        {
            int check = playerBound.Left + tileWidth/2;
            while (check < playerBound.Right)
            {
                TileRect tileRect = getCollisionRectangle(playerBound, check, checkY);
                if (!tileRect.rectangle.IsEmpty && tileRect.tileId == collisionTileId)
                {
                    playerBound.Offset(0, tileRect.rectangle.Height*mult);
                    if (mult == -1)
                    {
                        player.addEvent(new PlayerEventFallStop(player));
                        player.jumping = false;
                    }
                }
                check += tileHeight/2;
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
                check += tileWidth/2;
                if (check > playerBound.Bottom)
                    check = playerBound.Bottom;
            }
            return playerBound;
        }

        public Vector2 checkPlayerCollisions(Rectangle playerBound, Vector2 updateLocation)
        {
            Rectangle horizBound = playerBound;
            Rectangle vertBound = playerBound;
            
            if (updateLocation.Y < 0)
                playerBound = checkVertCollision(playerBound, playerBound.Top, 1);
            else if (updateLocation.Y > 0)
                playerBound = checkVertCollision(playerBound, playerBound.Bottom, -1);

            if (updateLocation.X > 0) 
                playerBound = checkHorizCollision(playerBound, playerBound.Right, -1);
            else if (updateLocation.X < 0) 
                playerBound = checkHorizCollision(playerBound, playerBound.Left, 1);

            checkFalling(playerBound, updateLocation);

            return playerBound.Location.ToVector2();
        }

        public TileRect getTileRectangle(int x, int y)
        {
            int xTile = ((int)x) / tileWidth;
            int yTile = ((int)y + heightDiff) / tileHeight;
            int tile = yTile * ((int)mapWidth) + xTile;
            TmxLayerTile tmxTile = collision.Tiles[tile];
            return new TileRect(tmxTile.Gid, new Rectangle(tmxTile.X * tileWidth, tmxTile.Y * tileHeight - heightDiff, tileWidth, tileHeight));
        }

        public static bool checkCollision(float x, float y)
        {
            int xTile = ((int)x) / tileWidth;
            int yTile = ((int)y + heightDiff) / tileHeight;
            int tile = yTile * ((int)mapWidth) + xTile;
            
        
            if (collision.Tiles[tile].Gid == 211)
               return true;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float capPosY = player.position.Y - screenHeightCap;
            float capNegY = player.position.Y + screenHeightCap;
            float capPosX = player.position.X + screenWidthCap;
            float capNegX = player.position.X - screenWidthCap;

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
            int xTile = ( (int)player.position.X + 15) / tileWidth;
            int yTile = ( (int)player.position.Y + 15 + heightDiff) / tileHeight;
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
