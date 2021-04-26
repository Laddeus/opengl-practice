using OpenGL;
using OpenGLPractice.GLMath;
using OpenGLPractice.Utilities;

namespace OpenGLPractice.GameObjects
{
    internal class Cube : GameObject
    {
        public Cube(string i_Name) : base(i_Name)
        {
        }

        protected override void DefineGameObject()
        {
            GLUT.glutSolidCube(1);
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
