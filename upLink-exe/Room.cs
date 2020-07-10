﻿using upLink_exe.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace upLink_exe
{
    public class Room
    {
        public List<GameObject> GameObjectList { get; set; }
        //public List<GameTile> GameTileList { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Game1 Game { get; set; }
        //public SoundManager Sounds { get; set; }
        private int roomToGoTo;


        /// <summary>
        /// frames/update
        /// </summary>
        /// 
            
        //public string NoteText { get; set; }
        public Room(Game1 game)
        {
            Game = game;

            //Sounds = new SoundManager();

            GameObjectList = new List<GameObject>();
            //GameTileList = new List<GameTile>();

            Width = 512;
            Height = 512;
        }
        public void Update()
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                obj.Update();
            }
            //Sounds.Update();
        }
        public void Destroy()
        {
            //Sounds.Destroy();
        }
        public void ChangeRoom(int room)
        {
            roomToGoTo = room;
        }

        public void Draw(SpriteBatch batch)
        {
            Game.GraphicsDevice.Clear(ConsoleColor.Black);
            Background.Draw(batch, ceiledOffset);
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                obj.Draw(batch, ceiledOffset);
            }
            for (int i = 0; i < GameTileList.Count; i++)
            {
                GameTile tile = GameTileList[i];
                tile.Draw(batch, ceiledOffset);
            }
            Lighting.Draw(batch, ceiledOffset);
            Lighting.LightingDifficulty = transitionProgress + 1;
            batch.Draw(whiteChunk, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), null, Color.Black * ((float)transitionProgress / maxTransitionProgress), 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }
        public void DisplayHitbox()
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                obj.drawHitbox = true;
            }
        }
        public void ProcessCommand(string cmd)
        {
            string[] parts = cmd.Split(' ');
            switch (parts[0])
            {
                case "width":
                    Width = int.Parse(parts[1]);
                    break;
                case "height":
                    Height = int.Parse(parts[1]);
                    break;
                case "createobject":
                    {
                        Type type = GameObject.GetObjectFromName(parts[1]);
                        Vector2 position = new Vector2(int.Parse(parts[2]), int.Parse(parts[3]));
                        GameObject obj = (GameObject)type.GetConstructor(new Type[] { typeof(Room), typeof(Vector2) }).Invoke(new object[] { this, position });
                        obj.Layer = int.Parse(parts[4]);
                        GameObjectList.Add(obj);
                        break;
                    }
                case "createtile":
                    {
                        Type type = GameTile.GetObjectFromName(parts[1]);
                        Vector2 position = new Vector2(int.Parse(parts[2]), int.Parse(parts[3]));
                        GameTile obj = (GameTile)type.GetConstructor(new Type[] { typeof(Room), typeof(Vector2) }).Invoke(new object[] { this, position });
                        obj.Layer = (float)int.Parse(parts[4]) / 100f;
                        GameTileList.Add(obj);
                        break;
                    }
                case "background":
                    {
                        Background.LoadBackground(parts[1]);
                        break;
                    }
                case "note":
                    {
                        for (int i = 1; i < parts.Length; i++)
                        {
                            NoteText += parts[i] + " ";
                        }
                        NoteText += "\n";
                        break;
                    }
            }
        }
        public bool CheckCollision(Rectangle collider)
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                if (obj.Solid)
                {
                    Rectangle targetRect = GameObject.AddVectorToRect(obj.Hitbox, obj.Position);
                    if (GameObject.RectangleInRectangle(collider, targetRect))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CheckTileCollision(GameTile checkingTile, Vector2 checkPos, params Type[] includeTypes)
        {
            Rectangle checkingRect = new Rectangle((int)checkingTile.Position.X, (int)checkingTile.Position.Y, (int)checkingTile.Size.X, (int)checkingTile.Size.Y);
            for (int i = 0; i < GameTileList.Count; i++)
            {
                GameTile tile = GameTileList[i];
                bool isValidTile = false;
                for (int j = 0; j < includeTypes.Length; j++)
                {
                    if (includeTypes[j] == tile.GetType())
                    {
                        isValidTile = true;
                        break;
                    }
                }
                if (!isValidTile || checkingTile == tile)
                {
                    continue;
                }
                Rectangle targetRect = new Rectangle((int)tile.Position.X, (int)tile.Position.Y, (int)tile.Size.X, (int)tile.Size.Y);
                if (GameObject.RectangleInRectangle(checkingRect, targetRect))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckTileAt(Vector2 checkPos)
        {
            for (int i = 0; i < GameTileList.Count; i++)
            {
                GameTile tile = GameTileList[i];
                if (checkPos.X >= tile.Position.X && checkPos.X < tile.Position.X + tile.Size.X && checkPos.Y >= tile.Position.Y && checkPos.Y < tile.Position.Y + tile.Size.Y)
                {
                    return true;
                }
            }
            return false;
        }
        public GameObject FindObject(string name)
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                if (obj.GetType() == GameObject.GetObjectFromName(name))
                {
                    return obj;
                }
            }
            return null;
        }
        public GameObject FindCollision(Rectangle collider, string name)
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                if (obj.GetType() == GameObject.GetObjectFromName(name))
                {
                    if (GameObject.RectangleInRectangle(collider, GameObject.AddVectorToRect(obj.Hitbox, obj.Position)))
                    {
                        return obj;
                    }
                }
            }
            return null;
        }
        public void Load(string filename)
        {
            string[] lines = File.ReadAllLines(filename, Encoding.UTF8);
            for (int i = 0; i < lines.Length; i++)
            {
                ProcessCommand(lines[i]);
            }

        }
        public static int HorizRectDistance(Rectangle a, Rectangle b) //not too sure where to put these
        {
            if (a.X > b.X) //a is to the right of b
            {
                return a.X - (b.X + b.Width);
            }
            else //a is to the left of b
            {
                return b.X - (a.X + a.Width);
            }
        }
        public static int VertiRectDistance(Rectangle a, Rectangle b)
        {
            if (a.Y > b.Y) //a is below b
            {
                return a.Y - (b.Y + b.Height);
            }
            else //a is above b
            {
                return b.Y - (a.Y + a.Height);
            }
        }
    }
    
}
