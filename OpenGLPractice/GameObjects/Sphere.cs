using OpenGL;
using OpenGLPractice.Utilities;

namespace OpenGLPractice.GameObjects
{
    internal class Sphere : GameObject
    {
        private readonly GLUquadric r_GLUquadric;

        public Vector3 Color { get; set; }

        public Sphere(string i_Name) : base(i_Name)
        {
            r_GLUquadric = GLU.gluNewQuadric();
            Color = new Vector3(0.85f, 0.85f, 0.85f);
        }

        ~Sphere()
        {
            GLU.gluDeleteQuadric(r_GLUquadric);
        }

        protected override void DefineGameObject()
        {
            GL.glColor3fv(Color.ToArray);
            GLU.gluSphere(r_GLUquadric, 0.5, 20, 20);
        }

        public override void Tick(float i_DeltaTime)
        {
        }
    }
}
