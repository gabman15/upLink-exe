using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upLink_exe
{
    public class SpriteData
    {
        public int CurrentFrame
        {
            get
            {
                return (int)currentFrame;
            }
            set
            {
                currentFrame = value;
            }
        }
        private float currentFrame;
        public SpriteEffects SpriteEffect { get; set; }
        public float Speed { get; set; }
        public Texture2D[] Frames { get; set; }
        public Vector2 Size { get; set; }
        public float Layer { get; set; }
        public Vector2 Offset { get; set; }
        public float Angle { get; set; }
        public Rectangle? Source { get; set; }
        public SpriteData()
        {
            CurrentFrame = 0;
            Speed = 1;
            Layer = 0;
            Offset = new Vector2(0, 0);
            SpriteEffect = SpriteEffects.None;
            Source = null;
        }
        public SpriteData(Texture2D[] frames) : base()
        {
            Frames = frames;
            CurrentFrame = 0;
            Speed = 1;
            Layer = 0;
            Offset = new Vector2(0, 0);
            SpriteEffect = SpriteEffects.None;
            Source = null;
        }
        public void Update()
        {
            if (Frames == null) return;
            currentFrame += 1;
            if (currentFrame < 0) currentFrame += Frames.Length;
            if (currentFrame >= Frames.Length) currentFrame -= Frames.Length;
        }
        public void Draw(SpriteBatch batch, Vector2 position)
        {
            Draw(batch, position, Color.White);
        }
        public void Draw(SpriteBatch batch, Vector2 position, Color color)
        {
            if (Frames != null)
            {
                Rectangle drawRect = new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y);
                batch.Draw(Frames[CurrentFrame], drawRect, Source, color, Angle, Offset, SpriteEffect, Layer);
            }
        }
        public void Change(Texture2D[] newSprite)
        {
            if (Frames != newSprite)
            {
                Frames = newSprite;
                CurrentFrame = 0;
            }
        }
    }
}
