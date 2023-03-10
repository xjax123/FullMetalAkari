using System;
using System.Collections.Generic;
using System.Text;
using Crankshaft.Primitives;
using Crankshaft.Data;
using Crankshaft.Exceptions;
using FullMetalAkari.Game.Objects.UI;
using FullMetalAkari.Game.Objects.Game;

namespace Crankshaft.Handlers
{
    public static class objectHandler
    {
        public static gameObject buildObject(objectData o)
        {
            gameObject intObject;
            switch (o.Type)
            {
                case "empty":
                    intObject = new gameObject(o);
                    break;
                case "scope":
                    intObject = new sniperCrosshair(o);
                    break;
                case "barrel":
                    intObject = new Barrel(o);
                    break;
                default:
                    throw new ObjectNotFoundException();
            }
            return intObject;
        }
    }
}
