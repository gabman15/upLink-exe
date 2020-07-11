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
        private KeyboardState keyState;
        public static float MoveSpeed = 3f;

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
            Vector2 velocity = Velocity;

            keyState = Keyboard.GetState();
            

            if (keyState.IsKeyDown(Keys.W))
                velocity.Y = -MoveSpeed;
            else if (keyState.IsKeyDown(Keys.S))
                velocity.Y = MoveSpeed;

            if (keyState.IsKeyDown(Keys.A))
                velocity.X = -MoveSpeed;
            else if (keyState.IsKeyDown(Keys.D))
                velocity.X = MoveSpeed;

            Position += velocity;
            base.Update();
        }
    }
}
