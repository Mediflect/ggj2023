using UnityEngine;

namespace Medi
{
    public static class Vector3Extensions
    {
        // Actual extensions

        public static Vector3 WithX(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }

        public static Vector3 WithY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }

        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector3 DirTo(this Vector3 vector, Vector3 other)
        {
            return (other - vector).normalized;
        }

        public static Vector3 Flatten(this Vector3 vector)
        {
            return new Vector3(vector.x, 0f, vector.z);
        }

        public static Vector3 Right(this Vector3 vector, Vector3 up)
        {
            return Vector3.Cross(up, vector);
        }


        // Static helpers

        /// <summary>
        /// Vector3.Lerp with smoothing at the limits.
        /// </summary>
        public static Vector3 SmoothLerp(Vector3 a, Vector3 b, float t)
        {
            return Vector3.Lerp(a, b, Mathf.SmoothStep(0, 1, t));
        }
    }
}