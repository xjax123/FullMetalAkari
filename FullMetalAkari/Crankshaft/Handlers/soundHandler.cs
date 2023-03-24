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
        private static Dictionary<string, SoundPlayer> soundLibrary = new Dictionary<string, SoundPlayer>();

        public static Dictionary<string, SoundPlayer> SoundLibrary { get => soundLibrary; set => soundLibrary = value; }

        public static void compileSounds(string dirPath)
        {
            SoundLibrary = new Dictionary<string, SoundPlayer>();
            string[] filenames = Directory.GetFiles(
            AppDomain.CurrentDomain.BaseDirectory + dirPath, "*.wav");

            foreach (string s in filenames)
            {
                SoundPlayer temp = new SoundPlayer(s);
                int index = s.LastIndexOf("\\");
                string name = s.Substring(index+1);
                index = name.LastIndexOf(".");
                name = name.Substring(0,index);
                Debug.WriteLine(name);
                SoundLibrary.Add(name, temp);
            }
        }
    }
}
