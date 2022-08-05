namespace LightsOutConsoleAppV2
{
    class Grid
    {
        /// <summary>
        /// Number of rows
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// Number of columns
        /// </summary>
        public int Columns { get; set; }
        /// <summary>
        /// The layout of the grid
        /// </summary>
        public int[,] Layout { get; set; }
        /// <summary>
        /// Score for the grid
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// True if the grid is a winner
        /// </summary>
        public bool Winner { get; set; }
    }
}
