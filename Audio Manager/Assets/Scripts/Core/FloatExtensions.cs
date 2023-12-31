namespace Core
{
    public static class FloatExtensions
    {
        public static float Decimals(this float f)
        {
            return f - (int)f;
        }
    }
}