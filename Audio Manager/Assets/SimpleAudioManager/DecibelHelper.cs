using UnityEngine;

namespace SimpleAudioManager
{
    public static class DecibelHelper
    {
        public static float LinearToDecibel(float value)
        {
            if (value != 0)
                return Mathf.Log10(value) * 20f;

            return -80.0f;
        }

        public static float DecibelToLinear(float value)
        {
            return Mathf.Pow(10f, value / 20f);
        }
    }
}