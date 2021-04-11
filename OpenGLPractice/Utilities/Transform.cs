using System;
using System.Diagnostics;
using OpenGL;

namespace OpenGLPractice.Utilities
{
    internal class Transform
    {
        public static int TransformationMatrixSize { get; } = 16;

        private readonly float[] r_AccumulatedTransformationMatrix = new float[TransformationMatrixSize];
        private readonly float[] r_AccumulatedTranslationMatrix = new float[TransformationMatrixSize];
        private readonly float[] r_AccumulatedScaleMatrix = new float[TransformationMatrixSize];
        private readonly float[] r_AccumulatedRotationMatrix = new float[TransformationMatrixSize];

        private Vector3 m_Position;
        private Vector3 m_Scale;
        private Vector3 m_Rotation;

        public float[] TransformationMatrix => r_AccumulatedTransformationMatrix;

        public Vector3 ForwardVector { get; private set; }

        public Vector3 BackwardVector => -ForwardVector;

        public Vector3 RightVector { get; private set; }

        public Vector3 LeftVector => -RightVector;

        public Vector3 UpVector { get; private set; }

        public Vector3 DownVector => -UpVector;

        public Transform()
        {
            ForwardVector = new Vector3(0, 0, 1);
            RightVector = new Vector3(-1, 0, 0);
            UpVector = new Vector3(0, 1, 0);

            m_Position = Vector3.Zero;
            m_Scale = new Vector3(1, 1, 1);
            initializeAccumulatedMatrices();
        }

        public Vector3 Position
        {
            get => m_Position;

            set
            {
                Translate(-m_Position);
                Translate(value);
                m_Position = value;
            }
        }

        public Vector3 Scale
        {
            get => m_Scale;

            set
            {
                ChangeScale(1.0f / m_Scale.X, 1.0f / m_Scale.Y, 1.0f / m_Scale.Z);
                ChangeScale(value);
                m_Scale = value;
            }
        }

        public Vector3 Rotation => m_Rotation;

        private void initializeAccumulatedMatrices()
        {
            GL.glPushMatrix();
            GL.glLoadIdentity();

            GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedTransformationMatrix);
            GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedTranslationMatrix);
            GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedScaleMatrix);
            GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedRotationMatrix);

            GL.glPopMatrix();
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
                //GL.glTranslatef(Position.X, Position.Y, Position.Z);
                GL.glRotatef(i_RotationAngle, i_X, i_Y, i_Z);
                //GL.glTranslatef(-Position.X, -Position.Y, -Position.Z);
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
                //float[] rotationMatrix = getObjectRotatioMatrix();
                //float[] rotationMatrixInverse = getObjectInverseRotationMatrix();
                //GL.glTranslatef(m_Position.X, m_Position.Y, m_Position.Z);
                //GL.glMultMatrixf(rotationMatrix);
                GL.glScalef(i_X, i_Y, i_Z);
                //GL.glMultMatrixf(rotationMatrixInverse);
                //GL.glTranslatef(-m_Position.X, -m_Position.Y, -m_Position.Z);
            }, r_AccumulatedScaleMatrix);

            m_Scale = m_Scale * new Vector3(i_X, i_Y, i_Z);
        }

        private void performTransformation(Action i_TransformationAction, float[] i_AccumulatedTransformationMatrix)
        {
            GL.glPushMatrix();
            GL.glLoadIdentity();

            i_TransformationAction.Invoke();

            accumulateTransformation(i_AccumulatedTransformationMatrix);
            updateAccumulatedTransformationMatrix();
            GL.glPopMatrix();
        }

        private void updateAccumulatedTransformationMatrix()
        {
            GL.glLoadIdentity();
            GL.glLoadMatrixf(r_AccumulatedTranslationMatrix);
            GL.glMultMatrixf(r_AccumulatedRotationMatrix);
            GL.glMultMatrixf(r_AccumulatedScaleMatrix);

            GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, r_AccumulatedTransformationMatrix);
        }

        private float[] getObjectRotatioMatrix()
        {
            float[] rotationMatrixRelativeToObject = new float[TransformationMatrixSize];

            GL.glPushMatrix();
            GL.glLoadIdentity();

            GL.glTranslatef(-Position.X, -Position.Y, -Position.Z);
            GL.glMultMatrixf(r_AccumulatedTransformationMatrix);

            GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, rotationMatrixRelativeToObject);

            GL.glPopMatrix();

            return rotationMatrixRelativeToObject;
        }

        private void calculateDirectionVectors()
        {
            float[] rotationMatrixRelativeToObject = getObjectRotatioMatrix();

            float[] rightVector = { -1, 0, 0, 1 };
            float[] upVector = { 0, 1, 0, 1 };
            float[] forwardVector = { 0, 0, 1, 1 };

            float[] rightVectorTransformed =
                rotateVectorRelativeToOrigin(r_AccumulatedRotationMatrix, rightVector);
            float[] upVectorTransformed =
                rotateVectorRelativeToOrigin(r_AccumulatedRotationMatrix, upVector);
            float[] forwardVectorTransformed =
                rotateVectorRelativeToOrigin(r_AccumulatedRotationMatrix, forwardVector);

            ForwardVector = new Vector3(forwardVectorTransformed).Normalized;
            RightVector = new Vector3(rightVectorTransformed).Normalized;
            UpVector = new Vector3(upVectorTransformed).Normalized;

            Debug.WriteLine("Using rotation matrix: ");
            Debug.WriteLine($"Forward Vector = {ForwardVector}");
            Debug.WriteLine($"Right Vector = {RightVector}");
            Debug.WriteLine($"Up Vector = {UpVector}");
        }

        private float[] rotateVectorRelativeToOrigin(float[] i_RotationMatrixRelativeToObject, float[] i_VectorToTransform)
        {
            const int k_MatrixDimension = 4;
            float[] vectorTransformed = new float[k_MatrixDimension];

            for (int i = 0; i < k_MatrixDimension; i++)
            {
                float rowResult = 0;
                for (int j = 0; j < k_MatrixDimension; j++)
                {
                    rowResult += i_RotationMatrixRelativeToObject[j * k_MatrixDimension + i] * i_VectorToTransform[j];
                }

                vectorTransformed[i] = rowResult;
            }

            return vectorTransformed;
        }

        private float[] getObjectInverseRotationMatrix()
        {
            float[] rotationMatrix = getObjectRotatioMatrix();
            const int k_MatrixDimension = 4;

            for (int i = 0; i < k_MatrixDimension; i++)
            {
                for (int j = i; j < k_MatrixDimension; j++)
                {
                    rotationMatrix[i * k_MatrixDimension + j] = rotationMatrix[j * k_MatrixDimension + i];
                }
            }

            return rotationMatrix;
        }

        private void accumulateTransformation(float[] i_AccumulatedTransformationMatrix)
        {
            GL.glMultMatrixf(i_AccumulatedTransformationMatrix);
            GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, i_AccumulatedTransformationMatrix);
        }
    }
}
