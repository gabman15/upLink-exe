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
using System.IO;

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

        public DialogueHandler(Room room, Vector2 pos, string filename, ContentManager content) : base(room, pos, new Vector2(0, 0), new Vector2(100, 100))
        {
            SpriteFont _font = content.Load<SpriteFont>("newFont");
            Texture2D _gray_square = content.Load<Texture2D>("gray_square");
            Texture2D _text_background = content.Load<Texture2D>("dialogue_background");

            _dialogues = new List<AbsDialogue>();
            string[] lines = File.ReadAllLines(filename, Encoding.UTF8);
            for (int i = 0; i < lines.Length; i++)
            {
                IntenseDialogue dialogue = new IntenseDialogue(lines[i], _font, _gray_square, _text_background, content);
                _dialogues.Add(dialogue);
            }

            _dialogues[0].Set_at_bottom();

            index = -1;
            things_to_update = new List<int>();
            is_running = false;
            released = true;
        }

        public override void Collision(Player player)
        {
            Console.WriteLine("Begin dialogue");
            is_running = true;
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
                    //Console.WriteLine("for loop");
                    _dialogues[things_to_update[i]].Draw(spriteBatch);
                }
            }
           
        }

        public override void Update()
        {
            //Console.WriteLine("running???? " + is_running);

            if (is_running)
            {
                var kstate = Keyboard.GetState();

                if (kstate.IsKeyDown(Keys.Space))
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
