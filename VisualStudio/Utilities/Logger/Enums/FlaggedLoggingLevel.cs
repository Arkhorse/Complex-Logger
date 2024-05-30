// ---------------------------------------------
// FlaggedLoggingLevel - by The Illusion
// ---------------------------------------------
// Reusage Rights ------------------------------
// You are free to use this script or portions of it in your own mods, provided you give me credit in your description and maintain this section of comments in any released source code
//
// Warning !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Ensure you change the namespace to whatever namespace your mod uses, so it doesnt conflict with other mods
// ---------------------------------------------

namespace ComplexLogger
{
	/// <summary>
	/// 
	/// </summary>
	[System.Flags]
	public enum FlaggedLoggingLevel
	{
		/// <summary>
		/// 
		/// </summary>
		None 		= 0b_0000_0000,
		/// <summary>
		/// 
		/// </summary>
		Trace 		= 0b_0000_0001,
		/// <summary>
		/// 
		/// </summary>
		Debug 		= 0b_0000_0010,
		/// <summary>
		/// 
		/// </summary>
		Verbose 	= 0b_0000_0100,
		/// <summary>
		/// 
		/// </summary>
		Warning 	= 0b_0000_1000,
		/// <summary>
		/// 
		/// </summary>
		Error 		= 0b_0001_0000,
		/// <summary>
		/// 
		/// </summary>
		Critical 	= 0b_0010_0000,
		/// <summary>
		/// 
		/// </summary>
		Exception	= 0b_0100_0000
	}
}
