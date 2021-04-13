using OpenGL;

namespace OpenGLPractice.GameObjects
{
    internal class Rod : GameObject
    {
        private readonly float r_InnerCylinderRadius;
        private readonly float r_OuterCylinderRadius;
        private readonly float r_OuterRingWidth;
        private readonly float r_Height;

        public Rod(string i_Name, float i_InnerRodRadius = 0.1f, float i_OuterRingWidth = 0.05f, float i_Height = 1) : base(i_Name)
        {
            r_InnerCylinderRadius = i_InnerRodRadius;
            r_OuterRingWidth = i_OuterRingWidth;
            r_Height = i_Height;
            r_OuterCylinderRadius = r_InnerCylinderRadius + r_OuterRingWidth;
        }

        protected override void DefineGameObject()
        {
            GL.glColor3f(1, 0, 0);
            GLU.gluCylinder(r_gluQuadric, r_InnerCylinderRadius, r_InnerCylinderRadius, r_Height, 20, 20);
            GL.glColor3f(0, 0, 1);
            GLU.gluCylinder(r_gluQuadric, r_OuterCylinderRadius, r_OuterCylinderRadius, r_Height, 20, 20);
            GL.glColor3f(1, 1, 0);
            GLU.gluDisk(r_gluQuadric, r_InnerCylinderRadius, r_OuterCylinderRadius, 20, 20);
            GL.glTranslatef(0, 0, r_Height);
            GLU.gluDisk(r_gluQuadric, r_InnerCylinderRadius, r_OuterCylinderRadius, 20, 20);
        }
    }
}
