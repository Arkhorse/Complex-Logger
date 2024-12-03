// ---------------------------------------------
// BuildInfo - by The Illusion
// ---------------------------------------------
// Reusage Rights ------------------------------
// You are free to use this script or portions of it in your own mods, provided you give me credit in your description and maintain this section of comments in any released source code
//
// Warning !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Ensure you change the namespace to whatever namespace your mod uses, so it doesnt conflict with other mods
// ---------------------------------------------

namespace ComplexLogger
{
	/// <summary></summary>
	public static class BuildInfo
	{
		#region Mandatory
		/// <summary>The machine readable name of the mod (no special characters or spaces)</summary>
		/// <remarks>
		/// <para>This is used in logs that relate to this mod. So this should always be Alphanumerical, without exception</para>
		/// </remarks>
		public const string Name							= "ComplexLogger";
		/// <summary>Who made the mod</summary>
		public const string Author							= "The Illusion";
		/// <summary>Current version</summary>
		/// <value>This should always be <see href="https://semver.org">Semantic Versioning</see></value>
		public const string Version							= "1.0.6";
		/// <summary>Name used on GUI's, like ModSettings</summary>
		public const string GUIName							= "Complex Logger";
		/// <summary>The minimum Melon Loader version that your mod requires</summary>
		/// <remarks>
		/// <para>This should only be increased if you actually require a newer version of MelonLoader. NEVER change it before an update has been properly tested</para>
		/// <para>If a user has a version less than this, MelonLoader will not load this mod</para>
		/// </remarks>
		public const string MelonLoaderVersion				= "0.6.1";
		#endregion

		#region Optional
		/// <summary>Who used to handle the mod</summary>
		/// <remarks>
		/// <para>This is currently unused</para>
		/// <para>When implemented, this will show the previous authors of this mod</para>
		/// </remarks>
		public const string[] PreviousAuthors				= null;
		/// <summary>What the mod does</summary>
		public const string Description						= null;
		/// <summary>Company that made it</summary>
		public const string Company							= null;
		/// <summary>A valid download link</summary>
		/// <remarks>
		/// <para>This must be a link to a file, not a site</para>
		/// </remarks>
		/// <example>https://github.com/Arkhorse/FuelManager/releases/download/latest/FuelManager.dll</example>
		public const string DownloadLink					= null;
		/// <summary>Copyright info</summary>
		/// <remarks>When updating the year, use the StartYear-CurrentYear format eg(Copyright © 2020-2024)</remarks>
		public const string Copyright						= "Copyright © 2024";
		/// <summary>Trademark info</summary>
		public const string Trademark						= null;
		/// <summary>Product Name (Generally use the Name)</summary>
		public const string Product							= "ComplexLogger";
		/// <summary>Culture info</summary>
		public const string Culture							= null;
		/// <summary>Priority of your mod. Most of the time you should not need to change this</summary>
		public const int Priority							= 0;
		#endregion
	}
}
