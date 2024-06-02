namespace ComplexLogger
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public class Settings : JsonModSettings
	{
		internal static Settings Instance { get; } = new();

		[Name("Trace")]
		public bool TRACE = false;

		[Name("Debug")]
		public bool DEBUG = false;

		[Name("Verbose")]
		public bool VERBOSE = false;

		[Name("Warning")]
		public bool WARNING = false;

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

		// this is used to set things when user clicks confirm. If you dont need this ability, dont include this method
		/// <inheritdoc/>
		protected override void OnConfirm()
		{
			base.OnConfirm();
			Instance.UpdateLevels();
		}

		/// <summary>
		/// 
		/// </summary>
		public void UpdateLevels()
		{
			if (TRACE) Main.AddLevel(FlaggedLoggingLevel.Trace);
			else Main.RemoveLevel(FlaggedLoggingLevel.Trace);

			if (DEBUG) Main.AddLevel(FlaggedLoggingLevel.Debug);
			else Main.RemoveLevel(FlaggedLoggingLevel.Debug);

			if (VERBOSE) Main.AddLevel(FlaggedLoggingLevel.Warning);
			else Main.RemoveLevel(FlaggedLoggingLevel.Warning);
		}

		// MUST be static
		// This is used to load the settings
		internal static void OnLoad()
		{
			Instance.AddToModSettings(BuildInfo.GUIName);
			Instance.UpdateLevels();
			Instance.RefreshGUI();
		}
	}
}
