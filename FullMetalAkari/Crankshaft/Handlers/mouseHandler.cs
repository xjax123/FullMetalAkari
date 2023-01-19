using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace FullMetalAkari.Crankshaft.Handlers
{
    public class mouseHandler
    {
        //A method to convert screen space into world space
        //for some arcane reason you need to tripple it if you are trying to clamp an object to your mouse
        //not sure why, but it is perfectly accurate once trippled, so im not complinaing.
        public Vector3 ConvertMouseToWorldSpace(float x, float y, float width, float height, Matrix4 projection_matrix, Matrix4 view_matrix)
        {
            //TODO: make this translation less shitty.
            //Translating to 3D Normalized Device Coordinates
            Vector3 NDCposition;
            NDCposition.X = (2.0f * x) / width - 1.0f;
            NDCposition.Y = 1.0f - (2.0f * y) / height;
            NDCposition.Z = 1f;

            //Translating to 4d Homogeneous Clip Coordinates
            Vector4 HCCposition;
            HCCposition.X = NDCposition.X;
            HCCposition.Y = NDCposition.Y;
            HCCposition.Z = -1.0f;
            HCCposition.W = 1.0f;

            //Translating to 4d Camera Coordinates
            Vector4 CCposition;
            CCposition = Matrix4.Invert(projection_matrix) * HCCposition;
            CCposition.Z = -1.0f;
            CCposition.W = 0.0f;

            //Translating to 4d World Coordinates
            Vector3 WCposition;
            WCposition = (Matrix4.Invert(view_matrix) * CCposition).Xyz;

            return WCposition;
        }
    }
}
