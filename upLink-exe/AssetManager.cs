using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upLink_exe
{
    public static class AssetManager
    {
        private static Dictionary<string, Texture2D[]> textureData = new Dictionary<string, Texture2D[]>();
        private static Dictionary<string, SoundEffect> soundData = new Dictionary<string, SoundEffect>();
        private static List<KeyValuePair<string, Action<Texture2D[]>>> textureRequests = new List<KeyValuePair<string, Action<Texture2D[]>>>();
        private static List<KeyValuePair<string, Action<SoundEffect>>> soundRequests = new List<KeyValuePair<string, Action<SoundEffect>>>();
        public static ContentManager Content;
        public static Texture2D[] LoadTexture(string name, string foldername, int frameCount)
        {
            Texture2D[] frames = new Texture2D[frameCount];
            if (foldername[foldername.Length - 1] != '\\')
            {
                foldername += '\\';
            }
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = Content.Load<Texture2D>(foldername + i.ToString());
            }
            for(int i = textureRequests.Count - 1; i >= 0; i--)
            {
                KeyValuePair<string, Action<Texture2D[]>> req = textureRequests[i];
                if(req.Key.Equals(name))
                {
                    req.Value.Invoke(frames);
                    textureRequests.RemoveAt(i);
                }
            }
            textureData[name] = frames;
            return frames;
        }
        public static void RequestTexture(string name, Action<Texture2D[]> callback)
        {
            if (textureData.ContainsKey(name))
            {
                callback.Invoke(textureData[name]);
            }
            else
            {
                textureRequests.Add(new KeyValuePair<string, Action<Texture2D[]>>(name, callback));
            }
        }
        public static SoundEffect LoadSound(string name, string path)
        {
            SoundEffect effect;
            effect = Content.Load<SoundEffect>(path);
            for (int i = soundRequests.Count - 1; i >= 0; i--)
            {
                KeyValuePair<string, Action<SoundEffect>> req = soundRequests[i];
                if (req.Key.Equals(name))
                {
                    req.Value.Invoke(effect);
                    soundRequests.RemoveAt(i);
                }
            }
            soundData[name] = effect;
            return effect;
        }
        /*public static Song LoadSong(string name, string path)
        {
            Song song = Content.Load<Song>(path);
            for (int i = songRequests.Count - 1; i >= 0; i--)
            {
                KeyValuePair<string, Action<Song>> req = songRequests[i];
                if (req.Key.Equals(name))
                {
                    req.Value.Invoke(song);
                    songRequests.RemoveAt(i);
                }
            }
            songData[name] = song;
            return song;
        }
        public static void RequestSong(string name, Action<Song> callback)
        {
            if (textureData.ContainsKey(name))
            {
                callback.Invoke(songData[name]);
            }
            else
            {
                songRequests.Add(new KeyValuePair<string, Action<Song>>(name, callback));
            }
        }*/
        public static void RequestSound(string name, Action<SoundEffect> callback)
        {
            if (soundData.ContainsKey(name))
            {
                callback.Invoke(soundData[name]);
            }
            else
            {
                soundRequests.Add(new KeyValuePair<string, Action<SoundEffect>>(name, callback));
            }
        }
    }
}
