﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace upLink_exe.GameObjects
{
    class BlockObject : GameObject
    {
        public BlockObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0,0), new Vector2(100,100))
        {
            Solid = true;
        }
    }
}
