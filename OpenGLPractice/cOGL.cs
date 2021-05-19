using System.Collections.Generic;
using System.Windows.Forms;
using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GameObjects;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice
{
    internal class cOGL
    {
        private readonly Control r_Panel;
        private int m_Width;
        private int m_Height;

        public GameEnvironment GameEnvironment { get; private set; }

        public List<GameObject> GameObjects => GameEnvironment.GameObjects;

        public Light Light => GameEnvironment.Light;

        public Camera Camera => GameEnvironment.Camera;

        public WorldCube WorldCube => GameEnvironment.WorldCube;

        public GameObject SelectedGameObjectForControl { get; set; }

        public cOGL(Control i_Panel)
        {
            r_Panel = i_Panel;
            m_Width = r_Panel.Width;
            m_Height = r_Panel.Height;

            InitializeGL();

            GameEnvironment = new GameEnvironment()
            {
                DrawShadows = true,
                UseLight = true,
                DrawReflections = true
            };
        }

        ~cOGL()
        {
            WGL.wglDeleteContext(m_uint_RC);
        }

        private uint m_uint_HWND = 0;

        public uint HWND
        {
            get { return m_uint_HWND; }
        }

        private uint m_uint_DC = 0;

        public uint DC
        {
            get { return m_uint_DC; }
        }

        private uint m_uint_RC = 0;

        public uint RC
        {
            get { return m_uint_RC; }
        }

        public void Draw()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
            {
                return;
            }

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);
            GLErrorCatcher.TryGLCall(() => GL.glLoadIdentity());

            Camera.ApplyCameraView();

            GameEnvironment.DrawScene();

            GLErrorCatcher.TryGLCall(() => GL.glFlush());
            WGL.wglSwapBuffers(m_uint_DC);
        }

        protected virtual void InitializeGL()
        {
            m_uint_HWND = (uint)r_Panel.Handle.ToInt32();
            m_uint_DC = WGL.GetDC(m_uint_HWND);

            // Not doing the following WGL.wglSwapBuffers() on the DC will
            // result in a failure to subsequently create the RC.
            WGL.wglSwapBuffers(m_uint_DC);

            WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
            WGL.ZeroPixelDescriptor(ref pfd);
            pfd.nVersion = 1;
            pfd.dwFlags = WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER;
            pfd.iPixelType = (byte)WGL.PFD_TYPE_RGBA;
            pfd.cColorBits = 32;
            pfd.cDepthBits = 32;
            pfd.cStencilBits = 32;
            pfd.iLayerType = (byte)WGL.PFD_MAIN_PLANE;

            int pixelFormatIndex = 0;
            pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
            if (pixelFormatIndex == 0)
            {
                MessageBox.Show("Unable to retrieve pixel format");
                return;
            }

            if (WGL.SetPixelFormat(m_uint_DC, pixelFormatIndex, ref pfd) == 0)
            {
                MessageBox.Show("Unable to set pixel format");
                return;
            }

            // Create rendering context
            m_uint_RC = WGL.wglCreateContext(m_uint_DC);
            if (m_uint_RC == 0)
            {
                MessageBox.Show("Unable to get rendering context");
                return;
            }

            if (WGL.wglMakeCurrent(m_uint_DC, m_uint_RC) == 0)
            {
                MessageBox.Show("Unable to make rendering context current");
                return;
            }

            initRenderingGL();
        }

        public void OnResize()
        {
            m_Width = r_Panel.Width;
            m_Height = r_Panel.Height;
            GLErrorCatcher.TryGLCall(() => GL.glViewport(0, 0, m_Width, m_Height));

            GLErrorCatcher.TryGLCall(() => GL.glMatrixMode(GL.GL_PROJECTION));
            GLErrorCatcher.TryGLCall(() => GL.glLoadIdentity());
            GLU.gluPerspective(90, ((double)m_Width) / m_Height, 1.0, 1000.0);

            GLErrorCatcher.TryGLCall(() => GL.glMatrixMode(GL.GL_MODELVIEW));
            Draw();
        }

        protected virtual void initRenderingGL()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
            {
                return;
            }

            if (this.m_Width == 0 || this.m_Height == 0)
            {
                return;
            }

            GLErrorCatcher.TryGLCall(() => GL.glShadeModel(GL.GL_SMOOTH));
            GLErrorCatcher.TryGLCall(() => GL.glClearColor(0.2f, 0.2f, 0.2f, 0.0f));
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_DEPTH_TEST));
            GLErrorCatcher.TryGLCall(() => GL.glDepthFunc(GL.GL_LEQUAL));

            GLErrorCatcher.TryGLCall(() => GL.glViewport(0, 0, this.m_Width, this.m_Height));
            GLErrorCatcher.TryGLCall(() => GL.glMatrixMode(GL.GL_PROJECTION));
            GLErrorCatcher.TryGLCall(() => GL.glLoadIdentity());

            // nice 3D
            GLU.gluPerspective(90, ((double)m_Width) / m_Height, 1.0, 1000.0);

            GLErrorCatcher.TryGLCall(() => GL.glMatrixMode(GL.GL_MODELVIEW));
            GLErrorCatcher.TryGLCall(() => GL.glLoadIdentity());

            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_LIGHTING));
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_COLOR_MATERIAL));
            // GLErrorCatcher.TryGLCall(() => GL.glLightModelfv(GL.GL_LIGHT_MODEL_AMBIENT, new float[] { 0.2f, 0.2f, 0.2f, 1.0f }));
            GLErrorCatcher.TryGLCall(() => GL.glLightModelf(GL.GL_LIGHT_MODEL_TWO_SIDE, 1.0f));
            GLErrorCatcher.TryGLCall(() => GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA));
        }
    }
}