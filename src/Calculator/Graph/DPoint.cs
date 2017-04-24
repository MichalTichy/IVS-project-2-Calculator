namespace Calculator.Graph
{
	/// <summary>
	/// Point representation on XY axis
	/// </summary>
	public struct DPoint
	{
		/// <summary>
		/// x axis
		/// </summary>
		public double X;
		/// <summary>
		/// y axis
		/// </summary>
		public double Y;

		/// <summary>
		/// Constructor representing point on xy axis
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public DPoint(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}
