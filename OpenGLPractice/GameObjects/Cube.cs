using System.Reflection;
using OpenGL;
using OpenGLPractice.Utilities;

namespace OpenGLPractice.GameObjects
{
    internal class Cube : GameObject
    {
        public Vector3 FrontFaceColor { get; set; }
        public Vector3 BackFaceColor { get; set; }
        public Vector3 RightFaceColor { get; set; }
        public Vector3 LeftFaceColor { get; set; }
        public Vector3 TopFaceColor { get; set; }
        public Vector3 BottomFaceColor { get; set; }

        public Cube(string i_Name) : base(i_Name)
        {
            SetColorForAllFaces(new Vector3(0.85f, 0.85f, 0.85f));
        }

        protected override void DefineGameObject()
        {
            GL.glBegin(GL.GL_QUADS);

            GL.glColor3f(FrontFaceColor.X, FrontFaceColor.Y, FrontFaceColor.Z);

            // front face
            GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            GL.glVertex3f(0.5f, -0.5f, 0.5f);
            GL.glVertex3f(0.5f, 0.5f, 0.5f);
            GL.glVertex3f(-0.5f, 0.5f, 0.5f);

            GL.glColor3f(RightFaceColor.X, RightFaceColor.Y, RightFaceColor.Z);

            // right face
            GL.glVertex3f(0.5f, -0.5f, 0.5f);
            GL.glVertex3f(0.5f, -0.5f, -0.5f);
            GL.glVertex3f(0.5f, 0.5f, -0.5f);
            GL.glVertex3f(0.5f, 0.5f, 0.5f);

            GL.glColor3f(BackFaceColor.X, BackFaceColor.Y, BackFaceColor.Z);

            // back face
            GL.glVertex3f(0.5f, -0.5f, -0.5f);
            GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            GL.glVertex3f(-0.5f, 0.5f, -0.5f);
            GL.glVertex3f(0.5f, 0.5f, -0.5f);

            GL.glColor3f(LeftFaceColor.X, LeftFaceColor.Y, LeftFaceColor.Z);

            // left face
            GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            GL.glVertex3f(-0.5f, 0.5f, 0.5f);
            GL.glVertex3f(-0.5f, 0.5f, -0.5f);

            GL.glColor3f(TopFaceColor.X, TopFaceColor.Y, TopFaceColor.Z);

            // top face
            GL.glVertex3f(-0.5f, 0.5f, 0.5f);
            GL.glVertex3f(0.5f, 0.5f, 0.5f);
            GL.glVertex3f(0.5f, 0.5f, -0.5f);
            GL.glVertex3f(-0.5f, 0.5f, -0.5f);

            GL.glColor3f(BottomFaceColor.X, BottomFaceColor.Y, BottomFaceColor.Z);
            // bottom face
            GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            GL.glVertex3f(0.5f, -0.5f, -0.5f);
            GL.glVertex3f(0.5f, -0.5f, 0.5f);

            GL.glEnd();
        }

        public void SetColorForAllFaces(Vector3 i_ColorToSet)
        {
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.Name.Contains("FaceColor"))
                {
                    propertyInfo.SetValue(this, i_ColorToSet);
                }
            }
        }
    }
}
