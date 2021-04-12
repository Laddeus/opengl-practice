using System;
using OpenGL;

namespace OpenGLPractice.Utilities
{
    internal class Camera
    {
        public Vector3 LookAtPosition { get; set; }

        public Vector3 EyePosition { get; set; }

        public Vector3 UpVector { get; set; }

        public float LookAtAngle { get; set; }

        public float LookAtDistance { get; set; }

        public Camera()
        {
            UpVector = new Vector3(0, 1, 0);
            EyePosition = new Vector3(10, 10, 5);
            LookAtPosition = Vector3.Zero;
            LookAtAngle = 0;
            LookAtDistance = 5;
        }

        public void ApplyChanges()
        {
            setEyePositionAroundLookAt();
            GLU.gluLookAt(EyePosition.X, EyePosition.Y, EyePosition.Z,
                LookAtPosition.X, LookAtPosition.Y, LookAtPosition.Z,
                UpVector.X, UpVector.Y, UpVector.Z);
        }

        private void setEyePositionAroundLookAt()
        {
            double radianAngle = LookAtAngle * Math.PI / 180.0;
            EyePosition = new Vector3((LookAtDistance * (float)Math.Cos(radianAngle)) + LookAtPosition.X, EyePosition.Y,
                (LookAtDistance * (float)Math.Sin(radianAngle)) + LookAtPosition.Z);
        }
    }
}
