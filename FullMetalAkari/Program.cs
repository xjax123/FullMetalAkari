//Cody By: Jackson Maclean
//Generic
using System;
using System.Windows;
//OpenTK
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace FullMetalAkari
{
    public static class Program
    {
        private static void Main()
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1920, 1080),
                Title = "Full Metal Akari",
                // This is needed to run on macos
                Flags = ContextFlags.ForwardCompatible,
            };

            // To create a new window, create a class that extends GameWindow, then call Run() on it.
            using (var window = new windowHandler(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }

        }
    }
}
