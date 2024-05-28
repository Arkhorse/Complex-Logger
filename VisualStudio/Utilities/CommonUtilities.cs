namespace ComplexLogger.Utilities
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public class CommonUtilities
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	{
		// NOTE: These "Get" methods are volitile. Ensure it is always up to date as otherwise it can break anything tied to GearItem
		// This is used to load prefab info of a GearItem
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull(nameof(name))]
		public static GearItem GetGearItemPrefab(string name) => GearItem.LoadGearItemPrefab(name);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull(nameof(name))]
		public static ToolsItem GetToolItemPrefab(string name) => GetGearItemComponentPrefab<ToolsItem>(name).GetComponent<ToolsItem>();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull(nameof(name))]
		public static ClothingItem GetClothingItemPrefab(string name) => GetGearItemComponentPrefab<ClothingItem>(name).GetComponent<ClothingItem>();

		/// <summary>
		/// Allows you to get any prefab from any valid component
		/// </summary>
		/// <typeparam name="T">The Component you want to get</typeparam>
		/// <param name="name">Name of the prefab</param>
		/// <returns>A valid prefab of the given Component or <see langword="null"/></returns>
		public static T GetGearItemComponentPrefab<T>(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<T>();

		/// <summary>
		/// Normalizes the name given to remove extra bits using regex for most of the changes
		/// </summary>
		/// <param name="name">The name of the thing to normalize</param>
		/// <returns>Normalized name without <c>(Clone)</c> or any numbers appended</returns>
		[return: NotNullIfNotNull(nameof(name))]
		public static string? NormalizeName(string name)
		{
			string name0 = Regex.Replace(name, @"(?:\(\d{0,}\))", string.Empty);
			string name1 = Regex.Replace(name0, @"(?:\s\d{0,})", string.Empty);
			string name2 = name1.Replace("(Clone)", string.Empty, System.StringComparison.InvariantCultureIgnoreCase);
			string name3 = name2.Replace("\0", string.Empty);
			return name3.Trim();
		}

		/// <summary>
		/// Checks if the player is currently involved in anything that would make modded actions unwanted
		/// </summary>
		/// <param name="PlayerManagerComponent">The current instance of the PlayerManager component, use <see cref="GameManager.GetPlayerManagerComponent()"/></param>
		/// <returns><see langword="true"/> if the player isnt dead, in a conversation, locked or in a cinematic</returns>
		public static bool IsPlayerAvailable(PlayerManager PlayerManagerComponent)
		{
			if (PlayerManagerComponent == null) return false;

			return (
				!(PlayerManagerComponent.m_ControlMode == PlayerControlMode.Dead)               &&
				!(PlayerManagerComponent.m_ControlMode == PlayerControlMode.InConversation)     &&
				!(PlayerManagerComponent.m_ControlMode == PlayerControlMode.Locked)             &&
				!(PlayerManagerComponent.m_ControlMode == PlayerControlMode.InFPCinematic)
			);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static bool IsInterfaceOpen()
		{
			return InterfaceManager.IsOverlayActiveCached();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gi"></param>
		/// <returns></returns>
		public static bool IsGearOperatable(GearItem gi)
		{
			return gi != null && gi.name != null && gi.name != "GEAR_Null";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="panel"></param>
		/// <param name="gi"></param>
		/// <returns></returns>
		public static bool IsGearOperatable<T>(T panel, GearItem gi) where T : Panel_Base
		{
			return panel != null && gi != null && gi.name != null && gi.name != "GEAR_Null";
		}
	}
}
