using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upLink_exe.GameObjects;
using Microsoft.Xna.Framework.Input;

namespace upLink_exe
{
    public class SawBlade : GameObject
    {

        private Texture2D[] sprites;
        private Texture2D[] postSprites;
        public SpriteData postSprite { get; set; }
        private Texture2D _sawpost;
        private Vector2 _post1;
        private Vector2 _post2;
        private bool _horizontal;
        private bool _forwards;
        private bool oneToTwo = true;


        public SawBlade(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(100, 100))
        {

            AssetManager.RequestTexture("turtle", (frames) =>
            {
                sprites = frames;
                Sprite = new SpriteData(sprites);
                Sprite.Size = new Vector2(100, 100);
                Sprite.Layer = Layer;
                Sprite.Offset = new Vector2(0, 0);
            });

            AssetManager.RequestTexture("yara", (frames) =>
            {
                postSprites = frames;
                postSprite = new SpriteData(postSprites);
                postSprite.Size = new Vector2(100, 100);
                postSprite.Layer = Layer;
                postSprite.Offset = new Vector2(0, 0);
            });


            _post1 = pos;
            _post2 = new Vector2(200, 300);//pos;
            //_sawpost = this.Game.Content.Load<Texture2D>("SawPost");

            _horizontal = false;
            _forwards = false;

            Hitbox = new Rectangle(0, 0, 100, 100);
        }

        public void setValues(Vector2 post1, Vector2 post2, bool is_horizontal)
        {
            _post1 = post1;
            _post2 = post2;
            _horizontal = is_horizontal;
        }

        public override void Update()
        {
            float distance = 1;

            if (oneToTwo)
            {
                Vector2 direction = Position - _post2;
            }
            //if (_horizontal) 
            //{
            //    if (Position.X == _post1.X || Position.X == (_post2.X - Size.X))
            //    {
            //        Reverse_direction();
            //    }
            //}
            //else
            //{
            //    if (Position.Y == _post1.Y || Position.Y == (_post2.Y - Size.Y))
            //    {
            //        Reverse_direction();
            //    }
            //}

            //if (_horizontal && _forwards)
            //{
            //    Position = new Vector2(Position.X + distance, Position.Y);
            //}
            //else if (_horizontal && !_forwards)
            //{
            //    Position = new Vector2(Position.X - distance, Position.Y);
            //}
            //else if (!_horizontal && _forwards)
            //{
            //    Position = new Vector2(Position.X, Position.Y + distance);
            //}
            //else
            //{
            //    Position = new Vector2(Position.X, Position.Y - distance);
            //}


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_sawpost, _post1, Color.White);
            //spriteBatch.Draw(_sawpost, _post2, Color.White);
            base.Draw(spriteBatch);
            postSprite?.Draw(spriteBatch, _post1);
            postSprite?.Draw(spriteBatch, _post2);

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
