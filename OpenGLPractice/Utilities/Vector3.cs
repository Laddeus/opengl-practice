using System;

namespace OpenGLPractice.Utilities
{
    /// <summary>
    /// The <see cref="Vector3"/> struct.
    /// Contains basic vector operations.
    /// </summary>
    internal struct Vector3
    {
        private const int k_VectorSize = 3;

        /// <summary>
        /// The zero vector (0, 0, 0).
        /// </summary>
        public static Vector3 Zero => new Vector3(0);

        /// <summary>
        /// The right vector (1, 0, 0).
        /// </summary>
        public static Vector3 Right => new Vector3(1, 0, 0);

        /// <summary>
        /// The left vector (-1, 0, 0).
        /// </summary>
        public static Vector3 Left => -Right;

        /// <summary>
        /// The up vector (0, 1, 0).
        /// </summary>
        public static Vector3 Up => new Vector3(0, 1, 0);

        /// <summary>
        /// The down vector (0, -1, 0).
        /// </summary>
        public static Vector3 Down => -Up;

        /// <summary>
        /// The forward vector (0, 0, 1).
        /// </summary>
        public static Vector3 Forward => new Vector3(0, 0, 1);

        /// <summary>
        /// The backward vector (0, 0, -1).
        /// </summary>
        public static Vector3 Backward => -Forward;

        private readonly float[] r_VectorValues;

        /// <summary>
        /// The x coordinate of this <see cref="Vector3"/> instance.
        /// </summary>
        public float X
        {
            get => r_VectorValues[0];
            set => r_VectorValues[0] = value;
        }

        /// <summary>
        /// The y coordinate of this <see cref="Vector3"/> instance.
        /// </summary>
        public float Y
        {
            get => r_VectorValues[1];
            set => r_VectorValues[1] = value;
        }

        /// <summary>
        /// The z coordinate of this <see cref="Vector3"/> instance.
        /// </summary>
        public float Z
        {
            get => r_VectorValues[2];
            set => r_VectorValues[2] = value;
        }

        /// <summary>
        /// The length or norm of this <see cref="Vector3"/> instance.
        /// </summary>
        public float Norm => Distance(Zero);

        /// <summary>
        /// This <see cref="Vector3"/> instance normalized.
        /// </summary>
        public Vector3 Normalized => this / Norm;

        /// <summary>
        /// This <see cref="Vector3"/> instance represented with an array of floats of size 3.
        /// </summary>
        public float[] ToArray => new float[] { X, Y, Z };

