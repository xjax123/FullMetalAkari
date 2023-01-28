using OpenTK.Mathematics;

namespace Crankshaft.Physics
{
    public struct Vector3
    {
        //TODO: add more functionality to the various math types to make conversions smoother.

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3(OpenTK.Mathematics.Vector3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }
        public Vector3(BulletSharp.Math.Vector3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public static Vector3 Zero { get { return new Vector3(0f,0f,0f); } }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override string ToString()
        {
            return $"[{X},{Y},{Z}]";
        }

        //various conversions needed to put a path job on the lack of internal compatability between OpenTK and BulletSharp
        public static implicit operator OpenTK.Mathematics.Vector3(Vector3 v)
        {
            return new OpenTK.Mathematics.Vector3(v.X, v.Y, v.Z);
        }

        public static implicit operator Vector3(OpenTK.Mathematics.Vector3 v)
        {
            return new Vector3(v);
        }

        public static implicit operator BulletSharp.Math.Vector3(Vector3 v)
        {
            return new BulletSharp.Math.Vector3(v.X, v.Y, v.Z);
        }

        public static implicit operator Vector3(BulletSharp.Math.Vector3 v)
        {
            return new Vector3(v);
        }
    }

    public struct Matrix4
    {
        public Matrix4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            M11 = m11;
            M21 = m21;
            M31 = m31;
            M41 = m41;

            M12 = m12;
            M22 = m22;
            M32 = m32;
            M42 = m42;

            M13 = m13;
            M23 = m23;
            M33 = m33;
            M43 = m43;

            M14 = m14;
            M24 = m24;
            M34 = m34;
            M44 = m44;

        }
        public Matrix4(Vector4 v1, Vector4 v2, Vector4 v3, Vector4 v4)
        {
            M11 = v1.X;
            M21 = v1.Y;
            M31 = v1.Z;
            M41 = v1.W;

            M12 = v2.X;
            M22 = v2.Y;
            M32 = v2.Z;
            M42 = v2.W;

            M13 = v3.X;
            M23 = v3.Y;
            M33 = v3.Z;
            M43 = v3.W;

            M14 = v4.X;
            M24 = v4.Y;
            M34 = v4.Z;
            M44 = v4.W;
        }
        public Matrix4(OpenTK.Mathematics.Matrix4 v)
        {
            M11 = v.M11;
            M21 = v.M21;
            M31 = v.M31;
            M41 = v.M41;

            M12 = v.M12;
            M22 = v.M22;
            M32 = v.M32;
            M42 = v.M42;

            M13 = v.M13;
            M23 = v.M23;
            M33 = v.M33;
            M43 = v.M43;

            M14 = v.M14;
            M24 = v.M24;
            M34 = v.M34;
            M44 = v.M44;
        }
        public Matrix4(BulletSharp.Math.Matrix v)
        {
            M11 = v.M11;
            M21 = v.M21;
            M31 = v.M31;
            M41 = v.M41;

            M12 = v.M12;
            M22 = v.M22;
            M32 = v.M32;
            M42 = v.M42;

            M13 = v.M13;
            M23 = v.M23;
            M33 = v.M33;
            M43 = v.M43;

            M14 = v.M14;
            M24 = v.M24;
            M34 = v.M34;
            M44 = v.M44;
        }

        public float M11 { get; set; }
        public float M12 { get; set; }
        public float M13 { get; set; }
        public float M14 { get; set; }
        public Vector4 Column1 { get { return new Vector4(M11,M12,M13,M14); } }

        public float M21 { get; set; }
        public float M22 { get; set; }
        public float M23 { get; set; }
        public float M24 { get; set; }
        public Vector4 Column2 { get { return new Vector4(M21, M22, M23, M24); } }


        public float M31 { get; set; }
        public float M32 { get; set; }
        public float M33 { get; set; }
        public float M34 { get; set; }
        public Vector4 Column3 { get { return new Vector4(M31, M32, M33, M34); } }

        public float M41 { get; set; }
        public float M42 { get; set; }
        public float M43 { get; set; }
        public float M44 { get; set; }
        public Vector4 Column4 { get { return new Vector4(M41, M42, M43, M44); } }

        public float Trace { get { return M11 + M22 + M33 + M44; } }
        public Vector4 Diagonal { get { return new Vector4(M11,M22,M33,M44); } }

        public static implicit operator OpenTK.Mathematics.Matrix4(Matrix4 v)
        {
            return new OpenTK.Mathematics.Matrix4(v.Column1,v.Column2,v.Column3,v.Column4);
        }
        public static implicit operator Matrix4(OpenTK.Mathematics.Matrix4 v)
        {
            return new Matrix4(v);
        }
        public static implicit operator BulletSharp.Math.Matrix(Matrix4 v)
        {
            return new BulletSharp.Math.Matrix(v.M11,v.M12,v.M13,v.M14, v.M21, v.M22, v.M23, v.M24, v.M31, v.M32, v.M33, v.M34, v.M41, v.M42, v.M43, v.M44);
        }
        public static implicit operator Matrix4(BulletSharp.Math.Matrix v)
        {
            return new Matrix4(v);
        }
    }
}
