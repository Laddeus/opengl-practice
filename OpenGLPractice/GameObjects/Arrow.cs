using OpenGL;
using OpenGLPractice.GLMath;
using OpenGLPractice.Utilities;

namespace OpenGLPractice.GameObjects
{
    internal class Arrow : GameObject
    {
        private GLUquadric m_QuadricObject;

        public Vector3 Color { get; set; } = new Vector3(0);

        public Arrow(string i_Name) : base(i_Name)
        {
            m_QuadricObject = GLU.gluNewQuadric();
        }

        ~Arrow()
        {
            GLU.gluDeleteQuadric(m_QuadricObject);
        }

        protected override void DefineGameObject()
        {
            GLErrorCatcher.TryGLCall(() => GL.glColor3fv(Color.ToArray));
            GLU.gluCylinder(m_QuadricObject, 0.2, 0, 0.5, 20, 20);
        }

        public override void Tick(float i_DeltaTime)
        {
        }
    }
}
