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
	/// <typeparam name="T"></typeparam>
	public class ComplexLogger<T> : BaseLogger<T> where T : MelonBase
	{
		/// <summary>
		/// 
		/// </summary>
		public ComplexLogger() { }


		// All Log methods should use the following order:
		// message, level, LogSubType, extra**, parameters
		// message is the log contents
		// level is which level this log is to be displayed at
		// extra** are things like exceptions (think args, kwargs from python).
		// parameters must be last due to it being a params object[]

		/// <summary>
		/// For when you need to log a separator
		/// </summary>
		/// <param name="level"></param>
		/// <param name="LogSubType"></param>
		/// <param name="parameters"></param>
		public void Log(FlaggedLoggingLevel level, LoggingSubType LogSubType, params object[] parameters) 
			=> Log(string.Empty, level, LogSubType, exception: null, parameters);

		/// <summary>
		/// for when you need to log a header
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <param name="LogSubType"></param>
		/// <param name="parameters"></param>
		public void Log(string message, FlaggedLoggingLevel level, LoggingSubType LogSubType, params object[] parameters)
			=> Log(message, level, LogSubType, exception: null, parameters);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <param name="parameters"></param>
		public void Log(string message, FlaggedLoggingLevel level, params object[] parameters) 
			=> Log(message, level, LoggingSubType.Normal, exception: null, parameters);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <param name="exception"></param>
		/// <param name="parameters"></param>
		public void Log(string message, FlaggedLoggingLevel level, System.Exception exception, params object[] parameters) 
			=> Log(message, level, LoggingSubType.Normal, exception: exception, parameters);

		/// <summary>
		/// Allows you to log an entire block, using <see cref="WriteLogBlock(string, string[], System.Drawing.Color)"/>
		/// </summary>
		/// <param name="Title">What you want the block to be titled</param>
		/// <param name="Lines">Each line that you want in that block</param>
		/// <param name="Color">The color of ALL lines in that block (use <see cref="Log(string, string[], List{System.Drawing.Color}, FlaggedLoggingLevel)"/> to be able to color the other lines individually)</param>
		/// <param name="Level">What level you want this block to write at</param>
		public void Log(string Title, string[] Lines, System.Drawing.Color Color, FlaggedLoggingLevel Level)
		{
			if (Main.CurrentLevel.HasFlag(Level))
			{
				WriteLogBlock(Title, Lines, Color);
			}
		}

		/// <summary>
		/// Allows you to log an entire block, using <see cref="WriteLogBlock(string, string[], System.Drawing.Color)"/>
		/// </summary>
		/// <param name="Title">What you want the block to be titled</param>
		/// <param name="Lines">Each line that you want in that block</param>
		/// <param name="Color">The color of ALL lines in that block (use <see cref="Log(string, string[], System.Drawing.Color, FlaggedLoggingLevel)"/> to be able to color the other lines individually)</param>
		/// <param name="Level">What level you want this block to write at</param>
		public void Log(string Title, string[] Lines, List<System.Drawing.Color> Color, FlaggedLoggingLevel Level)
		{
			if (Main.CurrentLevel.HasFlag(Level))
			{
				WriteLogBlock(Title, Lines, Color);
			}
		}

		/// <summary>
		/// Print a log if the current level matches the level given.
		/// </summary>
		/// <param name="message">Formatted string to use in this log</param>
		/// <param name="level">The level of this message (NOT the existing the level)</param>
		/// <param name="exception">The exception, if applicable, to display</param>
		/// <param name="LogSubType">Used to write separators only when the logging level matches the current flags</param>
		/// <param name="parameters">Any additional params</param>
		public void Log(string message, FlaggedLoggingLevel level, LoggingSubType? LogSubType, System.Exception? exception, params object[] parameters)
		{
			if (LogSubType != null && LogSubType != LoggingSubType.Normal)
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
				else if (LogSubType == LoggingSubType.Block)
				{
					throw new ComplexLoggerException("This LogSubType is not supported with the given arguements. Please use Log(string, string[], System.Drawing.Color, FlaggedLoggingLevel) or Log(string, string[], List<System.Drawing.Color>, FlaggedLoggingLevel)");
				}
			}

			List<object> Warning = [];
			List<object> Critical = [];
			Warning.Add(Color.red);
			Warning.AddRange(parameters);
			Critical.Add(Color.red);
			Critical.Add(FontStyle.Bold);
			Critical.AddRange(parameters);

			if (Main.CurrentLevel.HasFlag(level))
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
		#region Separators
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
		private void WriteSeperator(FlaggedLoggingLevel level, params object[] parameters)
		{
			if (Main.CurrentLevel.HasFlag(level)) WriteSeperator(parameters);
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
		private void WriteIntraSeparator(FlaggedLoggingLevel level, string message, params object[] parameters)
		{
			if (Main.CurrentLevel.HasFlag(level)) WriteIntraSeparator(message, parameters);
		}
		#endregion
		#region Exception
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
		#endregion
		#region LogBlock
		/// <summary>
		/// 
		/// </summary>
		/// <param name="FormatedMessage"></param>
		private void WriteLogBlock(string FormatedMessage)
		{
			Write(FormatedMessage);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="lines"></param>
		private void WriteLogBlock(string[] lines)
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
		private void WriteLogBlock(string Title, string[] Lines, System.Drawing.Color color)
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
		private void WriteLogBlock(string Title, string[] lines, List<System.Drawing.Color>? lineColors)
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
		#endregion
	}
}