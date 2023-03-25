using System;
using System.Collections.Generic;
using System.Text;
using WMPLib;

namespace FullMetalAkari.Crankshaft.Primitives
{
    class Sound
    {
        protected string path;
        protected string name;
        private byte volume;
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        public string Path { get => path; set => path = value; }
        public string Name { get => name; set => name = value; }
        public byte Volume { get => volume; set => volume = value; }

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
            Path = path;
            Name = name;
            setVolume(volume);
        }

        public void Play()
        {
            player = new WindowsMediaPlayer();
            player.settings.volume = volume;
            player.URL = Path;
        }

        public void PlayLoop()
        {
            player = new WindowsMediaPlayer();
            player.settings.volume = volume;
            player.URL = Path;
            player.settings.setMode("loop", true);
        }

        public void setVolume(byte v)
        {
            if (v < 100)
            {
                Volume = v;
            } else
            {

                Volume = 100;
            }
            player.settings.volume = (int) Volume;
        }
    }
}
