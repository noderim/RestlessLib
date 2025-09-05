using UnityEngine;

namespace RestlessLib.MathR
{
    public static class MathR
    {
        /// <summary>
        /// Remap function remap given value within InMix - InMax, to produce value between OutMin - OutMax lineary.
        /// </summary>
        public static float Remap(float value, float InMin, float InMax, float OutMin, float OutMax)
        {
            // Calculate the ratio of the input value within the input range
            float t = (value - InMin) / (InMax - InMin);


            // Linearly interpolate to the output range
            return OutMin + t * (OutMax - OutMin);
        }
        public static float Remap(float value, Vector2 InMinMax, Vector2 OutMinMax)
        {
            // Calculate the ratio of the input value within the input range
            float t = (value - InMinMax.x) / (InMinMax.y - InMinMax.x);

            // Linearly interpolate to the output range
            return OutMinMax.x + t * (OutMinMax.y - OutMinMax.x);
        }

        /// <summary>
        /// Rotate Vector By some degrees
        /// </summary>
        public static Vector2 RotateVector(Vector2 v, float degrees)
        {
            float theta = degrees * Mathf.Deg2Rad;
            float cosTheta = Mathf.Cos(theta);
            float sinTheta = Mathf.Sin(theta);
            return new Vector2(
                v.x * cosTheta - v.y * sinTheta,
                v.x * sinTheta + v.y * cosTheta
            );
        }

        public static Vector2 SnapToGrid(Vector2 Position, float StepsPerUnit)
        {
            Vector2 pos = Position;

            pos.x = Mathf.Round(pos.x * StepsPerUnit) / StepsPerUnit;
            pos.y = Mathf.Round(pos.y * StepsPerUnit) / StepsPerUnit;

            return pos;
        }
    }
}
