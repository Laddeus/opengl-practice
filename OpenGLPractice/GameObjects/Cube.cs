using System.Reflection;
using OpenGL;
using OpenGLPractice.Utilities;

namespace OpenGLPractice.GameObjects
{
    internal class Cube : GameObject
    {
        public Vector4 FrontFaceColor { get; set; }

        public Vector4 BackFaceColor { get; set; }

        public Vector4 RightFaceColor { get; set; }

        public Vector4 LeftFaceColor { get; set; }

        public Vector4 TopFaceColor { get; set; }

        public Vector4 BottomFaceColor { get; set; }

        public Cube(string i_Name) : base(i_Name)
        {
            FrontFaceColor = new Vector4(1, 0, 0, 0.3f);
            RightFaceColor = new Vector4(0, 1, 0, 0.3f);
            BackFaceColor = new Vector4(0, 0, 1, 0.3f);
            LeftFaceColor = new Vector4(1, 1, 0, 0.3f);
            BottomFaceColor = new Vector4(1, 0, 1, 0.3f);
            TopFaceColor = new Vector4(0, 1, 1, 0.3f);
        }

        protected override void DefineGameObject()
        {
            GL.glBegin(GL.GL_QUADS);

            GL.glColor4fv(FrontFaceColor.ToArray);

            // front face
            GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            GL.glVertex3f(0.5f, -0.5f, 0.5f);
            GL.glVertex3f(0.5f, 0.5f, 0.5f);
            GL.glVertex3f(-0.5f, 0.5f, 0.5f);

            GL.glColor4fv(RightFaceColor.ToArray);

            // right face
            GL.glVertex3f(0.5f, -0.5f, 0.5f);
            GL.glVertex3f(0.5f, -0.5f, -0.5f);
            GL.glVertex3f(0.5f, 0.5f, -0.5f);
            GL.glVertex3f(0.5f, 0.5f, 0.5f);

            GL.glColor4fv(BackFaceColor.ToArray);

            // back face
            GL.glVertex3f(0.5f, -0.5f, -0.5f);
            GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            GL.glVertex3f(-0.5f, 0.5f, -0.5f);
            GL.glVertex3f(0.5f, 0.5f, -0.5f);

            GL.glColor4fv(LeftFaceColor.ToArray);

            // left face
            GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            GL.glVertex3f(-0.5f, 0.5f, 0.5f);
            GL.glVertex3f(-0.5f, 0.5f, -0.5f);

            GL.glColor4fv(TopFaceColor.ToArray);

            // top face
            GL.glVertex3f(-0.5f, 0.5f, 0.5f);
            GL.glVertex3f(0.5f, 0.5f, 0.5f);
            GL.glVertex3f(0.5f, 0.5f, -0.5f);
            GL.glVertex3f(-0.5f, 0.5f, -0.5f);

            GL.glColor4fv(BottomFaceColor.ToArray);

            // bottom face
            GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            GL.glVertex3f(0.5f, -0.5f, -0.5f);
            GL.glVertex3f(0.5f, -0.5f, 0.5f);

            GL.glEnd();
        }

        public override void Tick(float i_DeltaTime)
        {
        }

        public void SetColorForAllFaces(Vector4 i_ColorToSet)
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
