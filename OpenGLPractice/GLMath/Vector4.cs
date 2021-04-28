using System;

namespace OpenGLPractice.GLMath
{
    internal struct Vector4
    {
        private const int k_VectorSize = 4;

        /// <summary>
        /// The zero vector (0, 0, 0, 0).
        /// </summary>
        public static Vector4 Zero => new Vector4(0);

        /// <summary>
        /// The x coordinate of this <see cref="Vector4"/> instance.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y coordinate of this <see cref="Vector4"/> instance.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The z coordinate of this <see cref="Vector4"/> instance.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// The w coordinate of this <see cref="Vector4"/> instance.
        /// </summary>
        public float W { get; set; }

        /// <summary>
        /// The length or norm of this <see cref="Vector4"/> instance.
        /// </summary>
        public float Norm => Distance(Zero);

        /// <summary>
        /// This  <see cref="Vector4"/> instance normalized.
        /// </summary>
        public Vector4 Normalized => this / (Norm != 0 ? Norm : 1);

        /// <summary>
        /// This <see cref="Vector4"/> instance represented with an array of floats of size 4.
        /// </summary>
        public float[] ToArray => new float[] { X, Y, Z, W };

        /// <summary>
        /// Converts this <see cref="Vector4"/> instance to <see cref="Vector3"/>.
        /// </summary>
        public Vector3 ToVector3 => new Vector3(X, Y, Z);

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="i_Scalar"></param>
        public Vector4(float i_Scalar)
        {
            X = i_Scalar;
            Y = i_Scalar;
            Z = i_Scalar;
            W = i_Scalar;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="i_X"></param>
        /// <param name="i_Y"></param>
        /// <param name="i_Z"></param>
        /// <param name="i_W"></param>
        public Vector4(float i_X, float i_Y, float i_Z, float i_W)
        {
            X = i_X;
            Y = i_Y;
            Z = i_Z;
            W = i_W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="i_VectorArray"></param>
        /// <exception cref="Exception">Thrown when the length of <see cref="i_VectorArray"/> is less than 4.</exception>
        public Vector4(float[] i_VectorArray)
        {
            if (i_VectorArray.Length >= k_VectorSize)
            {
                X = i_VectorArray[0];
                Y = i_VectorArray[1];
                Z = i_VectorArray[2];
                W = i_VectorArray[3];
            }
            else
            {
                throw new Exception($"Length of array argument must be {k_VectorSize} or more.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="i_VectorToCopy"></param>
        public Vector4(Vector4 i_VectorToCopy)
        {
            X = i_VectorToCopy.X;
            Y = i_VectorToCopy.Y;
            Z = i_VectorToCopy.Z;
            W = i_VectorToCopy.W;
        }

        /// <summary>
        /// Gets value of <see cref="Vector4" /> by index.
        /// </summary>
        /// <param name="i_Row"></param>
        /// <returns>The value of <see cref="Vector4"/> in the specified index position</returns>
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
                    case 2:
                        return Z;
                    case 3:
                        return W;
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
                    case 2:
                        Z = value;
                        break;
                    case 3:
                        W = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException($"{GetType().Name} is of size {k_VectorSize}");
                }
            }
        }

        /// <summary>
        /// Performs a sum of all elements in the specified <see cref="Vector4"/> instance.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A sum of the X, Y, Z and W values.</returns>
        public static float Sum(Vector4 i_Vector)
        {
            return i_Vector.X + i_Vector.Y + i_Vector.Z + i_Vector.W;
        }

        /// <summary>
        /// Determines whether this instance and the specified <see cref="Vector4"/> are equal.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns><see langword="true" /> if the vectors have the same X, Y, Z and W values; otherwise, <see langword="false" />.</returns>
        public static bool operator ==(Vector4 i_FirstVector, Vector4 i_SecondVector)
        {
            return i_FirstVector.Equals(i_SecondVector);
        }

        /// <summary>
        /// Determines whether this instance and the specified <see cref="Vector4"/> are NOT equal.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns><see langword="true" /> if one of the X, Y, Z or W values are different in each of the vectors.; otherwise, <see langword="false" />.</returns>
        public static bool operator !=(Vector4 i_FirstVector, Vector4 i_SecondVector)
        {
            return !(i_FirstVector == i_SecondVector);
        }

        /// <summary>
        /// Performs vector negation between the specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="i_VectorToNegate"></param>
        /// <returns>A new vector which is the negation of the supplied vector.</returns>
        public static Vector4 operator -(Vector4 i_VectorToNegate)
        {
            return (-1) * i_VectorToNegate;
        }

        /// <summary>
        /// Performs vector-vector addition between the specified <see cref="Vector4"/> and the second <see cref="Vector4"/>.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new vector which is the addition of two vectors.</returns>
        public static Vector4 operator +(Vector4 i_FirstVector, Vector4 i_SecondVector)
        {
            return new Vector4(i_FirstVector.X + i_SecondVector.X, i_FirstVector.Y + i_SecondVector.Y,
                i_FirstVector.Z + i_SecondVector.Z, i_FirstVector.W + i_SecondVector.W);
        }

        /// <summary>
        /// Performs vector-vector subtraction between the specified <see cref="Vector4"/> and the second <see cref="Vector4"/>.
        /// </summary>
        /// <param name="i_FirstVector"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new vector which is the subtraction of two vectors.</returns>
        public static Vector4 operator -(Vector4 i_FirstVector, Vector4 i_SecondVector)
        {
            return i_FirstVector + (-i_SecondVector);
        }

        public static Vector4 operator *(Vector4 i_FirstVector, Vector4 i_SecondVector)
        {
            return new Vector4(i_FirstVector.X * i_SecondVector.X, i_FirstVector.Y * i_SecondVector.Y,
                i_FirstVector.Z * i_SecondVector.Z, i_FirstVector.W * i_SecondVector.W);
        }

        /// <summary>
        /// Performs scalar-vector multiplication between the specified <see cref="float"/> and <see cref="Vector4"/>.
        /// </summary>
        /// <param name="i_Scalar"></param>
        /// <param name="i_SecondVector"></param>
        /// <returns>A new vector multiplied by a scalar</returns>
        public static Vector4 operator *(float i_Scalar, Vector4 i_SecondVector)
        {
            return new Vector4(i_Scalar * i_SecondVector.X, i_Scalar * i_SecondVector.Y, i_Scalar * i_SecondVector.Z, i_Scalar * i_SecondVector.W);
        }

        /// <summary>
        /// Performs vector-scalar division between the specified  <see cref="Vector4"/> and <see cref="float"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A new vector divided by a scalar.</returns>
        public static Vector4 operator /(Vector4 i_Vector, float i_Scalar)
        {
            return (1.0f / i_Scalar) * i_Vector;
        }

        /// <summary>
        /// Performs scalar-vector multiplication between the specified <see cref="Vector4"/> and <see cref="float"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <param name="i_Scalar"></param>
        /// <returns>A new vector multiplied by a scalar.</returns>
        public static Vector4 operator *(Vector4 i_Vector, float i_Scalar)
        {
            return i_Scalar * i_Vector;
        }

        /// <summary>
        /// Performs a sum of all elements in this <see cref="Vector4"/> instance.
        /// </summary>
        /// <returns>A sum of the X, Y, Z and W values.</returns>
        public float Sum()
        {
            return X + Y + Z + W;
        }

        /// <summary>
        /// Performs the dot product of this instance and specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>A new vector which is the dot product of this instance and specified <see cref="Vector4"/>.</returns>
        public float DotProduct(Vector4 i_Vector)
        {
            return (this * i_Vector).Sum();
        }

        /// <summary>
        /// Calculates the angle between this instance and specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The angle in degrees between this instance and specified <see cref="Vector4"/>.</returns>
        public float AngleWith(Vector4 i_Vector)
        {
            float dotProduct = DotProduct(i_Vector);

            return dotProduct / (Norm * i_Vector.Norm);
        }

        /// <summary>
        /// Calculates the squared distance between this instance and specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The squared distance between this instance and specified <see cref="Vector4"/>.</returns>
        public float SquaredDistance(Vector4 i_Vector)
        {
            Vector4 vectorDifference = this - i_Vector;
            Vector4 squaredDifferences = vectorDifference * vectorDifference;

            return squaredDifferences.Sum();
        }

        /// <summary>
        /// Calculates the euclidean distance between this instance and specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="i_Vector"></param>
        /// <returns>The euclidean distance between this instance and the specified <see cref="Vector4"/></returns>
        public float Distance(Vector4 i_Vector)
        {
            float squaredDistance = SquaredDistance(i_Vector);

            return squaredDistance == 0 ? 0 : (float)Math.Sqrt(squaredDistance);
        }

        public override bool Equals(object i_ObjectToCompare)
        {
            bool equals = false;

            if (i_ObjectToCompare != null)
            {
                if (i_ObjectToCompare is Vector4)
                {
                    equals = Equals((Vector4)i_ObjectToCompare);
                }
            }

            return equals;
        }

        /// <summary>
        /// Determines whether this instance and the specified <see cref="Vector4"/> are equal.
        /// </summary>
        /// <param name="i_VectorToCompare"></param>
        /// <returns><see langword="true" /> if the vectors have the same X, Y, an Z values; otherwise, <see langword="false" />.</returns>
        public bool Equals(Vector4 i_VectorToCompare)
        {
            return System.Math.Abs(X - i_VectorToCompare.X) < 0.01f && System.Math.Abs(Y - i_VectorToCompare.Y) < 0.01f && System.Math.Abs(Z - i_VectorToCompare.Z) < 0.01f &&
                   System.Math.Abs(W - i_VectorToCompare.W) < 0.01f;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z}, {W})";
        }
    }
}
