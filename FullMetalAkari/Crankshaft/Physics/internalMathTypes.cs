using OpenTK.Mathematics;

#pragma warning disable
namespace Crankshaft.Physics
{
    //UniVector & UniMatrix types
    //named for being universally compatable with Bulletsharp & OpenTK
    public struct UniVector3
    {
        public UniVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public UniVector3(OpenTK.Mathematics.Vector3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }
        public UniVector3(BulletSharp.Math.Vector3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public static UniVector3 Zero { get { return new UniVector3(0f,0f,0f); } }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector2 Xy { get { return new Vector2(X, Y); } set { X = value.X; Y = value.Y; } }
        public Vector2 XZ { get { return new Vector2(X, Z); } set { X = value.X; Z = value.Y; } }
        public Vector2 YZ { get { return new Vector2(Y, Z); } set { Y = value.X; Z = value.Y; } }

        public override string ToString()
        {
            return $"[{X},{Y},{Z}]";
        }

        //Implicit Type Conversions
        public static implicit operator OpenTK.Mathematics.Vector3(UniVector3 v)
        {
            return new OpenTK.Mathematics.Vector3(v.X, v.Y, v.Z);
        }

        public static implicit operator UniVector3(OpenTK.Mathematics.Vector3 v)
        {
            return new UniVector3(v);
        }

        public static implicit operator BulletSharp.Math.Vector3(UniVector3 v)
        {
            return new BulletSharp.Math.Vector3(v.X, v.Y, v.Z);
        }

        public static implicit operator UniVector3(BulletSharp.Math.Vector3 v)
        {
            return new UniVector3(v);
        }

        //Math Operations
        //Scalar-Vector Operations
        public static UniVector3 operator +(UniVector3 v, float s)
        {
            return new UniVector3(v.X + s, v.Y + s, v.Z + s);
        }
        public static UniVector3 operator -(UniVector3 v, float s)
        {
            return new UniVector3(v.X - s, v.Y - s, v.Z - s);
        }
        public static UniVector3 operator *(UniVector3 v, float s)
        {
            return new UniVector3(v.X*s,v.Y*s,v.Z*s);
        }
        public static UniVector3 operator /(UniVector3 v, float s)
        {
            float n = 1 / s;
            return new UniVector3(v.X * n, v.Y * n, v.Z * n);
        }

        //Vector-Vector Operations
        public static UniVector3 operator +(UniVector3 v, UniVector3 s)
        {
            return new UniVector3(v.X + s.X, v.Y + s.Y, v.Z + s.Z);
        }
        public static UniVector3 operator -(UniVector3 v, UniVector3 s)
        {
            return new UniVector3(v.X - s.X, v.Y - s.Y, v.Z - s.Z);
        }
        public static UniVector3 operator *(UniVector3 v, UniVector3 s)
        {
            return new UniVector3(v.X * s.X, v.Y * s.Y, v.Z * s.Z);
        }

        //Equivilency Operations
        public static bool operator !=(UniVector3 v, UniVector3 s)
        {
            if (v.X != s.X || v.Y != s.Y || v.Z != s.Z)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public static bool operator ==(UniVector3 v, UniVector3 s)
        {
            if (v.X == s.X && v.Y == s.Y && v.Z == s.Z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public struct UniMatrix
    {
        public UniMatrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
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
        public UniMatrix(Vector4 v1, Vector4 v2, Vector4 v3, Vector4 v4)
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
        public UniMatrix(OpenTK.Mathematics.Matrix4 v)
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
        public UniMatrix(BulletSharp.Math.Matrix v)
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
        public static UniMatrix Identity { get { return new UniMatrix(1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1); } }

        //Implicit Type Conversions
        public static implicit operator OpenTK.Mathematics.Matrix4(UniMatrix v)
        {
            return new OpenTK.Mathematics.Matrix4(v.Column1,v.Column2,v.Column3,v.Column4);
        }
        public static implicit operator UniMatrix(OpenTK.Mathematics.Matrix4 v)
        {
            return new UniMatrix(v);
        }
        public static implicit operator BulletSharp.Math.Matrix(UniMatrix v)
        {
            return new BulletSharp.Math.Matrix(v.M11,v.M12,v.M13,v.M14, v.M21, v.M22, v.M23, v.M24, v.M31, v.M32, v.M33, v.M34, v.M41, v.M42, v.M43, v.M44);
        }
        public static implicit operator UniMatrix(BulletSharp.Math.Matrix v)
        {
            return new UniMatrix(v);
        }

        //Math Operators, oh god do i hate matrix math.
        //Scarar-Matrix Math
        public static UniMatrix operator +(UniMatrix m, float n)
        {
            return new UniMatrix(
                m.M11 + n,
                m.M12 + n,
                m.M13 + n,
                m.M14 + n,
                m.M21 + n,
                m.M22 + n,
                m.M23 + n,
                m.M24 + n,
                m.M31 + n,
                m.M32 + n,
                m.M33 + n,
                m.M34 + n,
                m.M41 + n,
                m.M42 + n,
                m.M43 + n,
                m.M44 + n
                );
        }
        public static UniMatrix operator -(UniMatrix m, float n)
        {
            return new UniMatrix(
                m.M11 - n,
                m.M12 - n,
                m.M13 - n,
                m.M14 - n,
                m.M21 - n,
                m.M22 - n,
                m.M23 - n,
                m.M24 - n,
                m.M31 - n,
                m.M32 - n,
                m.M33 - n,
                m.M34 - n,
                m.M41 - n,
                m.M42 - n,
                m.M43 - n,
                m.M44 - n
                );
        }
        public static UniMatrix operator *(UniMatrix m, float n)
        {
            return new UniMatrix(
                m.M11 * n,
                m.M12 * n,
                m.M13 * n,
                m.M14 * n,
                m.M21 * n,
                m.M22 * n,
                m.M23 * n,
                m.M24 * n,
                m.M31 * n,
                m.M32 * n,
                m.M33 * n,
                m.M34 * n,
                m.M41 * n,
                m.M42 * n,
                m.M43 * n,
                m.M44 * n
                );
        }
        public static UniMatrix operator /(UniMatrix m, float n)
        {
            float d = 1 / n;
            return new UniMatrix(
                m.M11 * d,
                m.M12 * d,
                m.M13 * d,
                m.M14 * d,
                m.M21 * d,
                m.M22 * d,
                m.M23 * d,
                m.M24 * d,
                m.M31 * d,
                m.M32 * d,
                m.M33 * d,
                m.M34 * d,
                m.M41 * d,
                m.M42 * d,
                m.M43 * d,
                m.M44 * d
                );
        }

        //Matrix-Vector Math
        public static UniMatrix operator *(UniMatrix m, UniVector3 n)
        {
            return new UniMatrix(
                m.M11 * n.X,
                m.M12 * n.Y,
                m.M13 * n.Z,
                m.M14,
                m.M21 * n.X,
                m.M22 * n.Y,
                m.M23 * n.Z,
                m.M24,
                m.M31 * n.X,
                m.M32 * n.Y,
                m.M33 * n.Z,
                m.M34,
                m.M41 * n.X,
                m.M42 * n.Y,
                m.M43 * n.Z,
                m.M44
                );
        }
        public static UniMatrix operator *(UniMatrix m, Vector4 n)
        {
            return new UniMatrix(
                m.M11 * n.X,
                m.M12 * n.Y,
                m.M13 * n.Z,
                m.M14 * n.W,
                m.M21 * n.X,
                m.M22 * n.Y,
                m.M23 * n.Z,
                m.M24 * n.W,
                m.M31 * n.X,
                m.M32 * n.Y,
                m.M33 * n.Z,
                m.M34 * n.W,
                m.M41 * n.X,
                m.M42 * n.Y,
                m.M43 * n.Z,
                m.M44 * n.W
                );
        }

        //Matrix-Matrix Math
        public static UniMatrix operator +(UniMatrix m, UniMatrix n)
        {
            return new UniMatrix(
                m.M11 + n.M11,
                m.M12 + n.M12,
                m.M13 + n.M13,
                m.M14 + n.M14,
                m.M21 + n.M21,
                m.M22 + n.M22,
                m.M23 + n.M23,
                m.M24 + n.M24,
                m.M31 + n.M31,
                m.M32 + n.M32,
                m.M33 + n.M33,
                m.M34 + n.M34,
                m.M41 + n.M41,
                m.M42 + n.M42,
                m.M43 + n.M43,
                m.M44 + n.M44
                );
        }
        public static UniMatrix operator -(UniMatrix m, UniMatrix n)
        {
            return new UniMatrix(
                m.M11 - n.M11,
                m.M12 - n.M12,
                m.M13 - n.M13,
                m.M14 - n.M14,
                m.M21 - n.M21,
                m.M22 - n.M22,
                m.M23 - n.M23,
                m.M24 - n.M24,
                m.M31 - n.M31,
                m.M32 - n.M32,
                m.M33 - n.M33,
                m.M34 - n.M34,
                m.M41 - n.M41,
                m.M42 - n.M42,
                m.M43 - n.M43,
                m.M44 - n.M44
                );
        }

        public static UniMatrix operator *(UniMatrix m, UniMatrix n)
        {
            return new UniMatrix(
                m.M11 * n.M11 + m.M21 * n.M12 + m.M31 * n.M13 + m.M41 * n.M14,
                m.M12 * n.M11 + m.M22 * n.M12 + m.M32 * n.M13 + m.M42 * n.M14,
                m.M13 * n.M11 + m.M23 * n.M12 + m.M33 * n.M13 + m.M43 * n.M14,
                m.M14 * n.M11 + m.M24 * n.M12 + m.M34 * n.M13 + m.M44 * n.M14,

                m.M11 * n.M21 + m.M21 * n.M22 + m.M31 * n.M23 + m.M41 * n.M24,
                m.M12 * n.M21 + m.M22 * n.M22 + m.M32 * n.M23 + m.M42 * n.M24,
                m.M13 * n.M21 + m.M23 * n.M22 + m.M33 * n.M23 + m.M43 * n.M24,
                m.M14 * n.M21 + m.M24 * n.M22 + m.M34 * n.M23 + m.M44 * n.M24,

                m.M11 * n.M31 + m.M21 * n.M32 + m.M31 * n.M33 + m.M41 * n.M34,
                m.M12 * n.M31 + m.M22 * n.M32 + m.M32 * n.M33 + m.M42 * n.M34,
                m.M13 * n.M31 + m.M23 * n.M32 + m.M33 * n.M33 + m.M43 * n.M34,
                m.M14 * n.M31 + m.M24 * n.M32 + m.M34 * n.M33 + m.M44 * n.M34,

                m.M11 * n.M41 + m.M21 * n.M42 + m.M31 * n.M43 + m.M41 * n.M44,
                m.M12 * n.M41 + m.M22 * n.M42 + m.M32 * n.M43 + m.M42 * n.M44,
                m.M13 * n.M41 + m.M23 * n.M42 + m.M33 * n.M43 + m.M43 * n.M44,
                m.M14 * n.M41 + m.M24 * n.M42 + m.M34 * n.M43 + m.M44 * n.M44
                );
        }

        //Extra Math shit i need because the compiler is being bitchy
        public static UniMatrix operator *(UniMatrix m, Matrix4 n)
        {
            return new UniMatrix(
                m.M11 * n.M11 + m.M21 * n.M12 + m.M31 * n.M13 + m.M41 * n.M14,
                m.M12 * n.M11 + m.M22 * n.M12 + m.M32 * n.M13 + m.M42 * n.M14,
                m.M13 * n.M11 + m.M23 * n.M12 + m.M33 * n.M13 + m.M43 * n.M14,
                m.M14 * n.M11 + m.M24 * n.M12 + m.M34 * n.M13 + m.M44 * n.M14,

                m.M11 * n.M21 + m.M21 * n.M22 + m.M31 * n.M23 + m.M41 * n.M24,
                m.M12 * n.M21 + m.M22 * n.M22 + m.M32 * n.M23 + m.M42 * n.M24,
                m.M13 * n.M21 + m.M23 * n.M22 + m.M33 * n.M23 + m.M43 * n.M24,
                m.M14 * n.M21 + m.M24 * n.M22 + m.M34 * n.M23 + m.M44 * n.M24,

                m.M11 * n.M31 + m.M21 * n.M32 + m.M31 * n.M33 + m.M41 * n.M34,
                m.M12 * n.M31 + m.M22 * n.M32 + m.M32 * n.M33 + m.M42 * n.M34,
                m.M13 * n.M31 + m.M23 * n.M32 + m.M33 * n.M33 + m.M43 * n.M34,
                m.M14 * n.M31 + m.M24 * n.M32 + m.M34 * n.M33 + m.M44 * n.M34,

                m.M11 * n.M41 + m.M21 * n.M42 + m.M31 * n.M43 + m.M41 * n.M44,
                m.M12 * n.M41 + m.M22 * n.M42 + m.M32 * n.M43 + m.M42 * n.M44,
                m.M13 * n.M41 + m.M23 * n.M42 + m.M33 * n.M43 + m.M43 * n.M44,
                m.M14 * n.M41 + m.M24 * n.M42 + m.M34 * n.M43 + m.M44 * n.M44
                );
        }
        public static UniMatrix operator *(Matrix4 m, UniMatrix n)
        {
            return new UniMatrix(
                m.M11 * n.M11 + m.M21 * n.M12 + m.M31 * n.M13 + m.M41 * n.M14,
                m.M12 * n.M11 + m.M22 * n.M12 + m.M32 * n.M13 + m.M42 * n.M14,
                m.M13 * n.M11 + m.M23 * n.M12 + m.M33 * n.M13 + m.M43 * n.M14,
                m.M14 * n.M11 + m.M24 * n.M12 + m.M34 * n.M13 + m.M44 * n.M14,

                m.M11 * n.M21 + m.M21 * n.M22 + m.M31 * n.M23 + m.M41 * n.M24,
                m.M12 * n.M21 + m.M22 * n.M22 + m.M32 * n.M23 + m.M42 * n.M24,
                m.M13 * n.M21 + m.M23 * n.M22 + m.M33 * n.M23 + m.M43 * n.M24,
                m.M14 * n.M21 + m.M24 * n.M22 + m.M34 * n.M23 + m.M44 * n.M24,

                m.M11 * n.M31 + m.M21 * n.M32 + m.M31 * n.M33 + m.M41 * n.M34,
                m.M12 * n.M31 + m.M22 * n.M32 + m.M32 * n.M33 + m.M42 * n.M34,
                m.M13 * n.M31 + m.M23 * n.M32 + m.M33 * n.M33 + m.M43 * n.M34,
                m.M14 * n.M31 + m.M24 * n.M32 + m.M34 * n.M33 + m.M44 * n.M34,

                m.M11 * n.M41 + m.M21 * n.M42 + m.M31 * n.M43 + m.M41 * n.M44,
                m.M12 * n.M41 + m.M22 * n.M42 + m.M32 * n.M43 + m.M42 * n.M44,
                m.M13 * n.M41 + m.M23 * n.M42 + m.M33 * n.M43 + m.M43 * n.M44,
                m.M14 * n.M41 + m.M24 * n.M42 + m.M34 * n.M43 + m.M44 * n.M44
                );
        }
        public static UniMatrix operator *(UniMatrix m, BulletSharp.Math.Matrix n)
        {
            return new UniMatrix(
                m.M11 * n.M11 + m.M21 * n.M12 + m.M31 * n.M13 + m.M41 * n.M14,
                m.M12 * n.M11 + m.M22 * n.M12 + m.M32 * n.M13 + m.M42 * n.M14,
                m.M13 * n.M11 + m.M23 * n.M12 + m.M33 * n.M13 + m.M43 * n.M14,
                m.M14 * n.M11 + m.M24 * n.M12 + m.M34 * n.M13 + m.M44 * n.M14,

                m.M11 * n.M21 + m.M21 * n.M22 + m.M31 * n.M23 + m.M41 * n.M24,
                m.M12 * n.M21 + m.M22 * n.M22 + m.M32 * n.M23 + m.M42 * n.M24,
                m.M13 * n.M21 + m.M23 * n.M22 + m.M33 * n.M23 + m.M43 * n.M24,
                m.M14 * n.M21 + m.M24 * n.M22 + m.M34 * n.M23 + m.M44 * n.M24,

                m.M11 * n.M31 + m.M21 * n.M32 + m.M31 * n.M33 + m.M41 * n.M34,
                m.M12 * n.M31 + m.M22 * n.M32 + m.M32 * n.M33 + m.M42 * n.M34,
                m.M13 * n.M31 + m.M23 * n.M32 + m.M33 * n.M33 + m.M43 * n.M34,
                m.M14 * n.M31 + m.M24 * n.M32 + m.M34 * n.M33 + m.M44 * n.M34,

                m.M11 * n.M41 + m.M21 * n.M42 + m.M31 * n.M43 + m.M41 * n.M44,
                m.M12 * n.M41 + m.M22 * n.M42 + m.M32 * n.M43 + m.M42 * n.M44,
                m.M13 * n.M41 + m.M23 * n.M42 + m.M33 * n.M43 + m.M43 * n.M44,
                m.M14 * n.M41 + m.M24 * n.M42 + m.M34 * n.M43 + m.M44 * n.M44
                );
        }
        public static UniMatrix operator *(BulletSharp.Math.Matrix m, UniMatrix n)
        {
            return new UniMatrix(
                m.M11 * n.M11 + m.M21 * n.M12 + m.M31 * n.M13 + m.M41 * n.M14,
                m.M12 * n.M11 + m.M22 * n.M12 + m.M32 * n.M13 + m.M42 * n.M14,
                m.M13 * n.M11 + m.M23 * n.M12 + m.M33 * n.M13 + m.M43 * n.M14,
                m.M14 * n.M11 + m.M24 * n.M12 + m.M34 * n.M13 + m.M44 * n.M14,

                m.M11 * n.M21 + m.M21 * n.M22 + m.M31 * n.M23 + m.M41 * n.M24,
                m.M12 * n.M21 + m.M22 * n.M22 + m.M32 * n.M23 + m.M42 * n.M24,
                m.M13 * n.M21 + m.M23 * n.M22 + m.M33 * n.M23 + m.M43 * n.M24,
                m.M14 * n.M21 + m.M24 * n.M22 + m.M34 * n.M23 + m.M44 * n.M24,

                m.M11 * n.M31 + m.M21 * n.M32 + m.M31 * n.M33 + m.M41 * n.M34,
                m.M12 * n.M31 + m.M22 * n.M32 + m.M32 * n.M33 + m.M42 * n.M34,
                m.M13 * n.M31 + m.M23 * n.M32 + m.M33 * n.M33 + m.M43 * n.M34,
                m.M14 * n.M31 + m.M24 * n.M32 + m.M34 * n.M33 + m.M44 * n.M34,

                m.M11 * n.M41 + m.M21 * n.M42 + m.M31 * n.M43 + m.M41 * n.M44,
                m.M12 * n.M41 + m.M22 * n.M42 + m.M32 * n.M43 + m.M42 * n.M44,
                m.M13 * n.M41 + m.M23 * n.M42 + m.M33 * n.M43 + m.M43 * n.M44,
                m.M14 * n.M41 + m.M24 * n.M42 + m.M34 * n.M43 + m.M44 * n.M44
                );
        }
    }
}
