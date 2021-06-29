using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.GameObjects
{
    internal class TableTop : GameObject
    {
        private const float k_TableTopRadius = 8.0f;
        private const float k_TableTopHeight = 0.3f;
        private Texture r_TopWoodTexture;

        public float TopHeight => k_TableTopHeight;

        public float TableRadius => k_TableTopRadius;

        public TableTop(string i_Name, Texture i_TopWoodTexture = null)
            : base(i_Name)
        {
            Transform.Rotate(-90, 1, 0, 0);
            r_TopWoodTexture = i_TopWoodTexture;
        }

        protected override void DefineGameObject()
        {
            if (r_TopWoodTexture != null)
            {
                GL.glEnable(GL.GL_TEXTURE_2D);
                GLU.gluQuadricTexture(sr_GluQuadric, 1);
                r_TopWoodTexture.BindTexture();
            }

            // Bottom disk
            GLU.gluDisk(sr_GluQuadric, 0, k_TableTopRadius, 40, 40);
            GLU.gluCylinder(sr_GluQuadric, k_TableTopRadius, k_TableTopRadius, k_TableTopHeight, 40, 40);

            // Top disk
            GL.glTranslatef(0, 0, k_TableTopHeight);
            GLU.gluDisk(sr_GluQuadric, 0, k_TableTopRadius, 40, 40);

            GL.glDisable(GL.GL_TEXTURE_2D);
        }
    }
}
