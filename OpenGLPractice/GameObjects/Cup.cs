using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class Cup : GameObject
    {
        private const float k_CupHeight = 1.4f;
        private const double k_CupBottomInnerRadius = 0;
        private const double k_CupBaseRadius = 0.5;
        private const double k_CupTopRadius = 0.7;
        private const double k_CupLipOuterRadius = 0.05;
        private const double k_CupLipInnerRadius = 0.7;

        public float Height => k_CupHeight;

        public Vector3 Color;

        public Cup(string i_Name) : base(i_Name)
        {
            Color = new Vector3(0, 0, 1);
        }

        protected override void DefineGameObject()
        {
            GL.glTranslatef(0, k_CupHeight, 0);
            GL.glRotatef(90, 1, 0, 0);
            GL.glColor3fv(Color.ToArray);
            GLU.gluDisk(r_gluQuadric, k_CupBottomInnerRadius, k_CupBaseRadius, 20, 20);
            GLU.gluCylinder(r_gluQuadric, k_CupBaseRadius, k_CupTopRadius, k_CupHeight, 20, 20);
            GL.glTranslated(0, 0, k_CupHeight);
            GL.glColor3f(1, 1, 0);
            GLUT.glutSolidTorus(k_CupLipOuterRadius, k_CupLipInnerRadius, 20, 30);
        }

        public override void Tick(float i_DeltaTime)
        {
        }
    }
}
