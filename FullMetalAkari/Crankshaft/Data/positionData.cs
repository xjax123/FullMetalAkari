using System;
using System.Collections.Generic;
using System.Text;

namespace Crankshaft.Data
{
    public class positionData
    {
        //Required Data
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        //Functions
        //Empry Constructor
        public positionData()
        {
            X = 0;
            Y = 0;
            Z = 0;
            Scale = 0;
            Rotation = 0;
        }
        public override string ToString()
        {
            return "[ X:" + X + ", Y:" + Y + ", Z:" + Z + ", Scale:" + Scale + ", Rotation:"+ Rotation + "]";
        }
    }
}
