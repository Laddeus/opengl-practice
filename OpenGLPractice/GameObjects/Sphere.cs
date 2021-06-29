using System.Drawing.Drawing2D;
using System.Windows.Forms;
using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.GameObjects
{
    internal class Sphere : GameObject
    {
        private readonly Texture r_SphereTexture;
        private readonly float r_SphereRadius;

        public float Radius => r_SphereRadius;

        public Sphere(string i_Name, float i_SphereRadius = 0.5f, Texture i_SphereTexture = null) : base(i_Name)
        {
            r_SphereTexture = i_SphereTexture;
            r_SphereRadius = i_SphereRadius;
        }

        protected override void DefineGameObject()
        {
            if (r_SphereTexture != null)
            {
                GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_TEXTURE_2D));
                GLU.gluQuadricTexture(sr_GluQuadric, 1);
                r_SphereTexture.BindTexture();
            }

            GLU.gluSphere(sr_GluQuadric, r_SphereRadius, 40, 40);

            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_TEXTURE_2D));
        }
    }
}
