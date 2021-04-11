using OpenGL;

namespace OpenGLPractice.GameObjects
{
    class CubeWithArrow : GameObject
    {
        public Cube Cube { get; }
        public Arrow Arrow { get; }

        public CubeWithArrow(string i_Name) : base(i_Name)
        {
            Cube = new Cube("Cube");
            Cube.SetColorForAllFaces(new Vector3(1, 0, 0));
            Cube.FrontFaceColor = new Vector3(1, 1, 0);
            Cube.BackFaceColor = new Vector3(0, 0, 1);


            Arrow = new Arrow("ForwardArrow");
            Arrow.Color = new Vector3(0, 0, 0);
            Arrow.Transform.Translate(0.0f, 0.0f, 0.5f);
        }

        protected override void DefineGameObject()
        {
            //Cube.Draw();
            //Arrow.Draw();

            // supposedly a more efficient approach...
            Cube.CallList();
            GL.glMultMatrixf(Arrow.Transform.TransformationMatrix);
            Arrow.CallList();
        }
    }
}
