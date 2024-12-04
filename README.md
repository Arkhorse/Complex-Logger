# Installation
Ensure Melon Loader (v0.6.4) is installed and you have ran the game at least once (this is to allow ML to generate the needed assemblies)

## Dependencies
This mod currently only requires Mod Settings

# Usage for Users
There are a few options to toggle. Typically you will just install it and never need to do anything else. If your needing help troubleshooting things, and the mods your using use this mod, youll likely get asked to enable the debug option. This is done in the Mod Settings menu for this mod.

# Why
This mod was created because MelonLoader does not have the ability to do this for you. Its quite annoying to need to recompile mods just to add more logging. So this mod allows you to just write every inch of logging you want and not have that logging spam the log when the user doesnt want it to.

# Usage in Mods
You will need to add a project reference to this mods dll. Once that is done, there is a few more steps:
- Add `global using ComplexLogger;` to the top of your main file, this will allow you to use ComplexLogger's stuff anywhere
- Add `public static ComplexLogger<T> Logger = new();` to your `MelonMod`/`MelonPlugin` class. You could also use `internal`, but it must be accessable from other classes, so you cant use `private`
    - `<T>` requires a class that extends `MelonBase`, which can be a `MelonMod` or a `MelonPlugin`

So your main class should look something like this:
https://github.com/Arkhorse/Complex-Logger/blob/45c0099a8ae5a9f5abec8324500715a7ff5b5c4d/Docs/ExampleClass.cs#L1-L11

Once these things are added, you will be able to use the logger. No longer will you need to use defines for debug messages. No longer will you need to recompile a mod just to enable those define based debug messages.

Color is automatically handled, all `FlaggedLoggingLevel.Warning` messages are Yellow, `FlaggedLoggingLevel.Error` are Red, `FlaggedLoggingLevel.Critical` are Dark Red and `FlaggedLoggingLevel.Exception` is also red. I may change Error and Critical to another color, if this is wanted. This change would be done to be able to properly distinguish them from exceptions, but is not 100% required for users.

## Logging Sub Type
I also implemented a handy sub type that you can use to print separators and headers. In order to use them, you will need to use something along these examples:
- `Logger.Log(FlaggedLoggingLevel.Debug, LoggingSubType.Separator);`
- `Logger.Log("I am a header", FlaggedLoggingLevel.Debug, LoggingSubType.IntraSeparator);`
- `Logger.Log("This will print to the Unity console (or uConsole)", FlaggedLoggingLevel.Debug, LoggingSubType.uConsole);`

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
    - This is used for generic messages, like when you must print something to the log, without the user being able to disable the message
- [ ] Trace
    - Use this for logs that you dont really need for most cases
- [ ] Debug
    - This is your bread and butter. You'll likely use only this level
- [ ] Verbose
    - This is for debug information that is only useful when the normal debug info just isnt enough
    - Use this for logging things in Update methods
- [ ] Warning
    - Use this level for warnings that you dont absolutely need to be printed
- [ ] Error
    - **Always Enabled**
    - Use this for anything you consider to be an issue but isnt critical or an exception
- [ ] Critical
    - **Always Enabled**
    - Use this for anything critical that breaks
- [ ] Exception
    - **Always Enabled**
    - Use this for all exceptions. You should always be using a `Try/Catch` if the thing your doing can throw an exception. NOTE: Do not rethrow the exception, the ComplexLogger will handle everything. Use a return if you must end the method execution at that point

# Technical Details
This mod makes use of a generic class to handle extending the base MelonLogger. This means that you _cant_ use a class that doesnt extend the `MelonBase` class in some way as the class that you use to instantiate the ComplexLogger `class`.

This is how the mod actually handles the different levels: https://github.com/Arkhorse/Complex-Logger/blob/1310dbbe4309d7eb9e33cd291971f2d76b3217b7/VisualStudio/Utilities/Logger/ComplexLogger.cs#L147-L176
Exceptions have additional text and whatnot, so have their own dedicated method: https://github.com/Arkhorse/Complex-Logger/blob/1310dbbe4309d7eb9e33cd291971f2d76b3217b7/VisualStudio/Utilities/Logger/ComplexLogger.cs#L229-L240
