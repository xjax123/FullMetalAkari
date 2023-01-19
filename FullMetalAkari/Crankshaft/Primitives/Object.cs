using System;
using System.Collections.Generic;
using System.Text;
using FullMetalAkari.Crankshaft.Interfaces;

namespace FullMetalAkari.Crankshaft.Primitives
{
    class Object : IObject
    {
        //stored data
        int objectID;
        int instanceID;
        String name = "Unknown Object";

        public Object(int instanceID)
        {
            this.instanceID = instanceID;
        }

        public virtual void onClick()
        {
            throw new NotImplementedException();
        }

        public virtual void onDestroy()
        {
            throw new NotImplementedException();
        }

        public virtual void onLoad()
        {
            throw new NotImplementedException();
        }
    }
}
