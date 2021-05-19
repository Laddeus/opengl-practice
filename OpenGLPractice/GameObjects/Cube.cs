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
        private float angle = 0;

        public Cube(string i_Name) : base(i_Name)
        {
        }

        protected override void DefineGameObject()
        {
            GLUT.glutSolidCube(1);
        }

        public override void Tick(float i_DeltaTime)
        {
            ////if (time < 1000)
            ////{
            ////    float radians = (float)(angle * Math.PI / 180.0f);
            ////    Transform.Position = new Vector3((float)(Math.Cos(radians)), Transform.Position.Y,
            ////        (float)(Math.Sin(radians) + 0));
            ////    angle += 5f;
            ////    //Transform.RotateAround(0, new Vector3(1, 0, 0), Transform.eRotationAxis.Up);
            ////    time++;
            ////}
        }
    }
}
