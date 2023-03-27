using Crankshaft.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WMPLib;

#nullable enable
namespace FullMetalAkari.Game.Objects.Sounds
{
    enum SoundStatus{

    }

    //TODO: fix this to make it actually Async, not running off the main loop.
    public class gameMusic : Sound
    {
        public enum currentlyPlaying {
            Intro,
            Body,
            Outro
        }

        public enum loopState
        {
            Loop,
            NoLoop
        }

        private string? intro;
        private currentlyPlaying current;
        private loopState loop;
        protected string? Intro { get => intro; set => intro = value; }
        public currentlyPlaying Current { get => current; set => current = value; }
        public loopState Loop { get => loop; set => loop = value; }

        public gameMusic(string songPath, string? introPath, string name, loopState loop, byte volume = 50)
        {
            Path = songPath;
            Intro = introPath;
            Name = name;
            Volume = volume;
            Loop = loop;
        }

        public void onUpdate()
        {
            try
            {
                if (Player.playState == WMPPlayState.wmppsStopped && current == currentlyPlaying.Intro)
                {
                    Player = new WindowsMediaPlayer();
                    Player.settings.volume = Volume;
                    Player.URL = Path;
                    current = currentlyPlaying.Body;
                    Player.controls.play();
                    if (Loop == loopState.Loop)
                    {
                        Player.settings.setMode("loop", true);
                    }
                }
            } catch
            {
                Debug.WriteLine("Thready Busy, Action Skipped");
            }
        }

        public void startSong()
        {
            Player = new WindowsMediaPlayer();
            Player.settings.volume = Volume;
            Player.URL = Intro;
            current = currentlyPlaying.Intro;
        }
    }
}
