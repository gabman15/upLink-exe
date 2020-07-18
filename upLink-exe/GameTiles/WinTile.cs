using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace upLink_exe.GameTiles
{
    class WinTile : GameTile
    {
        public WinTile(Room room, Vector2 pos) : base(room, pos, new Vector2(800, 800))
        {
            AssetManager.RequestTexture("win", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(800, 800);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
            });
        }
    }
}
