using System;
using System.Collections.Generic;
using System.Text;

namespace FullMetalAkari.Crankshaft.Interfaces
{
    interface IObject
    {
        public void onLoad();
        public void onDestroy();
        public void onClick();
    }
}
