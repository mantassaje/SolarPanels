namespace SolarPanels.Models
{
    /// <summary>
    /// Coordinate point defined with float numbers.
    /// </summary>
    public struct FloatPoint
    {
        public float X { get; set; }
        public float Y { get; set; }

        /// <summary>
        /// Create coordinate point defined with float numbers.
        /// </summary>
        public FloatPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X}; {Y}";
        }
    }
}
