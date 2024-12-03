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
	/// <summary></summary>
	[System.Flags]
	public enum FlaggedLoggingLevel
	{
		/// <summary></summary>
		[Obsolete("Use Always instead")]
		None			= 0b_0000_0000,
		/// <summary>Use this for debug messages that dont matter 99% of the time</summary>
		Trace			= 0b_0000_0001,
		/// <summary>Your go to level for pretty much everything. If the method is being called on update, please use Verbose instead!</summary>
		Debug			= 0b_0000_0010,
		/// <summary>For those errors that you can use but dont matter much in general debugging. Also required to use this for any and all update methods!</summary>
		Verbose			= 0b_0000_0100,
		/// <summary>Used for when something happens that wont break things but is still something that shouldnt happen</summary>
		Warning			= 0b_0000_1000,
		/// <summary>Like warning, this is used for when something happens that shouldnt, but this one is for when it does break things</summary>
		Error			= 0b_0001_0000,
		/// <summary>This is for when things really break</summary>
		Critical		= 0b_0010_0000,
		/// <summary>This is intended to be used within exception catches, ensure to add the exception instance to your log call</summary>
		Exception		= 0b_0100_0000,
		/// <summary>For when you want to log the message at all times. Please dont use this for startup messages or anything that will print more than once or twice. Useful for console command printed logs</summary>
		Always			= 0b_1000_0000
	}
}
