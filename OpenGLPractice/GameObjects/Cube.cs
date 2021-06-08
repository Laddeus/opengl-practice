using System;
using System.Windows.Forms.VisualStyles;
using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class Cube : GameObject
    {
        private float time = 0;
        private float angle = 60;

        public Cube(string i_Name) : base(i_Name)
        {
        }

        protected override void DefineGameObject()
        {
            GLUT.glutSolidCube(1);
        }

        public override void Tick(float i_DeltaTime)
        {
            //if (time < 1000)
            {
                Transform.RotateAround(angle * i_DeltaTime, new Vector3(2, 0, 2), Vector3.Up);
                //angle += 5f;
            }
        }
    }
}
