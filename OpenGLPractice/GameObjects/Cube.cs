using OpenGL;
using OpenGLPractice.Game;

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
    }
}
