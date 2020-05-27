namespace DGD
{
    /// <summary>
    /// Helper methods for controller input scripts
    /// </summary>
    public static class XRControllerHelpers
    {
        /// <summary>
        /// Returns true if a number is between a range of int nums.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static bool IsWithin(this float value, float minimum, float maximum)
        {
            return value >= minimum && value <= maximum;
        }
    }
}