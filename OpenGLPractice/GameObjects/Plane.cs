using OpenGL;
using OpenGLPractice.Game;

namespace OpenGLPractice.GameObjects
{
    internal class Plane : GameObject
    {
        public Plane(string i_Name) : base(i_Name)
        {
        }

        protected override void DefineGameObject()
        {
            //GL.glBegin(GL.GL_QUADS);
            //GL.glNormal3f(0, 1, 0);
            //GL.glVertex3f(-5.0f, 0.0f, 5.0f);
            //GL.glVertex3f(5.0f, 0.0f, 5.0f);
            //GL.glVertex3f(5.0f, 0.0f, -5.0f);
            //GL.glVertex3f(-5.0f, 0.0f, -5.0f);
            //GL.glEnd();

            GLU.gluDisk(sr_GluQuadric, 0, 4, 40, 40);
        }
    }
}
