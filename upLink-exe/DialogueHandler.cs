using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using upLink_exe.GameObjects;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace upLink_exe
{
    public class DialogueHandler : GameObject
    {
        private List<AbsDialogue> _dialogues;
        private List<int> things_to_update;
        private int index;
        private bool is_running;
        private bool released;

        public DialogueHandler(Room room, Vector2 pos, Vector2 vel, Vector2 size) :base(room,pos,vel,size)
        {
            //nothing to see here 
        }

        public DialogueHandler(List<AbsDialogue> dialogues) :base(null,new Vector2(0,0), new Vector2(0, 0), new Vector2(0, 0))
        {
            _dialogues = dialogues;
            
            _dialogues[0].Set_at_bottom();

            index = -1;
            things_to_update = new List<int>();
            is_running = false;
            released = true;
        }
        
        public DialogueHandler(ContentManager content) : base(null, new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0))
        {
            SpriteFont _font = content.Load<SpriteFont>("test_font");
            Texture2D image1 = content.Load<Texture2D>("400x300_Yon");
            Texture2D image2 = content.Load<Texture2D>("400x300_Yara");
            Texture2D _gray_square = content.Load<Texture2D>("gray_square");
            Texture2D _text_background = content.Load<Texture2D>("dialogue_background");

            IntenseDialogue dia1 = new IntenseDialogue(image1, image2, "Chief Experimenter", "Yon", false, true, "", _font, _text_background, _gray_square);
            IntenseDialogue dia2 = new IntenseDialogue(image1, image2, "Chief Experimenter", "Yon", true, false, "", _font, _text_background, _gray_square);
            IntenseDialogue dia3 = new IntenseDialogue(image1, image2, "Chief Experimenter", "Yon", false, true, "", _font, _text_background, _gray_square);
            IntenseDialogue dia4 = new IntenseDialogue(image1, image2, "Chief Experimenter", "Yon", true, false, "", _font, _text_background, _gray_square);
            IntenseDialogue dia5 = new IntenseDialogue(image1, image2, "Chief Experimenter", "Yon", false, true, "", _font, _text_background, _gray_square);

            List<AbsDialogue> dialogues = new List<AbsDialogue>()
            {
                dia1, dia2, dia3, dia4, dia5,
            };
            
            _dialogues = dialogues;
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

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (is_running)
            {
                for (int i = 0; i < things_to_update.Count; i++)
                {
                    _dialogues[things_to_update[i]].Draw(spriteBatch);
                }
            }
           
        }

        public override void Update()
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
                    _dialogues[things_to_update[i]].Update();
                }

            }
     
        }
    }
}
