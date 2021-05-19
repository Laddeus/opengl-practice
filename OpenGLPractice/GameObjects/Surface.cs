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
        private readonly float r_QuadPieceSize;
        private readonly float r_XZCoverage;

        public Surface(string i_Name, float i_XZCoverage, float i_QuadPieceSize, Func<float, float, float> i_SurfaceFunctionXz = null) : base(i_Name)
        {
            r_SurfaceFunctionXZ = i_SurfaceFunctionXz;
            r_QuadPieceSize = i_QuadPieceSize;
            r_XZCoverage = i_XZCoverage;

            if (r_SurfaceFunctionXZ == null)
            {
                r_SurfaceFunctionXZ = (i_X, i_Y) => i_X * i_Y; // default surface function
            }
        }

        protected override void DefineGameObject()
        {
            GL.glBegin(GL.GL_TRIANGLES);

            for (float x = -r_XZCoverage; x <= r_XZCoverage; x += r_QuadPieceSize)
            {
                for (float z = -r_XZCoverage; z <= r_XZCoverage; z += r_QuadPieceSize)
                {
                    Vector3 topLeft = new Vector3(x, r_SurfaceFunctionXZ(x, z), z);
                    Vector3 topRight = new Vector3(x + r_QuadPieceSize, r_SurfaceFunctionXZ(x + r_QuadPieceSize, z), z);
                    Vector3 bottomRight = new Vector3(x + r_QuadPieceSize, r_SurfaceFunctionXZ(x + r_QuadPieceSize, z + r_QuadPieceSize), z + r_QuadPieceSize);
                    Vector3 bottomLeft = new Vector3(x, r_SurfaceFunctionXZ(x, z + r_QuadPieceSize), z + r_QuadPieceSize);

                    //// Triangle 1

                    Vector3 firstTriangleNormal = (bottomLeft - topLeft).CrossProduct(topRight - topLeft).Normalized;
                    GL.glNormal3fv(firstTriangleNormal.ToArray);
                    GL.glVertex3fv(bottomLeft.ToArray);
                    GL.glVertex3fv(topRight.ToArray);
                    GL.glVertex3fv(topLeft.ToArray);

                    //// Triangle 2

                    Vector3 secondTriangleNormal = (bottomRight - topRight).CrossProduct(topRight - bottomLeft).Normalized;
                    GL.glNormal3fv(secondTriangleNormal.ToArray);
                    GL.glVertex3fv(bottomLeft.ToArray);
                    GL.glVertex3fv(bottomRight.ToArray);
                    GL.glVertex3fv(topRight.ToArray);
                }
            }

            GL.glEnd();
        }
    }
}
