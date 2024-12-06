namespace ComplexLogger
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BaseLogger<T> where T : MelonBase
	{
		/// <summary>
		/// The base log method
		/// </summary>
		/// <param name="message">The formated string to add as the message</param>
		/// <param name="parameters">Any additional params, note that this must be either a single item or an array <c>Write("", [object, object])</c></param>
		public void Write(string message, params object[] parameters)
		{
			Melon<T>.Logger.Msg(message, parameters);
		}

		/// <summary>
		/// The base log method with color
		/// </summary>
		/// <param name="message">The formated string to add as the message</param>
		/// <param name="color">The color of the text to write</param>
		/// <param name="parameters">Any additional params, note that this must be either a single item or an array <c>Write("", [object, object])</c></param>
		protected void Write(string message, ConsoleColor color, params object[] parameters)
		{
			Melon<T>.Logger.Msg(color, message, parameters);
		}
	}
}