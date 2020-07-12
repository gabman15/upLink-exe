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
        private Vector2 _post1;
        private Vector2 _post2;
        private bool oneToTwo = true;


        public SawBlade(Room room, Vector2 pos, Vector2 post1, Vector2 post2) : base(room, pos, new Vector2(0, 0), new Vector2(100, 100))
        {

            // Load saw blade texture
            AssetManager.RequestTexture("turtle", (frames) =>
            {
                sprites = frames;
                Sprite = new SpriteData(sprites);
                Sprite.Size = new Vector2(100, 100);
                Sprite.Layer = Layer;
                Sprite.Offset = new Vector2(0, 0);
            });

            // Load saw post
            AssetManager.RequestTexture("yara", (frames) =>
            {
                postSprites = frames;
                postSprite = new SpriteData(postSprites);
                postSprite.Size = new Vector2(100, 100);
                postSprite.Layer = Layer;
                postSprite.Offset = new Vector2(0, 0);
            });


            _post1 = post1;
            _post2 = post2;

            Hitbox = new Rectangle(0, 0, 100, 100);
        }

        public override void Update()
        {
            if (Position == _post1)
            {
                oneToTwo = true;
            } else if (Position == _post2)
            {
                oneToTwo = false;
            }

            Vector2 direction;
            if (oneToTwo)
            {
                direction = Vector2.Normalize(_post2 - Position);
            }
            else
            {
                direction = Vector2.Normalize(_post1 - Position);
            }

            Position += direction/2 * 5; // Note: This cannot be arbitrary, as Position must exactly equal _post1/_post2. TODO: fix


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            postSprite?.Draw(spriteBatch, _post1);
            postSprite?.Draw(spriteBatch, _post2);

        }

        public override void Collision(Player player)
        {
            Console.WriteLine("Death!!!");
            currRoom.Game.RestartLevel();
        }

    }
}
