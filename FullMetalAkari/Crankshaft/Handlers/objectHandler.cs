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
        public static gameObject buildObject(objectData d)
        {
            gameObject intObject;
            String type = d.Type.ToLower();
            switch (type)
            {
                case "empty":
                    intObject = new gameObject(d);
                    break;
                case "scope":
                    intObject = new sniperCrosshair(d);
                    break;
                case "barrel":
                    intObject = new Barrel(d);
                    break;
                case "bottle":
                    intObject = new Bottle(d);
                    break;
                case "target":
                    intObject = new Target(d);
                    break;
                case "hud":
                    intObject = new HUD(d);
                    break;
                default:
                    throw new ObjectNotFoundException();
            }
            return intObject;
        }
    }
}
