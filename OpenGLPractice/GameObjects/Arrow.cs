using OpenGL;
using OpenGLPractice.Game;

namespace OpenGLPractice.GameObjects
{
    internal class Arrow : GameObject
    {
        public Arrow(string i_Name) : base(i_Name)
        {
        }

        protected override void DefineGameObject()
        {
            GLU.gluCylinder(sr_GluQuadric, 0.2, 0, 0.5, 20, 20);
        }

        public override void Tick(float i_DeltaTime)
        {
        }
    }
}