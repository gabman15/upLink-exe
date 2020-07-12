using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upLink_exe.GameObjects;

namespace upLink_exe
{
    public abstract class AbsDialogue : Component
    {

        public abstract void fade_out();
        public abstract void Set_at_bottom();
  
    }
}
