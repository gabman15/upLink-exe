using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upLink_exe
{
    public class SoundManager
    {
        public List<SoundEffectInstance> LiveSounds;
        public SoundEffectInstance CurrentMusic;
        public SoundManager()
        {
            LiveSounds = new List<SoundEffectInstance>();
            CurrentMusic = null;
        }
        public void PlayMusic(SoundEffect song = null, float volume = 0.1f)
        {
            if (song == null) return;
            CurrentMusic?.Stop();
            CurrentMusic?.Dispose();
            CurrentMusic = null;
            CurrentMusic = song.CreateInstance();
            CurrentMusic.Volume = volume;
            CurrentMusic.IsLooped = true;
            CurrentMusic.Play();
        }
        public void PlaySound(SoundEffect sound, float volume = 1f)
        {
            if (sound == null) return;
            SoundEffectInstance soundInstance = sound.CreateInstance();
            soundInstance.Volume = volume;
            soundInstance.Play();
            LiveSounds.Add(soundInstance);
        }
        public void Update()
        {
            for(int i = LiveSounds.Count - 1; i >= 0; i--)
            {
                if(LiveSounds[i].State == SoundState.Stopped)
                {
                    LiveSounds.RemoveAt(i);
                }
            }
        }
        public void Destroy()
        {
            for(int i = LiveSounds.Count - 1; i >= 0; i--)
            {
                SoundEffectInstance sound = LiveSounds[i];
                sound.Stop();
                sound.Dispose();
                LiveSounds.RemoveAt(i);
            }
            CurrentMusic?.Stop();
            CurrentMusic?.Dispose();
            CurrentMusic = null;
        }
    }
}
