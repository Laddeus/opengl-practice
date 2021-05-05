using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.GameObjects
{
    internal class WorldCube : GameObject
    {
        private float m_Size = 1.0f;
        private CubemapTexture m_CubemapTexture;

        private bool HasTexture => m_CubemapTexture != null;

        public float Size
        {
            get => m_Size;
            set
            {
                m_Size = value;
                Transform.Scale = new Vector3(m_Size);
            }
        }

        public WorldCube(string i_Name) : base(i_Name)
        {
        }

        protected override void DefineGameObject()
        {
            if (HasTexture)
            {
                defineObjectWithTextures();
            }
            else
            {
                defineObjectWithoutTextures();
            }
        }

        private void defineObjectWithoutTextures()
        {
            // front
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, 1.0f);
            GL.glEnd();

            // back
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, -1.0f);
            GL.glEnd();

            // left
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, -1.0f);
            GL.glEnd();

            // right
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, 1.0f);
            GL.glEnd();

            // top
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, -1.0f);
            GL.glEnd();

            // bottom
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, -1.0f, 1.0f);
            GL.glEnd();
        }

        public void defineObjectWithTextures()
        {
            // front
            m_CubemapTexture[CubemapTexture.eTextureFace.Front].BindTexture();
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, 1.0f);
            GL.glEnd();

            // back
            m_CubemapTexture[CubemapTexture.eTextureFace.Back].BindTexture();
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, -1.0f);
            GL.glEnd();

            // left
            m_CubemapTexture[CubemapTexture.eTextureFace.Left].BindTexture();
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, -1.0f);
            GL.glEnd();

            // right
            m_CubemapTexture[CubemapTexture.eTextureFace.Right].BindTexture();
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, 1.0f);
            GL.glEnd();

            // top
            m_CubemapTexture[CubemapTexture.eTextureFace.Top].BindTexture();
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, -1.0f);
            GL.glEnd();

            // bottom
            m_CubemapTexture[CubemapTexture.eTextureFace.Bottom].BindTexture();
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, -1.0f, 1.0f);
            GL.glEnd();
        }

        public void UseTexture(CubemapTexture i_SelectedCubemapTexture)
        {
            m_CubemapTexture = i_SelectedCubemapTexture;
        }
    }
}
