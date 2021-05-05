using System;
using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.OpenGLUtilities
{
    internal class Camera
    {
        public event Action CameraUpdated;

        public Vector3 LookAtPosition { get; set; }

        public Vector3 EyePosition { get; set; }

        public Vector3 UpVector { get; set; }

        public Vector3 Direction { get; set; }

        public float YawAngle { get; set; }

        public float PitchAngle { get; set; }

        public float Radius { get; set; }

        public Camera()
        {
            UpVector = new Vector3(0, 1, 0);
            EyePosition = Vector3.Zero;
            LookAtPosition = Vector3.Zero;
            Radius = 1.0f;
        }

        public void UpdateCameraFreeMovementDirection()
        {
            double yawAngleRadians = YawAngle * Math.PI / 180.0;
            double pitchAngleRadians = PitchAngle * Math.PI / 180.0;

            Direction = new Vector3(
                (float)(Math.Cos(yawAngleRadians) * Math.Cos(pitchAngleRadians)),
                (float)Math.Sin(pitchAngleRadians),
                (float)(Math.Sin(yawAngleRadians) * Math.Cos(pitchAngleRadians))).Normalized;

            LookAtPosition = Direction + EyePosition;
        }

        public void UpdateCameraPositionAroundLockedObject()
        {
            double yawAngleRadians = YawAngle * Math.PI / 180.0;
            double pitchAngleRadians = PitchAngle * Math.PI / 180.0;

            Vector3 vertexOnSphere = new Vector3(
                (float)(Math.Cos(yawAngleRadians) * Math.Cos(pitchAngleRadians)),
                (float)Math.Sin(pitchAngleRadians),
                (float)(Math.Sin(yawAngleRadians) * Math.Cos(pitchAngleRadians))).Normalized;

            EyePosition = Radius * vertexOnSphere + LookAtPosition;
        }

        public void ApplyCameraView()
        {
            GLU.gluLookAt(EyePosition.X, EyePosition.Y, EyePosition.Z,
                LookAtPosition.X, LookAtPosition.Y, LookAtPosition.Z,
                UpVector.X, UpVector.Y, UpVector.Z);

            OnCameraUpdated();
        }

        protected virtual void OnCameraUpdated()
        {
            if (CameraUpdated != null)
            {
                CameraUpdated.Invoke();
            }
        }
    }
}
