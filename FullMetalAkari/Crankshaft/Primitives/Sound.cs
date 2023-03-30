using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WMPLib;

namespace Crankshaft.Primitives
{
    public class Sound
    {
        protected string path;
        protected string name;
        private byte volume;
        private WindowsMediaPlayer player;
        public string Path { get => path; set => path = value; }
        public string Name { get => name; set => name = value; }
        public byte Volume { get => volume; set => volume = value; }
        public WindowsMediaPlayer Player { get => player; set => player = value; }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Sound()
        {
        }
        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <param name="path">file path of the sound</param>
        public Sound(string path)
        {
            Path = path;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">file path of the sound</param>
        /// <param name="name">name of the sound</param>
        /// <param name="volume">volume of the sound, values higher than 100 get clamped to 100</param>
        public Sound(string path, string name, byte volume = 50)
        {
            player = new WindowsMediaPlayer();
            Path = AppDomain.CurrentDomain.BaseDirectory + path;
            Name = name;
            setVolume(volume);
        }

        public virtual void Play()
        {
            try
            {
                Player = new WindowsMediaPlayer();
                Player.settings.volume = volume;
                Player.URL = Path;
            } catch
            {
                Debug.WriteLine("Thready Busy, Action Skipped");
            }
        }

        public virtual void PlayLoop()
        {
            try
            {
                Player = new WindowsMediaPlayer();
                Player.settings.volume = volume;
                Player.URL = Path;
                Player.settings.setMode("loop", true);
            } catch
            {
                Debug.WriteLine("Thready Busy, Action Skipped");
            }
        }

        public virtual void setVolume(byte v)
        {
            if (Player == null)
            {
                return;
            }
            if (v < 100)
            {
                Volume = v;
            }
            else
            {

                Volume = 100;
            }
            Player.settings.volume = Volume;
        }
    }
}
