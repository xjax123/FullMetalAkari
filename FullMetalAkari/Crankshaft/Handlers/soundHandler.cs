using Crankshaft.Primitives;
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
            string[] mp3Files = Directory.GetFiles(
            AppDomain.CurrentDomain.BaseDirectory + dirPath, "*.mp3");

            foreach (string s in wavFiles)
            {
                int index = s.LastIndexOf("\\");
                string name = s.Substring(index+1);
                index = name.LastIndexOf(".");
                name = name.Substring(0,index);
                Debug.WriteLine(name);
                SoundLibrary.Add(name, new Sound(s, name));
            }
            foreach (string s in mp3Files)
            {
                int index = s.LastIndexOf("\\");
                string name = s.Substring(index + 1);
                index = name.LastIndexOf(".");
                name = name.Substring(0, index);
                Debug.WriteLine(name);
                SoundLibrary.Add(name, new Sound(s, name));
            }


        }
    }
}
