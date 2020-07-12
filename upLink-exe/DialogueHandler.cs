using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using testing.GameObjects;

namespace upLink_exe
{
    public class DialogueHandler : Component
    {
        private List<AbsDialogue> _dialogues;
        private List<int> things_to_update;
        private int index;
        private bool is_running;
        private bool released;

        public DialogueHandler(List<AbsDialogue> dialogues)
        {
            _dialogues = dialogues;
            
            _dialogues[0].Set_at_bottom();

            index = -1;
            things_to_update = new List<int>();
            is_running = false;
            released = true;
        }

        public void Next()
        {
            //Console.WriteLine("index: " + index);
            //Console.WriteLine("dia.count: " + _dialogues.Count);

            if (index < _dialogues.Count - 1) {
                if (index != -1)
                {
                    _dialogues[index].fade_out();
                }
                index++;
                things_to_update.Add(index);
            } 
            else if (index == _dialogues.Count - 1)
            {
                //Console.WriteLine("hit else");
                _dialogues[index].fade_out();
                is_running = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (is_running)
            {
                for (int i = 0; i < things_to_update.Count; i++)
                {
                    _dialogues[things_to_update[i]].Draw(gameTime, spriteBatch);
                }
            }
           
        }

        public override void Update(GameTime gameTime)
        {
            if (is_running)
            {
                var kstate = Keyboard.GetState();

                if (kstate.IsKeyDown(Keys.F))
                {
                    if (released)
                    {
                        this.Next();
                        released = false;
                    }
                }
                else
                {
                    released = true;
                }

                if (things_to_update.Count >= 3)
                {
                    things_to_update.RemoveAt(0);
                }

                for (int i = 0; i < things_to_update.Count; i++)
                {
                    //Console.WriteLine("things.count is: " + things_to_update[i]);
                    _dialogues[things_to_update[i]].Update(gameTime);
                }

            }
     
        }
    }
}
