using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.GameObjects
{
    internal class Sphere : GameObject
    {
        public Texture SphereTexture { get; set; }

        public Sphere(string i_Name) : base(i_Name)
        {
            DisplayShadow = true;
        }

        protected override void DefineGameObject()
        {
            if (SphereTexture != null)
            {
                GLU.gluQuadricTexture(sr_GluQuadric, 1);
                SphereTexture.BindTexture();
            }

            GLU.gluSphere(sr_GluQuadric, 0.5, 20, 20);
        }
    }
}
