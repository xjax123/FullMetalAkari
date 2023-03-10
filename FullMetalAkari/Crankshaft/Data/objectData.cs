using OpenTK.Graphics.OpenGL4;

#nullable enable
namespace Crankshaft.Data
{
    public class objectData
    {
        //Required Data
        public int InstanceID { get; set; }
        public string Type { get; set; }
        public float Mass { get; set; }
        public positionData Position { get; set; }

        //Optional Data
        public int? Variant { get; set; }


        //Functions
        public override string ToString()
        {
            return "[ID:" + InstanceID + ", objType:" + Type + ", " + Position.ToString() + "]";
        }
    }
}
