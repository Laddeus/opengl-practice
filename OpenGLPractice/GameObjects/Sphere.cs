using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class Sphere : GameObject
    {
        public Sphere(string i_Name) : base(i_Name)
        {
            //UseMaterial = true;
            //Material.Ambient = new Vector4(0.247250f, 0.199500f, 0.074500f, 1.000000f);
            //Material.Diffuse = new Vector4(0.751640f, 0.606480f, 0.226480f, 1.000000f);
            //Material.Specular = new Vector4(0.628281f, 0.555802f, 0.366065f, 1.000000f);
            //Material.Shininess = 51.2f;
        }

        protected override void DefineGameObject()
        {
            GLU.gluSphere(r_gluQuadric, 0.5, 20, 20);
        }

        public override void Tick(float i_DeltaTime)
        {
        }
    }
}
