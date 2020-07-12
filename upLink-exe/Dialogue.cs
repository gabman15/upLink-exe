using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace upLink_exe
{
    public class Dialogue : AbsDialogue
    {

        private Vector2 _background_pos;
        private Vector2 _picture_pos;
        private Vector2 _text_pos;
        private Texture2D _background;
        private Texture2D _picture;
        private SpriteFont _font;
        private string _text;
        private float _speed;
        private float MaxLineWidth = 525;
        private float _fade;
        private bool _fading_out = false;

        private const float MIN_Y = 490;


        public Dialogue(Texture2D background, Texture2D picture, string text, SpriteFont font, float speed)
        {

            _background = background;
            _picture = picture;
            _font = font;
            //_text = Wrap_text(text, font);
            _text = text;
            _speed = speed;

            
            _fade = 0;
            

            _background_pos = new Vector2(15, 490);
            _picture_pos = new Vector2(40, 515);
            _text_pos = new Vector2(220, 515);
            
        }

        //
        //taken from https://gist.github.com/Sankra/5585584
        //
/*
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
        */
        public override void Set_at_bottom()
        {
            _background_pos = new Vector2(15, 860);
            _picture_pos = new Vector2(40, 885);
            _text_pos = new Vector2(220, 885);
        }

        public override void fade_out()
        {
            _fading_out = true;
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

            spriteBatch.Draw(_background, _background_pos, Color.White * _fade);
            spriteBatch.Draw(_picture, _picture_pos, Color.White * _fade);
            spriteBatch.DrawString(_font, _text, _text_pos, Color.Black * _fade);
        }

        public override void Update()
        {

            float distance = _speed;

            if (_background_pos.Y >= MIN_Y)
            {
                _background_pos = new Vector2(_background_pos.X, _background_pos.Y - distance);
                _picture_pos = new Vector2(_picture_pos.X, _picture_pos.Y - distance);
                _text_pos = new Vector2(_text_pos.X, _text_pos.Y - distance);
            }
           
        }
    }

   
}
