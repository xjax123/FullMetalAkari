using Crankshaft.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crankshaft.Primitives
{
    public class Error : gameObject
    {
        public Error(objectData d) : base(d)
        {
            onLoad();
        }
    }
}
