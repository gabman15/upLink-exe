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
        public static float MoveSpeed = 1;

        public Player(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(100, 100))
        {
            spawnLoc = pos;

            AssetManager.RequestTexture("yara", (frames) =>
            {
                //Console.WriteLine(frames.Length);
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
            Console.WriteLine("pos: " + Position);
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
                }
                else if (!keyState.IsKeyDown(Keys.S) && oldKeyState.IsKeyDown(Keys.S))
                {
                    velocity.Y = MoveSpeed;
                    movementStage = 10;
                }
                else if (!keyState.IsKeyDown(Keys.A) && oldKeyState.IsKeyDown(Keys.A))
                {
                    velocity.X = -MoveSpeed;
                    movementStage = 10;
                }
                else if (!keyState.IsKeyDown(Keys.D) && oldKeyState.IsKeyDown(Keys.D))
                {
                    velocity.X = MoveSpeed;
                    movementStage = 10;
                }
            }

            Position += velocity*10;
            Velocity = velocity;
            oldKeyState = keyState;
            base.Update();
        }
    }
}
