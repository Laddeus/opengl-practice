using System;
using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.Utilities
{
    internal class Camera
    {
        public event Action CameraUpdated;

        private Vector3 m_LookAtPosition;

        public Vector3 LookAtPosition
        {
            get => m_LookAtPosition;
            set
            {
                m_LookAtPosition = value;
                setEyePositionAroundLookAt();
            }
        }

        public Vector3 EyePosition { get; set; }

        public Vector3 UpVector { get; set; }

        public float LookAtVerticalAngle { get; set; }

        private float m_LookAtHorizontalAngle;

        public float LookAtHorizontalAngle
        {
            get => m_LookAtHorizontalAngle;
            set
            {
                m_LookAtHorizontalAngle = value;
                setEyePositionAroundLookAt();
            }
        }

        private float m_LookAtDistance;

        public float LookAtDistance
        {
            get => m_LookAtDistance;
            set
            {
                m_LookAtDistance = value;
                setEyePositionAroundLookAt();
            }
        }

        public Camera()
        {
            UpVector = new Vector3(0, 1, 0);
            EyePosition = Vector3.Zero;
            LookAtPosition = Vector3.Zero;
            LookAtDistance = 5;
            LookAtHorizontalAngle = 0;
        }

        public void ApplyChanges()
        {
            //setEyePositionAroundLookAt();
            GLU.gluLookAt(EyePosition.X, EyePosition.Y, EyePosition.Z,
                LookAtPosition.X, LookAtPosition.Y, LookAtPosition.Z,
                UpVector.X, UpVector.Y, UpVector.Z);

            OnCameraUpdated();
        }

        private void setEyePositionAroundLookAt()
        {
            double radianHorizontalAngle = LookAtHorizontalAngle * Math.PI / 180.0;

            EyePosition = new Vector3((LookAtDistance * (float)Math.Cos(radianHorizontalAngle)) + LookAtPosition.X, LookAtDistance + LookAtPosition.Y,
                (LookAtDistance * (float)Math.Sin(radianHorizontalAngle)) + LookAtPosition.Z);
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
