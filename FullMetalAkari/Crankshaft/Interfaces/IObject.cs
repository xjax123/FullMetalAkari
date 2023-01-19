using System;
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

        //DOCUMENTATION UNFINISED
        public shaderHandler GetShader();

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

        //there probably should be more here.
    }
}
