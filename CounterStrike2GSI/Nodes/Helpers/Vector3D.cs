
using System;
using System.Text.RegularExpressions;

namespace CounterStrike2GSI.Nodes.Helpers
{
    /// <summary>
    /// Struct representing 3D vectors.
    /// </summary>
    public struct Vector3D
    {
        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public float X = 0;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public float Y = 0;

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public float Z = 0;

        private Regex _vector_regex = new Regex(@"([+-]?[0-9]*[.]?[0-9]+), ([+-]?[0-9]*[.]?[0-9]+), ([+-]?[0-9]*[.]?[0-9]+)");

        /// <summary>
        /// Default constructor with given X, Y, and Z coordinates.
        /// </summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Construct Vector3D from a properly formatted string.
        /// </summary>
        /// <param name="str">The string to construct the Vector3D from.</param>
        public Vector3D(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            if (_vector_regex.IsMatch(str))
            {
                var match = _vector_regex.Match(str);

                X = Convert.ToSingle(match.Groups[1].Value);
                Y = Convert.ToSingle(match.Groups[2].Value);
                Z = Convert.ToSingle(match.Groups[3].Value);
            }
        }

        /// <summary>
        /// Equates this Vector3D object to another object.
        /// </summary>
        /// <param name="obj">The other object to compare against.</param>
        /// <returns>True if the two objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            return obj is Vector3D other &&
                   X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
        }

        /// <summary>
        /// Calculates unique hash code for this object.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = 547961354;
            hashCode = hashCode * -578989432 + X.GetHashCode();
            hashCode = hashCode * -578989432 + Y.GetHashCode();
            hashCode = hashCode * -578989432 + Z.GetHashCode();
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[X: {X}, Y: {Y}, Z: {Z}]";
        }
    }
}
