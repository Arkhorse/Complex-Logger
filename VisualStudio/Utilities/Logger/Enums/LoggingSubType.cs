namespace ComplexLogger.Utilities.Logger.Enums
{
    /// <summary>
    /// Simple enum to handle the different types of logging
    /// </summary>
    public enum LoggingSubType
    {
		/// <summary>
		/// General use. This is used by default
		/// </summary>
		Normal,
		/// <summary>
		/// To print a separator
		/// </summary>
		Separator,
		/// <summary>
		/// To print a header
		/// </summary>
		IntraSeparator,
		/// <summary>
		/// Print to the Unity Console
		/// </summary>
		uConsole,
		/// <summary>
		/// Print a block to the log NOT IMPLEMENTED AT THIS TIME (Writing this is a bit complex), see the class for the current syntax
		/// </summary>
		Block
	}
}
