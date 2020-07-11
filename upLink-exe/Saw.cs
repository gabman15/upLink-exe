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
    public class Saw : Component
    {

        private Vector2 _left_position;
        private Vector2 _right_position;
        private Vector2 _saw_position;
        private Texture2D _saw_post;
        private Texture2D _saw;
        private float _speed;
        private bool _forwards;
        private bool _horizontal;

        public Saw(Texture2D saw, Texture2D saw_post, Vector2 left_position, Vector2 right_position, Vector2 saw_position, float speed, bool horizontal)
        {
            _saw = saw;
            _saw_post = saw_post;
            _left_position = left_position;
            _right_position = right_position;
            _speed = speed;
            _saw_position = saw_position;
            _forwards = true;
            _horizontal = horizontal;
        }

        public override void Update(GameTime gameTime)
        {
            float distance = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_horizontal)
            {
                if (_saw_position.X == _left_position.X || _saw_position.X == (_right_position.X - _saw.Width))
                {
                    Reverse_direction();
                }
            }
            else
            {
                if (_saw_position.Y == _left_position.Y || _saw_position.Y == (_right_position.Y - _saw.Width))
                {
                    Reverse_direction();
                }
            }

            if (_horizontal && _forwards)
            {
                _saw_position = new Vector2(_saw_position.X + distance, _saw_position.Y);
            }
            else if (_horizontal && !_forwards)
            {
                _saw_position = new Vector2(_saw_position.X - distance, _saw_position.Y);
            }
            else if (!_horizontal && _forwards)
            {
                _saw_position = new Vector2(_saw_position.X, _saw_position.Y + distance);
            }
            else
            {
                _saw_position = new Vector2(_saw_position.X, _saw_position.Y - distance);
            }

         
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_saw_post, _left_position, Color.White);
            spriteBatch.Draw(_saw_post, _right_position, Color.White);
            spriteBatch.Draw(_saw, _saw_position, Color.White);
        }

        private void Reverse_direction()
        {
            if (_forwards)
            {
                _forwards = false;
            }
            else
            {
                _forwards = true;
            }
        }
        
    }
}
