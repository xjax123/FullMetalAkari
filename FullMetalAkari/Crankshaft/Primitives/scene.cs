using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Data;

namespace Crankshaft.Primitives
{
    public class Scene
    {
        public string sceneID;
        public string sceneName;
        public List<gameObject> objects = new List<gameObject>();
        public Scene(sceneData d, List<gameObject> objs)
        {
            this.sceneID = d.sceneID;
            this.sceneName = d.sceneName;
            this.objects = objs;
        }
    }
}
