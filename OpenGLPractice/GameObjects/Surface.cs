using System;
using System.Globalization;
using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class Surface : GameObject
    {
        private readonly Func<float, float, float> r_SurfaceFunctionXZ;

        public Surface(string i_Name, Func<float, float, float> i_SurfaceFunctionXz = null) : base(i_Name)
        {
            r_SurfaceFunctionXZ = i_SurfaceFunctionXz;

            if (r_SurfaceFunctionXZ == null)
            {
                r_SurfaceFunctionXZ = (i_X, i_Y) => i_X * i_Y; // default surface function
            }
        }

        protected override void DefineGameObject()
        {
            GL.glBegin(GL.GL_QUADS);

            for (float x = -5; x <= 4; x += 0.1f)
            {
                for (float z = -5; z <= 4; z += 0.1f)
                {
                    Vector3 topLeft = new Vector3(x, r_SurfaceFunctionXZ(x, z), z);
                    Vector3 topRight = new Vector3(x + 0.1f, r_SurfaceFunctionXZ(x + 0.1f, z), z);
                    Vector3 bottomRight = new Vector3(x + 0.1f, r_SurfaceFunctionXZ(x + 0.1f, z + 0.1f), z + 0.1f);
                    Vector3 bottomLeft = new Vector3(x, r_SurfaceFunctionXZ(x, z + 0.1f), z + 0.1f);

                    Vector3 quadNormal = (bottomLeft - topLeft).CrossProduct(topRight - topLeft).Normalized;
                    GL.glNormal3fv(quadNormal.ToArray);
                    GL.glVertex3fv(topLeft.ToArray);
                    GL.glVertex3fv(topRight.ToArray);
                    GL.glVertex3fv(bottomRight.ToArray);
                    GL.glVertex3fv(bottomLeft.ToArray);
                }
            }

            GL.glEnd();
        }
    }
}
