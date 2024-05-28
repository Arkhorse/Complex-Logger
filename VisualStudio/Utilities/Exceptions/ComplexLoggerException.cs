namespace ComplexLogger.Utilities.Exceptions
{
	/// <inheritdoc/>
	[System.Serializable]
	public class ComplexLoggerException : System.Exception
	{
		/// <inheritdoc/>
		public ComplexLoggerException() : base() { }

		/// <inheritdoc/>
		public ComplexLoggerException(string message) : base(message) { }

		/// <inheritdoc/>
		public ComplexLoggerException(string message, System.Exception innerException) : base(message, innerException) { }
	}
}
