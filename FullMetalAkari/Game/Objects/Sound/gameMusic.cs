using Crankshaft.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WMPLib;

#nullable enable
namespace FullMetalAkari.Game.Objects.Sounds
{
    public class gameMusic : Sound
    {
        public gameMusic(string path, string name) : base(path, name)
        {
        }

        public gameMusic(string path, string name, int volume) : base(path, name, volume)
        {
        }
    }
}
