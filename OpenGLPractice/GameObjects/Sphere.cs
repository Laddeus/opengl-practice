using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class Sphere : GameObject
    {
        public Vector3 Color { get; set; }

        public Sphere(string i_Name) : base(i_Name)
        {
            Material.Ambient = new Vector4(0.10f, 0.19f, 0.17f, 1.0f);
            Material.Diffuse = new Vector4(0.40f, 0.74f, 0.69f, 1.0f);
            Material.Specular = new Vector4(0.31f, 0.31f, 0.31f, 1.0f);
            Material.Shininess = 13;
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
