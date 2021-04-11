using OpenGL;
using OpenGLPractice.Utilities;

namespace OpenGLPractice.GameObjects
{
    class Arrow : GameObject
    {
        private GLUquadric m_QuadricObject;

        public Vector3 Color { get; set; }

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
            GL.glColor3fv(Color.ToArray);
            GLU.gluCylinder(m_QuadricObject, 0.2, 0, 0.5, 20, 20);
        }
    }
}
