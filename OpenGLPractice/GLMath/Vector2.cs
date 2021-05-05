using System;

namespace OpenGLPractice.GLMath
{
    internal struct Vector2
    {
        private const int k_VectorSize = 2;

        /// <summary>
        /// The zero vector (0, 0).
        /// </summary>
        public static Vector2 Zero => new Vector2(0);

        /// <summary>
        /// The right vector (1, 0).
        /// </summary>
        public static Vector2 Right => new Vector2(1, 0);

        /// <summary>
        /// The left vector (-1, 0).
        /// </summary>
        public static Vector2 Left => -Right;

        /// <summary>
        /// The up vector (0, 1).
        /// </summary>
        public static Vector2 Up => new Vector2(0, 1);

        /// <summary>
        /// The down vector (0, -1).
        /// </summary>
        public static Vector2 Down => -Up;

        /// <summary>
        /// The x coordinate of this <see cref="Vector2"/> instance.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y coordinate of this <see cref="Vector2"/> instance.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The length or norm of this <see cref="Vector2"/> instance.
        /// </summary>
        public float Norm => Distance(Zero);

        /// <summary>
        /// This <see cref="Vector2"/> instance normalized.
        /// </summary>
        public Vector2 Normalized => this / (Norm != 0 ? Norm : 1);

