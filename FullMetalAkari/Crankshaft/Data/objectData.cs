using OpenTK.Graphics.OpenGL4;

namespace Crankshaft.Data
{
    public class objectData
    {
        public int InstanceID { get; set; }
        public string type { get; set; }
        public float mass { get; set; }
        public positionData position { get; set; }
        public override string ToString()
        {
            return "[ID:" + InstanceID + ", objType:" + type + ", " + position.ToString() + "]";
        }
    }
}
