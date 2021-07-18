using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class MirrorSurface : GameObject
    {
        private readonly float r_MirrorSurfaceRadius;
        private readonly Vector4 r_MirrorColor = new Vector4(0.23125f, 0.23125f, 0.23125f, 0.3f);

        public MirrorSurface(string i_Name, float i_MirrorSurfaceRadius = 1)
            : base(i_Name)
        {
            r_MirrorSurfaceRadius = i_MirrorSurfaceRadius;
            IsTransparent = v_IsTransparent;
            Color = r_MirrorColor;
            Transform.Rotate(90, 0, 1, 0);
        }

        protected override void DefineGameObject()
        {
            GLU.gluDisk(sr_GluQuadric, 0, r_MirrorSurfaceRadius, 40, 40);
        }
    }
}
