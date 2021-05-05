using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.GameObjects
{
    internal class Rod : GameObject
    {
        private readonly float r_InnerCylinderRadius;
        private readonly float r_OuterCylinderRadius;
        private readonly float r_OuterRingWidth;
        private readonly float r_Height;

        public float Height => r_Height * Transform.Scale.Y;

        public float RodRadius => r_OuterCylinderRadius * Transform.Scale.X; // assuming uniform scaling

        public Rod(string i_Name, float i_InnerRodRadius = 0.1f, float i_OuterRingWidth = 0.05f, float i_Height = 1) : base(i_Name)
        {
            r_InnerCylinderRadius = i_InnerRodRadius;
            r_OuterRingWidth = i_OuterRingWidth;
            r_Height = i_Height;
            r_OuterCylinderRadius = r_InnerCylinderRadius + r_OuterRingWidth;
        }

        protected override void DefineGameObject()
        {
            GLErrorCatcher.TryGLCall(() => GL.glRotatef(-90, 1, 0, 0));
            //// GLErrorCatcher.TryGLCall(() => GL.glColor3f(0, 0, 1));
            GLU.gluCylinder(sr_GluQuadric, r_InnerCylinderRadius, r_InnerCylinderRadius, r_Height, 20, 20);
            //// GLErrorCatcher.TryGLCall(() => GL.glColor3f(1, 0, 0));
            GLU.gluCylinder(sr_GluQuadric, r_OuterCylinderRadius, r_OuterCylinderRadius, r_Height, 20, 20);
            //// GLErrorCatcher.TryGLCall(() => GL.glColor3f(1, 1, 0));
            GLU.gluDisk(sr_GluQuadric, r_InnerCylinderRadius, r_OuterCylinderRadius, 20, 20);
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(0, 0, r_Height));
            GLU.gluDisk(sr_GluQuadric, r_InnerCylinderRadius, r_OuterCylinderRadius, 20, 20);
        }

        public override void Tick(float i_DeltaTime)
        {
        }
    }
}
