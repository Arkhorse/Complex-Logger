#region System Directives
global using System;
global using System.Text;
global using System.Text.RegularExpressions;
#endregion
#region Il2Cpp Directives
global using Il2CppInterop.Runtime;
global using Il2CppTLD.Gear;
#endregion
#region Unity Directives
global using UnityEngine.AddressableAssets;
#endregion
#region Mod Directives
global using ComplexLogger.Utilities;
global using ComplexLogger.Utilities.Exceptions;
global using ComplexLogger.Utilities.JSON;
#endregion

namespace ComplexLogger
{
	/// <inheritdoc/>
	public class Main : MelonMod
	{
		/// <summary>
		/// The current logging level. Levels are bitwise added or removed.
		/// </summary>
		public static FlaggedLoggingLevel CurrentLevel = FlaggedLoggingLevel.Error | FlaggedLoggingLevel.Critical | FlaggedLoggingLevel.Exception | FlaggedLoggingLevel.Always;
		/// <summary>
		/// 
		/// </summary>
		internal static ComplexLogger<Main> Logger { get; } = new();

		// Perhaps change these to extensions
		/// <summary>
		/// Add a flag to the existing list
		/// </summary>
		/// <param name="level">The level to add</param>
		public static void AddLevel(FlaggedLoggingLevel level)
		{
			if (CurrentLevel.HasFlag(level))
			{
				Logger.Log($"Level has already been added: {level}", FlaggedLoggingLevel.Verbose);
				return;
			}

			CurrentLevel |= level;
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
				Logger.Log($"Removing \"FlaggedLoggingLevel.None\" is not supported", FlaggedLoggingLevel.Verbose);
				return false;
			}
			else if (level == FlaggedLoggingLevel.Error)
			{
				Logger.Log($"Removing \"FlaggedLoggingLevel.Error\" is not supported", FlaggedLoggingLevel.Verbose);
				return false;
			}
			else if (level == FlaggedLoggingLevel.Critical)
			{
				Logger.Log($"Removing \"FlaggedLoggingLevel.Critical\" is not supported", FlaggedLoggingLevel.Verbose);
				return false;
			}
			else if (level == FlaggedLoggingLevel.Exception)
			{
				Logger.Log($"Removing \"FlaggedLoggingLevel.Exception\" is not supported", FlaggedLoggingLevel.Verbose);
				return false;
			}

			Logger.Log($"Removed {level}", FlaggedLoggingLevel.Verbose);
			CurrentLevel &= ~level;

			return true;
		}

		/// <inheritdoc/>
		public override void OnInitializeMelon()
		{
			Settings.OnLoad();
		}
	}
}