        /// <summary>
        /// This <see cref="Vector2"/> instance represented with an array of floats of size 3.
        /// </summary>
        public float[] ToArray => new float[] { X, Y };

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="i_Scalar"></param>
        public Vector2(float i_Scalar)
        {
            X = i_Scalar;
            Y = i_Scalar;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="i_X"></param>
        /// <param name="i_Y"></param>
        public Vector2(float i_X, float i_Y)
        {
            X = i_X;
            Y = i_Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="i_VectorArray"></param>
        /// <exception cref="Exception">Thrown when the length of <see cref="i_VectorArray"/> is less than 2</exception>
        public Vector2(float[] i_VectorArray)
        {
            if (i_VectorArray.Length >= k_VectorSize)
            {
                X = i_VectorArray[0];
                Y = i_VectorArray[1];
            }
            else
            {
                throw new Exception($"Length of array argument must be {k_VectorSize} or more.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="i_VectorToCopy"></param>
        public Vector2(Vector2 i_VectorToCopy)
        {
            X = i_VectorToCopy.X;
            Y = i_VectorToCopy.Y;
        }

        /// <summary>
        /// Gets value of <see cref="Vector2" /> by index.
        /// </summary>
        /// <param name="i_Row"></param>
        /// <returns>The value of <see cref="Vector2"/> in the specified index position</returns>
        public float this[int i_Row]
        {
            get
            {
                switch (i_Row)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    default:
                        throw new IndexOutOfRangeException($"{GetType().Name} is of size {k_VectorSize}");
                }
            }

            set
            {
                switch (i_Row)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException($"{GetType().Name} is of size {k_VectorSize}");
                }
            }
        }

        /// <summary>
        /// Linearly interpolates between two vectors.
        /// </summary>
        /// <param name="i_SourceVector"></param>
        /// <param name="i_DestinationVector"></param>
        /// <param name="i_InterpolationRatio"></param>
        /// <returns>A <see cref="Vector2"/> interpolated value</returns>
        public static Vector2 LinearlyInterpolate(Vector2 i_SourceVector, Vector2 i_DestinationVector, float i_InterpolationRatio)
        {
            return i_SourceVector * (1.0f - i_InterpolationRatio) + i_DestinationVector * i_InterpolationRatio;
        }

        /// <summary>
        /// Performs a sum of all elements in the specified <see cref="Vector2"/> instance.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A sum of the X and Y values.</returns>
        public static float Sum(Vector2 i_Vector)
        {
            return i_Vector.X + i_Vector.Y;
        }

        /// <summary>
        /// Determines whether this instance and the specified <see cref="Vector2"/> are equal.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns><see langword="true" /> if the vectors have the same X and Y values; otherwise, <see langword="false" />.</returns>
        public static bool operator ==(Vector2 i_FirstVector, Vector2 i_SecondVector)
        {
            return i_FirstVector.Equals(i_SecondVector);
        }

        /// <summary>
        /// Determines whether this instance and the specified <see cref="Vector2"/> are NOT equal.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns><see langword="true" /> if one of the X or Y values are different in each of the vectors.; otherwise, <see langword="false" />.</returns>
        public static bool operator !=(Vector2 i_FirstVector, Vector2 i_SecondVector)
        {
            return !(i_FirstVector == i_SecondVector);
        }

        /// <summary>
        /// Performs vector negation between the specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="i_VectorToNegate"></param>
        /// <returns>A new vector which is the negation of the supplied vector.</returns>
        public static Vector2 operator -(Vector2 i_VectorToNegate)
        {
            return (-1) * i_VectorToNegate;
        }

        /// <summary>
        /// Performs vector-vector addition between the specified <see cref="Vector2"/> and the second <see cref="Vector2"/>.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new vector which is the addition of two vectors.</returns>
        public static Vector2 operator +(Vector2 i_FirstVector, Vector2 i_SecondVector)
        {
            return new Vector2(i_FirstVector.X + i_SecondVector.X, i_FirstVector.Y + i_SecondVector.Y);
        }

        /// <summary>
        /// Performs vector-vector subtraction between the specified <see cref="Vector2"/> and the second <see cref="Vector2"/>.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new vector which is the subtraction of two vectors.</returns>
        public static Vector2 operator -(Vector2 i_FirstVector, Vector2 i_SecondVector)
        {
            return i_FirstVector + (-i_SecondVector);
        }

        public static Vector2 operator *(Vector2 i_FirstVector, Vector2 i_SecondVector)
        {
            return new Vector2(i_FirstVector.X * i_SecondVector.X, i_FirstVector.Y * i_SecondVector.Y);
        }

        /// <summary>
        /// Performs scalar-vector multiplication between the specified <see cref="float"/> and <see cref="Vector2"/>.
        /// </summary>
        /// <param name="i_Scalar"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new <see cref="Vector2"/> multiplied by a scalar</returns>
        public static Vector2 operator *(float i_Scalar, Vector2 i_SecondVector)
        {
            return new Vector2(i_Scalar * i_SecondVector.X, i_Scalar * i_SecondVector.Y);
        }

        /// <summary>
        /// Performs vector-scalar division between the specified  <see cref="Vector2"/> and <see cref="float"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A new vector divided by a scalar.</returns>
        public static Vector2 operator /(Vector2 i_Vector, float i_Scalar)
        {
            return (1.0f / i_Scalar) * i_Vector;
        }

        /// <summary>
        /// Performs scalar-vector multiplication between the specified <see cref="Vector2"/> and <see cref="float"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A new vector multiplied by a scalar.</returns>
        public static Vector2 operator *(Vector2 i_Vector, float i_Scalar)
        {
            return i_Scalar * i_Vector;
        }

        /// <summary>
        /// Performs a sum of all elements in this <see cref="Vector2"/> instance.
        /// </summary>
        /// <returns>A sum of the X and Y values.</returns>
        public float Sum()
        {
            return X + Y;
        }

        /// <summary>
        /// Performs the dot product of this instance and specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A new vector which is the dot product of this instance and specified <see cref="Vector2"/>.</returns>
        public float DotProduct(Vector2 i_Vector)
        {
            return (this * i_Vector).Sum();
        }

        /// <summary>
        /// Calculates the angle between this instance and specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The angle in degrees between this instance and specified <see cref="Vector2"/>.</returns>
        public float AngleWith(Vector2 i_Vector)
        {
            float dotProduct = DotProduct(i_Vector);

            return dotProduct / (Norm * i_Vector.Norm);
        }

        /// <summary>
        /// Calculates the squared distance between this instance and specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The squared distance between this instance and specified <see cref="Vector2"/>.</returns>
        public float SquaredDistance(Vector2 i_Vector)
        {
            Vector2 vectorDifference = this - i_Vector;
            Vector2 squaredDifferences = vectorDifference * vectorDifference;

            return squaredDifferences.Sum();
        }

        /// <summary>
        /// Calculates the euclidean distance between this instance and specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The euclidean distance between this instance and the specified <see cref="Vector2"/></returns>
        public float Distance(Vector2 i_Vector)
        {
            float squaredDistance = SquaredDistance(i_Vector);

            return squaredDistance == 0 ? 0 : (float)Math.Sqrt(squaredDistance);
        }

        public override bool Equals(object i_ObjectToCompare)
        {
            bool equals = false;

            if (i_ObjectToCompare != null)
            {
                if (i_ObjectToCompare is Vector2)
                {
                    equals = Equals((Vector2)i_ObjectToCompare);
                }
            }

            return equals;
        }

        /// <summary>
        /// Determines whether this instance and the specified <see cref="Vector2"/> are equal.
        /// </summary>
        /// <param name="i_VectorToCompare"></param>
        /// <returns><see langword="true" /> if the vectors have the same X and Y values; otherwise, <see langword="false" />.</returns>
        public bool Equals(Vector2 i_VectorToCompare)
        {
            return System.Math.Abs(X - i_VectorToCompare.X) < 0.01f && System.Math.Abs(Y - i_VectorToCompare.Y) < 0.01f;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();

                return hashCode;
            }
        }

        public Vector2 Clone()
        {
            return new Vector2(X, Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
