using OpenGL;
using OpenGLPractice.GLMath;
using OpenGLPractice.Utilities;

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

        public Cup(string i_Name) : base(i_Name)
        {
            Color = new Vector4(0, 0, 1, 1.0f);
        }

        protected override void DefineGameObject()
        {
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(0, k_CupHeight, 0));
            GLErrorCatcher.TryGLCall(() => GL.glRotatef(90, 1, 0, 0));
            GLErrorCatcher.TryGLCall(() => GL.glColor3fv(Color.ToArray));
            GLU.gluDisk(r_gluQuadric, k_CupBottomInnerRadius, k_CupBaseRadius, 20, 20);
            GLU.gluCylinder(r_gluQuadric, k_CupBaseRadius, k_CupTopRadius, k_CupHeight, 20, 20);
            GLErrorCatcher.TryGLCall(() => GL.glTranslated(0, 0, k_CupHeight));
            GLErrorCatcher.TryGLCall(() => GL.glColor3f(1, 1, 0));
            GLUT.glutSolidTorus(k_CupLipOuterRadius, k_CupLipInnerRadius, 20, 30);
        }

        public override void Tick(float i_DeltaTime)
        {
        }
    }
}
