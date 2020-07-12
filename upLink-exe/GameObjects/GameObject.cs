﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upLink_exe.GameObjects
{
    public class GameObject
    {
        public static float CollisionPrecision = 1;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Size { get; set; }
        public SpriteData Sprite { get; set; }
        public Rectangle Hitbox { get; set; }
        public Room currRoom { get; set; }
        public bool Solid { get; set; }
        private float layer;
        public float Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value / 100;
                if (Sprite != null)
                {
                    Sprite.Layer = layer;
                }
            }
        }

        

        public GameObject(Room room, Vector2 pos, Vector2 vel, Vector2 size)
        {
            Solid = false;
            currRoom = room;
            Position = pos;
            Velocity = vel;
            Size = size;
            Sprite = null;
            Layer = 0;
            Hitbox = new Rectangle(0, 0, (int)size.X, (int)size.Y);
        }

        public virtual void Update()
        {
            //Console.WriteLine("Object updating!");
            if (Velocity.X != 0 || Velocity.Y != 0)
            {
                Vector2 vel = Velocity;
                for (int i = 0; i < currRoom.GameObjectList.Count; i++)
                {
                    GameObject obj = currRoom.GameObjectList[i];
                    if (obj.Solid)
                    {
                        //feel like there should be a better way to do collisions but idk that way
                        Rectangle targetRect = AddVectorToRect(obj.Hitbox, obj.Position, VectorCeil(obj.Velocity));
                        Rectangle fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(new Vector2(0, vel.Y)));
                        while (RectangleInRectangle(fromRect, targetRect))
                        {
                            if (Math.Abs(vel.Y) < CollisionPrecision)
                            {
                                vel.Y = 0;
                                break;
                            }
                            else
                            {
                                vel.Y -= Math.Sign(vel.Y) * CollisionPrecision;
                                fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(new Vector2(0, vel.Y)));
                            }
                        }
                        fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(new Vector2(vel.X, 0)));
                        while (RectangleInRectangle(fromRect, targetRect))
                        {
                            if (Math.Abs(vel.X) < CollisionPrecision)
                            {
                                vel.X = 0;
                                break;
                            }
                            else
                            {
                                vel.X -= Math.Sign(vel.X) * CollisionPrecision;
                                fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(new Vector2(vel.X, 0)));
                            }
                        }
                        fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(vel));
                        while (RectangleInRectangle(fromRect, targetRect))
                        {
                            if (Math.Abs(vel.X) < CollisionPrecision)
                            {
                                vel.X = 0;
                                break;
                            }
                            else
                            {
                                vel.X -= Math.Sign(vel.X) * CollisionPrecision;
                            }
                            if (Math.Abs(vel.Y) < CollisionPrecision)
                            {
                                vel.Y = 0;
                                break;
                            }
                            else
                            {
                                vel.Y -= Math.Sign(vel.Y) * CollisionPrecision;
                            }
                            fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(vel));
                        }
                    }
                }
                Velocity = vel;
            }
            //Position += Velocity;
            Sprite?.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine(Sprite?.CurrentFrame);
            //Console.WriteLine("sprite size: " + Sprite.Size);
            Sprite?.Draw(spriteBatch, Position - Sprite.Offset);
        }
        public static Vector2 VectorCeil(Vector2 val)
        {
            return new Vector2((float)Math.Ceiling(Math.Abs(val.X)) * Math.Sign(val.X), (float)Math.Ceiling(Math.Abs(val.Y)) * Math.Sign(val.Y));
        }

        public static bool RectangleInRectangle(Rectangle a, Rectangle b)
        {
            return a.X < b.X + b.Width && a.X + a.Width > b.X && a.Y < b.Y + b.Height && a.Y + a.Height > b.Y; //flipped comparisons for y since original code is for cartesian coords
        }
        public static Rectangle AddVectorToRect(Rectangle rect, params Vector2[] vecs)
        {
            Rectangle newRect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            foreach (Vector2 vec in vecs)
            {
                newRect.X += (int)vec.X;
                newRect.Y += (int)vec.Y;
            }
            return newRect;
        }
        public static Type GetObjectFromName(string name)
        {
            switch (name)
            {
                case "player":
                    return typeof(Player);
                case "":
                    return typeof(GameObject);
            }
            return null;
        }
    }
}
