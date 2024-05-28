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
global using ComplexLogger.Utilities.Logger;
global using ComplexLogger.Utilities.Logger.Enums;
#endregion

namespace ComplexLogger
{

	/// <inheritdoc/>
	public class Main : MelonMod
	{
		/// <inheritdoc/>
		public override void OnInitializeMelon()
		{
			Settings.OnLoad();
		}
	}
}
