using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class Cube : GameObject
    {
        public Vector4 Color;

        public Cube(string i_Name) : base(i_Name)
        {
            Color = new Vector4(1, 0, 0, 1);
        }

        protected override void DefineGameObject()
        {
            //GL.glBegin(GL.GL_QUADS);

            //GL.glColor4fv(FrontFaceColor.ToArray);

            //// front face
            //GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            //GL.glVertex3f(0.5f, -0.5f, 0.5f);
            //GL.glVertex3f(0.5f, 0.5f, 0.5f);
            //GL.glVertex3f(-0.5f, 0.5f, 0.5f);

            //GL.glColor4fv(RightFaceColor.ToArray);

            //// right face
            //GL.glVertex3f(0.5f, -0.5f, 0.5f);
            //GL.glVertex3f(0.5f, -0.5f, -0.5f);
            //GL.glVertex3f(0.5f, 0.5f, -0.5f);
            //GL.glVertex3f(0.5f, 0.5f, 0.5f);

            //GL.glColor4fv(BackFaceColor.ToArray);

            //// back face
            //GL.glVertex3f(0.5f, -0.5f, -0.5f);
            //GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            //GL.glVertex3f(-0.5f, 0.5f, -0.5f);
            //GL.glVertex3f(0.5f, 0.5f, -0.5f);

            //GL.glColor4fv(LeftFaceColor.ToArray);

            //// left face
            //GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            //GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            //GL.glVertex3f(-0.5f, 0.5f, 0.5f);
            //GL.glVertex3f(-0.5f, 0.5f, -0.5f);

            //GL.glColor4fv(TopFaceColor.ToArray);

            //// top face
            //GL.glVertex3f(-0.5f, 0.5f, 0.5f);
            //GL.glVertex3f(0.5f, 0.5f, 0.5f);
            //GL.glVertex3f(0.5f, 0.5f, -0.5f);
            //GL.glVertex3f(-0.5f, 0.5f, -0.5f);

            //GL.glColor4fv(BottomFaceColor.ToArray);

            //// bottom face
            //GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            //GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            //GL.glVertex3f(0.5f, -0.5f, -0.5f);
            //GL.glVertex3f(0.5f, -0.5f, 0.5f);

            //GL.glEnd();

            GL.glEnable(GL.GL_BLEND);
            GL.glEnable(GL.GL_CULL_FACE);

            GL.glColor4fv(Color.ToArray);
            GL.glCullFace(GL.GL_FRONT);
            GLUT.glutSolidCube(1);
            GL.glCullFace(GL.GL_BACK);
            GLUT.glutSolidCube(1);

            GL.glDisable(GL.GL_BLEND);
            GL.glDisable(GL.GL_CULL_FACE);
        }

        public override void Tick(float i_DeltaTime)
        {
        }

        //public void SetColorForAllFaces(Vector4 i_ColorToSet)
        //{
        //    foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
        //    {
        //        if (propertyInfo.Name.Contains("FaceColor"))
        //        {
        //            propertyInfo.SetValue(this, i_ColorToSet);
        //        }
        //    }
        //}
    }
}
