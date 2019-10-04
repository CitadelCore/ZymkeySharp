namespace ZymkeySharp
{
    public struct ZkAccelAxisDataType
    {
        /// <summary>
        /// Axis reading in units of G-force
        /// </summary>
        public double g;

        /// <summary>
        /// The direction of force along the axis which caused a tap event.
        /// </summary>
        public int tapDirection;
    }
}