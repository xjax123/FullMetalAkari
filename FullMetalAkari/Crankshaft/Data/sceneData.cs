using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Primitives;

namespace Crankshaft.Data
{
    public class sceneData
    {
        public string sceneID { get; set; }
        //Game Or Menu
        public string type { get; set; }
        public string nextScene { get; set; }
        public string sceneName { get; set; }
        public objectData[] objects { get; set; }
    }
}
