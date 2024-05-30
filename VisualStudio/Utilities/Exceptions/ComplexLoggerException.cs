namespace ComplexLogger.Utilities.Exceptions
{
	/// <summary>
	/// Represents an exception when attempting to use the Complex Logger
	/// </summary>
	[System.Serializable]
	public class ComplexLoggerException : System.Exception
	{
		/// <inheritdoc/>
		public ComplexLoggerException() : base() { }

		/// <inheritdoc/>
		public ComplexLoggerException(string? message) : base(message) { }

		/// <inheritdoc/>
		public ComplexLoggerException(string? paramName, string? message) : base(message) { }

		/// <inheritdoc/>
		public ComplexLoggerException(string? message, System.Exception innerException) : base(message, innerException) { }
	}
}
