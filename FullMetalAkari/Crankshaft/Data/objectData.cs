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
        public bool Clickable { get; set; } = true;


        //Functions

        //empty constructor
        public objectData ()
        {
            InstanceID = 0;
            Type = "error";
            Mass = 0;
            Position = new positionData();
        }
        public override string ToString()
        {
            return "[ID:" + InstanceID + ", objType:" + Type + ", " + Position.ToString() + "]";
        }
    }
}
