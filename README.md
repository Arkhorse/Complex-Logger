# Installation
Ensure Melon Loader (v0.6.1) is installed and you have ran the game at least once (this is to allow ML to generate the needed assemblies)

## Dependencies
This mod currently only requires Mod Settings

# Usage for Users
There are a few options to toggle. Typically you will just install it and never need to do anything else. If your needing help troubleshooting things, and the mods your using use this mod, youll likely get asked to enable the debug option. This is done in the Mod Settings menu for this mod.

# Why
This mod was created because MelonLoader does not have the ability to do this for you. Its quite annoying to need to recompile mods just to add more logging. So this mod allows you to just write every inch of logging you want and not have that logging spam the log when the user doesnt want it to.

# Usage in Mods
You will need to add a project reference to this mods dll. Once that is done, there is a few more steps:
- Add `using ComplexLogger;`
- Add `public static ComplexLogger<T> Logger = new();`
    - You dont need to use the `public` keyword.
    - `<T>` requires a class that extends `MelonBase`, which can be `MelonMod` or `MelonPlugin`

So your main class should look something like this:
```cs
using ComplexLogger;

namespace ModNamespace
{
    public class Main : MelonMod
    {
        public static ComplexLogger Logger { get; private set; }

        public override void OnInitializeMelon()
        {
            Logger ??= new();
            // code
        }
    }
}
```

Once these things are added, you will be able to use the logger. No longer will you need to use defines for debug messages. No longer will you need to recompile a mod just to enable those define based debug messages.

You can also add color and other text changes using the `params object[]` array. This should be properly handled, but is currently not fully tested.

# Technical Details
This mod makes use of a generic class to handle extending the base MelonLogger. Note that you _cant_ use a class that doesnt extend the `MelonBase` class in some way as the class that you use to instantiate the ComplexLogger instance.

## Logging Sub Type
I also implemented a handy sub type that you can use to print separators and headers. In order to use them, you will need to use something along these examples:
- `Logger.Log(string.Empty, FlaggedLoggingLevel.Debug, null, LoggingSubType.Separator);`
- `Logger.Log("I am a header", FlaggedLoggingLevel.Debug, null, LoggingSubType.IntraSeparator);`
- `Logger.Log("This will print to the Unity console (or uConsole)", FlaggedLoggingLevel.Debug, null, LoggingSubType.uConsole);`

Note: I forgot to write a specific function to handle the sub type without the exception. In a future update, you will not need the `null` argument and there will be another function to handle the separator, without a message string.

## Writing Blocks
Another feature is the ability to write entire log blocks. This feature is a bit more complex, but not that hard to use:

### Simple
- `Logger.Log("Title", ["line 1", "line 2", "line 3"], FlaggedLoggingLevel.Debug, LoggingSubType.Block);`

### Simple Colored
- `Logger.Log("Title", ["line 1", "line 2", "line 3"], Color.Red, FlaggedLoggingLevel.Debug, LoggingSubType.Block);`

### Colored
- `Logger.Log("Title", ["line 1", "line 2", "line 3"], [Color.Blue, Color.Red, Color.Red], FlaggedLoggingLevel.Debug, LoggingSubType.Block);`

You could also define everything first:

```cs
string[] lines = [ "line 1", "line 2", "line 3" ];
List<System.Drawing.Color> LineColors = [ Color.Blue, Color.Red, Color.Red ];

Logger.Log("Title", lines, LineColors, FlaggedLoggingLevel.Debug, LoggingSubType.Block);
```

## Levels
- [ ] None
    - **Always Enabled**
- [ ] Trace
    - Use this for logs that you dont really need for most cases
- [ ] Debug
    - This is your bread and butter. You'll likely use only this level
- [ ] Verbose
    - This is for debug information that is only useful when the normal debug info just isnt enough
- [ ] Warning
    - Use this level for warnings that you dont absolutely need to be printed
- [ ] Error
    - **Always Enabled**
- [ ] Critical
    - **Always Enabled**
- [ ] Exception
    - **Always Enabled**