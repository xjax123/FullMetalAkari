using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Handlers;
using Crankshaft.Primitives;
using Crankshaft.Exceptions;
using Crankshaft.Data;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Crankshaft.Handlers
{
    public static class sceneHandler
    {
        private static Dictionary<string, sceneData> sceneLibrary = new Dictionary<string, sceneData>();

        public static Dictionary<string, sceneData> SceneLibrary { get => sceneLibrary; set => sceneLibrary = value; }

        /// <summary>
        /// loads a scene from the library 
        /// </summary>
        /// <param name="id">ID of the scene, as defined in its .sdta</param>
        /// <returns>a compiled Scene</returns>
        /// <remarks>must have called compileScenes at some point, other wise this function will never work.</remarks>
        public static void loadScene(string id)
        {
            sceneData localScene;
            if (SceneLibrary.ContainsKey(id))
            {
                SceneLibrary.TryGetValue(id, out localScene);
                windowHandler.ActiveWindow.Title = localScene.sceneName;
                List<gameObject> intList = new List<gameObject>();
                foreach (objectData o in localScene.objects)
                {
                    intList.Add(objectHandler.buildObject(o));
                }
                Scene compScene = new Scene(localScene, intList);
                windowHandler.ActiveScene = compScene;

                foreach (gameObject g in windowHandler.ActiveScene.objects)
                {
                    g.onLoad();
                }
            }
            else
            {
                throw new SceneNotFoundException();
            }
        }

        /// <summary>
        /// compiles the scenes from the specified file path into the scene libraray (a static dictionary)
        /// </summary>
        /// <param name="dirPath">path to the Directiory that you are storing scene files in.</param>
        /// <remarks>each scene must have a unique ID field & be JSON structured files ending with '.sdta'</remarks>
        public static void compileScenes(string dirPath)
        {
            string[] filenames = Directory.GetFiles(
            AppDomain.CurrentDomain.BaseDirectory + dirPath, "*.sdta");

            foreach (string s in filenames)
            {
                string fs = File.ReadAllText(s);
                sceneData scndta = JsonConvert.DeserializeObject<sceneData>(fs);
                SceneLibrary.TryAdd(scndta.sceneID,scndta);
                Debug.WriteLine($"{scndta.sceneName} loaded");
            }
            Debug.WriteLine("All scenes loaded");
        }


    }
}
