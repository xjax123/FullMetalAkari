using System;
using System.Collections.Generic;
using System.Text;

namespace Crankshaft.Data
{
    public class positionData
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float scale { get; set; }
        public float rotation { get; set; }

        public override string ToString()
        {
            return "[ X:" + X + ", Y:" + Y + ", Z:" + Z + ", Scale:" + scale + ", Rotation:"+ rotation + "]";
        }
    }
}
