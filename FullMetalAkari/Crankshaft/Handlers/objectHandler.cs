using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Primitives;
using Crankshaft.Data;
using Crankshaft.Exceptions;
using FullMetalAkari.Game.Objects.UI;

namespace Crankshaft.Handlers
{
    public static class objectHandler
    {
        public static gameObject buildObject(objectData o)
        {
            gameObject intObject;
            switch (o.type)
            {
                case "empty":
                    intObject = new gameObject(o);
                    break;
                case "scope":
                    intObject = new sniperCrosshair(o);
                    break;
                default:
                    throw new ObjectNotFoundException();
            }
            return intObject;
        }
    }
}
