using System;
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
        private readonly float[,,] r_ControlPoints;

        public Surface(string i_Name, float i_XZCoverage = 10.0f, float i_QuadPieceSize = 0.1f, Func<float, float, float> i_SurfaceFunctionXz = null) : base(i_Name)
        {
            r_SurfaceFunctionXZ = i_SurfaceFunctionXz;
            r_QuadPieceSize = i_QuadPieceSize;
            r_XZCoverage = i_XZCoverage;

            Random random = new Random();
            if (r_SurfaceFunctionXZ == null)
            {
                r_SurfaceFunctionXZ = (i_X, i_Y) => (float)(Math.Cos(Math.Abs(i_X) + Math.Abs(i_Y))); // default surface function
            }

            UseDisplayList = true;

            Color = new Vector4(0, 1, 0, 1);

            r_ControlPoints = new float[4, 4, 3];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    r_ControlPoints[i, j, 0] = i - 2;
                    r_ControlPoints[i, j, 1] = r_SurfaceFunctionXZ(i - 2, j - 2);
                    r_ControlPoints[i, j, 2] = j - 2;
                }
            }
        }

        protected override void DefineGameObject()
        {
            //GL.glMap2f(GL.GL_MAP2_VERTEX_3, 0.0f, 1.0f, 3, 4, 0.0f, 1.0f, 3 * 4, 4, r_ControlPoints.Cast<float>().ToArray());
            //GL.glEnable(GL.GL_MAP2_VERTEX_3);

            //GL.glDisable(GL.GL_AUTO_NORMAL);
            //GL.glDisable(GL.GL_NORMALIZE);
            //GL.glMap2f(GL.GL_MAP2_NORMAL, 0.0f, 1.0f, 3, 4, 0.0f, 1.0f, 3 * 4, 4, r_ControlPoints.Cast<float>().ToArray());
            //GL.glEnable(GL.GL_MAP2_NORMAL);

            //GL.glBegin(GL.GL_QUADS);

            //for (float u = 0; u < 1.0f; u += 1.0f / 50)
            //{
            //    for (float v = 0; v < 1.0f; v += 1.0f / 50)
            //    {
            //        GL.glEvalCoord2d(u, v);
            //        GL.glEvalCoord2d(u, v + 1.0f / 50);
            //        GL.glEvalCoord2d(u + 1.0f / 50, v + 1.0f / 50);
            //        GL.glEvalCoord2d(u + 1.0f / 50, v);
            //    }
            //}

            //GL.glEnd();

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
