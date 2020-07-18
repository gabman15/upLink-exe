using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using upLink_exe.GameObjects;


namespace upLink_exe
{
    class IntenseDialogue : AbsDialogue
    {

        private int _people;
        private SpriteData _person1;
        private SpriteData _person2;
        private Vector2 _location1;
        private Vector2 _location2;
        private Vector2 _background_location;
        private string _name1;
        private string _name2;
        private SpriteData _background;
        private SpriteData _gray_square;
        private string _text;
        private SpriteFont _font;
        private const float MaxLineWidth = 700;
        private float _fade = 0;
        private bool _fading_out = false;
        private bool _speaking1;
        private bool _speaking2;

        public IntenseDialogue(string line, SpriteFont font, Texture2D gray_square, Texture2D text_background, ContentManager content)
        {
            string[] parts = line.Split(';');
            if (parts[0] == "twoperson")
            {
                Texture2D person1 = content.Load<Texture2D>(parts[1]);
                Texture2D person2 = content.Load<Texture2D>(parts[2]);
                string name1 = parts[3];
                string name2 = parts[4];
                bool speaking1 = (parts[5] == "true");
                bool speaking2 = (parts[6] == "true");
                string text = parts[7];
                setUpTwoPersonDialogue(person1, person2, name1, name2, speaking1, speaking2, text, font, text_background, gray_square);
            }
            else if (parts[0] == "oneperson")
            {
                Texture2D person = content.Load<Texture2D>(parts[1]);
                string name = parts[2];
                string text = parts[3];
                setUpOnePersonDialogue(person, name, text, font, text_background, gray_square);
            }
            else
            {
                Console.WriteLine("Unknown Dialogue Type: " + parts[0]);
            }
            

        }

        public IntenseDialogue(Texture2D person1, Texture2D person2, string name1, string name2, bool speaking1, bool speaking2, string text, SpriteFont font, Texture2D background, Texture2D gray_square)
        {
            setUpTwoPersonDialogue(person1, person2, name1, name2, speaking1, speaking2, text, font, background, gray_square);
        }

        public IntenseDialogue(Texture2D person1, string name1, string text, SpriteFont font, Texture2D background, Texture2D gray_square)
        {
            setUpOnePersonDialogue(person1, name1, text, font, background, gray_square);
        }

        public void setUpTwoPersonDialogue(Texture2D person1, Texture2D person2, string name1, string name2, bool speaking1, bool speaking2, string text, SpriteFont font, Texture2D background, Texture2D gray_square)
        {
            Texture2D[] person_one = { person1 };
            Texture2D[] person_two = { person2 };
            Texture2D[] background_array = { background };
            Texture2D[] gray_square_array = { gray_square };

            _people = 2;
            _person1 = new SpriteData(person_one);
            _person2 = new SpriteData(person_two);

            _person1.Size = new Vector2(300, 400);
            _person2.Size = new Vector2(300, 400);

            _name1 = name1;
            _name2 = name2;
            _background = new SpriteData(background_array);
            _gray_square = new SpriteData(gray_square_array);

            _background.Size = new Vector2(300, 770);
            _gray_square.Size = new Vector2(800, 800);

            _location1 = new Vector2(50, 90);
            _location2 = new Vector2(450, 90);
            _background_location = new Vector2(15, 490);
            _text = Wrap_text(text, font);
            _font = font;
            _speaking1 = speaking1;
            _speaking2 = speaking2;

            _person1.Layer = 0.10f;
            _person2.Layer = 0.10f;
            _background.Layer = 0.10f;
            _gray_square.Layer = 0.09f;
        }

        public void setUpOnePersonDialogue(Texture2D person1, string name1, string text, SpriteFont font, Texture2D background, Texture2D gray_square)
        {
            Texture2D[] person_one = { person1 };
            Texture2D[] background_array = { background };
            Texture2D[] gray_square_array = { gray_square };

            _people = 1;
            _person1 = new SpriteData(person_one);
            _name1 = name1;
            _background = new SpriteData(background_array);
            _gray_square = new SpriteData(gray_square_array);

            _people = 1;
            _name1 = name1;
            _location1 = new Vector2(250, 90);
            _background_location = new Vector2(15, 490);
            _text = Wrap_text(text, font);
            _font = font;

            _person1.Layer = 0.10f;
            _background.Layer = 0.10f;
            _gray_square.Layer = 0.09f;
        }

        public override void fade_out()
        {
            _fading_out = true;
        }

        public override void Set_at_bottom()
        {
            //does nothing lol BUT DO NOT DELETE
        }

        //
        //taken from https://gist.github.com/Sankra/5585584
        //

        private string Wrap_text(string text, SpriteFont font)
        {
            //Console.WriteLine(font);
            Console.WriteLine(text);
            if (font.MeasureString(text).X < MaxLineWidth)
            {
                return text;
            }

            string[] words = text.Split(' ');
            StringBuilder wrappedText = new StringBuilder();
            float linewidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;
            for (int i = 0; i < words.Length; ++i)
            {
                Vector2 size = font.MeasureString(words[i]);
                if (linewidth + size.X < MaxLineWidth)
                {
                    linewidth += size.X + spaceWidth;
                }
                else
                {
                    wrappedText.Append("\n");
                    linewidth = size.X + spaceWidth;
                }
                wrappedText.Append(words[i]);
                wrappedText.Append(" ");
            }

            return wrappedText.ToString();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {

            if (!_fading_out && _fade < 1)
            {
                _fade += .03f;
            }
            else if (_fading_out && _fade > 0)
            {
                _fade -= .03f;
            }

            _gray_square.Draw(spriteBatch, new Vector2(0, 0), Color.DarkGray * .6f * _fade);

            if (_people == 1)
            {
                //Console.WriteLine("people=1");
                _person1.Draw(spriteBatch, _location1, Color.White * _fade);
                _background.Draw(spriteBatch, _background_location, Color.White * _fade);

                spriteBatch.DrawString(_font, _name1, new Vector2(30, 500), Color.Black * _fade, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0.11f);
                spriteBatch.DrawString(_font, _text, new Vector2(50, 550), Color.Black * _fade, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0.11f);
            }
            else
            {
                //Console.WriteLine("people=2");
                Color color1 = Color.Gray;
                Color color2 = Color.Gray;
                if (_speaking1)
                    color1 = Color.White;
                if (_speaking2)
                    color2 = Color.White;

                //Console.WriteLine(_person1.Frames[0]);
                _person1.Draw(spriteBatch, _location1, Color.White * _fade);
                _person2.Draw(spriteBatch, _location2, Color.White * _fade);
                _background.Draw(spriteBatch, _background_location, Color.White * _fade);

                spriteBatch.DrawString(_font, _text, new Vector2(50, 550), Color.Black * _fade, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0.11f);

                if (_speaking1)
                {
                   
                    spriteBatch.DrawString(_font, _name1, new Vector2(30, 500), Color.Black * _fade, 0, new Vector2(0,0), 1, SpriteEffects.None, 0.11f);

                }
                else
                {
                    //spriteBatch.DrawString()

                    //spriteBatch.DrawString(_font, _text, new Vector2(30,500), Color.Black);
                    spriteBatch.DrawString(_font, _name2, new Vector2(30, 500), Color.Black * _fade, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0.11f);
                }
            }
        }

        public override void Update()
        {
            //nothing to do here...
        }
    }
}
