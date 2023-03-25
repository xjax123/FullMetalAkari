using System;
using System.Collections.Generic;
using System.Text;

namespace Crankshaft.Data
{
    public class timingData
    {
        //Required Data
        public double? AppearTime { get; set; }
        public double? DisappearTime { get; set; }

        public override string ToString()
        {
            return $"[In:{AppearTime} Seconds Out:{DisappearTime} Seconds]";
        }
    }
}