        /// <summary>
        /// Converts this <see cref="Vector3"/> instance to <see cref="Vector2"/>.
        /// </summary>
        public Vector2 ToVector2 => new Vector2(X, Y);

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="i_Scalar"></param>
        public Vector3(float i_Scalar)
        {
            r_VectorValues = new float[] { i_Scalar, i_Scalar, i_Scalar };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="i_X"></param>
        /// <param name="i_Y"></param>
        /// <param name="i_Z"></param>
        public Vector3(float i_X, float i_Y, float i_Z) : this(0)
        {
            X = i_X;
            Y = i_Y;
            Z = i_Z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="i_VectorArray"></param>
        /// <exception cref="Exception">Thrown when the length of <see cref="i_VectorArray"/> is less than 3</exception>
        public Vector3(float[] i_VectorArray) : this(0)
        {
            if (i_VectorArray.Length >= k_VectorSize)
            {
                X = i_VectorArray[0];
                Y = i_VectorArray[1];
                Z = i_VectorArray[2];
            }
            else
            {
                throw new Exception($"Length of array argument must be {k_VectorSize} or more.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="i_VectorToCopy"></param>
        public Vector3(Vector3 i_VectorToCopy) : this(0)
        {
            X = i_VectorToCopy.X;
            Y = i_VectorToCopy.Y;
            Z = i_VectorToCopy.Z;
        }

        /// <summary>
        /// Gets value of <see cref="Vector3" /> by index.
        /// </summary>
        /// <param name="i_Row"></param>
        /// <returns>The value of <see cref="Vector3"/> in the specified index position</returns>
        public float this[int i_Row]
        {
            get
            {
                if (i_Row >= k_VectorSize)
                {
                    throw new IndexOutOfRangeException($"{GetType().Name} is of size {k_VectorSize}");
                }

                return r_VectorValues[i_Row];
            }

            set
            {
                if (i_Row >= k_VectorSize)
                {
                    throw new IndexOutOfRangeException($"{GetType().Name} is of size {k_VectorSize}");
                }

                r_VectorValues[i_Row] = value;
            }
        }

        /// <summary>
        /// Performs a sum of all elements in the specified <see cref="Vector3"/> instance.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A sum of the X, Y, and Z values.</returns>
        public static float Sum(Vector3 i_Vector)
        {
            return i_Vector.X + i_Vector.Y + i_Vector.Z;
        }

        /// <summary>
        /// Determines whether this instance and the specified <see cref="Vector3"/> are equal.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns><see langword="true" /> if the vectors have the same X, Y, an Z values; otherwise, <see langword="false" />.</returns>
        public static bool operator ==(Vector3 i_FirstVector, Vector3 i_SecondVector)
        {
            return i_FirstVector.Equals(i_SecondVector);
        }

        /// <summary>
        /// Determines whether this instance and the specified <see cref="Vector3"/> are NOT equal.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns><see langword="true" /> if one of the X, Y or Z values are different in each of the vectors.; otherwise, <see langword="false" />.</returns>
        public static bool operator !=(Vector3 i_FirstVector, Vector3 i_SecondVector)
        {
            return !(i_FirstVector == i_SecondVector);
        }

        /// <summary>
        /// Performs vector negation between the specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="i_VectorToNegate"></param>
        /// <returns>A new vector which is the negation of the supplied vector.</returns>
        public static Vector3 operator -(Vector3 i_VectorToNegate)
        {
            return (-1) * i_VectorToNegate;
        }

        /// <summary>
        /// Performs vector-vector addition between the specified <see cref="Vector3"/> and the second <see cref="Vector3"/>.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new vector which is the addition of two vectors.</returns>
        public static Vector3 operator +(Vector3 i_FirstVector, Vector3 i_SecondVector)
        {
            return new Vector3(i_FirstVector.X + i_SecondVector.X, i_FirstVector.Y + i_SecondVector.Y,
                i_FirstVector.Z + i_SecondVector.Z);
        }

        /// <summary>
        /// Performs vector-vector subtraction between the specified <see cref="Vector3"/> and the second <see cref="Vector3"/>.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new vector which is the subtraction of two vectors.</returns>
        public static Vector3 operator -(Vector3 i_FirstVector, Vector3 i_SecondVector)
        {
            return i_FirstVector + (-i_SecondVector);
        }

        public static Vector3 operator *(Vector3 i_FirstVector, Vector3 i_SecondVector)
        {
            return new Vector3(i_FirstVector.X * i_SecondVector.X, i_FirstVector.Y * i_SecondVector.Y, i_FirstVector.Z * i_SecondVector.Z);
        }

        /// <summary>
        /// Performs scalar-vector multiplication between the specified <see cref="float"/> and <see cref="Vector3"/>.
        /// </summary>
        /// <param name="i_Scalar"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new vector multiplied by a scalar</returns>
        public static Vector3 operator *(float i_Scalar, Vector3 i_SecondVector)
        {
            return new Vector3(i_Scalar * i_SecondVector.X, i_Scalar * i_SecondVector.Y, i_Scalar * i_SecondVector.Z);
        }

        /// <summary>
        /// Performs vector-scalar division between the specified  <see cref="Vector3"/> and <see cref="float"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A new vector divided by a scalar.</returns>
        public static Vector3 operator /(Vector3 i_Vector, float i_Scalar)
        {
            return (1.0f / i_Scalar) * i_Vector;
        }

        /// <summary>
        /// Performs scalar-vector multiplication between the specified <see cref="Vector3"/> and <see cref="float"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A new vector multiplied by a scalar.</returns>
        public static Vector3 operator *(Vector3 i_Vector, float i_Scalar)
        {
            return i_Scalar * i_Vector;
        }

        /// <summary>
        /// Performs a sum of all elements in this <see cref="Vector3"/> instance.
        /// </summary>
        /// <returns>A sum of the X, Y, and Z values.</returns>
        public float Sum()
        {
            return X + Y + Z;
        }

        /// <summary>
        /// Performs the dot product of this instance and specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A new vector which is the dot product of this instance and specified <see cref="Vector3"/>.</returns>
        public float DotProduct(Vector3 i_Vector)
        {
            return (this * i_Vector).Sum();
        }

        /// <summary>
        /// Performs the cross product of this instance and specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A new vector which is the cross product of this instance and specified <see cref="Vector3"/>.</returns>
        public Vector3 CrossProduct(Vector3 i_Vector)
        {
            return new Vector3((Y * i_Vector.Z) - (Z * i_Vector.Y),
                (Z * i_Vector.X) - (X * i_Vector.Z),
                (X * i_Vector.Y) - (Y * i_Vector.X));
        }

        /// <summary>
        /// Calculates the angle between this instance and specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The angle in degrees between this instance and specified <see cref="Vector3"/>.</returns>
        public float AngleWith(Vector3 i_Vector)
        {
            float dotProduct = DotProduct(i_Vector);

            return dotProduct / (Norm * i_Vector.Norm);
        }

        /// <summary>
        /// Calculates the squared distance between this instance and specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The squared distance between this instance and specified <see cref="Vector3"/>.</returns>
        public float SquaredDistance(Vector3 i_Vector)
        {
            Vector3 vectorDifference = this - i_Vector;
            Vector3 squaredDifferences = vectorDifference * vectorDifference;

            return squaredDifferences.Sum();
        }

        /// <summary>
        /// Calculates the euclidean distance between this instance and specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The euclidean distance between this instance and the specified <see cref="Vector3"/></returns>
        public float Distance(Vector3 i_Vector)
        {
            float squaredDistance = SquaredDistance(i_Vector);

            return (float)Math.Sqrt(squaredDistance);
        }

        public override bool Equals(object i_ObjectToCompare)
        {
            bool equals = false;

            if (i_ObjectToCompare != null)
            {
                if (i_ObjectToCompare is Vector3)
                {
                    equals = Equals((Vector3)i_ObjectToCompare);
                }
            }

            return equals;
        }

        /// <summary>
        /// Determines whether this instance and the specified <see cref="Vector3"/> are equal.
        /// </summary>
        /// <param name="i_VectorToCompare"></param>
        /// <returns><see langword="true" /> if the vectors have the same X, Y, an Z values; otherwise, <see langword="false" />.</returns>
        public bool Equals(Vector3 i_VectorToCompare)
        {
            return Math.Abs(X - i_VectorToCompare.X) < 0.01f && Math.Abs(Y - i_VectorToCompare.Y) < 0.01f &&
                   Math.Abs(Z - i_VectorToCompare.Z) < 0.01f;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();

                return hashCode;
            }
        }

        public Vector3 Clone()
        {
            return new Vector3(X, Y, Z);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}