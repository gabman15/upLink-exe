using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using upLink_exe.GameObjects;


namespace upLink_exe
{
    class IntenseDialogue : AbsDialogue
    {

        private int _people;
        private Texture2D _person1;
        private Texture2D _person2;
        private Vector2 _location1;
        private Vector2 _location2;
        private Vector2 _background_location;
        private string _name1;
        private string _name2;
        private Texture2D _background;
        private Texture2D _gray_square;
        private string _text;
        private SpriteFont _font;
        private const float MaxLineWidth = 700;
        private float _fade = 0;
        private bool _fading_out = false;
        private bool _speaking1;
        private bool _speaking2;

        public IntenseDialogue(Texture2D person1, Texture2D person2, string name1, string name2, bool speaking1, bool speaking2, string text, SpriteFont font, Texture2D background, Texture2D gray_square)
        {
            _people = 2;
            _person1 = person1;
            _person2 = person2;
            _name1 = name1;
            _name2 = name2;
            _background = background;
            _gray_square = gray_square;
            _location1 = new Vector2(50, 90);
            _location2 = new Vector2(450, 90);
            _background_location = new Vector2(15, 490);
            _text = Wrap_text(text, font);
            _font = font;
            _speaking1 = speaking1;
            _speaking2 = speaking2;
        }

        public IntenseDialogue(Texture2D person1, string name1, string text, SpriteFont font, Texture2D background, Texture2D gray_square)
        {
            _people = 1;
            _person1 = person1;
            _name1 = name1;
            _background = background;
            _gray_square = gray_square;
            _location1 = new Vector2(250, 90);
            _background_location = new Vector2(15, 490);
            _text = Wrap_text(text, font);
            _font = font;
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


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (!_fading_out && _fade < 1)
            {
                _fade += .03f;
            }
            else if (_fading_out && _fade > 0)
            {
                _fade -= .03f;
            }

            spriteBatch.Draw(_gray_square, new Vector2(0, 0), Color.DarkGray * .6f * _fade);

            if (_people == 1)
            {   
                spriteBatch.Draw(_person1, _location1, Color.White * _fade);
                spriteBatch.Draw(_background, _background_location, Color.White * _fade);
                spriteBatch.DrawString(_font, _name1, new Vector2(30, 500), Color.Black * _fade);
                spriteBatch.DrawString(_font, _text, new Vector2(50, 550), Color.Black * _fade);
            }
            else
            {

                Color color1 = Color.DarkGray;
                Color color2 = Color.DarkGray;
                if (_speaking1)
                    color1 = Color.White;
                if (_speaking2)
                    color2 = Color.White;

                spriteBatch.Draw(_person1, _location1, color1 * _fade);
                spriteBatch.Draw(_person2, _location2, color2 * _fade);
                spriteBatch.Draw(_background, _background_location, Color.White * _fade);

                spriteBatch.DrawString(_font, _text, new Vector2(50, 550), Color.Black * _fade);
                if (_speaking1)
                {
                    spriteBatch.DrawString(_font, _name1, new Vector2(30, 500), Color.Black * _fade);
                }
                else
                {
                    spriteBatch.DrawString(_font, _name2, new Vector2(30, 500), Color.Black * _fade);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            //nothing to do here...
        }
    }
}
