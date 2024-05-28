// ---------------------------------------------
// ComplexLogger - by The Illusion
// ---------------------------------------------
// Reusage Rights ------------------------------
// You are free to use this script or portions of it in your own mods, provided you give me credit in your description and maintain this section of comments in any released source code
//
// Warning !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Ensure you change the namespace to whatever namespace your mod uses, so it doesnt conflict with other mods
// ---------------------------------------------

using MelonLoader.Pastel;

namespace ComplexLogger
{
	/// <summary>
	/// 
	/// </summary>
	public class ComplexLogger<T> : BaseLogger<T> where T : MelonBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="levels"></param>
		public ComplexLogger(FlaggedLoggingLevel[]? levels = null)
		{
			Instance = this;

			AddLevel(FlaggedLoggingLevel.None);
			AddLevel(FlaggedLoggingLevel.Error);
			AddLevel(FlaggedLoggingLevel.Critical);
			AddLevel(FlaggedLoggingLevel.Exception);

			if (levels == null) return;

			foreach (var level in levels)
			{
				AddLevel(level);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public static ComplexLogger<T>? Instance { get; set; }

		/// <summary>
		/// The current logging level. Levels are bitwise added or removed.
		/// </summary>
		public static FlaggedLoggingLevel CurrentLevel { get; private set; }

		/// <summary>
		/// Add a flag to the existing list
		/// </summary>
		/// <param name="level">The level to add</param>
		public static void AddLevel(FlaggedLoggingLevel level)
		{
			if (ComplexLogger<T>.CurrentLevel.HasFlag(level))
			{
				Instance?.Log($"Attempting to add already existing level: {level}", FlaggedLoggingLevel.Debug);
				return;
			}

			ComplexLogger<T>.CurrentLevel |= level;

			Instance?.Log($"Added flag {level}", FlaggedLoggingLevel.Debug);
		}

		/// <summary>
		/// Remove a flag from the list
		/// </summary>
		/// <param name="level">Level to remove</param>
		/// <remarks>Attempting to remove "<see cref="FlaggedLoggingLevel.None"/>" or "<see cref="FlaggedLoggingLevel.Exception"/>" is not supported</remarks>
		public static bool RemoveLevel(FlaggedLoggingLevel level)
		{
			if (level == FlaggedLoggingLevel.None)
			{
				Instance?.Log("Attempting to remove \"FlaggedLoggingLevel.None\" is not supported", FlaggedLoggingLevel.Debug);
				return false;
			}
			else if (level == FlaggedLoggingLevel.Error)
			{
				Instance?.Log("Attempting to remove \"FlaggedLoggingLevel.Error\" is not supported", FlaggedLoggingLevel.Debug);
				return false;
			}
			else if (level == FlaggedLoggingLevel.Critical)
			{
				Instance?.Log("Attempting to remove \"FlaggedLoggingLevel.Critical\" is not supported", FlaggedLoggingLevel.Debug);
				return false;
			}
			else if (level == FlaggedLoggingLevel.Exception)
			{
				Instance?.Log("Attempting to remove \"FlaggedLoggingLevel.Exception\" is not supported", FlaggedLoggingLevel.Debug);
				return false;
			}

			ComplexLogger<T>.CurrentLevel &= ~level;

			Instance?.Log($"Removed flag {level}", FlaggedLoggingLevel.Debug);
			return true;
		}

		// All Log methods should use the following order:
		// message, level, extra**, parameters
		// message is the log contents
		// level is which level this log is to be displayed at
		// extra** are things like exceptions (think args, kwargs from python).
		// parameters must be last due to it being a params object[]

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <param name="parameters"></param>
		public void Log(string message, FlaggedLoggingLevel level, params object[] parameters)
		{
			Log(message,level, null, LoggingSubType.Normal, parameters);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <param name="exception"></param>
		/// <param name="parameters"></param>
		public void Log(string message, FlaggedLoggingLevel level, System.Exception exception, params object[] parameters)
		{
			Log(message, level, exception, LoggingSubType.Normal, parameters);
		}

		/// <summary>
		/// Print a log if the current level matches the level given.
		/// </summary>
		/// <param name="message">Formatted string to use in this log</param>
		/// <param name="level">The level of this message (NOT the existing the level)</param>
		/// <param name="exception">The exception, if applicable, to display</param>
		/// <param name="LogSubType">Used to write separators only when the logging level matches the current flags</param>
		/// <param name="parameters">Any additional params</param>
		/// <remarks>
		/// <para>Use <see cref="WriteSeperator(object[])"/> or <see cref="WriteIntraSeparator(string, object[])"/> for seperators without using the flagged level</para> 
		/// </remarks>
		public void Log(string message, FlaggedLoggingLevel level, System.Exception? exception, LoggingSubType LogSubType, params object[] parameters)
		{
			if (LogSubType != LoggingSubType.Normal)
			{
				if (LogSubType == LoggingSubType.Separator)
				{
					WriteSeperator(level);
					return;
				}
				else if (LogSubType == LoggingSubType.IntraSeparator)
				{
					WriteIntraSeparator(level, message);
					return;
				}
				else if (LogSubType == LoggingSubType.uConsole)
				{
					if (exception != null)
					{
						uConsole.Log(exception.Message);
					}
					uConsole.Log(message);
					return;
				}
			}

			List<object> Warning = [];
			List<object> Critical = [];
			Warning.Add(Color.red);
			Warning.AddRange(parameters);
			Critical.Add(Color.red);
			Critical.Add(FontStyle.Bold);
			Critical.AddRange(parameters);

			if (ComplexLogger<T>.CurrentLevel.HasFlag(level))
			{
				switch (level)
				{
					case FlaggedLoggingLevel.Trace:
						Write($"[TRACE] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Debug:
						Write($"[DEBUG] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Verbose:
						Write($"[INFO] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Warning:
						Write($"[WARNING] {message}", Warning);
						break;
					case FlaggedLoggingLevel.Error:
						Write($"[ERROR] {message}", Warning);
						break;
					case FlaggedLoggingLevel.Critical:
						Write($"[CRITICAL] {message}", Critical);
						break;
					case FlaggedLoggingLevel.Exception:
						WriteException(message, exception);
						break;
					default:
						break;
				}
				return;
			}
			return;
		}

		/// <summary>
		/// Prints a seperator
		/// </summary>
		/// <param name="parameters">Any additional params</param>
		private void WriteSeperator(params object[] parameters)
		{
			Write("==============================================================================", parameters);
		}

		/// <summary>
		/// Prints a seperator
		/// </summary>
		/// <param name="parameters">Any additional params</param>
		/// <param name="level">The level of this message (NOT the existing the level)</param>
		public void WriteSeperator(FlaggedLoggingLevel level, params object[] parameters)
		{
			if (ComplexLogger<T>.CurrentLevel.HasFlag(level)) WriteSeperator(parameters);
		}

		/// <summary>
		/// Logs an "Intra Seperator", allowing you to print headers
		/// </summary>
		/// <param name="message">The header name. Should be short</param>
		/// <param name="parameters">Any additional params</param>
		private void WriteIntraSeparator(string message, params object[] parameters)
		{
			Write($"=========================   {message}   =========================", parameters);
		}

		/// <summary>
		/// Logs an "Intra Seperator", allowing you to print headers
		/// </summary>
		/// <param name="message">The header name. Should be short</param>
		/// <param name="level">The level of this message (NOT the existing the level)</param>
		/// <param name="parameters">Any additional params</param>
		public void WriteIntraSeparator(FlaggedLoggingLevel level, string message, params object[] parameters)
		{
			if (ComplexLogger<T>.CurrentLevel.HasFlag(level)) WriteIntraSeparator(message, parameters);
		}

		/// <summary>
		/// Prints a log with <c>[EXCEPTION]</c> at the start.
		/// </summary>
		/// <param name="message">The formated string to add as the message. Displayed before the exception</param>
		/// <param name="exception">The exception thrown</param>
		/// <remarks>
		/// <para>This is done as building the exception otherwise can be tedious</para>
		/// </remarks>
		private void WriteException(string message, System.Exception? exception)
		{
			System.Text.StringBuilder sb = new();

			sb.Append("[EXCEPTION]");
			sb.Append(message);

			if (exception != null) sb.AppendLine(exception.Message);
			else sb.AppendLine("Exception was null");

			Write(sb.ToString());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="FormatedMessage"></param>
		public void WriteLogBlock(string FormatedMessage)
		{
			Write(FormatedMessage);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="lines"></param>
		public void WriteLogBlock(string[] lines)
		{
			System.Text.StringBuilder LogBlock = new();

			foreach (string line in lines)
			{
				LogBlock.AppendLine(line);
			}

			WriteLogBlock(LogBlock.ToString());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Title"></param>
		/// <param name="Lines"></param>
		/// <param name="color"></param>
		public void WriteLogBlock(string Title, string[] Lines, System.Drawing.Color color)
		{
			// ensure to create a list of colors equal to the number of entries in Lines
			List<System.Drawing.Color> LineColors = [.. Lines.Select(s => color)];
			WriteLogBlock(Title, Lines, LineColors);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Title"></param>
		/// <param name="lines"></param>
		/// <param name="lineColors"></param>
		/// <exception cref="ComplexLoggerException"></exception>
		public void WriteLogBlock(string Title, string[] lines, List<System.Drawing.Color>? lineColors)
		{
			// if the lineColors array is not null, check the length to ensure that they are equal
			if (lineColors != null)
			{
				if (lines.Length != lineColors?.Count)
				{
					throw new ComplexLoggerException($"WriteLogBlock({Title}, {lines.Length}, {lineColors?.Count})::Attempting to build a LogBlock requires equal length arrays for both parameters \'lines\' and \'lineColors\'");
				}
			}

			System.Text.StringBuilder LogBlock = new();

			LogBlock.AppendLine(Title);

			// if the lineColors array is not null, append each line with the associated color
			if (lineColors != null)
			{
				for (int i = 0; i < lines.Length; i++)
				{
					LogBlock.AppendLine(lines[i].Pastel(lineColors[i]));
				}
			}
			// if the lineColors array is null, use the default color when appending each line
			else
			{
				for (int i = 0; i < lines.Length; i++)
				{
					LogBlock.AppendLine(lines[i]);
				}
			}

			WriteLogBlock(LogBlock.ToString());
		}
	}
}