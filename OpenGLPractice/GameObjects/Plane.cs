using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class Plane : GameObject
    {
        public Plane(string i_Name) : base(i_Name)
        {
            Transform.Scale = new Vector3(5.0f, 1, 5.0f);
        }

        protected override void DefineGameObject()
        {
            GL.glBegin(GL.GL_QUADS);
            GL.glNormal3f(0, 1, 0);
            GL.glVertex3f(-1.0f, 0.0f, 1.0f);
            GL.glVertex3f(1.0f, 0.0f, 1.0f);
            GL.glVertex3f(1.0f, 0.0f, -1.0f);
            GL.glVertex3f(-1.0f, 0.0f, -1.0f);
            GL.glEnd();
        }
    }
}
