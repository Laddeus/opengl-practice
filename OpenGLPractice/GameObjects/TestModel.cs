using System.Diagnostics;
using System.IO;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Loaders;
using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;
using Texture = ObjLoader.Loader.Data.VertexData.Texture;

namespace OpenGLPractice.GameObjects
{
    internal class TestModel : GameObject
    {
        private LoadResult r_ModelLoadResult;
        private OpenGLUtilities.Texture texture;
        public TestModel(string i_Name) : base(i_Name)
        {
            ObjLoaderFactory objLoaderFactory = new ObjLoaderFactory();
            IObjLoader modelLoader = objLoaderFactory.Create();

            FileStream modelFileStream = new FileStream("AnyConv.com__vial.obj", FileMode.Open);
            r_ModelLoadResult = modelLoader.Load(modelFileStream);
            Debug.WriteLine(r_ModelLoadResult.Vertices.Count);
            Debug.WriteLine(r_ModelLoadResult.Normals.Count);
            Debug.WriteLine(r_ModelLoadResult.Textures.Count);
            Debug.WriteLine(r_ModelLoadResult.Materials.Count);
            Debug.WriteLine(r_ModelLoadResult.Groups.Count);
            Transform.Scale = new Vector3(0.1f);

            Color = new Vector4(1.0f, 0, 0, 1.0f);
            //texture = new OpenGLUtilities.Texture("vial.jpg");
        }

        protected override void DefineGameObject()
        {
            //GL.glEnable(GL.GL_TEXTURE_2D);
            //texture.BindTexture();
            

            Group firstGroup = r_ModelLoadResult.Groups[0];
            if (firstGroup.Material != null)
            {
                // TODO: apply material
            }

            switch (firstGroup.Faces[0].Count)
            {
                case 3:
                    GL.glBegin(GL.GL_TRIANGLES);
                    break;
                case 4:
                    GL.glBegin(GL.GL_QUADS);
                    break;
                default:
                    break;
            }

            foreach (Face firstGroupFace in firstGroup.Faces)
            {
                for (int i = 0; i < firstGroupFace.Count; i++)
                {
                    
                    FaceVertex faceVertex = firstGroupFace[i];
                    Vertex vertex = r_ModelLoadResult.Vertices[faceVertex.VertexIndex-1];

                    if (r_ModelLoadResult.Normals.Count > 0)
                    {
                        Normal normal = r_ModelLoadResult.Normals[faceVertex.NormalIndex-1];
                        GL.glNormal3f(normal.X, normal.Y, normal.Z);
                    }

                    if (r_ModelLoadResult.Textures.Count > 0)
                    {
                        Texture texture = r_ModelLoadResult.Textures[faceVertex.TextureIndex-1];
                        GL.glTexCoord2d(texture.X, texture.Y);

                    }

                    GL.glVertex3f(vertex.X, vertex.Y, vertex.Z);
                }
            }

            GL.glEnd();
            //texture.UnbindTexture();
            //GL.glDisable(GL.GL_TEXTURE_2D);
        }
    }
}
