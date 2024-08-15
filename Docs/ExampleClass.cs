global using ComplexLogger;

namespace ModNamespace
{
    // This is your primary class, which you point to in your AssemblyInfo.cs file
    public class Main : MelonMod
    {
		// You should use a type that is accessable from all other classes within your mod. You can also use internal instead of public
        public static ComplexLogger<Main> Logger { get; } = new();
    }
}
