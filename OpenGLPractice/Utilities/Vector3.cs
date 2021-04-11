using System;

namespace OpenGLPractice.Utilities
{
    /// <summary>
    /// The <see cref="Vector3"/> struct.
    /// Contains basic vector operations.
    /// </summary>
    internal struct Vector3
    {
        /// <summary>
        /// The zero vector (0, 0, 0).
        /// </summary>
        public static Vector3 Zero => new Vector3(0, 0, 0);

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

        /// <summary>
        /// The x coordinate of the vector.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y coordinate of the vector.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The z coordinate of the vector.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// The length also known as norm -> |V|.
        /// </summary>
        public float Norm => Distance(Zero);

        /// <summary>
        /// The vector normalized.
        /// </summary>
        public Vector3 Normalized => this / Norm;

        /// <summary>
        /// The vector represented with an array of floats of size 3.
        /// </summary>
        public float[] ToArray => new float[] { X, Y, Z };

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="i_X"></param>
        /// <param name="i_Y"></param>
        /// <param name="i_Z"></param>
        public Vector3(float i_X, float i_Y, float i_Z)
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
        public Vector3(float[] i_VectorArray)
        {
            if (i_VectorArray.Length >= 3)
            {
                X = i_VectorArray[0];
                Y = i_VectorArray[1];
                Z = i_VectorArray[2];
            }
            else
            {
                throw new Exception("Length of array argument must be 3 or more.");
            }
        }

        /// <summary>
        /// Sums up the X, Y, and Z values up to one value.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A sum of the X, Y, and Z values.</returns>
        public static float Sum(Vector3 i_Vector)
        {
            return i_Vector.X + i_Vector.Y + i_Vector.Z;
        }

        /// <summary>
        /// Determines whether the two vectors are equal.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns><see langword="true" /> if the vectors have the same X, Y, an Z values; otherwise, <see langword="false" />.</returns>
        public static bool operator ==(Vector3 i_FirstVector, Vector3 i_SecondVector)
        {
            return i_FirstVector.Equals(i_SecondVector);
        }

        /// <summary>
        /// Determines whether the two vectors are not equal.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns><see langword="true" /> if one of the X, Y or Z values are different in each of the vectors.; otherwise, <see langword="false" />.</returns>
        public static bool operator !=(Vector3 i_FirstVector, Vector3 i_SecondVector)
        {
            return !(i_FirstVector == i_SecondVector);
        }

        /// <summary>
        /// Performs negation on the supplied vector.
        /// </summary>
        /// <param name="i_VectorToNegate"></param>
        /// <returns>A new vector which is the negation of the supplied vector.</returns>
        public static Vector3 operator -(Vector3 i_VectorToNegate)
        {
            return new Vector3(-i_VectorToNegate.X, -i_VectorToNegate.Y, -i_VectorToNegate.Z);
        }

        /// <summary>
        /// Performs vector addition between two vectors.
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
        /// Performs vector subtraction between two vectors.
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
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="i_Scalar"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new vector multiplied by a scalar</returns>
        public static Vector3 operator *(float i_Scalar, Vector3 i_SecondVector)
        {
            return new Vector3(i_Scalar * i_SecondVector.X, i_Scalar * i_SecondVector.Y, i_Scalar * i_SecondVector.Z);
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A new vector divided by a scalar.</returns>
        public static Vector3 operator /(Vector3 i_Vector, float i_Scalar)
        {
            return (1.0f / i_Scalar) * i_Vector;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A new vector multiplied by a scalar.</returns>
        public static Vector3 operator *(Vector3 i_Vector, float i_Scalar)
        {
            return i_Scalar * i_Vector;
        }

        /// <summary>
        /// Performs the dot product on the calling vector with the supplied vector.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A new vector which is the dot product between the calling vector and the supplied vector.</returns>
        public float DotProduct(Vector3 i_Vector)
        {
            return (X * i_Vector.X) + (Y * i_Vector.Y) + (Z * i_Vector.Z);
        }

        /// <summary>
        /// Performs the cross product between the calling vector and the supplied vector.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A new vector which is the cross product between the calling vector and the supplied vector.</returns>
        public Vector3 CrossProduct(Vector3 i_Vector)
        {
            return new Vector3((Y * i_Vector.Z) - (Z * i_Vector.Y),
                (Z * i_Vector.X) - (X * i_Vector.Z),
                (X * i_Vector.Y) - (Y * i_Vector.X));
        }

        /// <summary>
        /// Calculates the angle between the calling vector and the supplied vector.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The angle between the calling vector and the supplied vector.</returns>
        public float AngleWith(Vector3 i_Vector)
        {
            float dotProduct = DotProduct(i_Vector);

            return dotProduct / (Norm * i_Vector.Norm);
        }

        /// <summary>
        /// Calculates the squared distance between the calling vector and the supplied vector.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The squared distance between the calling vector and the supplied vector.</returns>
        public float SquaredDistance(Vector3 i_Vector)
        {
            Vector3 difference = this - i_Vector;
            Vector3 squaredDifferences = difference * difference;

            return Sum(squaredDifferences);
        }

        /// <summary>
        /// Calculates the euclidean distance between the calling vector and the supplied vector.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The euclidean distance between the calling vector and the supplied vector.</returns>
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
            Vector3 difference = this - i_VectorToCompare;

            return difference.X == 0 && difference.Y == 0 && difference.Z == 0;
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

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }
}