using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upLink_exe.GameObjects;

namespace upLink_exe
{
    public class SawBlade : GameObject
    {

        public GameObject(Room room, Vector2 pos, Vector2 vel, Vector2 size)
        {
            Solid = false;
            currRoom = room;
            Position = pos;
            Velocity = vel;
            Size = size;
            Sprite = null;
            Layer = 0;
            Hitbox = new Rectangle(0, 0, (int)size.X, (int)size.Y);
        }

    }
}
