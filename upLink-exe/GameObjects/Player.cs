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
        public Player(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            var velocity = new Vector2();

            var speed = 3f;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                velocity.Y = -speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                velocity.Y = speed;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X = -speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X = speed;

            Position += velocity;

            float x = Position.X;
            float y = Position.Y;

            if (x > Game1.ScreenWidth - _texture.Width / 2)
                x = Game1.ScreenWidth - _texture.Width / 2;
            else if (x < _texture.Width / 2)
                x = _texture.Width / 2;

            if (y > Game1.ScreenHeight - _texture.Height / 2)
                y = Game1.ScreenHeight - _texture.Height / 2;
            else if (y < _texture.Height / 2)
                y = _texture.Height / 2;

            Position = new Vector2(x, y);
        }
    }
}
