using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upLink_exe.GameObjects
{
    public class Player : GameObject
    {
        private Vector2 spawnLoc;
        private Texture2D[] idleSprite;
        private KeyboardState keyState, oldKeyState = Keyboard.GetState();
        private int movementStage = 0;
        public static float MoveSpeed = 10;
        public string draggingWire;
        public GameObject draggingFrom;
        private bool placedWire;
        private Vector2 prevPosition;

        public Player(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(100, 100))
        {
            spawnLoc = pos;
            prevPosition = Position;
            draggingWire = "";
            placedWire = false;

            AssetManager.RequestTexture("turtle", (frames) =>
            {
                idleSprite = frames;
                Sprite = new SpriteData(idleSprite);
                Sprite.Size = new Vector2(100, 100);
                Sprite.Layer = Layer;
                Sprite.Offset = new Vector2(0, 0);
            });

            
            Hitbox = new Rectangle(0, 0, 100, 100);
        }

        public override void Update()
        {
            Vector2 velocity = Velocity;
            keyState = Keyboard.GetState();

            if (movementStage > 0)
                movementStage -= 1;

            // Free for commands
            if (movementStage == 0)
            {
                // Stop movement
                velocity = new Vector2();

                // Take in new commands
                if (!keyState.IsKeyDown(Keys.W) && oldKeyState.IsKeyDown(Keys.W))
                {
                    velocity.Y = -MoveSpeed;
                    movementStage = 10;
                    placedWire = false;
                }
                else if (!keyState.IsKeyDown(Keys.S) && oldKeyState.IsKeyDown(Keys.S))
                {
                    velocity.Y = MoveSpeed;
                    movementStage = 10;
                    placedWire = false;
                }
                else if (!keyState.IsKeyDown(Keys.A) && oldKeyState.IsKeyDown(Keys.A))
                {
                    velocity.X = -MoveSpeed;
                    movementStage = 10;
                    placedWire = false;
                }
                else if (!keyState.IsKeyDown(Keys.D) && oldKeyState.IsKeyDown(Keys.D))
                {
                    velocity.X = MoveSpeed;
                    movementStage = 10;
                    placedWire = false;
                }
            }
            

            //Drag Wires
            if (draggingWire != "" && !placedWire)
            {
                if (draggingWire == "red")
                {
                    GameObject obj = new RedWire(currRoom, Position);
                    obj.Layer = 2;
                    currRoom.GameObjectList.Add(obj);
                    currRoom.GameObjectIntersectList.Add(false);
                }
                if (draggingWire == "green")
                {
                    GameObject obj = new GreenWire(currRoom, Position);
                    obj.Layer = 2;
                    currRoom.GameObjectList.Add(obj);
                    currRoom.GameObjectIntersectList.Add(false);
                }
                if (draggingWire == "blue")
                {
                    GameObject obj = new BlueWire(currRoom, Position);
                    obj.Layer = 2;
                    currRoom.GameObjectList.Add(obj);
                    currRoom.GameObjectIntersectList.Add(false);
                }
                if (draggingWire == "orange")
                {
                    GameObject obj = new OrangeWire(currRoom, Position);
                    obj.Layer = 2;
                    currRoom.GameObjectList.Add(obj);
                    currRoom.GameObjectIntersectList.Add(false);
                }
                placedWire = true;
            }



            // Deal with collisions
            bool collisionOccured = false;
            bool solidCollisionOccured = false;
            for (int i = 0; i < currRoom.GameObjectList.Count; i++)
            {
                Vector2 initialVelocity = velocity;

                GameObject obj = currRoom.GameObjectList[i];
                //Console.WriteLine("obj: " + obj);
                if (obj == this)
                    continue;

                // Check for collision
                Tuple<bool, bool, Vector2> collisionTuple = checkCollision(Position, Hitbox, velocity, obj);
                collisionOccured = collisionTuple.Item1;
                solidCollisionOccured = collisionTuple.Item2;
                velocity = collisionTuple.Item3;


                // Check if there was a previous collision
                bool prevCollisionOccured = currRoom.GameObjectIntersectList[i];

                if (movementStage == 0 && collisionOccured && !prevCollisionOccured)
                {
                    Console.WriteLine("New collision with object");
                    Console.WriteLine(obj);
                    obj.Collision(this);
                    currRoom.GameObjectIntersectList[i] = collisionOccured;
                }
            }

            prevPosition = Position;
            
            Position += velocity;
            if (solidCollisionOccured)
            {
                velocity.Y = 0;
                velocity.X = 0;
            }

            Velocity = velocity;
            oldKeyState = keyState;
            base.Update();
        }

        // Return: isCollision, isSolidCollision, new velocity vector
        private static Tuple<bool, bool, Vector2> checkCollision(Vector2 position, Rectangle hitbox, Vector2 velocity, GameObject obj)
        {
            Vector2 initialVelocity = velocity;
            bool isCollision = false;
            bool isSolidCollision = false;
            if (obj.Solid)
            {
                Rectangle targetRect = AddVectorToRect(obj.Hitbox, obj.Position, VectorCeil(obj.Velocity));
                Rectangle fromRect = AddVectorToRect(hitbox, position, VectorCeil(new Vector2(0, velocity.Y)));
                while (RectangleInRectangle(fromRect, targetRect))
                {
                    if (Math.Abs(velocity.Y) < CollisionPrecision)
                    {
                        velocity.Y = 0;
                        break;
                    }
                    else
                    {
                        velocity.Y -= Math.Sign(velocity.Y) * CollisionPrecision;
                        fromRect = AddVectorToRect(hitbox, position, VectorCeil(new Vector2(0, velocity.Y)));
                    }
                }
                fromRect = AddVectorToRect(hitbox, position, VectorCeil(new Vector2(velocity.X, 0)));
                while (RectangleInRectangle(fromRect, targetRect))
                {
                    if (Math.Abs(velocity.X) < CollisionPrecision)
                    {
                        velocity.X = 0;
                        break;
                    }
                    else
                    {
                        velocity.X -= Math.Sign(velocity.X) * CollisionPrecision;
                        fromRect = AddVectorToRect(hitbox, position, VectorCeil(new Vector2(velocity.X, 0)));
                    }
                }
                fromRect = AddVectorToRect(hitbox, position, VectorCeil(velocity));
                while (RectangleInRectangle(fromRect, targetRect))
                {
                    if (Math.Abs(velocity.X) < CollisionPrecision)
                    {
                        velocity.X = 0;
                        break;
                    }
                    else
                    {
                        velocity.X -= Math.Sign(velocity.X) * CollisionPrecision;
                    }
                    if (Math.Abs(velocity.Y) < CollisionPrecision)
                    {
                        velocity.Y = 0;
                        break;
                    }
                    else
                    {
                        velocity.Y -= Math.Sign(velocity.Y) * CollisionPrecision;
                    }
                    fromRect = AddVectorToRect(hitbox, position, VectorCeil(velocity));
                }
                if (initialVelocity != velocity)
                {
                    isCollision = true;
                    isSolidCollision = true;
                }
            }
            else
            {
                Rectangle targetRect = AddVectorToRect(obj.Hitbox, obj.Position, VectorCeil(obj.Velocity));

                Rectangle fromRect = AddVectorToRect(hitbox, position, VectorCeil(new Vector2(0, velocity.Y)));
                if (RectangleInRectangle(fromRect, targetRect))
                    isCollision = true;

                fromRect = AddVectorToRect(hitbox, position, VectorCeil(new Vector2(velocity.X, 0)));
                if (RectangleInRectangle(fromRect, targetRect))
                    isCollision = true;

                fromRect = AddVectorToRect(hitbox, position, VectorCeil(velocity));
                if (RectangleInRectangle(fromRect, targetRect))
                    isCollision = true;
            }
            return new Tuple<bool, bool, Vector2>(isCollision, isSolidCollision, velocity);
        }
    }
}
