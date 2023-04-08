using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Channels;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL;
using WMPLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Crankshaft.Primitives
{
    public class Sound
    {
        //Protected variables
        protected int source;
        protected int buffer;
        protected byte[] soundData;
        protected int channels;
        protected int bytes;
        protected int samples;

        //Public variables
        private string path;
        private string name;
        private int volume;
        private bool loopState;

        public string Path { get => path; set => path = value; }
        public string Name { get => name; set => name = value; }
        public int Volume { get => volume; set => volume = value; }
        public bool LoopState { get => loopState; set => loopState = value; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Filepath for the sound file</param>
        /// <param name="name">Name of the sound</param>
        public Sound(string path, string name) 
        {
            buffer = AL.GenBuffer();
            source = AL.GenSource();
            this.path = path;
            this.name = name;
            volume = 100;
            loopState = false;

            soundData = loadSound(File.Open(path, FileMode.Open), out channels, out bytes, out samples);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Filepath for the sound file</param>
        /// <param name="name">Name of the sound</param>
        /// <param name="volume">Volume of the sound</param>
        public Sound(string path, string name, int volume)
        {
            buffer = AL.GenBuffer();
            source = AL.GenSource();
            this.path = path;
            this.name = name;
            volume = 100;
            loopState = false;

            soundData = loadSound(File.Open(path, FileMode.Open), out channels, out bytes, out samples);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Filepath for the sound file</param>
        /// <param name="name">Name of the sound</param>
        /// <param name="volume">Volume of the sound</param>
        /// <param name="loopState">Whether the sound should loop or not</param>
        public Sound(string path, string name, int volume, bool loopState)
        {
            buffer = AL.GenBuffer();
            source = AL.GenSource();
            this.path = path;
            this.name = name;
            this.volume = volume;
            this.loopState = loopState;

            soundData = loadSound(File.Open(path, FileMode.Open), out channels, out bytes, out samples);
        }

        public virtual void Play()
        {
            AL.BufferData(buffer,GetSoundFormat(channels, bytes), buffer, soundData.Length, samples);
        }

        public virtual void Pause()
        {
        }

        public virtual void Stop()
        {
        }

        public virtual void setVolume(byte v)
        {
        }

        public ALSourceState queryState()
        {
            AL.GetSource(source, ALGetSourcei.SourceState, out int state);
            return (ALSourceState) state;
        }

        public static ALFormat GetSoundFormat(int channels, int bits)
        {
            switch (channels)
            {
                case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default: throw new NotSupportedException("The specified sound format is not supported.");
            }
        }

        private byte[] loadSound(Stream stream, out int Channels, out int bytesPerSample, out int sampleRate)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (BinaryReader reader = new BinaryReader(stream))
            {
                int format_chunk_size = reader.ReadInt32();
                int audio_format = reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                int byte_rate = reader.ReadInt32();
                int block_align = reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                string data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int data_chunk_size = reader.ReadInt32();

                Channels = num_channels;
                bytesPerSample = bits_per_sample;
                sampleRate = sample_rate;

                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }
    }
}
