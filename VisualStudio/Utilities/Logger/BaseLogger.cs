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
	}
}