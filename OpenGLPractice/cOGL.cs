using System.Collections.Generic;
using System.Windows.Forms;
using OpenGL;
using OpenGLPractice.GameObjects;

namespace OpenGLPractice
{
    class cOGL
    {
        Control p;
        int Width;
        int Height;

        public CubeWithArrow CubeWithArrow { get; }


        private GLUquadric obj;
        private Cube m_Ground;
        public Camera Camera { get; }

        public List<GameObject> GameObjects { get; set; }

        public GameObject SelectedGameObjectForControl { get; set; }

        public cOGL(Control pb)
        {
            p = pb;
            Width = p.Width;
            Height = p.Height;
            InitializeGL();

            GameObjects = new List<GameObject>();

            obj = GLU.gluNewQuadric();
            //CubeWithArrow = new CubeWithArrow("CubeWithArrow");

            Camera = new Camera();
            //m_Ground = new Cube("Ground");
            //m_Ground.SetColorForAllFaces(new Vector3(1, 1, 1));
            //m_Ground.Transform.Translate(0, -1, 0);
            //m_Ground.Transform.Scale(10, 0.1f, 10);
            //m_Ground.Transform.Translate(0, -0.5f, 0);

        }

        ~cOGL()
        {
            GLU.gluDeleteQuadric(obj);
            WGL.wglDeleteContext(m_uint_RC);
        }

        uint m_uint_HWND = 0;

        public uint HWND
        {
            get { return m_uint_HWND; }
        }

        uint m_uint_DC = 0;

        public uint DC
        {
            get { return m_uint_DC; }
        }
        uint m_uint_RC = 0;

        public uint RC
        {
            get { return m_uint_RC; }
        }

        void DrawOldAxes()
        {
            //for this time
            //Lights positioning is here!!!
            float[] pos = new float[4];
            pos[0] = 10; pos[1] = 10; pos[2] = 10; pos[3] = 1;
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, pos);
            GL.glDisable(GL.GL_LIGHTING);

            //INITIAL axes
            GL.glEnable(GL.GL_LINE_STIPPLE);
            GL.glLineStipple(1, 0xFF00);  //
                                          // ted   
            GL.glBegin(GL.GL_LINES);
            //x  RED
            GL.glColor3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(-5.0f, 0.0f, 0.0f);
            GL.glVertex3f(5.0f, 0.0f, 0.0f);
            //y  GREEN 
            GL.glColor3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.0f, -5.0f, 0.0f);
            GL.glVertex3f(0.0f, 5.0f, 0.0f);
            //z  BLUE
            GL.glColor3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, 0.0f, -5.0f);
            GL.glVertex3f(0.0f, 0.0f, 5.0f);
            GL.glEnd();

            GL.glColor3f(0.0f, 0.0f, 1.0f);

            GL.glTranslatef(0, 0, 5);
            GLU.gluCylinder(obj, 0.2, 0, 1, 20, 20);
            GL.glTranslatef(0, 0, -5);

            GL.glColor3f(1.0f, 0.0f, 0.0f);
            GL.glTranslatef(5, 0, 0);
            GL.glRotatef(90, 0, 1, 0);

            GLU.gluCylinder(obj, 0.2, 0, 1, 20, 20);
            GL.glRotatef(-90, 0, 1, 0);
            GL.glTranslatef(-5, 0, 0);

            GL.glColor3f(0, 1, 0);
            GL.glTranslatef(0, 5, 0);
            GL.glRotatef(-90, 1, 0, 0);
            GLU.gluCylinder(obj, 0.2, 0, 1, 20, 20);
            GL.glRotatef(90, 1, 0, 0);
            GL.glTranslatef(0, -5, 0);
            GL.glDisable(GL.GL_LINE_STIPPLE);
        }

        public void Draw()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);

            GL.glLoadIdentity();


            if (SelectedGameObjectForControl != null)
            {
                Vector3 cameraYOffset = new Vector3(0, 3, 0);
                Camera.LookAtPosition = SelectedGameObjectForControl.Transform.Position;
                Camera.EyePosition = SelectedGameObjectForControl.Transform.Position + cameraYOffset;
            }

            Camera.ApplyChanges();

            DrawOldAxes();
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw();
            }

            GL.glFlush();
            WGL.wglSwapBuffers(m_uint_DC);
        }

        protected virtual void InitializeGL()
        {
            m_uint_HWND = (uint)p.Handle.ToInt32();
            m_uint_DC = WGL.GetDC(m_uint_HWND);

            // Not doing the following WGL.wglSwapBuffers() on the DC will
            // result in a failure to subsequently create the RC.
            WGL.wglSwapBuffers(m_uint_DC);

            WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
            WGL.ZeroPixelDescriptor(ref pfd);
            pfd.nVersion = 1;
            pfd.dwFlags = (WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER);
            pfd.iPixelType = (byte)(WGL.PFD_TYPE_RGBA);
            pfd.cColorBits = 32;
            pfd.cDepthBits = 32;
            pfd.iLayerType = (byte)(WGL.PFD_MAIN_PLANE);

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
            //Create rendering context
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
            Width = p.Width;
            Height = p.Height;
            GL.glViewport(0, 0, Width, Height);

            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();
            GLU.gluPerspective(45.0, ((double)Width) / Height, 1.0, 1000.0);

            GL.glMatrixMode(GL.GL_MODELVIEW);
            Draw();
        }

        protected virtual void initRenderingGL()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;
            if (this.Width == 0 || this.Height == 0)
                return;
            GL.glClearColor(0.5f, 0.5f, 0.5f, 0.0f);
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);

            GL.glViewport(0, 0, this.Width, this.Height);
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            //nice 3D
            GLU.gluPerspective(60, ((double)Width) / Height, 1.0, 1000.0);

            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();
        }
    }
}