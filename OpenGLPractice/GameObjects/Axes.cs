using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.GameObjects
{
    internal class Axes : GameObject
    {
        public Axes(string i_Name) : base(i_Name)
        {
            DisplayShadow = false;
        }

        protected override void DefineGameObject()
        {
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_LINE_STIPPLE));
            GLErrorCatcher.TryGLCall(() => GL.glLineStipple(1, 0xFF00));
            GLErrorCatcher.TryGLCall(() => GL.glBegin(GL.GL_LINES));

            // X Axis
            GL.glColor3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(-5.0f, 0.0f, 0.0f);
            GL.glVertex3f(5.0f, 0.0f, 0.0f);

            // Y Axis
            GL.glColor3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.0f, -5.0f, 0.0f);
            GL.glVertex3f(0.0f, 5.0f, 0.0f);

            // Z Axis
            GL.glColor3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, 0.0f, -5.0f);
            GL.glVertex3f(0.0f, 0.0f, 5.0f);

            GL.glEnd();

            // Arrow tips

            // Z arrow tip
            GLErrorCatcher.TryGLCall(() => GL.glColor3f(0.0f, 0.0f, 1.0f));
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(0, 0, 5));

            GLU.gluCylinder(sr_GluQuadric, 0.2, 0, 1, 20, 20);

            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(0, 0, -5));

            // X arrow tip
            GLErrorCatcher.TryGLCall(() => GL.glColor3f(1.0f, 0.0f, 0.0f));
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(5, 0, 0));
            GLErrorCatcher.TryGLCall(() => GL.glRotatef(90, 0, 1, 0));

            GLU.gluCylinder(sr_GluQuadric, 0.2, 0, 1, 20, 20);

            GLErrorCatcher.TryGLCall(() => GL.glRotatef(-90, 0, 1, 0));
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(-5, 0, 0));

            // Y arrow tip
            GLErrorCatcher.TryGLCall(() => GL.glColor3f(0, 1, 0));
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(0, 5, 0));
            GLErrorCatcher.TryGLCall(() => GL.glRotatef(-90, 1, 0, 0));

            GLU.gluCylinder(sr_GluQuadric, 0.2, 0, 1, 20, 20);

            GLErrorCatcher.TryGLCall(() => GL.glRotatef(90, 1, 0, 0));
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(0, -5, 0));
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_LINE_STIPPLE));
        }
    }
}
