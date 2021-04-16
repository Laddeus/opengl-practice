using System;
using OpenGL;

namespace OpenGLPractice.Utilities
{
    internal class Camera
    {
        public Vector3 LookAtPosition { get; set; }

        public Vector3 EyePosition { get; set; }

        public Vector3 UpVector { get; set; }

        public float LookAtHorizontalAngle { get; set; }

        public float LookAtVerticalAngle { get; set; }

        public float LookAtDistance { get; set; }

        public Camera()
        {
            UpVector = new Vector3(0, 1, 0);
            EyePosition = new Vector3(1, 1, 1);
            LookAtPosition = Vector3.Zero;
            LookAtHorizontalAngle = 0;
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
            double radianHorizontalAngle = LookAtHorizontalAngle * Math.PI / 180.0;

            EyePosition = new Vector3((LookAtDistance * (float)Math.Cos(radianHorizontalAngle)) + LookAtPosition.X, LookAtDistance + LookAtPosition.Y,
                (LookAtDistance * (float)Math.Sin(radianHorizontalAngle)) + LookAtPosition.Z);
        }
    }
}
