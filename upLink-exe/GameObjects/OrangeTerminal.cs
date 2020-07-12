using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace upLink_exe.GameObjects
{
    class OrangeTerminal : GameObject
    {
        public bool completed;
        public OrangeTerminal(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(100, 100))
        {
            AssetManager.RequestTexture("orangeTerminal", (frames) =>
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
            if (!completed)
            {
                if (player.draggingWire == "")
                {
                    player.draggingWire = "orange";
                    player.draggingFrom = this;
                }
                else if (player.draggingWire == "orange" && player.draggingFrom != this)
                {
                    completed = true;
                    ((OrangeTerminal)player.draggingFrom).completed = true;
                    player.draggingWire = "";
                    player.draggingFrom = null;
                }
            }
        }
        public void UpdateWithPlayerPos(Player player)
        {
            if ((Math.Abs(Position.X - player.Position.X) >= 100 || Math.Abs(Position.Y - player.Position.Y) >= 100) && completed)
                Solid = true;
        }
    }
}
