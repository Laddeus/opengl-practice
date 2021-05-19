using System;
using System.Diagnostics;
using System.Windows.Forms;
using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.OpenGLUtilities
{
    internal class Transform
    {
        public enum eRotationAxis
        {
            Up,
            Right,
            Forward
        }

        public static int TransformationMatrixSize { get; } = 16;

        private readonly float[] r_AccumulatedTransformationsMatrix = new float[TransformationMatrixSize];
        private readonly float[] r_AccumulatedTranslationMatrix = new float[TransformationMatrixSize];
        private readonly float[] r_AccumulatedScaleMatrix = new float[TransformationMatrixSize];
        private readonly float[] r_AccumulatedRotationMatrix = new float[TransformationMatrixSize];

        private Vector3 m_Position;
        private Vector3 m_Scale;
        private Vector3 m_Rotation;

        public float[] TransformationsMatrix => r_AccumulatedTransformationsMatrix;

        public float[] RotationMatrix => r_AccumulatedRotationMatrix;

        public Vector3 ForwardVector { get; private set; }

        public Vector3 BackwardVector => -ForwardVector;

        public Vector3 RightVector { get; private set; }

        public Vector3 LeftVector => -RightVector;

        public Vector3 UpVector { get; private set; }

        public Vector3 DownVector => -UpVector;

        public Vector3 Position
        {
            get => m_Position;

            set
            {
                Translate(value - m_Position);
                m_Position = value;
            }
        }

        public Vector3 Scale
        {
            get => m_Scale;

            set
            {
                ChangeScale(value.X / m_Scale.X, value.Y / m_Scale.Y, value.Z / m_Scale.Z);
                m_Scale = value;
            }
        }

        public Vector3 Rotation => m_Rotation;

        public Transform()
        {
            ForwardVector = new Vector3(0, 0, 1);
            RightVector = new Vector3(-1, 0, 0);
            UpVector = new Vector3(0, 1, 0);

            m_Position = Vector3.Zero;
            m_Scale = new Vector3(1.0f);
            m_Rotation = Vector3.Zero;
            initializeAccumulatedMatrices();
        }

        public static float[] CalculateShadowMatrix(Matrix3 i_PlaneCoordinates, Vector4 i_LightPosition)
        {
            float[] shadowMatrix = new float[TransformationMatrixSize];

            Vector3 planeNormal = getPlaneNormal(i_PlaneCoordinates);
            float planeDistance = (-1) * planeNormal.DotProduct(i_PlaneCoordinates[0]);
            Vector4 planeCoefficients = new Vector4(planeNormal.X, planeNormal.Y, planeNormal.Z, planeDistance);
            float planeLightDotProduct = i_LightPosition.DotProduct(planeCoefficients);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        shadowMatrix[i * 4 + j] = planeLightDotProduct - i_LightPosition[j] * planeCoefficients[i];
                    }
                    else
                    {
                        shadowMatrix[i * 4 + j] = 0.0f - i_LightPosition[j] * planeCoefficients[i];
                    }
                }
            }

            return shadowMatrix;
        }

        private static Vector3 getPlaneNormal(Matrix3 i_PlaneCoordinates)
        {
            Vector3 firstVector = i_PlaneCoordinates[0] - i_PlaneCoordinates[1];
            Vector3 secondVector = i_PlaneCoordinates[1] - i_PlaneCoordinates[2];

            return firstVector.CrossProduct(secondVector).Normalized;
        }

        private void initializeAccumulatedMatrices()
        {
            GLErrorCatcher.TryGLCall(() => GL.glPushMatrix());
            GLErrorCatcher.TryGLCall(() => GL.glLoadIdentity());

            GLErrorCatcher.TryGLCall(() => GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedTransformationsMatrix));
            GLErrorCatcher.TryGLCall(() => GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedTranslationMatrix));
            GLErrorCatcher.TryGLCall(() => GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedScaleMatrix));
            GLErrorCatcher.TryGLCall(() => GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedRotationMatrix));

            GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
        }

        public void Translate(Vector3 i_TranslateVector)
        {
            Translate(i_TranslateVector.X, i_TranslateVector.Y, i_TranslateVector.Z);
        }

        public void Translate(float i_X, float i_Y, float i_Z)
        {
            performTransformation(() => GL.glTranslatef(i_X, i_Y, i_Z), r_AccumulatedTranslationMatrix);
            m_Position += new Vector3(i_X, i_Y, i_Z);
        }

        public void Rotate(float i_RotationAngle, Vector3 i_RotationAxis)
        {
            Rotate(i_RotationAngle, i_RotationAxis.X, i_RotationAxis.Y, i_RotationAxis.Z);
        }

        public void Rotate(float i_RotationAngle, float i_X, float i_Y, float i_Z)
        {
            performTransformation(() =>
            {
                GLErrorCatcher.TryGLCall(() => GL.glRotatef(i_RotationAngle, i_X, i_Y, i_Z));
            }, r_AccumulatedRotationMatrix);

            calculateDirectionVectors();
        }

        public void ChangeScale(Vector3 i_ScaleVector)
        {
            ChangeScale(i_ScaleVector.X, i_ScaleVector.Y, i_ScaleVector.Z);
        }

        public void ChangeScale(float i_X, float i_Y, float i_Z)
        {
            performTransformation(() =>
            {
                GLErrorCatcher.TryGLCall(() => GL.glScalef(i_X, i_Y, i_Z));
            }, r_AccumulatedScaleMatrix);

            m_Scale *= new Vector3(i_X, i_Y, i_Z);
        }

        private void performTransformation(Action i_TransformationAction, float[] i_AccumulatedTransformationMatrix)
        {
            GLErrorCatcher.TryGLCall(() => GL.glPushMatrix());
            GLErrorCatcher.TryGLCall(() => GL.glLoadIdentity());

            i_TransformationAction.Invoke();

            accumulateTransformation(i_AccumulatedTransformationMatrix);
            updateAccumulatedTransformationMatrix();
            GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
        }

        private void updateAccumulatedTransformationMatrix()
        {
            GLErrorCatcher.TryGLCall(() => GL.glLoadIdentity());
            GLErrorCatcher.TryGLCall(() => GL.glLoadMatrixf(r_AccumulatedTranslationMatrix));
            GLErrorCatcher.TryGLCall(() => GL.glMultMatrixf(r_AccumulatedRotationMatrix));
            GLErrorCatcher.TryGLCall(() => GL.glMultMatrixf(r_AccumulatedScaleMatrix));

            GLErrorCatcher.TryGLCall(() => GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedTransformationsMatrix));
        }

        private void calculateDirectionVectors()
        {
            Matrix4 rotationMatrix = new Matrix4(r_AccumulatedRotationMatrix);
            Vector4 forwardVector4 = new Vector4(0, 0, 1, 1);
            Vector4 upVector4 = new Vector4(0, 1, 0, 1);
            Vector4 rightVector4 = new Vector4(-1, 0, 0, 1);

            ForwardVector = (rotationMatrix * forwardVector4).ToVector3.Normalized;
            RightVector = (rotationMatrix * rightVector4).ToVector3.Normalized;
            UpVector = (rotationMatrix * upVector4).ToVector3.Normalized;

            Debug.WriteLine("Using rotation matrix: ");
            Debug.WriteLine($"Forward Vector = {ForwardVector}");
            Debug.WriteLine($"Right Vector = {RightVector}");
            Debug.WriteLine($"Up Vector = {UpVector}");
        }

        private void accumulateTransformation(float[] i_AccumulatedTransformationMatrix)
        {
            GLErrorCatcher.TryGLCall(() => GL.glMultMatrixf(i_AccumulatedTransformationMatrix));
            GLErrorCatcher.TryGLCall(() => GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, i_AccumulatedTransformationMatrix));
        }
    }
}