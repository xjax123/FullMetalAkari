using Crankshaft.Physics;

#pragma warning disable
namespace Crankshaft.Animation
{
    public struct Keyframe
    {
        public float Time { get; set; }
        public UniVector3 Position { get; set; }

        public Keyframe(float t, UniVector3 p)
        {
            Time = t;
            Position = p;
        }

        public static bool operator ==(Keyframe a, Keyframe b)
        {
            if (a.Time == b.Time && a.Position == b.Position)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public static bool operator !=(Keyframe a, Keyframe b)
        {
            if (a.Time != b.Time && a.Position != b.Position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"[{Time}, {Position.ToString()}]";
        }
    }
}
