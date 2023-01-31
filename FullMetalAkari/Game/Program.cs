//Cody By: Jackson Maclean
//Generic
using System;
using System.Windows;
//OpenTK
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
//Internal
using Crankshaft.Handlers;

namespace FullMetalAkari
{
    public static class Program
    {
        private static void Main()
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1920, 1080),
                WindowState = WindowState.Maximized,
                Title = "Full Metal Akari",
                // This is needed to run on macos
                Flags = ContextFlags.ForwardCompatible,
            };

            //Create The Game Window
            using (var window = new windowHandler(GameWindowSettings.Default, nativeWindowSettings, "/Game/Scenes", "demo"))
            {
                window.Run();
            }

        }
    }
}
