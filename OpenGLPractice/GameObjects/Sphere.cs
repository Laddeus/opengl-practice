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
        public Texture SphereTexture { get; set; }

        public Sphere(string i_Name) : base(i_Name)
        {
            DisplayShadow = true;
            UseMaterial = false;

            Material.Diffuse = new Vector4(1.0f);
            Material.Ambient = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            Material.Specular = new Vector4(1.0f);
            Material.Shininess = 128;
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
