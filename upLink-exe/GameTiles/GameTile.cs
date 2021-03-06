﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upLink_exe.GameTiles
{
    public abstract class GameTile
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public SpriteData Sprite { get; set; }
        public Room Room { get; set; }
        private float layer;
        public float Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value;
                if (Sprite != null)
                {
                    Sprite.Layer = layer;
                }
            }
        }
        public GameTile(Room room, Vector2 pos, Vector2 size)
        {
            Position = pos;
            Size = size;
            Sprite = null;
            //Layer = 0;
            Room = room;
        }
        public void Update()
        {
            Sprite?.Update();
        }
        public virtual void Draw(SpriteBatch batch)
        {
            Sprite?.Draw(batch, Position);
        }
        public static Type GetObjectFromName(string name)
        {
            switch(name)
            {
                case "labFloor":
                    return typeof(LabFloorTile);
                case "wall":
                    return typeof(WallTile);
                case "labBench":
                    return typeof(LabBenchTile);
                case "grass":
                    return typeof(GrassTile);
                case "tree":
                    return typeof(TreeTile);
                case "rock":
                    return typeof(RockTile);
                case "door":
                    return typeof(DoorTile);
                case "yon":
                    return typeof(YonTile);
                case "win":
                    return typeof(WinTile);
            }
            return null;
        }
    }
}
