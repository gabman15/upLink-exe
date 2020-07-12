using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace upLink_exe.GameObjects
{
    class RedWire : GameObject
    {
        public RedWire(Room room, Vector2 pos) : base(room, pos, new Vector2(0,0), new Vector2(100,100))
        {
            AssetManager.RequestTexture("redWire", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(100, 100);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
            });
        }

        public override void Collision(Player player)
        {
            Console.WriteLine(Position + ":" + player.justPlacedWire?.Position);
            if (player.draggingWire != "" && Position != player.justPlacedWire.Position)
            {
                Console.WriteLine("collide");
                player.draggingWire = "";
                player.draggingFrom = null;
            }
        }
    }
}
