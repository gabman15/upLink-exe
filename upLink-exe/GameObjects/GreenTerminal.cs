using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace upLink_exe.GameObjects
{
    class GreenTerminal : GameObject
    {
        public bool completed;
        public GreenTerminal(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(100, 100))
        {
            AssetManager.RequestTexture("greenTerminal", (frames) =>
            {
                completed = false;
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(100, 100);
                Sprite.Layer = Layer;
                Sprite.Offset = new Vector2(0, 0);
            });
        }

        public override void Collision(Player player)
        {
            player.draggingWire = "green";
        }
    }
}
