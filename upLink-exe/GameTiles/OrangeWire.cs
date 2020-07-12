using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace upLink_exe.GameTiles
{
    class OrangeWire : GameTile
    {
        public OrangeWire(Room room, Vector2 pos) : base(room, pos, new Vector2(100,100))
        {
            AssetManager.RequestTexture("orangeWire", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(100, 100);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
            });
        }
    }
}
