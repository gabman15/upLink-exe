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
    // Placeholder class copied from Player, used to test collisions
    public class NPC : GameObject
    {
        private Texture2D[] idleSprite;
        public static float MoveSpeed = 3f;


        public NPC(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(100, 100))
        {
            AssetManager.RequestTexture("turtle", (frames) =>
            {
                idleSprite = frames;
                Sprite = new SpriteData(idleSprite);
                Sprite.Size = new Vector2(100, 100);
                Sprite.Layer = Layer;
                Sprite.Offset = new Vector2(0, 0);
            });


            Hitbox = new Rectangle(0, 0, 100, 100);
            Solid = false;

        }
    }
}
