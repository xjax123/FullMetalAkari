using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crankshaft.Animation
{
    struct Character
    {
        uint TextureHandle { get; set; }
        Vector2 Size { get; set; }
        Vector2 Bearing { get; set; }
        uint Advance { get; set; }
    }
}
