// ---------------------------------------------
// ConversionUtilities - by The Illusion
// ---------------------------------------------
// Reusage Rights ------------------------------
// You are free to use this script or portions of it in your own mods, provided you give me credit in your description and maintain this section of comments in any released source code
//
// Warning !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Ensure you change the namespace to whatever namespace your mod uses, so it doesnt conflict with other mods
// ---------------------------------------------
#pragma warning disable CS1591
namespace ComplexLogger.Utilities
{
	/// <summary>
	/// 
	/// </summary>
	public class ConversionUtilities
	{
		public class Miles
		{
            /// <summary>
            /// Converts MPH to M/S
            /// </summary>
            /// <param name="input">The MPH to convert</param>
            /// <returns></returns>
            public static double ToMetersPerSecond(double input)
            {
                return input * 0.44704;
            }

            /// <summary>
            /// Converts MPH to KM/H
            /// </summary>
            /// <param name="input">The MPH to convert</param>
            /// <returns></returns>
            public static double ToKilometersPerHour(double input)
			{
				return input * 1.609344;
			}
		}

		public class Kilometers
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="input"></param>
			/// <returns></returns>
			public static double ToMetersPerSecond(double input)
			{
				return input / 3.6;
			}

			public static double ToMilesPerHour(double input)
			{
                return input / 1.609344;
            }
		}

		public class Meters
		{

		}

		/// <summary>
		/// Rounds the input up to the nearest int
		/// </summary>
		/// <param name="input">The float to convert</param>
		/// <returns></returns>
		public static int GetNormalizedSpeed(double input)
		{
			double num = Math.Round(input, 0, MidpointRounding.ToEven);
			return (int)num;
		}
	}
}
#pragma warning restore CS1591