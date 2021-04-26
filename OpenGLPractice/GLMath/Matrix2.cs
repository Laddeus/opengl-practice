using System;
using System.Drawing.Drawing2D;
using System.Text;

namespace OpenGLPractice.GLMath
{
    internal struct Matrix2
    {
        private const int k_NumberOfColumns = 2;

        /// <summary>
        /// The Identity matrix.
        /// <para>(1, 0)</para>
        /// <para>(0, 1)</para>
        /// </summary>
        public static Matrix2 Identity => new Matrix2(1.0f);

        private readonly Vector2[] r_MatrixColumns;

        /// <summary>
        /// Gets the transpose of this <see cref="Matrix2"/> instance.
        /// </summary>
        public Matrix2 Transpose => transpose();

        public float Determinant => calculateDeterminant();

        public Matrix2 Inverse => inverse();

        public bool IsInvertible => Determinant != 0;

        /// <summary>
        /// Gets the <see cref="float"/> array representation of this <see cref="Matrix2"/> instance.
        /// </summary>
        public float[] ToArray => toArray();

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2" /> struct.
        /// </summary>
        /// <param name="i_Scalar"></param>
        public Matrix2(float i_Scalar)
        {
            r_MatrixColumns = new Vector2[]
            {
                    new Vector2(i_Scalar, 0),
                    new Vector2(0, i_Scalar),
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2" /> struct.
        /// </summary>
        /// <param name="i_MatrixArray"></param>
        public Matrix2(float[] i_MatrixArray) : this(0)
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
        /// Initializes a new instance of the <see cref="Matrix2" /> struct.
        /// </summary>
        /// <param name="i_Columns"></param>
        public Matrix2(Vector2[] i_Columns) : this(0)
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
        /// Gets a column by index from this <see cref="Matrix2"/> instance.
        /// </summary>
        /// <param name="i_Column"></param>
        /// <returns>A <see cref="Vector2"/></returns>
        public Vector2 this[int i_Column]
        {
            get
            {
                if (i_Column >= k_NumberOfColumns)
                {
                    throw new IndexOutOfRangeException($"{GetType().Name} has {k_NumberOfColumns} columns");
                }

                return new Vector2(r_MatrixColumns[i_Column]);
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
        /// Performs matrix multiplication between this <see cref="Matrix2"/> instance and the specified <see cref="Matrix2"/> 
        /// </summary>
        /// <param name="i_FirstMatrix"></param>
        /// <param name="i_SecondMatrix"></param>
        /// <returns>A <see cref="Matrix2"/> which represents the multiplication of <paramref name="i_FirstMatrix"/> with <paramref name="i_SecondMatrix"/></returns>
        public static Matrix2 operator *(Matrix2 i_FirstMatrix, Matrix2 i_SecondMatrix)
        {
            Matrix2 multiplicationMatrixResult = new Matrix2(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                Vector2 newColumn = new Vector2(0);
                for (int j = 0; j < k_NumberOfColumns; j++)
                {
                    Vector2 row = i_FirstMatrix.GetRow(i);
                    Vector2 column = i_SecondMatrix.GetColumn(j);
                    newColumn[j] = i_FirstMatrix.GetColumn(i).DotProduct(i_SecondMatrix.GetRow(j));
                }

                multiplicationMatrixResult[i] = newColumn;
            }

            return multiplicationMatrixResult;
        }

        /// <summary>
        /// Performs matrix-vector multiplication between this <see cref="Matrix2"/> instance and the specified <see cref="Vector2"/> 
        /// </summary>
        /// <param name="i_Matrix"></param>
        /// <param name="i_Vector"></param>
        /// <returns>A <see cref="Vector2"/> which represents the multiplication of <paramref name="i_Matrix"/> with <paramref name="i_Vector"/></returns>
        public static Vector2 operator *(Matrix2 i_Matrix, Vector2 i_Vector)
        {
            Vector2 matrixMultiplicationVectorResult = new Vector2(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                matrixMultiplicationVectorResult[i] = i_Matrix.GetColumn(i).DotProduct(i_Vector);
            }

            return matrixMultiplicationVectorResult;
        }

        /// <summary>
        /// Performs matrix-scalar multiplication between this <see cref="Matrix2"/> instance and the specified <see cref="float"/> 
        /// </summary>
        /// <param name="i_Matrix"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A <see cref="Matrix2"/> which represents the multiplication of <paramref name="i_Matrix"/> with <paramref name="i_Scalar"/></returns>
        public static Matrix2 operator *(Matrix2 i_Matrix, float i_Scalar)
        {
            Matrix2 scalarMultiplicationResult = new Matrix2(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                scalarMultiplicationResult[i] = i_Matrix[i] * i_Scalar;
            }

            return scalarMultiplicationResult;
        }

        /// <summary>
        /// Gets a row by index from this <see cref="Matrix2"/> instance.
        /// </summary>
        /// <param name="i_RowIndex"></param>
        /// <returns>A <see cref="Vector2"/></returns>
        public Vector2 GetRow(int i_RowIndex)
        {
            Vector2 row = new Vector2(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                row[i] = this[i][i_RowIndex];
            }

            return row;
        }

        /// <summary>
        /// Gets a column by index from this <see cref="Matrix2"/> instance.
        /// </summary>
        /// <param name="i_ColumnIndex"></param>
        /// <returns>A <see cref="Vector2"/></returns>
        public Vector2 GetColumn(int i_ColumnIndex)
        {
            return this[i_ColumnIndex];
        }

        /// <summary>
        /// Gets the transpose of this <see cref="Matrix2"/> instance.
        /// </summary>
        /// <returns>A <see cref="Matrix2"/> which is the transpose of this instance</returns>
        private Matrix2 transpose()
        {
            Matrix2 transposedMatrix = new Matrix2(0);

            for (int i = 0; i < k_NumberOfColumns; i++)
            {
                transposedMatrix[i] = GetRow(i);
            }

            return transposedMatrix;
        }

        private float calculateDeterminant()
        {
            return this[0][0] * this[1][1] - this[0][1] * this[1][0];
        }

        private Matrix2 inverse()
        {
            Matrix2 inverseMatrix = this;

            if (IsInvertible)
            {
                Matrix2 adjunctMatrix = new Matrix2(new Vector2[]
                {
                    new Vector2(this[1][1], -this[0][1]),
                    new Vector2(-this[1][0], this[0][0])
                });

                inverseMatrix = adjunctMatrix * (1.0f / Determinant);
            }

            return inverseMatrix;
        }

        /// <summary>
        /// Gets the <see cref="float"/> array representation of this <see cref="Matrix2"/> instance.
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