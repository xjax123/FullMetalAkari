using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Handlers;
using Crankshaft.Primitives;
using Crankshaft.Exceptions;

namespace Crankshaft.Handlers
{
    public static class sceneHandler
    {
        private static Dictionary<string, scene> sceneLibrary;

        public static scene buildScene()
        {
            throw new NotImplementedException();
        }

        public static scene loadScene(string id)
        {
            scene localScene;
            if (sceneLibrary.ContainsKey(id))
            {
                sceneLibrary.TryGetValue(id, out localScene);
                return localScene;
            }
            else
            {
                throw new SceneNotFoundException();
            }
        }

        public static void compileScenes(string filePath)
        {
            throw new NotImplementedException();
        }


    }
}
