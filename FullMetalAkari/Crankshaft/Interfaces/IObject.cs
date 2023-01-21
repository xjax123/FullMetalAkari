using System;
using OpenTK.Mathematics;
using FullMetalAkari.Crankshaft.Handlers;

namespace FullMetalAkari.Crankshaft.Interfaces
{
    interface IObject
    {
        /*
        TODO: Improve this interface
        This interface should provide basic instructions for the and constraints that all IObject implimentations should follow.
        */

        //Define what special behaviour occurs on object creation
        public void onLoad();
        
        //Define what special behavior occurs on object destruction
        public void onDestroy();
        
        //Define what happens when the object is clicked.
        public void onClick();

        //Define base behavior the object should do on each update frame
        public void onUpdateFrame();

        //Define base behavior that the object should do on each render frame
        public void onRenderFrame();

        //handles translation on XYZ
        public void translateObject(Vector3 translation);

        //handles scaling
        public void scaleObject(float scale);

        //Object ID's are an inherent proprety to define the object should never be changed after runtime.
        public String getObjID();

        //Generally Instance ID's should be set on creation of the object, but theyre not an inherent property.
        public int getInstID();
        public void setInstID(int v);

        //Generally Names should be an inherent propery, but some situations may benefit from allowing a setter.
        public String getName();
        public void setName(String v);

        //DOCUMENTATION UNFINISED
        public textureHandler getTexture();
        public void setTexture(textureHandler texture);

        //DOCUMENTATION UNFINISED
        public shaderHandler getShader();
        public void setShader(shaderHandler shader);

        //DOCUMENTATION UNFINISED
        public float[] getVerts();

        //DOCUMENTATION UNFINISED
        public uint[] getIndices();

        //DOCUMENTATION UNFINISED
        public int getVertexBufferObject();
        public void setVertexBufferObject(int v);

        //DOCUMENTATION UNFINISED
        public int getVertexArrayObject();
        public void setVertexArrayObject(int v);

        //DOCUMENTATION UNFINISED
        public int getElementBufferObject();
        public void setElementBufferObject(int v);

        //DOCUMENTATION UNFINISED
        public Matrix4 getProjectionMatrix();
        public void setProjectionMatrix(Matrix4 projection);

        //DOCUMENTATION UNFINISED
        public Matrix4 getViewMatrix();
        public void setViewMatrix(Matrix4 view);


        //there probably should be more here.
    }
}
