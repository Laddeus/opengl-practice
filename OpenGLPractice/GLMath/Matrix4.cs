using System;
using System.Text;

namespace OpenGLPractice.GLMath
{
    internal struct Matrix4
    {
        private const int k_NumberOfColumns = 4;

        /// <summary>
        /// The Identity matrix.
        /// <para>(1, 0, 0, 0)</para>
        /// <para>(0, 1, 0, 0)</para>
        /// <para>(0, 0, 1, 0)</para>
        /// <para>(0, 0, 0, 1)</para>
        /// </summary>
        public static Matrix4 Identity => new Matrix4(1.0f);

        private readonly Vector4[] r_MatrixColumns;

        /// <summary>
        /// Gets the transpose of this <see cref="Matrix4"/> instance.
        /// </summary>
        public Matrix4 Transpose => transpose();

        /// <summary>
        /// Gets the <see cref="float"/> array representation of this <see cref="Matrix4"/> instance.
        /// </summary>
        public float[] ToArray => toArray();

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4" /> struct.
        /// </summary>
        /// <param name="i_Scalar"></param>
        public Matrix4(float i_Scalar)
        {
            r_MatrixColumns = new Vector4[]
            {
                    new Vector4(i_Scalar, 0, 0, 0),
                    new Vector4(0, i_Scalar, 0, 0),
                    new Vector4(0, 0, i_Scalar, 0),
                    new Vector4(0, 0, 0, i_Scalar),
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4" /> struct.
        /// </summary>
        /// <param name="i_MatrixArray"></param>
        public Matrix4(float[] i_MatrixArray) : this(0)
        {
            if (i_MatrixArray.Length < k_NumberOfColumns * k_NumberOfColumns)
            {
                throw new ArgumentOutOfRangeException(
                    $"{GetType().Name} requires a matrix array of size {k_NumberOfColumns * k_NumberOfColumns} at least");
            }

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                for (int j = 0; j < k_NumberOfColumns; j++)
                {
                    r_MatrixColumns[i][j] = i_MatrixArray[(j * k_NumberOfColumns) + i];
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4" /> struct.
        /// </summary>
        /// <param name="i_Columns"></param>
        public Matrix4(Vector4[] i_Columns) : this(0)
        {
            if (i_Columns.Length < k_NumberOfColumns)
            {
                throw new ArgumentOutOfRangeException(
                    $"{GetType().Name} requires column array of size {k_NumberOfColumns}");
            }

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                this[i] = i_Columns[i];
            }
        }

        /// <summary>
        /// Gets a column by index from this <see cref="Matrix4"/> instance.
        /// </summary>
        /// <param name="i_Column"></param>
        /// <returns>A <see cref="Vector4"/></returns>
        public Vector4 this[int i_Column]
        {
            get
            {
                if (i_Column >= k_NumberOfColumns)
                {
                    throw new IndexOutOfRangeException($"{GetType().Name} has {k_NumberOfColumns} columns");
                }

                return new Vector4(r_MatrixColumns[i_Column]);
            }

            set
            {
                if (i_Column >= k_NumberOfColumns)
                {
                    throw new IndexOutOfRangeException($"{GetType().Name} has {k_NumberOfColumns} columns");
                }

                r_MatrixColumns[i_Column] = value;
            }
        }

        /// <summary>
        /// Performs matrix multiplication between this <see cref="Matrix4"/> instance and the specified <see cref="Matrix4"/> 
        /// </summary>
        /// <param name="i_FirstMatrix"></param>
        /// <param name="i_SecondMatrix"></param>
        /// <returns>A <see cref="Matrix4"/> which represents the multiplication of <paramref name="i_FirstMatrix"/> with <paramref name="i_SecondMatrix"/></returns>
        public static Matrix4 operator *(Matrix4 i_FirstMatrix, Matrix4 i_SecondMatrix)
        {
            Matrix4 multiplicationMatrixResult = new Matrix4(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                Vector4 newColumn = new Vector4(0);
                for (int j = 0; j < k_NumberOfColumns; j++)
                {
                    Vector4 row = i_FirstMatrix.GetRow(i);
                    Vector4 column = i_SecondMatrix.GetColumn(j);
                    newColumn[j] = i_FirstMatrix.GetColumn(i).DotProduct(i_SecondMatrix.GetRow(j));
                }

                multiplicationMatrixResult[i] = newColumn;
            }

            return multiplicationMatrixResult;
        }

        /// <summary>
        /// Performs matrix-vector multiplication between this <see cref="Matrix4"/> instance and the specified <see cref="Vector4"/> 
        /// </summary>
        /// <param name="i_Matrix"></param>
        /// <param name="i_Vector"></param>
        /// <returns>A <see cref="Vector4"/> which represents the multiplication of <paramref name="i_Matrix"/> with <paramref name="i_Vector"/></returns>
        public static Vector4 operator *(Matrix4 i_Matrix, Vector4 i_Vector)
        {
            Vector4 matrixMultiplicationVectorResult = new Vector4(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                matrixMultiplicationVectorResult[i] = i_Matrix.GetColumn(i).DotProduct(i_Vector);
            }

            return matrixMultiplicationVectorResult;
        }

        /// <summary>
        /// Performs matrix-scalar multiplication between this <see cref="Matrix4"/> instance and the specified <see cref="float"/> 
        /// </summary>
        /// <param name="i_Matrix"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A <see cref="Matrix4"/> which represents the multiplication of <paramref name="i_Matrix"/> with <paramref name="i_Scalar"/></returns>
        public static Matrix4 operator *(Matrix4 i_Matrix, float i_Scalar)
        {
            Matrix4 scalarMultiplicationResult = new Matrix4(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                scalarMultiplicationResult[i] = i_Matrix[i] * i_Scalar;
            }

            return scalarMultiplicationResult;
        }

        /// <summary>
        /// Gets a row by index from this <see cref="Matrix4"/> instance.
        /// </summary>
        /// <param name="i_RowIndex"></param>
        /// <returns>A <see cref="Vector4"/></returns>
        public Vector4 GetRow(int i_RowIndex)
        {
            Vector4 row = new Vector4(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                row[i] = this[i][i_RowIndex];
            }

            return row;
        }

        /// <summary>
        /// Gets a column by index from this <see cref="Matrix4"/> instance.
        /// </summary>
        /// <param name="i_ColumnIndex"></param>
        /// <returns>A <see cref="Vector4"/></returns>
        public Vector4 GetColumn(int i_ColumnIndex)
        {
            return this[i_ColumnIndex];
        }

        /// <summary>
        /// Gets the transpose of this <see cref="Matrix4"/> instance.
        /// </summary>
        /// <returns>A <see cref="Matrix4"/> which is the transpose of this instance</returns>
        private Matrix4 transpose()
        {
            Matrix4 transposedMatrix = new Matrix4(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                transposedMatrix[i] = GetRow(i);
            }

            return transposedMatrix;
        }

        /// <summary>
        /// Gets the <see cref="float"/> array representation of this <see cref="Matrix4"/> instance.
        /// </summary>
        /// <returns>A <see cref="float"/> array</returns>
        private float[] toArray()
        {
            float[] matrixArray = new float[k_NumberOfColumns * k_NumberOfColumns];

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                for (int j = 0; j < k_NumberOfColumns; j++)
                {
                    matrixArray[(i * k_NumberOfColumns) + j] = this[j][i];
                }
            }

            return matrixArray;
        }

        public override string ToString()
        {
            StringBuilder matrixString = new StringBuilder();

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                matrixString.AppendLine(GetRow(i).ToString());
            }

            return matrixString.ToString();
        }
    }
}