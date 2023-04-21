using Crankshaft.Primitives;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Text;

namespace Crankshaft.Handlers
{
    static class soundHandler
    {
        private static Dictionary<string, Sound> soundLibrary = new Dictionary<string, Sound>();

        public static Dictionary<string, Sound> SoundLibrary { get => soundLibrary; set => soundLibrary = value; }
        public static void compileSounds(string dirPath)
        {
            SoundLibrary = new Dictionary<string, Sound>();
            string[] wavFiles = Directory.GetFiles(
            AppDomain.CurrentDomain.BaseDirectory + dirPath, "*.wav");
            foreach (string s in wavFiles)
            {
                int index = s.LastIndexOf("\\");
                string name = s.Substring(index+1);
                index = name.LastIndexOf(".");
                name = name.Substring(0,index);
                Debug.WriteLine(name.ToLower());
                SoundLibrary.Add(name.ToLower(), new Sound(s, name.ToLower()));
            }

        }

        public static Sound retrieveSound(string name)
        {
            bool attempt = soundLibrary.TryGetValue(name.ToLower(), out Sound sound);
            if (!attempt)
            {
                throw new FileNotFoundException(name);
            }
            return sound;
        }

        public static void CheckALError(string str)
        {
            ALError error = AL.GetError();
            if (error != ALError.NoError)
            {
                Debug.WriteLine($"ALError at '{str}': {AL.GetErrorString(error)}");
            }
        }
    }
}
