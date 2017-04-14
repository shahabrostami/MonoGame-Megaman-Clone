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
        TmxLayerTile collisionTile;
        TmxLayer background, wall, collision;
        Player player;
        static Map inst;

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
            heightDiff = (map.Height*tileHeight) - height;
            
            player.location = new Vector2((int)map.ObjectGroups[0].Objects[0].X, (int)map.ObjectGroups[0].Objects[0].Y - heightDiff);
            player.setMap(this);
        }


        public void Update()
        {
        }

        public Rectangle getCollisionRectangle(Rectangle player, int x, int y)
        {
            Rectangle rect = getTileRectangle(x, y);
            if (rect != Rectangle.Empty)
                return Rectangle.Intersect(player, rect);
            return rect;
        }

        public Vector2 checkCollisions(Rectangle playerBound, Vector2 updateLocation)
        {
            // Need to convert this into rectangle bound collision
            if (updateLocation.Y == 0)
            {
                if (checkCollision(playerBound.X + 7, playerBound.Y + 29) &&
                        checkCollision(playerBound.X + 28, playerBound.Y + 29))
                    player.falling = true;
            }

            int check = playerBound.Right;
            while (check != playerBound.Left && updateLocation.Y > 0)
            {
                Rectangle collisionRect = getCollisionRectangle(playerBound, check, playerBound.Bottom);
                if (!collisionRect.IsEmpty)
                {
                    playerBound.Offset(0, -collisionRect.Height);
                    player.falling = false;
                    player.jumping = false;
                }
                check -= 16;
                if (check <= playerBound.Left)
                    check = playerBound.Left;
            }

            // if (falling)
            //    player.falling = true;

            check = playerBound.Left;

            while (check != playerBound.Right && updateLocation.Y < 0)
            {
                Rectangle collisionRect = getCollisionRectangle(playerBound, check, playerBound.Top);
                if (!collisionRect.IsEmpty)
                    playerBound.Offset(0, collisionRect.Height);

                check += 16;
                if (check >= playerBound.Right)
                    check = playerBound.Right;
            }

            check = playerBound.Top;
            while (check != playerBound.Bottom && updateLocation.X > 0)
            {
                Rectangle collisionRect = getCollisionRectangle(playerBound, playerBound.Right, check);
                if (!collisionRect.IsEmpty)
                    playerBound.Offset(-collisionRect.Width, 0);

                check += 16;
                if (check >= playerBound.Bottom)
                    check = playerBound.Bottom;
            }

            check = playerBound.Bottom;
            while (check != playerBound.Top && updateLocation.X < 0)
            {
                Rectangle collisionRect = getCollisionRectangle(playerBound, playerBound.Left, check);
                if (!collisionRect.IsEmpty) 
                    playerBound.Offset(collisionRect.Width, 0);
                check -= 16;
                if (check <= playerBound.Top)
                    check = playerBound.Top;
            }



            // Continue

            /*
            if (update.Y == 0)
            {
                if (checkCollision(playerLocation.X + 7, playerLocation.Y + 29) &&
                        checkCollision(playerLocation.X + 28, playerLocation.Y + 29))
                    player.setFalling(true);
            }
            else if (checkCollision(playerLocation.X + 7, newLocation.Y + 4) &&
                    checkCollision(playerLocation.X + 7, newLocation.Y + 27) &&
                    checkCollision(playerLocation.X + 28, newLocation.Y + 27) &&
                    checkCollision(playerLocation.X + 28, newLocation.Y + 4))
            {
                Console.WriteLine("Updating Y");
                playerLocation.Y = newLocation.Y;
            }
            else
            {
                if (update.Y < 0)
                {
                    if (!(checkCollision(playerLocation.X + 7, newLocation.Y + 4) &&
                    checkCollision(playerLocation.X + 28, newLocation.Y + 4)))
                    {
                        Console.WriteLine("Colliding Y Up");
                    }
                }
                else if (update.Y > 0)
                {
                    if (!(checkCollision(playerLocation.X + 7, newLocation.Y + 27) &&
                        checkCollision(playerLocation.X + 28, newLocation.Y + 27)))
                    {
                        playerLocation.Y = newLocation.Y;
                        Console.WriteLine("Colliding Y Down" + playerLocation.Y);
                        player.setJumping(false);
                        player.setFalling(false);
                        playerLocation.Y -= (playerLocation.Y % 16) - 4;
                        Console.WriteLine("Blocation.Y: " + newLocation.Y + " updateLocation.Y: " + update.Y + "Alocation.Y" + playerLocation.Y);
                    }
                }
            }
            if ((checkCollision(newLocation.X + 7, playerLocation.Y + 4) &&
                checkCollision(newLocation.X + 7, playerLocation.Y + 27) &&
                checkCollision(newLocation.X + 28, playerLocation.Y + 27) &&
                checkCollision(newLocation.X + 28, playerLocation.Y + 4)))
            {
                Console.WriteLine("Updating X");
                playerLocation.X = newLocation.X;
            }
            */

            return new Vector2(playerBound.X, playerBound.Y);
        }

        public Rectangle getTileRectangle(int x, int y)
        {
            int xTile = ((int)x) / tileWidth;
            int yTile = ((int)y + heightDiff) / tileHeight;
            int tile = yTile * ((int)mapWidth) + xTile;
            TmxLayerTile tmxTile = collision.Tiles[tile];
            if (tmxTile.Gid == 211)
                return new Rectangle(tmxTile.X * tileWidth, tmxTile.Y * tileHeight - heightDiff, tileWidth, tileHeight);
            else
                return Rectangle.Empty;
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
